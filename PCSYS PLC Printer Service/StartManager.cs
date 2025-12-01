// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Printer_Service.StartManager
// Assembly: PCSYS PLC Printer Service, Version=1.0.0.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 857E55AE-E165-426D-B46A-8A20B764DAF5
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Printer Service.exe

using PCSYS_Event_Log;
using PCSYS_Plc_AppSettings;
using PCSYS_PPG_Info;
using PCSYS_PPG_LPS_ProxyConnector;
using PCSYS_PPG_LPS_ProxyConnector.DataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace PCSYS_PLC_Printer_Service;

public class StartManager
{
  public void Start(object state)
  {
    try
    {
      DataTable table = new IBSExchangeData().GetConnection((IEnumerable<IbsDataLink>) ServiceMain.AppSettings.DataLinks).GetData("SELECT tprinters.printerid,tprinters.itemname,tppglineprinters.lineid,tppglineprinters.labeltypeid FROM dbo.tprinters INNER JOIN tppglineprinters ON tprinters.printerid = tppglineprinters.printeraliasid WHERE tppglineprinters.labeltypeid > 0").Tables[0];
      try
      {
        foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        {
          List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>();
          string str;
          try
          {
            str = new DataTools().GetPrintStatus(row["itemname"].ToString(), ServiceMain.AppSettings.DataLinks, (InfoService) null);
          }
          catch (Exception ex)
          {
            LogTools.WriteToEventLog("PCSYS PLC Printer Service Error: call GetPrintStatus " + ex.Message, EventLogEntryType.FailureAudit, "PCSYS PLC Server", -1, LogTools.Operation.Plc);
            str = "Unknow";
          }
          if (str == "Online")
          {
            string sqlString = $"IF EXISTS (SELECT id FROM tppglineprinterstatus WHERE printerid = {row["printerid"]} AND status = 0) " + $"DELETE FROM tppglineprinterstatus WHERE printerid = {row["printerid"]}";
            if (new IBSExchangeData().GetConnection((IEnumerable<IbsDataLink>) ServiceMain.AppSettings.DataLinks).ExecuteScalarInt(sqlString) > 0)
              continue;
          }
          int num = str == "Online" ? 1 : 0;
          string sqlString1 = $"IF EXISTS (SELECT id FROM tppglineprinterstatus WHERE printerid = {row["printerid"]}) " + $"UPDATE tppglineprinterstatus SET statustext = @statustext,status = {num} WHERE printerid = {row["printerid"]} ELSE " + $" INSERT INTO tppglineprinterstatus (printerid,lineid,labeltypeid,statustext,status) VALUES ({row["printerid"]}," + $"{row["lineid"]},{row["labeltypeid"]},@statustext,{num})";
          externalParameterList.Add(new IBSExternalParameter()
          {
            Name = "@statustext",
            Value = (object) str
          });
          new IBSExchangeData().GetConnection((IEnumerable<IbsDataLink>) ServiceMain.AppSettings.DataLinks).ExecuteNonQueryWithParameters(sqlString1, externalParameterList.ToArray());
        }
      }
      catch (Exception ex)
      {
        LogTools.WriteToEventLog($"PCSYS PLC Printer Service Error: {ex.Message} for printer", EventLogEntryType.FailureAudit, "PCSYS PLC Server", -1, LogTools.Operation.Plc);
      }
      try
      {
        string sqlString = "SELECT tprinters.itemname FROM tppglineprinters INNER JOIN tppglines ON tppglineprinters.lineid = tppglines.Id INNER JOIN tprinters ON tppglineprinters.printeraliasid = tprinters.printerid WHERE(tppglines.type = 1)";
        string printerName = new IBSExchangeData().GetConnection((IEnumerable<IbsDataLink>) ServiceMain.AppSettings.DataLinks).ExecuteScalarString(sqlString);
        if (printerName == string.Empty)
        {
          StartManager.UpdatePlcPrinterStatus(0, "Printer is missing", ServiceMain.AppSettings.DataLinks);
        }
        else
        {
          string printStatus = new DataTools().GetPrintStatus(printerName, ServiceMain.AppSettings.DataLinks, (InfoService) null);
          StartManager.UpdatePlcPrinterStatus(printStatus == "Online" ? 1 : 0, printStatus, ServiceMain.AppSettings.DataLinks);
        }
      }
      catch (Exception ex)
      {
        LogTools.WriteToEventLog($"PCSYS PLC Printer Service Error: {ex.Message} for printer", EventLogEntryType.FailureAudit, "PCSYS PLC Server", -1, LogTools.Operation.Plc);
      }
    }
    catch (Exception ex)
    {
      LogTools.WriteToEventLog("PCSYS PLC Printer Service Error: " + ex.Message, EventLogEntryType.SuccessAudit, "PCSYS PLC Server", -1, LogTools.Operation.Plc);
    }
  }

  public static void UpdatePlcPrinterStatus(int status, string text, List<IbsDataLink> links)
  {
    int palletLineId = new DataTools().GetPalletLineId(links);
    string sqlString = $"IF (SELECT COUNT(*) FROM tppgplcstatus) > 0 UPDATE tppgplcstatus SET printerstatus = {status},printerstatustext = @printerstatustext" + $" ELSE INSERT INTO tppgplcstatus (printerstatus,printerstatustext) VALUES ({status},@printerstatustext)";
    List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
    {
      new IBSExternalParameter()
      {
        Name = "@printerstatustext",
        Value = (object) text
      }
    };
    new IBSExchangeData().GetConnection((IEnumerable<IbsDataLink>) links).ExecuteNonQueryWithParameters(sqlString, externalParameterList.ToArray());
    if (status == 0)
      DataTools.UpdateLineStatus(palletLineId, LineStatus.Print_error, links);
    else
      DataTools.ResetLineStatus(palletLineId, LineStatus.Print_error, links);
  }
}
