// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Service.Main
// Assembly: PCSYS PLC Service, Version=1.0.7.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 3ECB826A-1CFB-42EB-9747-AB0DACAA30C1
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Service.exe

using PCSYS_Event_Log;
using PCSYS_Plc_AppSettings;
using PCSYS_PLC_Service.PLCConnection;
using PCSYS_PLC_Service.Worker;
using PCSYS_PPG_Info;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace PCSYS_PLC_Service
{

    public class Main : ServiceBase
    {
        public static Settings AppSettings;
        public static RegisterStatus PlcStatus = RegisterStatus.None;
        private const string Appname = "PCSYS PLC Server";
        private static int _palletlineid;
        private IContainer components = (IContainer)null;

        private Thread RunThread { get; set; }

        public Timer Statetimer { get; set; }

        public Main() => this.InitializeComponent();

        protected override void OnStart(string[] args)
        {
            try
            {
                Main.AppSettings = new SettingsTools().GetSettings("PCSYS PLC Server");
                if (Main.AppSettings?.DataLinks == null || Main.AppSettings.DataLinks.Count == 0)
                {
                    LogTools.WriteToEventLog("The link is missing", EventLogEntryType.Error, "PCSYS PLC Server", -1, LogTools.Operation.Plc);
                    return;
                }
                PlcConfig.LoadConfiguration();
                Main._palletlineid = DatabaseTools.GetPalletLineId();
                if (!Main.AppSettings.IsOffLine)
                {
                    try
                    {
                        PlcConn.InitializePlcTcpConnection();
                        bool connected = PlcConn.Client.Connected;
                        LogTools.WriteToEventLog(connected ? "The PLC Connection is ok" : "The PLC Connection is not established", EventLogEntryType.SuccessAudit, Main._palletlineid, LogTools.Operation.Plc, Main.AppSettings.DataLinks);
                        if (!connected)
                        {
                            this.Stop();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogTools.WriteToEventLog(ex.Message, EventLogEntryType.FailureAudit, Main._palletlineid, LogTools.Operation.Plc, Main.AppSettings.DataLinks);
                        this.Stop();
                        return;
                    }
                }
                LogTools.WriteToEventLog("Stating the Service", EventLogEntryType.SuccessAudit, Main._palletlineid, LogTools.Operation.Plc, Main.AppSettings.DataLinks);
                this.RunThread = new Thread(new ThreadStart(this.RunService));
                this.RunThread.Start();
                this.Statetimer = new Timer(new TimerCallback(new Main.StartManager().Start), (object)null, 5000, 5000);
            }
            catch (Exception ex)
            {
                LogTools.WriteToEventLog(ex.Message, EventLogEntryType.Error, "PCSYS PLC Server", -1, LogTools.Operation.Plc);
                this.Stop();
                return;
            }
            try
            {
                LogTools.WriteToEventLog("Start PCSYS PLC Server", EventLogEntryType.SuccessAudit, Main._palletlineid, LogTools.Operation.Plc, Main.AppSettings.DataLinks);
            }
            catch (Exception ex)
            {
                LogTools.WriteToEventLog(ex.Message, EventLogEntryType.Error, "PCSYS PLC Server", -1, LogTools.Operation.Plc);
            }
        }

        protected override void OnStop()
        {
            try
            {
                if (!Main.AppSettings.IsOffLine && PlcConn.Client.Connected)
                    PlcConn.DisconnectPlcTcpConnection();
                if (Main.AppSettings.DataLinks == null)
                    return;
                LogTools.WriteToEventLog("Stop PCSYS PLC Server", EventLogEntryType.SuccessAudit, Main._palletlineid, LogTools.Operation.Plc, Main.AppSettings.DataLinks);
                DatabaseTools.UpdatePlcStatus(PCSYS_Plc_AppSettings.PlcStatus.Stopped, "Stopped");
                DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Stopped, Main.AppSettings.DataLinks);
            }
            catch (Exception ex)
            {
                LogTools.WriteToEventLog(ex.Message, EventLogEntryType.Error, "PCSYS PLC Server", Main._palletlineid, LogTools.Operation.Plc);
            }
        }

        private static void ReadAllReadOnlyFields()
        {
            foreach (Register register in PlcConfig.Registers)
            {
                foreach (RegisterField field in register.Fields)
                    PlcTools.ReadValue(register.RegName, field.Type);
            }
        }

        private void RunService()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(2000);
                    try
                    {
                        Main.ReadAllReadOnlyFields();
                        if (!Main.AppSettings.IsOffLine)
                        {
                            if (!PlcConn.Client.Connected)
                            {
                                DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Error, Main.AppSettings.DataLinks);
                                continue;
                            }
                            DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Running, Main.AppSettings.DataLinks);
                        }
                        PlcTools.SetReqRegister();
                    }
                    catch (Exception ex)
                    {
                        LogTools.WriteToEventLog(ex.Message, EventLogEntryType.FailureAudit, Main._palletlineid, LogTools.Operation.Plc, Main.AppSettings.DataLinks);
                        continue;
                    }
                    Main._palletlineid = DatabaseTools.GetPalletLineId();
                    RegisterStatus status = PlcTools.GetStatus();
                    int lineId = 0;
                    OrderInformation orderInformation1 = new OrderInformation();
                    int num1 = 0;
                    try
                    {
                        if (status != Main.PlcStatus)
                        {
                            LogTools.WriteToEventLog($"Status changs to {status}", EventLogEntryType.SuccessAudit, Main._palletlineid, LogTools.Operation.Plc, Main.AppSettings.DataLinks);
                            int num2;
                            try
                            {
                                num2 = (int)PlcTools.ReadValue(DataBlockName.DB101, FieldType.LineNo).Value;
                            }
                            catch (Exception ex)
                            {
                                num2 = 0;
                            }
                            OrderInformation orderInformation2;
                            if (num2 > 0)
                            {
                                lineId = new DataTools().GetLineId(num2, PlcConfig.AppSettings.DataLinks);
                                if (!DatabaseTools.LineExists(num2))
                                {
                                    LogTools.WriteToEventLog($"Line number {num2} does not exists", EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                    PlcTools.WriteValue(DataBlockName.DB102, FieldType.ErrorCode, 1021020L);
                                    PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.RegisterPalletAck, 1);
                                    DataTools.UpdateLineStatus(lineId, LineStatus.Error, Main.AppSettings.DataLinks);
                                    PlcTools.WriteValue(DataBlockName.DB102, FieldType.SSCC, 0L);
                                    DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Error, Main.AppSettings.DataLinks);
                                    break;
                                }
                                LogTools.WriteToEventLog("Change status to " + Enum.GetName(typeof(RegisterStatus), (object)status), EventLogEntryType.SuccessAudit, lineId, LogTools.Operation.Plc, Main.AppSettings.DataLinks);
                                orderInformation2 = DatabaseTools.GetLineData(num2);
                                num1 = Convert.ToInt32(DatabaseTools.GetQty(orderInformation2.MaterialNumber).Value);
                            }
                            else
                            {
                                orderInformation2 = new OrderInformation();
                                num1 = 0;
                            }
                            Main.PlcStatus = status;
                            switch (status)
                            {
                                case RegisterStatus.None:
                                    Thread.Sleep(2000);
                                    break;
                                case RegisterStatus.Reset:
                                    Labelresult labelresult1 = LabelTools.PrintResetLabel(PlcConfig.AppSettings.ResetCode);
                                    DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Stopped, Main.AppSettings.DataLinks);
                                    DataTools.ReleaseLineStatusFromError(num2, LineStatus.Stopped, Main.AppSettings.DataLinks);
                                    if (labelresult1.HasAnError)
                                    {
                                        PlcTools.WriteValue(DataBlockName.DB102, FieldType.ErrorCode, 1011010L);
                                        LogTools.WriteToEventLog(labelresult1.ErrorMessage, EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                    }
                                    PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.ResetAck, 1);
                                    goto case RegisterStatus.None;
                                case RegisterStatus.RegisterPallet:
                                    int num3;
                                    try
                                    {
                                        num3 = (int)PlcTools.ReadValue(DataBlockName.DB101, FieldType.LineNo).Value;
                                    }
                                    catch (Exception ex)
                                    {
                                        num3 = 0;
                                    }
                                    OrderInformation lineData = DatabaseTools.GetLineData(num3);
                                    if (lineData.HasAnError)
                                    {
                                        PlcTools.WriteValue(DataBlockName.DB102, FieldType.ErrorCode, 1021010L);
                                        LogTools.WriteToEventLog(lineData.ErrorMessage, EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Error, Main.AppSettings.DataLinks);
                                        PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.RegisterPalletAck, 1);
                                        Main.PlcStatus = RegisterStatus.None;
                                        goto case RegisterStatus.None;
                                    }
                                    int int32_1 = Convert.ToInt32(DatabaseTools.GetQty(lineData.MaterialNumber).Value);
                                    PlcAction plcAction1 = PlcTools.ReadValue(DataBlockName.DB101, FieldType.Qty);
                                    if (plcAction1.Status == PlcActionStatus.Failed)
                                    {
                                        PlcTools.WriteValue(DataBlockName.DB102, FieldType.ErrorCode, 1023040L);
                                        LogTools.WriteToEventLog("Qty is missing", EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Error, Main.AppSettings.DataLinks);
                                        PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.RegisterPalletAck, 1);
                                        Main.PlcStatus = RegisterStatus.None;
                                        goto case RegisterStatus.None;
                                    }
                                    int qty1 = int.Parse(plcAction1.Value.ToString());
                                    int packitemqty = 1;
                                    try
                                    {
                                        DatabaseAction packItemCount = DataTools.GetPackItemCount(lineData.MaterialNumber, PlcConfig.AppSettings.DataLinks);
                                        if (!string.IsNullOrEmpty(packItemCount.ErrorMessage))
                                            LogTools.WriteToEventLog(packItemCount.ErrorMessage, EventLogEntryType.SuccessAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        else
                                            packitemqty = (int)packItemCount.Value;
                                    }
                                    catch (Exception ex)
                                    {
                                        LogTools.WriteToEventLog("Sum error: " + ex.Message, EventLogEntryType.SuccessAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                    }
                                    LogTools.WriteToEventLog($"Sum qty: {qty1}", EventLogEntryType.SuccessAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                    string valueByFieldNameId = new ExcelTools().GetRowValueByFieldNameId(lineData.MaterialNumber, "DK", PlcConfig.AppSettings.EanFieldId, PlcConfig.AppSettings.DataLinks);
                                    if (string.IsNullOrEmpty(valueByFieldNameId))
                                    {
                                        LogTools.WriteToEventLog("EAN is missing", EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        PlcTools.WriteValue(DataBlockName.DB104, FieldType.ErrorCode, 1034075L);
                                    }
                                    if (lineData.ErrorCode == 1021014L)
                                    {
                                        CreateBarcodeResult newBarCode = new DataTools().CreateNewBarCode(lineData, num3, true, Main.AppSettings.DataLinks);
                                        LogTools.WriteToEventLog($"Counter increased to {newBarCode.Counter}", EventLogEntryType.SuccessAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        if (newBarCode.HasAnError)
                                        {
                                            PlcTools.WriteValue(DataBlockName.DB102, FieldType.ErrorCode, 1023040L);
                                            LogTools.WriteToEventLog(newBarCode.ErrorMessage, EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                            DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Error, Main.AppSettings.DataLinks);
                                            PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.RegisterPalletAck, 1);
                                            Main.PlcStatus = RegisterStatus.None;
                                            goto case RegisterStatus.None;
                                        }
                                        LogTools.WriteToEventLog($"Line  {num3} stopped", EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        PlcTools.WriteValue(DataBlockName.DB102, FieldType.ErrorCode, 1023040L);
                                        PlcTools.WriteValue(DataBlockName.DB102, FieldType.SSCC, (long)newBarCode.Counter);
                                        PlcTools.WriteValue(DataBlockName.DB102, FieldType.MaxPallet, (long)Convert.ToInt32(int32_1));
                                        PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.RegisterPalletAck, 1);
                                        try
                                        {
                                            DataTools.InsertPalletQueue(lineData.FillingOrder, num3, (long)newBarCode.Counter, newBarCode.Sscc, valueByFieldNameId, qty1, packitemqty, Main.AppSettings.DataLinks);
                                            Main.PlcStatus = RegisterStatus.None;
                                            goto case RegisterStatus.None;
                                        }
                                        catch (Exception ex)
                                        {
                                            LogTools.WriteToEventLog($"Cannot insert   {newBarCode.Counter}. Error: {ex.Message}", EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                            goto case 0;
                                        }
                                    }
                                    else
                                    {
                                        if (lineData.ErrorCode > 0L)
                                        {
                                            PlcTools.WriteValue(DataBlockName.DB102, FieldType.ErrorCode, lineData.ErrorCode);
                                            PlcTools.WriteValue(DataBlockName.DB102, FieldType.SSCC, 0L);
                                            LogTools.WriteToEventLog(lineData.ErrorMessage, EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                            PlcTools.WriteValue(DataBlockName.DB102, FieldType.MaxPallet, 0L);
                                            DataTools.UpdateLineStatus(lineId, LineStatus.Error, Main.AppSettings.DataLinks);
                                            PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.RegisterPalletAck, 1);
                                            Main.PlcStatus = RegisterStatus.None;
                                            goto case RegisterStatus.None;
                                        }
                                        CreateBarcodeResult newBarCode = new DataTools().CreateNewBarCode(lineData, num3, true, Main.AppSettings.DataLinks);
                                        if (newBarCode.HasAnError)
                                        {
                                            PlcTools.WriteValue(DataBlockName.DB102, FieldType.ErrorCode, 1023040L);
                                            PlcTools.WriteValue(DataBlockName.DB102, FieldType.SSCC, 0L);
                                            LogTools.WriteToEventLog(newBarCode.ErrorMessage, EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                            DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Error, Main.AppSettings.DataLinks);
                                            PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.RegisterPalletAck, 1);
                                            Main.PlcStatus = RegisterStatus.None;
                                            goto case RegisterStatus.None;
                                        }
                                        if (int.Parse(int32_1.ToString()) < qty1 * packitemqty)
                                        {
                                            PlcTools.WriteValue(DataBlockName.DB102, FieldType.ErrorCode, 1023042L);
                                            LogTools.WriteToEventLog($"Max pallet smaller than {qty1 * packitemqty}", EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        }
                                        else
                                            PlcTools.WriteValue(DataBlockName.DB102, FieldType.ErrorCode, 0L);
                                        PlcTools.WriteValue(DataBlockName.DB102, FieldType.SSCC, (long)Convert.ToInt32(newBarCode.Counter));
                                        PlcTools.WriteValue(DataBlockName.DB102, FieldType.MaxPallet, (long)Convert.ToInt32(int32_1));
                                        PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.RegisterPalletAck, 1);
                                        LogTools.WriteToEventLog($"Counter increased to {newBarCode.Counter}", EventLogEntryType.SuccessAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Running, Main.AppSettings.DataLinks);
                                        try
                                        {
                                            DataTools.InsertPalletQueue(lineData.FillingOrder, num3, (long)newBarCode.Counter, newBarCode.Sscc, valueByFieldNameId, qty1, packitemqty, Main.AppSettings.DataLinks);
                                        }
                                        catch (Exception ex)
                                        {
                                            LogTools.WriteToEventLog($"Can't update {newBarCode.Counter} Error: {ex.Message}", EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        }
                                        Main.PlcStatus = RegisterStatus.None;
                                        goto case RegisterStatus.None;
                                    }
                                case RegisterStatus.PreparePallet:
                                    if (num2 < 1)
                                    {
                                        PlcTools.WriteValue(DataBlockName.DB104, FieldType.ErrorCode, 0L);
                                        DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Manuel_Print, Main.AppSettings.DataLinks);
                                        DataTools.InsertManuelPrintData(orderInformation2.FillingOrder, 0, LabelTools.GetPrinterAliasId(), Main.AppSettings.DataLinks);
                                        PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.PreparePalletAck, 1);
                                        LogTools.WriteToEventLog("Switch to Manuel print. SSCC = 0 ", EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        goto case RegisterStatus.None;
                                    }
                                    PlcAction plcAction2 = PlcTools.ReadValue(DataBlockName.DB103, FieldType.Qty);
                                    if (plcAction2.Status == PlcActionStatus.Failed)
                                    {
                                        LogTools.WriteToEventLog($"Can't get Qty for line {num2}", EventLogEntryType.FailureAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        PlcTools.WriteValue(DataBlockName.DB104, FieldType.ErrorCode, 1033010L);
                                        PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.PreparePalletAck, 1);
                                        DataTools.UpdateLineStatus(lineId, LineStatus.Error, Main.AppSettings.DataLinks);
                                        goto case RegisterStatus.None;
                                    }
                                    int qty2 = (int)plcAction2.Value;
                                    PlcAction plcAction3 = PlcTools.ReadValue(DataBlockName.DB103, FieldType.SSCC);
                                    if (plcAction3.Status == PlcActionStatus.Failed)
                                    {
                                        LogTools.WriteToEventLog($"Can't get SSCC for line {num2}", EventLogEntryType.FailureAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        PlcTools.WriteValue(DataBlockName.DB104, FieldType.ErrorCode, 1033015L);
                                        PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.PreparePalletAck, 1);
                                        DataTools.UpdateLineStatus(lineId, LineStatus.Error, Main.AppSettings.DataLinks);
                                        goto case RegisterStatus.None;
                                    }
                                    int num4 = (int)plcAction3.Value;
                                    if (num4 == 0)
                                    {
                                        Main._palletlineid = DatabaseTools.GetPalletLineId();
                                        LogTools.WriteToEventLog($"SSCC is empty. Manuel print for pallet line {Main._palletlineid}", EventLogEntryType.SuccessAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        DatabaseTools.UpdateLineStatus(Main._palletlineid, qty2.ToString(), LineStatus.Manuel_Print);
                                        PlcTools.WriteValue(DataBlockName.DB104, FieldType.ErrorCode, 0L);
                                        DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Manuel_Print, Main.AppSettings.DataLinks);
                                        DataTools.InsertManuelPrintData(orderInformation2.FillingOrder, qty2, LabelTools.GetPrinterAliasId(), Main.AppSettings.DataLinks);
                                        PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.PreparePalletAck, 1);
                                        LogTools.WriteToEventLog("Switch to Manuel print. SSCC is 0", EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        goto case RegisterStatus.None;
                                    }
                                    PalletQueue palletQueue1 = DataTools.GetPalletQueue((long)num4, PlcConfig.AppSettings.DataLinks);
                                    LogTools.WriteToEventLog($"Material number = {palletQueue1.OrderInfo.MaterialNumber}", EventLogEntryType.Information, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                    LogTools.WriteToEventLog($"Material qty =  {qty2}", EventLogEntryType.SuccessAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                    int int32_2 = Convert.ToInt32(DataTools.GetMaxQty(palletQueue1.OrderInfo.MaterialNumber, PlcConfig.AppSettings.DataLinks).Value.ToString().Trim());
                                    if (qty2 * palletQueue1.ItemPackCount > int32_2)
                                    {
                                        LogTools.WriteToEventLog($"Qty >  {int32_2 * palletQueue1.ItemPackCount}", EventLogEntryType.SuccessAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        DatabaseTools.InsertUserInput(num4, Main._palletlineid, $"Max. items on pallet is {int32_2 * palletQueue1.ItemPackCount}. The current pallet contains {qty2} items!. Order: {palletQueue1.OrderInfo.FillingOrder} Material: {palletQueue1.OrderInfo.MaterialNumber}", Convert.ToInt32(int32_2 * palletQueue1.ItemPackCount));
                                        LogTools.WriteToEventLog("Wait for user input", EventLogEntryType.SuccessAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        PlcTools.WriteValue(DataBlockName.DB104, FieldType.ErrorCode, 1034052L);
                                        PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.PreparePalletAck, 1);
                                        goto case RegisterStatus.None;
                                    }
                                    LogTools.WriteToEventLog("Create label for pallet", EventLogEntryType.SuccessAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                    string printerAlias = DataTools.GetPrinterAlias(PlcConfig.AppSettings.DataLinks);
                                    PalletQueue palletQueue2 = new PalletQueue();
                                    PalletQueue palletQueue3;
                                    try
                                    {
                                        palletQueue3 = DataTools.GetPalletQueue((long)num4, PlcConfig.AppSettings.DataLinks);
                                        palletQueue3.Qty = qty2;
                                    }
                                    catch (Exception ex)
                                    {
                                        LogTools.WriteToEventLog(ex.Message, EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        PlcTools.WriteValue(DataBlockName.DB104, FieldType.ErrorCode, 1034010L);
                                        DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Plc_error, Main.AppSettings.DataLinks);
                                        goto case 0;
                                    }
                                    PrintResult printResult = new DataTools().PrintPalletLabel(PrintMode.Print, palletQueue3.OrderInfo, 0, printerAlias, palletQueue3.Sscc, 1, qty2 > 0 ? qty2 : palletQueue3.Qty, PlcConfig.AppSettings.DataLinks, PlcConfig.AppSettings.ReportPath, palletQueue3.LineNumber);
                                    if (!string.IsNullOrEmpty(printResult.ErrorMessage) && !printResult.ErrorMessage.ToLower().StartsWith("ok"))
                                    {
                                        LogTools.WriteToEventLog(printResult.ErrorMessage, EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        PlcTools.WriteValue(DataBlockName.DB104, FieldType.ErrorCode, 1034060L);
                                        DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Plc_error, Main.AppSettings.DataLinks);
                                    }
                                    else
                                    {
                                        LogTools.WriteToEventLog($"The SSCC number {palletQueue3.Sscc} is printed ", EventLogEntryType.SuccessAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                        DataTools.DeletePrinterQueue((long)num4, PlcConfig.AppSettings.DataLinks);
                                        try
                                        {
                                            LogTools.WriteToEventLog("Inserting order info to database", EventLogEntryType.SuccessAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                            try
                                            {
                                                DataTools.SavePrinted(palletQueue3, PlcConfig.AppSettings.DataLinks);
                                            }
                                            catch (Exception ex)
                                            {
                                                LogTools.WriteToEventLog("Cannot insert to SAP " + ex.Message, EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                                PlcTools.WriteValue(DataBlockName.DB104, FieldType.ErrorCode, 1034071L);
                                            }
                                            PlcTools.WriteValue(DataBlockName.DB104, FieldType.SSCC, palletQueue3.Counter);
                                            PlcTools.WriteValue(DataBlockName.DB104, FieldType.ErrorCode, 0L);
                                        }
                                        catch (Exception ex)
                                        {
                                            LogTools.WriteToEventLog($"Cannot insert the SSCC number {palletQueue3.Sscc} Error: {ex.Message}", EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                            PlcTools.WriteValue(DataBlockName.DB104, FieldType.ErrorCode, 1034070L);
                                            PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.PreparePalletAck, 1);
                                            DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Plc_error, Main.AppSettings.DataLinks);
                                            PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.PreparePalletAck, 1);
                                            goto case 0;
                                        }
                                    }
                                    PlcTools.WriteValue(DataBlockName.DB104, FieldType.ErrorCode, 0L);
                                    PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.PreparePalletAck, 1);
                                    goto case RegisterStatus.None;
                                case RegisterStatus.ReleasePallet:
                                    Labelresult labelresult2 = LabelTools.PrintResetLabel(PlcConfig.AppSettings.ResetCode);
                                    if (labelresult2.HasAnError)
                                    {
                                        PlcTools.WriteValue(DataBlockName.DB104, FieldType.ErrorCode, 1045000L);
                                        LogTools.WriteToEventLog(labelresult2.ErrorMessage, EventLogEntryType.Error, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                    }
                                    PlcTools.WriteStatusValue(DataBlockName.DB100, FieldRegisterType.ReleasePalletAck, 1);
                                    LogTools.WriteToEventLog("Printer is released", EventLogEntryType.SuccessAudit, lineId, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
                                    DataTools.UpdateLineStatus(Main._palletlineid, LineStatus.Stopped, Main.AppSettings.DataLinks);
                                    goto case RegisterStatus.None;
                                default:
                                    LogTools.WriteToEventLog("Default", EventLogEntryType.FailureAudit, lineId, LogTools.Operation.Plc, Main.AppSettings.DataLinks);
                                    goto case RegisterStatus.None;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogTools.WriteToEventLog(ex.Message, EventLogEntryType.FailureAudit, lineId, LogTools.Operation.Plc, Main.AppSettings.DataLinks);
                        DataTools.UpdateLineStatus(lineId, LineStatus.Error, Main.AppSettings.DataLinks);
                    }
                }
            }
            catch (Exception ex)
            {
                LogTools.WriteToEventLog(ex.Message, EventLogEntryType.FailureAudit, -1, LogTools.Operation.Plc, Main.AppSettings.DataLinks);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer)new System.ComponentModel.Container();
            this.ServiceName = "Service1";
        }

        public class StartManager
        {
            public void Start(object state)
            {
                int palletLineId = new DataTools().GetPalletLineId(Main.AppSettings.DataLinks);
                if (!Main.AppSettings.IsOffLine)
                {
                    DatabaseTools.UpdatePlcStatus(PlcConn.Client.Connected ? PCSYS_Plc_AppSettings.PlcStatus.Running : PCSYS_Plc_AppSettings.PlcStatus.Stopped, PlcConn.Client.Connected ? "Running" : "Stopped");
                    if (!PlcConn.Client.Connected)
                        DataTools.UpdateLineStatus(palletLineId, LineStatus.Plc_error, Main.AppSettings.DataLinks);
                    else
                        DataTools.ResetLineStatus(palletLineId, LineStatus.Plc_error, Main.AppSettings.DataLinks);
                }
                else
                    DatabaseTools.UpdatePlcStatus(PCSYS_Plc_AppSettings.PlcStatus.Running, "Started");
            }
        }
    }
}