// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Service.Worker.LabelTools
// Assembly: PCSYS PLC Service, Version=1.0.7.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 3ECB826A-1CFB-42EB-9747-AB0DACAA30C1
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Service.exe

using PCSYS_PLC_Service.PLCConnection;
using PCSYS_PPG_Info;
using PCSYS_PPG_LPS_ProxyConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PCSYS_PLC_Service.Worker;

public static class LabelTools
{
  public static OrderInformation PrintLabel(
    OrderInformation labelData,
    int palletTypeId,
    string sscc,
    int qty)
  {
    if (PlcConfig.AppSettings.PtfPath == string.Empty)
      return new OrderInformation()
      {
        ErrorMessage = "PTF path is empty",
        HasAnError = true,
        ErrorCode = 210
      };
    if (!Directory.Exists(PlcConfig.AppSettings.PtfPath))
    {
      try
      {
        Directory.CreateDirectory(PlcConfig.AppSettings.PtfPath);
      }
      catch (Exception ex)
      {
        return new OrderInformation()
        {
          ErrorMessage = "Can't create PTF path " + PlcConfig.AppSettings.PtfPath,
          HasAnError = true,
          ErrorCode = 220
        };
      }
    }
    try
    {
      string printerAlias = DataTools.GetPrinterAlias(PlcConfig.AppSettings.DataLinks);
      if (printerAlias == string.Empty)
        return new OrderInformation()
        {
          ErrorMessage = "Printer Alias is missing",
          HasAnError = true,
          ErrorCode = 100
        };
      string document = LabelTools.GetDocument(palletTypeId);
      if (document == string.Empty)
        return new OrderInformation()
        {
          ErrorMessage = "Document is missing",
          HasAnError = true,
          ErrorCode = 110
        };
      string fileName = Path.Combine(PlcConfig.AppSettings.PtfPath, $"{Guid.NewGuid():N}.txt");
      string str = Path.Combine(PlcConfig.AppSettings.PtfPath, "Logs");
      if (!Directory.Exists(str))
        Directory.CreateDirectory(str);
      string logFileName = Path.Combine(str, $"{Guid.NewGuid():N}.txt");
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("PRINTER = " + printerAlias);
      sb.AppendLine("DOCUMENT = " + document);
      sb.AppendLine("SSCC = " + sscc);
      sb.AppendLine($"Qty = {qty}");
      sb.AppendLine($"FillingOrder = {labelData.FillingOrder}");
      sb.AppendLine($"Destination = {labelData.Destination}");
      sb.AppendLine($"Material = {labelData.MaterialNumber}");
      sb.AppendLine($"QtyOnPallet = {qty}");
      sb.AppendLine($"Sequence = {labelData.Sequence}");
      sb.AppendLine("LOGFILENAME = " + logFileName);
      Labelresult labelresult = LabelTools.WaitForPrintResponce(sb, fileName, logFileName);
      if (labelresult.HasAnError)
        return new OrderInformation()
        {
          ErrorMessage = labelresult.ErrorMessage,
          HasAnError = true,
          ErrorCode = 120
        };
    }
    catch (Exception ex)
    {
      return new OrderInformation()
      {
        ErrorMessage = ex.Message,
        HasAnError = true,
        ErrorCode = 120
      };
    }
    return new OrderInformation() { HasAnError = false };
  }

  public static Labelresult PrintResetLabel(string code)
  {
    code = "#!A1#!CA#DCA#G#!P1$";
    if (PlcConfig.AppSettings.PtfPath == string.Empty)
      return new Labelresult()
      {
        ErrorMessage = "PTF path is empty",
        HasAnError = true,
        ErrorCode = 210
      };
    if (!Directory.Exists(PlcConfig.AppSettings.PtfPath))
    {
      try
      {
        Directory.CreateDirectory(PlcConfig.AppSettings.PtfPath);
      }
      catch (Exception ex)
      {
        return new Labelresult()
        {
          ErrorMessage = "Can't create PTF path " + PlcConfig.AppSettings.PtfPath,
          HasAnError = true,
          ErrorCode = 220
        };
      }
    }
    try
    {
      string printerAlias = DataTools.GetPrinterAlias(PlcConfig.AppSettings.DataLinks);
      if (printerAlias == string.Empty)
        return new Labelresult()
        {
          ErrorMessage = "Printer Alias is missing",
          HasAnError = true,
          ErrorCode = 100
        };
      string fileName = Path.Combine(PlcConfig.AppSettings.PtfPath, $"{Guid.NewGuid():N}.txt");
      string str = Path.Combine(PlcConfig.AppSettings.PtfPath, "Logs");
      if (!Directory.Exists(str))
        Directory.CreateDirectory(str);
      string logFileName = Path.Combine(str, $"{Guid.NewGuid():N}.txt");
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("PRINTER = " + printerAlias);
      sb.AppendLine("DOCUMENTIMAGE = " + code);
      sb.AppendLine("LOGFILENAME = " + logFileName);
      sb.AppendLine("PRINTERMODE = DIRECT");
      sb.AppendLine("PRINTERFILEFORMAT = STRING");
      sb.AppendLine("PRINTERFAMILYNAME = Avery_Dennison");
      Labelresult labelresult = LabelTools.WaitForPrintResponce(sb, fileName, logFileName);
      if (labelresult.HasAnError)
        return new Labelresult()
        {
          ErrorMessage = labelresult.ErrorMessage,
          HasAnError = true,
          ErrorCode = 120
        };
    }
    catch (Exception ex)
    {
      return new Labelresult()
      {
        ErrorMessage = ex.Message,
        HasAnError = true,
        ErrorCode = 120
      };
    }
    return new Labelresult() { HasAnError = false };
  }

  public static Labelresult WaitForPrintResponce(
    StringBuilder sb,
    string fileName,
    string logFileName)
  {
    using (StreamWriter streamWriter = new StreamWriter(fileName))
    {
      streamWriter.Write(sb.ToString());
      streamWriter.Flush();
    }
    bool fileexists = File.Exists(logFileName);
    if (!Task.Run((Action) (() =>
    {
      while (!fileexists)
      {
        fileexists = File.Exists(logFileName);
        Thread.Sleep(1000);
      }
    })).Wait(TimeSpan.FromSeconds(30.0)))
      return new Labelresult()
      {
        ErrorMessage = "Timeout",
        ErrorCode = 230
      };
    if (File.Exists(logFileName))
    {
      string str = File.ReadAllText(logFileName);
      if (!str.ToLower().StartsWith("ok"))
        return new Labelresult()
        {
          HasAnError = true,
          ErrorMessage = str,
          ErrorCode = 240 /*0xF0*/
        };
    }
    return new Labelresult() { HasAnError = false };
  }

  private static string GetDocument(int pallettypeid)
  {
    string sqlString = $"SELECT tdocuments.itemname FROM dbo.tdocuments INNER JOIN tppgpalletdocuments ON tdocuments.documentid = tppgpalletdocuments.documentid INNER JOIN tdocumentsversion ON tdocuments.documentid = tdocumentsversion.documentid {$" WHERE(tdocumentsversion.active = 1) AND((tppgpalletdocuments.pallettype = {pallettypeid}) "}{$"OR ({pallettypeid} = 0 AND tppgpalletdocuments.isdefault = 1))"}";
    return new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) PlcConfig.AppSettings.DataLinks).ExecuteScalarString(sqlString);
  }

  public static int GetPrinterAliasId()
  {
    string sqlString = "SELECT tprinters.printerid FROM tppglines INNER JOIN tppglineprinters ON tppglines.Id = tppglineprinters.lineid INNER JOIN tprinters ON tppglineprinters.printeraliasid = tprinters.printerid  WHERE(tppglines.type = 1)";
    string s = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) PlcConfig.AppSettings.DataLinks).ExecuteScalarString(sqlString);
    return s != string.Empty ? int.Parse(s) : 0;
  }
}
