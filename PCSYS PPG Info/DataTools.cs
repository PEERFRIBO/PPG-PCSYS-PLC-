// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.DataTools
// Assembly: PCSYS PPG Info, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 1DA98507-3547-4F13-89C5-8F8721C43394
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG Info.dll

using Dart.Sockets;
using PCSYS_Event_Log;
using PCSYS_Plc_AppSettings;
using PCSYS_PPG_Info.PrintServiceInfo;
using PCSYS_PPG_LPS_ProxyConnector;
using PCSYS_PPG_LPS_ProxyConnector.DataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

#nullable disable
namespace PCSYS_PPG_Info;

public class DataTools
{
  private string GetPath(string profileName, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
    {
      new IBSExternalParameter()
      {
        Name = "@itemname",
        Value = (object) profileName
      }
    };
    string sqlString = "SELECT tprofileptf.path FROM tprofiles INNER JOIN tprofileptf ON tprofiles.profileid = tprofileptf.profileid  WHERE(tprofiles.itemname = @itemname)";
    string path = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarStringWithParameters(sqlString, externalParameterList.ToArray());
    if (path == string.Empty)
      throw new Exception("Path is empty for the Profile " + profileName);
    return Directory.Exists(path) ? path : throw new Exception($"Can't find the path {path} for the Profile {profileName}");
  }

  private string GetDocumentName(
    long materialNumber,
    int lineNo,
    int labelTypeId,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT tdocuments.itemname FROM tppgprintlabeltypes INNER JOIN tppgproductmapping ON tppgprintlabeltypes.Id = tppgproductmapping.printlabeltypeid INNER JOIN tppgproducts ON dbo.tppgproductmapping.productid = dbo.tppgproducts.id INNER JOIN tdocuments ON tppgprintlabeltypes.documentid = dbo.tdocuments.documentid" + $" WHERE(tppgproducts.materialnumber = {materialNumber}) AND(tppgproductmapping.labeltypeid = {labelTypeId})";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarString(sqlString);
  }

  public List<string> GetFieldNames(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData("SELECT name FROM tppgfieldnames ORDER BY name").Tables[0].Rows.Cast<DataRow>().Select<DataRow, string>((System.Func<DataRow, string>) (row => row["name"].ToString())).ToList<string>();
  }

  public Dictionary<int, string> GetFieldNameList(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData("SELECT id,name FROM tppgfieldnames ORDER BY name").Tables[0].Rows.Cast<DataRow>().ToDictionary<DataRow, int, string>((System.Func<DataRow, int>) (row => int.Parse(row["id"].ToString())), (System.Func<DataRow, string>) (row => row["name"].ToString()));
  }

  public DataSet GetPrinters(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData("SELECT * FROM tppgprinters");
  }

  public DataSet GetDestinationCodes(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData("SELECT * FROM tppgdestinationcodes");
  }

  private string GetLabelTypeName(long materialNumber, int labelTypeId, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT tppglabelnames.labelname FROM tppgproducts INNER JOIN  tppgproductmapping ON tppgproducts.id = tppgproductmapping.productid INNER JOIN tppglabeltypes ON tppgproductmapping.labeltypeid = tppglabeltypes.id INNER JOIN tppgprintlabeltypes ON tppgproductmapping.printlabeltypeid = tppgprintlabeltypes.Id INNER JOIN tppglabelnames ON tppgprintlabeltypes.labelnameid = tppglabelnames.id" + $" WHERE(tppgproducts.materialnumber = {materialNumber}) AND (tppgproductmapping.labeltypeid = {labelTypeId})";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarString(sqlString);
  }

  public List<PrintResult> Print(
    string profileName,
    List<DocumentInfo> documents,
    int lineNo,
    OrderInformation order,
    string statusReportPath,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links,
    int direction = 0)
  {
    return documents.Select<DocumentInfo, PrintResult>((System.Func<DocumentInfo, PrintResult>) (doc => this.Print(PrintMode.Print, profileName, doc.PrinterName, lineNo, order, doc.LabelTypeId, 1, statusReportPath, links, direction))).ToList<PrintResult>();
  }

  private int GetServerId(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT id  FROM tservers WHERE servername = @servername";
    List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
    {
      new IBSExternalParameter()
      {
        Name = "@servername",
        Value = (object) System.Net.Dns.GetHostName()
      }
    };
    return int.Parse(new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarStringWithParameters(sqlString, externalParameterList.ToArray()));
  }

  private List<string> GetPrinterOnLine(int lineNumber, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    int serverId = this.GetServerId(links);
    string sqlString = "SELECT tprinters.itemname FROM tinstalledprinters INNER JOIN tprinters INNER JOIN tppglineprinters ON tprinters.printerid = tppglineprinters.printeraliasid INNER JOIN tppglines ON tppglineprinters.lineid = tppglines.Id INNER JOIN tusedprinters ON tprinters.printerid = tusedprinters.printerid ON tinstalledprinters.id = tusedprinters.installedprinterid " + $" WHERE tppglines.linenumber = {lineNumber} AND serverid = {serverId}";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString).Tables[0].Rows.Cast<DataRow>().Select<DataRow, string>((System.Func<DataRow, string>) (row => row["itemname"].ToString())).ToList<string>();
  }

  public void SendResetCode(string printerName, int lineNumber, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string str = this.ResetPrinter(printerName, links);
    LogTools.WriteToEventLog(str == string.Empty ? "Sent Reset code to " + printerName : str, str == string.Empty ? EventLogEntryType.SuccessAudit : EventLogEntryType.Error, "PCSYS Line Print", lineNumber, LogTools.Operation.Printing);
  }

  public static OrderInformation GetOrderInformation(long fillingOrder, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT tppgproducts.materialnumber, tppgsaporders.fillingorder, tppgsaporders.sequence, tppgsaporders.destination  FROM tppgsaporders INNER JOIN " + $"tppgproducts ON tppgsaporders.productid = tppgproducts.id WHERE fillingorder = {fillingOrder}";
    DataTable table = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString).Tables[0];
    if (table.Rows.Count == 0)
      return new OrderInformation()
      {
        ErrorCode = 1021010,
        ErrorMessage = $"Can't find data order {fillingOrder}"
      };
    DataRow row = table.Rows[0];
    long num1 = row["materialnumber"] != DBNull.Value ? long.Parse(row["materialnumber"].ToString()) : -1L;
    if (num1 == -1L)
      return new OrderInformation()
      {
        ErrorCode = 1021016,
        ErrorMessage = "Material number is missing"
      };
    long num2 = row["fillingorder"] != DBNull.Value ? long.Parse(row["fillingorder"].ToString()) : -1L;
    if (num2 == -1L)
      return new OrderInformation()
      {
        ErrorCode = 1021018,
        ErrorMessage = "Filling order is missing"
      };
    long num3 = row["sequence"] != DBNull.Value ? long.Parse(row["sequence"].ToString()) : -1L;
    if (num3 == -1L)
      return new OrderInformation()
      {
        ErrorCode = 1021020,
        ErrorMessage = "Sequence is missing"
      };
    long num4 = row["destination"] != DBNull.Value ? long.Parse(row["destination"].ToString()) : -1L;
    if (num4 == -1L)
      return new OrderInformation()
      {
        ErrorCode = 1021020,
        ErrorMessage = "Destination is missing"
      };
    return new OrderInformation()
    {
      Destination = new long?(num4),
      FillingOrder = new long?(num2),
      MaterialNumber = new long?(num1),
      Sequence = new long?(num3)
    };
  }

  public PrintResult Print(
    PrintMode mode,
    string profileName,
    string printerAlias,
    int lineNo,
    OrderInformation order,
    int labelTypeId,
    int labelCount,
    string printStatusPath,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links,
    int direction = 0)
  {
    string path = this.GetPath(profileName, links);
    if (!Directory.Exists(path))
      return new PrintResult()
      {
        ErrorMessage = $"The path {path} does not exists",
        HasError = true
      };
    Dictionary<string, string> labelData = this.GetLabelData(order.MaterialNumber.Value, labelTypeId, links);
    string str1 = Path.Combine(path, "Images");
    if (!this.CheckPath(str1))
      return new PrintResult()
      {
        ErrorMessage = "Can't create path " + str1,
        HasError = true
      };
    string str2 = Path.Combine(path, "Logs");
    PrintResult printResult;
    if (this.CheckPath(str2))
    {
      printResult = this.PrintLabel(mode, printerAlias, lineNo, order, labelTypeId, labelCount, path, str1, str2, labelData, links, printStatusPath, direction);
    }
    else
    {
      printResult = new PrintResult();
      printResult.ErrorMessage = "Can't create path " + str2;
      printResult.HasError = true;
    }
    return printResult;
  }

  private PrintResult PrintLabel(
    PrintMode mode,
    string printerAlias,
    int lineNo,
    OrderInformation order,
    int labelTypeId,
    int labelCount,
    string path,
    string imagefilepath,
    string logfilepath,
    Dictionary<string, string> dataFields,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links,
    string PrintReportPath = "",
    int direction = 0)
  {
    try
    {
      string documentName = this.GetDocumentName(order.MaterialNumber.Value, lineNo, labelTypeId, links);
      if (documentName == string.Empty)
        return new PrintResult()
        {
          ErrorMessage = "Document is missing",
          HasError = true
        };
      string str1 = Guid.NewGuid().ToString("N");
      string path1 = Path.Combine(imagefilepath, str1 + ".jpg");
      string str2 = Path.Combine(logfilepath, str1 + ".txt");
      string ptfName = Path.Combine(path, str1 + ".txt");
      long? nullable = order.MaterialNumber;
      string labelTypeName = this.GetLabelTypeName(nullable.Value, labelTypeId, links);
      string str3 = Path.Combine(path, "Scripts");
      if (!Directory.Exists(str3))
        Directory.CreateDirectory(str3);
      string str4 = Guid.NewGuid().ToString("N");
      string str5 = Path.Combine(str3, str4 + ".txt");
      InitilizePrinter initilizePrinter = this.GetInitilizePrinter(printerAlias, links);
      int printerId = this.GetPrinterId(printerAlias, links);
      int installedPrinterId = this.GetInstalledPrinterId(printerAlias, links);
      string printerCode = this.GetPrinterCode(this.GetInstalledPrinterFamilyId(printerAlias, links), printerId, PrinterCode.Code, links);
      LabelInfo labelInfo = this.GetLabelInfo(installedPrinterId, PrinterCode.Code, links);
      if (labelInfo.Rotation == -1)
        labelInfo.Rotation = direction;
      bool flag = printerCode.IndexOf("<APP>", StringComparison.Ordinal) > -1 && mode == PrintMode.Print;
      if (flag)
      {
        int num1 = printerCode.IndexOf("<APP>", StringComparison.Ordinal);
        int num2 = printerCode.IndexOf("</APP>", StringComparison.Ordinal);
        string str6 = printerCode.Substring(num1 + 5, num2 - 5);
        string str7 = printerCode.Substring(num2 + 6, printerCode.Length - num2 - 6).Replace("$", string.Empty);
        string s = $"{documentName}\r\n{str7}\r\n{direction}" + $"\r\nFillingOrder\r\n{order.FillingOrder}" + $"\r\nMaterial\r\n{order.MaterialNumber}" + $"\r\nBatch\r\n{order.Sequence}" + $"\r\nBatchNumber\r\n{order.Sequence}" + $"\r\nVDBName\r\n{order.MaterialNumber}" + string.Format("\r\nPrintDateTime\r\n{0:yyyy-MM-dd hh:mm:ss}", (object) DateTime.Now);
        foreach (KeyValuePair<string, string> dataField in dataFields)
          s = $"{s}\r\n{dataField.Key}\r\n{dataField.Value}";
        int length = str6.IndexOf(":", StringComparison.CurrentCulture);
        string hostNameOrAddress = str6.Substring(0, length);
        int port = int.Parse(str6.Substring(length + 1, str6.Length - length - 1));
        try
        {
          Tcp tcp = new Tcp();
          tcp.Connect(new TcpSession(new Dart.Sockets.IPEndPoint(hostNameOrAddress, port)));
          if (tcp.State != Dart.Sockets.ConnectionState.Connected)
            return new PrintResult()
            {
              ErrorMessage = "No connection to " + hostNameOrAddress,
              HasError = true
            };
          tcp.Write(Encoding.UTF8.GetBytes(s));
          tcp.Close();
        }
        catch (Exception ex)
        {
          return new PrintResult()
          {
            ErrorMessage = ex.Message,
            HasError = true
          };
        }
      }
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("DOCUMENT = " + documentName);
      sb.AppendLine("PRINTER = " + printerAlias);
      sb.AppendLine($"FillingOrder = {order.FillingOrder}");
      sb.AppendLine(string.Format("PrintDateTime = {0:yyyy-MM-dd hh:mm:ss}", (object) DateTime.Now));
      sb.AppendLine(mode == PrintMode.View ? "IMAGEFILENAME = " + path1 : "PRINTERFILE = " + str5);
      sb.AppendLine($"LABELCOUNT = {labelCount}");
      sb.AppendLine("LOGFILENAME = " + str2);
      sb.AppendLine("Label_Control_Number = " + labelTypeName);
      sb.AppendLine($"Material = {order.MaterialNumber}");
      sb.AppendLine($"Batch = {order.Sequence}");
      StringBuilder stringBuilder = sb;
      nullable = order.Destination;
      int destination1;
      if (!nullable.HasValue)
      {
        destination1 = 0;
      }
      else
      {
        nullable = order.Destination;
        destination1 = (int) nullable.Value;
      }
      List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links1 = links;
      string str8 = "Destination = " + this.GetDestinationCode(destination1, links1);
      stringBuilder.AppendLine(str8);
      sb.AppendLine($"TURNLABEL = {labelInfo.Rotation}");
      sb.AppendLine($"LineNumber = {lineNo}");
      if (mode == PrintMode.Print)
      {
        if (!flag)
        {
          sb.AppendLine("INITIALIZATIONPRINTERNAME = " + initilizePrinter.PrinterName);
          sb.AppendLine("PRINTERCODES = " + printerCode);
          sb.AppendLine("SENDFILETOPRINT = YES");
        }
        if (PrintReportPath != string.Empty && labelCount == 1)
        {
          string path2 = PrintReportPath;
          nullable = order.FillingOrder;
          string order1 = nullable.ToString();
          string labelName = labelTypeName;
          string reportFileName = this.CreateReportFileName(path2, order1, labelName);
          sb.AppendLine("IMAGEFILENAME = " + path1);
          sb.AppendLine("Picture = " + path1);
          sb.AppendLine("PRINTSTATUSREPORT = PrintDocument");
          sb.AppendLine("PRINTSTATUSREPORTDESTINATION = " + reportFileName);
        }
      }
      foreach (KeyValuePair<string, string> dataField in dataFields)
        sb.AppendLine($"{dataField.Key} = {dataField.Value}");
      this.PrintLabelExecute(ptfName, str2, sb, links);
      LogFileInfo logFileInfo;
      for (logFileInfo = this.ReadLogFile(str2); !logFileInfo.Status; logFileInfo = this.ReadLogFile(str2))
        Thread.Sleep(100);
      if (!logFileInfo.Message.StartsWith("OK", StringComparison.OrdinalIgnoreCase))
        return new PrintResult()
        {
          ErrorMessage = logFileInfo.Message,
          HasError = true
        };
      if (mode != PrintMode.View | flag)
        return new PrintResult() { HasError = false };
      if (!System.IO.File.Exists(path1))
        return new PrintResult()
        {
          ErrorMessage = "The image file is missing",
          HasError = true
        };
      using (FileStream fileStream = new FileStream(path1, FileMode.Open, FileAccess.ReadWrite, FileShare.None, 1024 /*0x0400*/, FileOptions.DeleteOnClose))
      {
        using (MemoryStream destination2 = new MemoryStream())
        {
          fileStream.CopyTo((Stream) destination2);
          return new PrintResult()
          {
            Picture = destination2.ToArray()
          };
        }
      }
    }
    catch (Exception ex)
    {
      return new PrintResult()
      {
        ErrorMessage = ex.Message,
        HasError = true
      };
    }
  }

  private string CreateReportFileName(string path, string order, string labelName)
  {
    DateTime now = DateTime.Now;
    if (!Directory.Exists(path))
      return string.Empty;
    path = Path.Combine(path, now.Year.ToString());
    if (!Directory.Exists(path))
      Directory.CreateDirectory(path);
    path = Path.Combine(path, now.ToString("MM"));
    if (!Directory.Exists(path))
      Directory.CreateDirectory(path);
    path = Path.Combine(path, now.ToString("dd"));
    if (!Directory.Exists(path))
      Directory.CreateDirectory(path);
    return Path.Combine(path, $"{order}-{labelName}.Pdf");
  }

  private PrintResult PrintLabelExecute(
    string ptfName,
    string logfileName,
    StringBuilder sb,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    int num = 0;
    PrintResult printResult;
    for (printResult = this.PrintLabel(ptfName, logfileName, sb); printResult.HasError && num < 2; ++num)
      printResult = this.PrintLabel(ptfName, logfileName, sb);
    if (printResult.HasError)
    {
      GenericDataProviderClient connection = DataTools.GetConnection(DataTools.GetUrlConnection(links));
      try
      {
        connection.ActivateService("PCSYS PTF Service", ProcessToolsServiceAction.Restart);
        Thread.Sleep(1000);
        return this.PrintLabelExecute(ptfName, logfileName, sb, links);
      }
      catch (Exception ex)
      {
      }
      return new PrintResult() { HasError = true };
    }
    return new PrintResult() { HasError = false };
  }

  private PrintResult PrintLabel(string ptfName, string logfileName, StringBuilder sb)
  {
    using (StreamWriter streamWriter = new StreamWriter(ptfName))
    {
      streamWriter.Write(sb.ToString());
      streamWriter.Flush();
    }
    if (!Task.Run((Action) (() =>
    {
      while (!System.IO.File.Exists(logfileName))
        Thread.Sleep(100);
    })).Wait(TimeSpan.FromMilliseconds(60000.0)))
      return new PrintResult()
      {
        ErrorMessage = "Timeout",
        HasError = true
      };
    return new PrintResult() { HasError = false };
  }

  private string ReplaceVariable(DataTable table, string fieldName)
  {
    DataRow[] dataRowArray = table.Select($"fieldname = '{fieldName}'");
    return dataRowArray.Length != 0 ? dataRowArray[0]["variablename"].ToString() : fieldName;
  }

  public static DataTable GetSapUnSendCodes(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData("SELECT * FROM tppgsapreturncodes WHERE sendtosap IS NULL").Tables[0];
  }

  public static void UpdateSapCodes(string code, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = $"UPDATE tppgsapreturncodes SET sendtosap = getdate() WHERE code =  '{code}'";
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQuery(sqlString);
  }

  public static void InsertManuelPrintData(
    long? fillingOrder,
    int qty,
    int printerId,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQuery("DELETE FROM tppgmanuelprint");
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQuery("INSERT INTO tppgmanuelprint (fillingorder,qty,printerid) " + $"VALUES ({fillingOrder},{qty},{printerId})");
  }

  public DataTable GetPrintedSapData(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "EXEC PPG_GetPrintedSapJobs";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString).Tables[0];
  }

  private InitilizePrinter GetInitilizePrinterInfo(string printerAlias) => (InitilizePrinter) null;

  private LogFileInfo ReadLogFile(string logFileName)
  {
    try
    {
      using (FileStream fileStream = System.IO.File.Open(logFileName, FileMode.Open, FileAccess.Read, FileShare.None))
      {
        using (StreamReader streamReader = new StreamReader((Stream) fileStream))
          return new LogFileInfo()
          {
            Message = streamReader.ReadToEnd(),
            Status = true
          };
      }
    }
    catch (Exception ex)
    {
      return new LogFileInfo() { Status = false };
    }
  }

  public int GetPalletLineNo(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT linenumber FROM tppglines where type = 1";
    string str = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarString(sqlString);
    return str == string.Empty ? -1 : Convert.ToInt32(str);
  }

  public int GetPalletLineId(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT id FROM tppglines where type = 1";
    string str = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarString(sqlString);
    return str == string.Empty ? -1 : Convert.ToInt32(str);
  }

  public int GetLineId(int lineNo, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = $"SELECT id FROM tppglines where linenumber = {lineNo} ";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarInt(sqlString);
  }

  public string ResetPrinter(string printerName, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string printerCode = this.GetPrinterCode(this.GetInstalledPrinterFamilyId(printerName, links), this.GetPrinterId(printerName, links), PrinterCode.Reset, links);
    return this.SendCodeToPrinter(printerName, links, printerCode);
  }

  public string ResetPrinter(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    return this.ResetPrinter(DataTools.GetPrinterAlias(links), links);
  }

  public static string GetPrinterAlias(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT tprinters.itemname FROM tppglines INNER JOIN tppglineprinters ON tppglines.Id = tppglineprinters.lineid INNER JOIN tprinters ON tppglineprinters.printeraliasid = tprinters.printerid  WHERE(tppglines.type = 1)";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarString(sqlString);
  }

  private string SendCodeToPrinter(string printerName, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links, string code)
  {
    string path1 = this.GetPath("PPG", links);
    if (!Directory.Exists(path1))
    {
      try
      {
        Directory.CreateDirectory(path1);
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
    }
    try
    {
      string path2 = Path.Combine(path1, $"{Guid.NewGuid():N}.txt");
      string str = Path.Combine(path1, "Logs");
      if (!Directory.Exists(str))
        Directory.CreateDirectory(str);
      string logfilename = Path.Combine(str, $"{Guid.NewGuid():N}.txt");
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("PRINTER = " + printerName);
      stringBuilder.AppendLine("DOCUMENTIMAGE = " + code.Replace("$", string.Empty));
      stringBuilder.AppendLine("LOGFILENAME = " + logfilename);
      stringBuilder.AppendLine("PRINTERMODE = DIRECT");
      stringBuilder.AppendLine("PRINTERFILEFORMAT = STRING");
      stringBuilder.AppendLine("PRINTERFAMILYNAME = Avery_Dennison");
      using (StreamWriter streamWriter = new StreamWriter(path2))
      {
        streamWriter.Write(stringBuilder.ToString());
        streamWriter.Flush();
      }
      if (!Task.Run((Action) (() =>
      {
        while (!System.IO.File.Exists(logfilename))
          Thread.Sleep(500);
      })).Wait(TimeSpan.FromMilliseconds(20000.0)))
        return "Timeout";
      LogFileInfo logFileInfo;
      for (logFileInfo = this.ReadLogFile(logfilename); !logFileInfo.Status; logFileInfo = this.ReadLogFile(logfilename))
        Thread.Sleep(100);
      return !logFileInfo.Message.StartsWith("OK", StringComparison.OrdinalIgnoreCase) ? logFileInfo.Message : string.Empty;
    }
    catch (Exception ex)
    {
      return ex.Message;
    }
  }

  public PrintResult PrintPallet(
    PrintMode mode,
    OrderInformation data,
    string printAlias,
    string statusReportPath,
    int labelCount,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    int palletLineNo = this.GetPalletLineNo(links);
    if (palletLineNo < 0)
      return new PrintResult()
      {
        ErrorMessage = "Can't find a line number for Pallet ",
        HasError = true
      };
    DataTable table = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData($"SELECT * FROM tppgprintedsscc WHERE sscc = '{data.Sscc}'").Tables[0];
    int lineNo = 0;
    int qtyOnPallet = data.QtyOnPallet;
    bool flag = false;
    string sscc;
    if (table.Rows.Count > 0)
    {
      sscc = table.Rows[0]["sscc"].ToString();
      lineNo = int.Parse(table.Rows[0]["lineno"].ToString());
      qtyOnPallet = int.Parse(table.Rows[0]["qtyonpallet"].ToString());
      data.FillingOrder = new long?(long.Parse(table.Rows[0]["fillingorder"].ToString()));
      data.Destination = new long?(long.Parse(table.Rows[0]["destination"].ToString()));
      data.Sequence = new long?(long.Parse(table.Rows[0]["sequence"].ToString()));
      data.MaterialNumber = new long?(long.Parse(table.Rows[0]["materialnumber"].ToString()));
    }
    else
    {
      flag = true;
      sscc = new DataTools().CreateNewBarCode(data, Convert.ToInt32(palletLineNo), true, links).Sscc;
    }
    PrintResult printResult;
    try
    {
      printResult = this.PrintPalletLabel(mode, data, 0, printAlias, sscc, labelCount, qtyOnPallet, links, statusReportPath, lineNo);
      if (flag)
      {
        PalletQueue pq = new PalletQueue();
        pq.OrderInfo = data;
        pq.Sscc = sscc;
        pq.Qty = data.QtyOnPallet;
        pq.Ean = DataTools.GetEan(data.MaterialNumber, links).Value.ToString();
        DatabaseAction packItemCount = DataTools.GetPackItemCount(data.MaterialNumber, links);
        if (!string.IsNullOrEmpty(packItemCount.ErrorMessage))
          return new PrintResult()
          {
            ErrorMessage = packItemCount.ErrorMessage,
            HasError = true
          };
        pq.ItemPackCount = int.Parse(packItemCount.Value.ToString());
        DataTools.SavePrinted(pq, links);
        DataTools.UpdateManuelPrintData(pq, links);
      }
    }
    catch (Exception ex)
    {
      return new PrintResult()
      {
        ErrorMessage = ex.Message,
        HasError = true
      };
    }
    return printResult;
  }

  public static void SavePrintedSsccLabels(
    OrderInformation order,
    string sscc,
    int lineNo,
    int qty,
    int packages,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
    {
      new IBSExternalParameter()
      {
        Name = "@sscc",
        Value = (object) sscc
      },
      new IBSExternalParameter()
      {
        Name = "@materialnumber",
        Value = (object) order.MaterialNumber
      },
      new IBSExternalParameter()
      {
        Name = "@fillingorder",
        Value = (object) order.FillingOrder
      },
      new IBSExternalParameter()
      {
        Name = "@sequence",
        Value = (object) order.Sequence
      },
      new IBSExternalParameter()
      {
        Name = "@destination",
        Value = (object) order.Destination
      }
    };
    string sqlString = "INSERT INTO tppgprintedsscc (sscc,materialnumber,fillingorder,sequence,destination,qtyonpallet,packages,[lineno],printtime) VALUES (" + $"@sscc,@materialnumber,@fillingorder,@sequence,@destination,{qty},{packages},{lineNo},GetDate())";
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQueryWithParameters(sqlString, externalParameterList.ToArray());
  }

  private int GetPrinterId(string printerName, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT printerid FROM dbo.tprinters WHERE itemname = @itemname";
    List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
    {
      new IBSExternalParameter()
      {
        Name = "@itemname",
        Value = (object) printerName
      }
    };
    string s = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarStringWithParameters(sqlString, externalParameterList.ToArray());
    return s == string.Empty ? -1 : int.Parse(s);
  }

  public PrintResult PrintPallet(
    PrintMode mode,
    OrderInformation labelData,
    int palletTypeId,
    string printerAlias,
    string sscc,
    int labelCount,
    int qtyOnPallet,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links,
    string printReportPath,
    int lineNo = 0)
  {
    if (mode == PrintMode.View)
      return this.PrintPalletLabel(mode, labelData, palletTypeId, printerAlias, sscc, labelCount, qtyOnPallet, links, string.Empty, lineNo);
    for (int index = 0; index < 2; ++index)
    {
      PrintResult printResult = this.PrintPalletLabel(mode, labelData, palletTypeId, printerAlias, sscc, labelCount, qtyOnPallet, links, printReportPath, lineNo);
      if (!printResult.HasError)
        return printResult;
    }
    return this.PrintPalletLabel(mode, labelData, palletTypeId, printerAlias, sscc, labelCount, qtyOnPallet, links, printReportPath, lineNo);
  }

  public PrintResult PrintPalletLabel(
    PrintMode mode,
    OrderInformation labelData,
    int palletTypeId,
    string printerAlias,
    string sscc,
    int labelCount,
    int qtyOnPallet,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links,
    string PrintReportPath = "",
    int lineNo = 0)
  {
    string path1 = this.GetPath("PPG", links);
    if (!Directory.Exists(path1))
      return new PrintResult()
      {
        ErrorMessage = $"The path {path1} does not exists",
        HasError = true
      };
    Dictionary<string, string> productData = this.GetProductData(labelData.MaterialNumber.Value, links);
    string str1 = Path.Combine(path1, "Images");
    if (!this.CheckPath(str1))
      return new PrintResult()
      {
        ErrorMessage = "Can't create path " + str1,
        HasError = true
      };
    string path2 = Path.Combine(path1, "Logs");
    if (!Directory.Exists(path2))
    {
      try
      {
        Directory.CreateDirectory(path2);
      }
      catch (Exception ex)
      {
        return new PrintResult()
        {
          ErrorMessage = "Can't create PTF path " + path2,
          HasError = true
        };
      }
    }
    try
    {
      string document = DataTools.GetDocument(palletTypeId, links);
      if (document == string.Empty)
        return new PrintResult()
        {
          ErrorMessage = "Document is missing",
          HasError = true
        };
      string ptfName = Path.Combine(path1, $"{Guid.NewGuid():N}.txt");
      string str2 = Path.Combine(path1, "Scripts");
      if (!Directory.Exists(str2))
        Directory.CreateDirectory(str2);
      InitilizePrinter initilizePrinter = this.GetInitilizePrinter(printerAlias, links);
      string str3 = Path.Combine(path1, "Logs");
      if (!Directory.Exists(str3))
        Directory.CreateDirectory(str3);
      string str4 = Path.Combine(str3, $"{Guid.NewGuid():N}.txt");
      string path3 = Path.Combine(str1, $"{Guid.NewGuid():N}.jpg");
      int printerId = this.GetPrinterId(printerAlias, links);
      string printerCode = this.GetPrinterCode(this.GetInstalledPrinterFamilyId(printerAlias, links), printerId, PrinterCode.Code, links);
      string str5 = Path.Combine(str2, $"{Guid.NewGuid():N}Pallet.txt");
      string destinationCode = this.GetDestinationCode((int) labelData.Destination.Value, links);
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("PRINTER = " + printerAlias);
      sb.AppendLine("DOCUMENT = " + document);
      sb.AppendLine("PRINTFILENAME = " + document);
      sb.AppendLine($"LABELCOUNT = {labelCount}");
      sb.AppendLine(mode == PrintMode.View ? "IMAGEFILENAME = " + path3 : "PRINTERFILE = " + str5);
      sb.AppendLine("SSCC = " + sscc);
      sb.AppendLine($"Qty = {labelCount}");
      sb.AppendLine($"FillingOrder = {labelData.FillingOrder}");
      sb.AppendLine("Destination = " + destinationCode);
      sb.AppendLine($"ItemNumber = {labelData.MaterialNumber}");
      sb.AppendLine($"Material = {labelData.MaterialNumber}");
      sb.AppendLine($"QtyOnPallet = {qtyOnPallet}");
      sb.AppendLine($"BatchNumber = {labelData.Sequence}");
      sb.AppendLine($"Batch = {labelData.Sequence}");
      sb.AppendLine("LOGFILENAME = " + str4);
      sb.AppendLine($"LineNumber = {lineNo}");
      sb.AppendLine($"PrintDateTime = {DateTime.Now}");
      sb.AppendLine("INITIALIZATIONPRINTERNAME = " + initilizePrinter.PrinterName);
      sb.AppendLine("PRINTERCODES = " + printerCode);
      sb.AppendLine("SENDFILETOPRINT = YES");
      if (PrintReportPath != string.Empty && labelCount == 1)
      {
        string reportFileName = this.CreateReportFileName(PrintReportPath, labelData.FillingOrder.ToString(), sscc);
        sb.AppendLine("IMAGEFILENAME = " + path3);
        sb.AppendLine("Picture = " + path3);
        sb.AppendLine("PRINTSTATUSREPORT = PrintDocument");
        sb.AppendLine("PRINTSTATUSREPORTDESTINATION = " + reportFileName);
      }
      foreach (KeyValuePair<string, string> keyValuePair in productData)
        sb.AppendLine($"{keyValuePair.Key} = {keyValuePair.Value}");
      this.PrintLabelExecute(ptfName, str4, sb, links);
      LogFileInfo logFileInfo;
      for (logFileInfo = this.ReadLogFile(str4); !logFileInfo.Status; logFileInfo = this.ReadLogFile(str4))
        Thread.Sleep(100);
      if (!logFileInfo.Message.StartsWith("OK", StringComparison.OrdinalIgnoreCase))
        return new PrintResult()
        {
          ErrorMessage = logFileInfo.Message,
          HasError = true
        };
      if (mode != PrintMode.View)
        return new PrintResult();
      if (!System.IO.File.Exists(path3))
        return new PrintResult()
        {
          ErrorMessage = "The image file is missing",
          HasError = true
        };
      using (FileStream fileStream = new FileStream(path3, FileMode.Open, FileAccess.ReadWrite, FileShare.None, 1024 /*0x0400*/, FileOptions.DeleteOnClose))
      {
        using (MemoryStream destination = new MemoryStream())
        {
          fileStream.CopyTo((Stream) destination);
          return new PrintResult()
          {
            Picture = destination.ToArray()
          };
        }
      }
    }
    catch (Exception ex)
    {
      return new PrintResult()
      {
        ErrorMessage = ex.Message,
        HasError = true
      };
    }
  }

  private static string GetDocument(int pallettypeid, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = $"SELECT tdocuments.itemname FROM dbo.tdocuments INNER JOIN tppgpalletdocuments ON tdocuments.documentid = tppgpalletdocuments.documentid INNER JOIN tdocumentsversion ON tdocuments.documentid = tdocumentsversion.documentid {$" WHERE(tdocumentsversion.active = 1) AND((tppgpalletdocuments.pallettype = {pallettypeid}) "}{$"OR ({pallettypeid} = 0 AND tppgpalletdocuments.isdefault = 1))"}";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarString(sqlString);
  }

  private int GetInstalledPrinterId(string printerName, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    int serverId = this.GetServerId(links);
    try
    {
      string sqlString = "SELECT tinstalledprinters.id FROM dbo.tprinters INNER JOIN tusedprinters ON tprinters.printerid = tusedprinters.printerid INNER JOIN tinstalledprinters ON tusedprinters.installedprinterid = tinstalledprinters.id " + $"WHERE itemname = @itemname AND serverid = {serverId}";
      List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
      {
        new IBSExternalParameter()
        {
          Name = "@itemname",
          Value = (object) printerName
        }
      };
      return int.Parse(new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarStringWithParameters(sqlString, externalParameterList.ToArray()));
    }
    catch (Exception ex)
    {
      return -1;
    }
  }

  private int GetInstalledPrinterFamilyId(string printerName, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    int serverId = this.GetServerId(links);
    try
    {
      string sqlString = "SELECT tinstalledprinters.printerfamily FROM dbo.tprinters INNER JOIN tusedprinters ON tprinters.printerid = tusedprinters.printerid INNER JOIN tinstalledprinters ON tusedprinters.installedprinterid = tinstalledprinters.id " + $"WHERE itemname = @itemname AND serverid = {serverId}";
      List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
      {
        new IBSExternalParameter()
        {
          Name = "@itemname",
          Value = (object) printerName
        }
      };
      return int.Parse(new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarStringWithParameters(sqlString, externalParameterList.ToArray()));
    }
    catch (Exception ex)
    {
      return -1;
    }
  }

  private string GetDestinationCode(int destination, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    try
    {
      string sqlString = $"SELECT code FROM tppgdestinationcodes WHERE destination = {destination}";
      return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarString(sqlString);
    }
    catch (Exception ex)
    {
      return string.Empty;
    }
  }

  private InitilizePrinter GetInitilizePrinter(string printerName, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString1 = "SELECT tinstalledprinters.printerfamily FROM dbo.tprinters INNER JOIN tusedprinters ON tprinters.printerid = tusedprinters.printerid INNER JOIN tinstalledprinters ON tusedprinters.installedprinterid = tinstalledprinters.id " + $"WHERE itemname = @itemname AND serverid = {this.GetServerId(links)}";
    List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
    {
      new IBSExternalParameter()
      {
        Name = "@itemname",
        Value = (object) printerName
      },
      new IBSExternalParameter()
      {
        Name = "@servername",
        Value = (object) "localhost"
      }
    };
    string str1 = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarStringWithParameters(sqlString1, externalParameterList.ToArray());
    if (str1 == string.Empty)
      throw new Exception("Printer Family is missing");
    string sqlString2 = "SELECT id,printerid FROM tppgprinters WHERE printerfamilyid = " + str1;
    DataTable table = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString2).Tables[0];
    if (table.Rows.Count == 0)
      throw new Exception("Initilize Printer info is missing");
    string sqlString3 = $"SELECT itemname FROM tprinters WHERE printerid = {table.Rows[0]["printerid"]}";
    string str2 = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarString(sqlString3);
    if (str2 == string.Empty)
      throw new Exception("Initilize Printer does not exists");
    return new InitilizePrinter()
    {
      PrinterName = str2,
      PrinterId = int.Parse(table.Rows[0]["id"].ToString())
    };
  }

  private bool CheckPath(string path)
  {
    try
    {
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      return true;
    }
    catch (Exception ex)
    {
      return false;
    }
  }

  public static DataTable GetStatistic(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT count,avg,errors,hour FROM tstatistics where date = cast(getdate() AS date)";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString).Tables[0];
  }

  public static DataTable GetAllLogs(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData("SELECT TOP(1000) message AS Message,operation AS Operation,applicationname as Application,eventlogentrytype AS Type,logdatetime AS Time FROM tppgeventlog ORDER BY id DESC").Tables[0];
  }

  public static DataTable GetLogs(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData("SELECT TOP(100) message AS Message,eventlogentrytype AS Type,FORMAT(logdatetime,'hh:mm:ss') AS Time FROM tppgeventlog" + $" WHERE datediff(day, logdatetime, '{DateTime.Now.Date:yyyy-MM-dd}') = 0 AND applicationname = 'PCSYS_PLC_Service' ORDER BY id DESC").Tables[0];
  }

  public static List<LogItem> GetEventLogs(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    return DataTools.GetLogs(links).Rows.Cast<DataRow>().Select<DataRow, LogItem>((System.Func<DataRow, LogItem>) (log => new LogItem()
    {
      EventType = (EventLogEntryType) log["Type"],
      Message = log["Message"].ToString(),
      Time = log["Time"].ToString()
    })).ToList<LogItem>();
  }

  private static int GetLineNoByLineId(int lineId, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarInt($"SELECT linenumber FROM tppglines WHERE id = {lineId}");
  }

  public DataTable GetUserInput(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT tppglinedata.sscccode,tppglinedata.qtyonpalletplc, tppglinedata.qtyonpalletmaster, tppglinedata.value  FROM tppglines INNER JOIN tppglinedata ON tppglines.Id = tppglinedata.lineid WHERE(tppglinedata.status = 3)";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString).Tables[0];
  }

  public static string ExecuteUserResponse(
    int code,
    int value,
    string statusReportPath,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    LogTools.WriteToEventLog("Create label for pallet", EventLogEntryType.SuccessAudit, code, LogTools.Operation.Plc, links);
    string printerAlias = DataTools.GetPrinterAlias(links);
    PalletQueue pq = new PalletQueue();
    try
    {
      pq = DataTools.GetPalletQueue((long) code, links);
      pq.Qty = value;
    }
    catch (Exception ex)
    {
      LogTools.WriteToEventLog(ex.Message, EventLogEntryType.Error, pq.LineNumber, LogTools.Operation.Plc, links);
      return "Can't find pallet informations";
    }
    PrintResult printResult = new DataTools().PrintPalletLabel(PrintMode.Print, pq.OrderInfo, 0, printerAlias, pq.Sscc, 1, pq.Qty, links, statusReportPath, pq.LineNumber);
    if (!string.IsNullOrEmpty(printResult.ErrorMessage) && !printResult.ErrorMessage.ToLower().StartsWith("ok"))
      LogTools.WriteToEventLog(printResult.ErrorMessage, EventLogEntryType.Error, code, LogTools.Operation.Plc, links);
    else
      DataTools.DeletePrinterQueue((long) code, links);
    DataTools.SavePrinted(pq, links);
    return string.Empty;
  }

  private static void UpdateManuelPrintData(PalletQueue pq, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    try
    {
      string sqlString = $"UPDATE tppgmanuelprint SET fillingorder = {pq.OrderInfo.FillingOrder}, qty = {pq.Qty}";
      new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQuery(sqlString);
    }
    catch (Exception ex)
    {
      LogTools.WriteToEventLog("Manuel print data error " + ex.Message, EventLogEntryType.Error, pq.LineNumber, LogTools.Operation.Plc, links);
    }
  }

  public static void SavePrinted(PalletQueue pq, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    try
    {
      int qty = pq.Qty * pq.ItemPackCount;
      string code = pq.OrderInfo.FillingOrder.ToString().PadLeft(12, '0') + pq.Sscc.PadLeft(20, '0') + qty.ToString().PadLeft(13, '0') + pq.Ean.PadLeft(13, '0');
      LogTools.WriteToEventLog("SAP info: " + code, EventLogEntryType.SuccessAudit, pq.LineNumber, LogTools.Operation.Plc, links);
      new DataTools().InsertReturnToSapData(code, links);
      DataTools.SavePrintedSsccLabels(pq.OrderInfo, pq.Sscc, pq.LineNumber, qty, pq.Qty, links);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message, ex);
    }
  }

  public static void UpdateLineStatus(int lineId, LineStatus status, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = $"UPDATE tppglinedata SET status = {(int) status} WHERE lineid = {lineId} AND status <> {(int) status} AND status <> 4";
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQuery(sqlString);
  }

  public static void UpdatePalletLineStatus(
    int palletLineId,
    int lineNo,
    int ssccCode,
    string messages,
    LineStatus status,
    int value,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = $"UPDATE tppglinedata SET status = {(int) status},sourcelinenumber = {lineNo}," + $"sscccode = '{ssccCode}',Messages = @Massages,value = {value} WHERE lineid = {palletLineId}";
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQuery(sqlString);
  }

  public static void ReleaseLineStatusFromError(
    int lineId,
    LineStatus status,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = $"UPDATE tppglinedata SET status = {(int) status} WHERE lineid = {lineId} AND status <> 1";
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQuery(sqlString);
  }

  public static void InsertPalletQueue(
    long? fillingOrder,
    int lineNo,
    long counter,
    string sscc,
    string ean,
    int qty,
    int packitemqty,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    DataTools.DeletePrinterQueue(counter, links);
    string sqlString = "INSERT INTO tppgpalletqueue (counter,fillingorder,[lineno],qty,sscc,ean,packitemqty) VALUES " + $"({counter},{fillingOrder},{lineNo},{qty},'{sscc}','{ean}',{packitemqty})";
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQuery(sqlString);
  }

  public static void DeletePrinterQueue(long counter, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = $"DELETE FROM tppgpalletqueue WHERE counter = {counter}";
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQuery(sqlString);
  }

  public static PalletQueue GetPalletQueue(long counter, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = $"SELECT * FROM tppgpalletqueue WHERE counter = {counter}";
    DataTable table = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString).Tables[0];
    if (table.Rows.Count == 0)
      throw new Exception($"Counter {counter} is missing inside Pallet Queue");
    return new PalletQueue()
    {
      OrderInfo = DataTools.GetOrderInformation(long.Parse(table.Rows[0]["fillingorder"].ToString()), links),
      Qty = int.Parse(table.Rows[0]["qty"].ToString()),
      LineNumber = int.Parse(table.Rows[0]["lineno"].ToString()),
      Sscc = table.Rows[0]["sscc"].ToString(),
      Ean = table.Rows[0]["ean"].ToString(),
      ItemPackCount = int.Parse(table.Rows[0]["packitemqty"].ToString())
    };
  }

  public static void ResetLineStatus(int lineId, LineStatus status, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = $"UPDATE tppglinedata SET status = {1} WHERE lineid = {lineId} AND status = {(int) status}";
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQuery(sqlString);
  }

  public void InsertReturnToSapData(string code, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
    {
      new IBSExternalParameter()
      {
        Name = "@sapcode",
        Value = (object) code
      }
    };
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQueryWithParameters("INSERT INTO tppgsapreturncodes (sapcode,created) VALUES (@sapcode,getdate())", externalParameterList.ToArray());
  }

  public static DatabaseAction GetMaxQty(long? material, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    try
    {
      string sqlString = "SELECT tppgproductdata.text FROM tppgproducts INNER JOIN tppgproductdata ON tppgproducts.id = tppgproductdata.productid INNER JOIN tppgfieldnames ON tppgproductdata.columnid = tppgfieldnames.id " + $"WHERE(tppgproducts.materialnumber = {material}) AND (tppgfieldnames.isfullpalletfield = 1)";
      string str = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarString(sqlString);
      DatabaseAction maxQty;
      if (!(str == string.Empty))
      {
        maxQty = new DatabaseAction()
        {
          Value = (object) str
        };
      }
      else
      {
        maxQty = new DatabaseAction();
        maxQty.ErrorMessage = $"Can't find Qty for Materiale no {material}";
      }
      return maxQty;
    }
    catch (Exception ex)
    {
      return new DatabaseAction()
      {
        ErrorMessage = ex.Message
      };
    }
  }

  public static DatabaseAction GetEan(long? material, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    try
    {
      string sqlString = "SELECT tppgproductdata.text FROM tppgproducts INNER JOIN tppgproductdata ON tppgproducts.id = tppgproductdata.productid INNER JOIN tppgfieldnames ON tppgproductdata.columnid = tppgfieldnames.id " + $"WHERE(tppgproducts.materialnumber = {material}) AND (tppgfieldnames.isean = 1)";
      string str = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarString(sqlString);
      if (string.IsNullOrEmpty(str))
        return new DatabaseAction()
        {
          ErrorMessage = "EAN is empty"
        };
      DatabaseAction ean;
      if (!(str == string.Empty))
      {
        ean = new DatabaseAction() { Value = (object) str };
      }
      else
      {
        ean = new DatabaseAction();
        ean.ErrorMessage = $"Can't find EAN for Materiale no {material}";
      }
      return ean;
    }
    catch (Exception ex)
    {
      return new DatabaseAction()
      {
        ErrorMessage = ex.Message
      };
    }
  }

  public static DatabaseAction GetPackItemCount(long? material, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    try
    {
      string sqlString = "SELECT tppgproductdata.text FROM tppgproducts INNER JOIN tppgproductdata ON tppgproducts.id = tppgproductdata.productid INNER JOIN tppgfieldnames ON tppgproductdata.columnid = tppgfieldnames.id " + $"WHERE(tppgproducts.materialnumber = {material}) AND (tppgfieldnames.isqtypritem = 1)";
      string str = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarString(sqlString);
      LogTools.WriteToEventLog("Qty sql  " + sqlString, EventLogEntryType.SuccessAudit, 0, LogTools.Operation.Plc, links);
      DatabaseAction packItemCount;
      if (!(str == string.Empty))
      {
        packItemCount = new DatabaseAction()
        {
          Value = (object) Convert.ToInt32(str.Trim())
        };
      }
      else
      {
        packItemCount = new DatabaseAction();
        packItemCount.ErrorMessage = $"Can't find UnitPackQty for Materiale no {material}";
      }
      return packItemCount;
    }
    catch (Exception ex)
    {
      return new DatabaseAction()
      {
        ErrorMessage = ex.Message
      };
    }
  }

  public static DataTable GetSendToSap(DateTime date, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT * FROM tppgsapreturncodes WHERE created IS NOT NULL ORDER BY id DESC";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString).Tables[0];
  }

  public PalletQueue GetPalletQueueById(int ssccId, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = $"SELECT * FROM tppgprintedsscc WHERE id = {ssccId}";
    DataTable table = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString).Tables[0];
    if (table.Rows.Count == 0)
      return new PalletQueue();
    return new PalletQueue()
    {
      Sscc = table.Rows[0]["sscc"].ToString(),
      Qty = int.Parse(table.Rows[0]["qtyonpallet"].ToString()),
      LineNumber = int.Parse(table.Rows[0]["lineno"].ToString()),
      OrderInfo = new OrderInformation()
      {
        FillingOrder = new long?(long.Parse(table.Rows[0]["fillingorder"].ToString())),
        MaterialNumber = new long?(long.Parse(table.Rows[0]["materialnumber"].ToString())),
        QtyOnPallet = int.Parse(table.Rows[0]["qtyonpallet"].ToString()),
        Sequence = new long?(long.Parse(table.Rows[0]["sequence"].ToString()))
      }
    };
  }

  public void InsertDataToConstore(
    PalletQueue queue,
    string connectionString,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    try
    {
      Dictionary<string, string> productData = this.GetProductData(queue.OrderInfo.MaterialNumber.Value, links);
      KeyValuePair<string, string> keyValuePair = productData.FirstOrDefault<KeyValuePair<string, string>>((System.Func<KeyValuePair<string, string>, bool>) (a => a.Key == "Description"));
      string str1 = keyValuePair.Value;
      keyValuePair = productData.FirstOrDefault<KeyValuePair<string, string>>((System.Func<KeyValuePair<string, string>, bool>) (a => a.Key == "EAN13"));
      string str2 = keyValuePair.Value;
      keyValuePair = productData.FirstOrDefault<KeyValuePair<string, string>>((System.Func<KeyValuePair<string, string>, bool>) (a => a.Key == "EAN13B"));
      string str3 = keyValuePair.Value;
      keyValuePair = productData.FirstOrDefault<KeyValuePair<string, string>>((System.Func<KeyValuePair<string, string>, bool>) (a => a.Key == "FullPalletQty"));
      string str4 = keyValuePair.Value;
      keyValuePair = productData.FirstOrDefault<KeyValuePair<string, string>>((System.Func<KeyValuePair<string, string>, bool>) (a => a.Key == "Volume"));
      string str5 = keyValuePair.Value;
      keyValuePair = productData.FirstOrDefault<KeyValuePair<string, string>>((System.Func<KeyValuePair<string, string>, bool>) (a => a.Key == "Volume_Unit"));
      string str6 = keyValuePair.Value;
      string sqlString1 = "IF EXISTS (SELECT ProductID FROM Product WHERE PartNumber = @partnumber) SELECT ProductID FROM Product WHERE PartNumber = @partnumber ELSE INSERT INTO  Product (PartNumber,Description,EAN13,EAN13B,PartAmount,FullPalletQTY,Volume) VALUES (" + $"@partnumber,@description,@ean13,@ean13b,{queue.Qty},{str4},@volume) SELECT SCOPE_IDENTITY()";
      List<IBSExternalParameter> externalParameterList1 = new List<IBSExternalParameter>();
      IBSExternalParameter externalParameter1 = new IBSExternalParameter();
      externalParameter1.Name = "@partnumber";
      long? nullable = queue.OrderInfo.MaterialNumber;
      externalParameter1.Value = (object) nullable.ToString();
      externalParameterList1.Add(externalParameter1);
      externalParameterList1.Add(new IBSExternalParameter()
      {
        Name = "@description",
        Value = (object) str1
      });
      externalParameterList1.Add(new IBSExternalParameter()
      {
        Name = "@ean13",
        Value = (object) str2
      });
      externalParameterList1.Add(new IBSExternalParameter()
      {
        Name = "@ean13b",
        Value = (object) (str3 ?? string.Empty)
      });
      externalParameterList1.Add(new IBSExternalParameter()
      {
        Name = "@volume",
        Value = (object) $"{str5} {str6}"
      });
      List<IBSExternalParameter> externalParameterList2 = externalParameterList1;
      DataTable table1 = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetExternalDataWithParameters(DataProvider.MSSQL, connectionString, sqlString1, externalParameterList2.ToArray(), false).Tables[0];
      if (table1.Rows.Count == 0)
        throw new Exception("Can't find any product");
      string str7 = table1.Rows[0][0].ToString();
      string sqlString2 = "IF EXISTS (SELECT ManufacturingOrderID FROM ManOrder WHERE FillingOrderNo = @FillingOrderNo) SELECT ManufacturingOrderID FROM ManOrder WHERE FillingOrderNo = @FillingOrderNo ELSE INSERT INTO ManOrder (ManufacturingON,LineNumber,ProductID,StatusID,StartDate,EndDate,SequenceNO,FillingOrderNo) VALUES (" + $"@SequenceNo,{queue.LineNumber},{str7},10,getdate(),getdate(),@SequenceNo,@FillingOrderNo) SELECT SCOPE_IDENTITY()";
      List<IBSExternalParameter> externalParameterList3 = new List<IBSExternalParameter>();
      IBSExternalParameter externalParameter2 = new IBSExternalParameter();
      externalParameter2.Name = "@SequenceNo";
      nullable = queue.OrderInfo.Sequence;
      externalParameter2.Value = (object) nullable.ToString();
      externalParameterList3.Add(externalParameter2);
      IBSExternalParameter externalParameter3 = new IBSExternalParameter();
      externalParameter3.Name = "@FillingOrderNo";
      nullable = queue.OrderInfo.FillingOrder;
      externalParameter3.Value = (object) nullable.ToString();
      externalParameterList3.Add(externalParameter3);
      List<IBSExternalParameter> externalParameterList4 = externalParameterList3;
      DataTable table2 = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetExternalDataWithParameters(DataProvider.MSSQL, connectionString, sqlString2, externalParameterList4.ToArray(), false).Tables[0];
      if (table2.Rows.Count == 0)
        throw new Exception("Can't find any Man. order");
      string str8 = table2.Rows[0][0].ToString();
      string sqlString3 = "INSERT INTO StockCarrier (Name,Barcode,PackingTypeID,StockCarrierTypeID,StockCarrierStatusID,LocationID,QTY,StockCarrierDate,LocationDate,TotalNoOfItems,TotalNoOfItemsAdj,QTYOrig,UserInt1,LastChanged) " + $" VALUES (@sscc,@sscc,2,10,10,213971,{queue.Packages},getdate(),getdate(),{queue.Qty},{queue.Qty},{queue.Packages},{str8},getdate())";
      List<IBSExternalParameter> externalParameterList5 = new List<IBSExternalParameter>()
      {
        new IBSExternalParameter()
        {
          Name = "@sscc",
          Value = (object) queue.Sscc.Substring(2, 18)
        }
      };
      new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetExternalDataWithParameters(DataProvider.MSSQL, connectionString, sqlString3, externalParameterList5.ToArray(), false);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message, ex);
    }
  }

  public void InsertSendDateToReturnToSapData(string code, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = $"UPDATE tppgsapreturncodes SET sendtosap = getdate(),created = getdate() WHERE sapcode = '{code}' ";
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQuery(sqlString);
  }

  public void InsertSendDateToReturnToSapCreateData(string code, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = $"UPDATE tppgsapreturncodes SET created = getdate() WHERE sapcode = '{code}' ";
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQuery(sqlString);
  }

  public void InsertSendDateToConstore(string code, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = $"UPDATE tppgsapreturncodes SET sendtoconstore = getdate() WHERE sapcode = '{code}'";
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQuery(sqlString);
  }

  public static int CalculateCheckDigit(ulong label)
  {
    int num = 0;
    bool flag = true;
    for (; label > 0UL; label /= 10UL)
    {
      if (flag)
        num += (int) (label % 10UL) * 3;
      else
        num += (int) (label % 10UL);
      flag = !flag;
    }
    return (10 - num % 10) % 10;
  }

  public string TestSSCC(string format, int counter)
  {
    format = format.Replace($"[{(System.Enum) OrderField.Material}]", "MATERIAL");
    format = format.Replace($"[{(System.Enum) OrderField.FillingOrder}]", "ORDER");
    format = format.Replace($"[{(System.Enum) OrderField.Sequence}]", "SEQ");
    format = format.Replace($"[{(System.Enum) OrderField.Destination}]", "DEST");
    int length = format.IndexOf("[{", StringComparison.Ordinal);
    int num = format.IndexOf("}]", StringComparison.Ordinal);
    if (length < 0 || num < 0 || length >= num)
      return "The counter value is missing";
    string format1 = format.Substring(length + 2, num - length - 2);
    string str = counter.ToString(format1);
    return format.Substring(0, length) + str;
  }

  public CreateBarcodeResult CreateNewBarCode(
    OrderInformation data,
    int line,
    bool update,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString1 = "SELECT tppgssccformats.id,tppgssccformats.format FROM tppglines INNER JOIN tppglineprinters ON tppglines.Id = tppglineprinters.lineid INNER JOIN tppgssccformats ON tppglineprinters.ssccformatid = dbo.tppgssccformats.id WHERE(tppglines.type = 1)";
    DataTable table = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString1).Tables[0];
    if (table.Rows.Count == 0)
      return new CreateBarcodeResult()
      {
        ErrorMessage = "Can't find any SSCC Format for pallet line",
        HasAnError = true
      };
    int int32 = Convert.ToInt32(table.Rows[0][0]);
    string str1 = table.Rows[0]["format"].ToString().Replace($"[{(System.Enum) OrderField.Material}]", data.MaterialNumber.ToString()).Replace($"[{(System.Enum) OrderField.FillingOrder}]", data.FillingOrder.ToString()).Replace($"[{(System.Enum) OrderField.Sequence}]", data.Sequence.ToString()).Replace($"[{(System.Enum) OrderField.Destination}]", data.Destination.ToString());
    foreach (KeyValuePair<string, string> keyValuePair in new ExcelTools().GetProductData(data.MaterialNumber.Value, "DK", links))
      str1 = str1.Replace($"[{keyValuePair.Key}]", keyValuePair.Value);
    int length = str1.IndexOf("[{", StringComparison.Ordinal);
    int num1 = str1.IndexOf("}]", StringComparison.Ordinal);
    if (length < 0 || num1 < 0 || length >= num1)
      return new CreateBarcodeResult()
      {
        ErrorMessage = "The counter value is missing",
        HasAnError = true
      };
    string format = str1.Substring(length + 2, num1 - length - 2);
    string sqlString2 = !update ? $"SELECT countervalue FROM tppgssccformats WHERE id = {int32}" : $"IF ((SELECT countervalue FROM tppgssccformats WHERE id = {int32}) >= (SELECT maxvalue FROM tppgssccformats WHERE id = {int32})) SELECT -1 " + $" ELSE BEGIN DECLARE @sn int UPDATE tppgssccformats  SET countervalue = countervalue + 1,@sn = countervalue  WHERE id = {int32} SELECT @sn END";
    int num2 = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarInt(sqlString2);
    if (num2 == -1)
      return new CreateBarcodeResult()
      {
        ErrorMessage = "Maximum value is reached",
        HasAnError = true
      };
    string str2 = num2.ToString(format);
    string str3 = str1.Substring(0, length) + str2;
    string str4 = str3 + (object) DataTools.CalculateCheckDigit(Convert.ToUInt64(str3));
    return new CreateBarcodeResult()
    {
      Sscc = str4,
      HasAnError = false,
      Counter = num2
    };
  }

  public Dictionary<string, string> GetLabelData(
    long materialNumber,
    int labelTypeId,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT tppglabelnametext.text FROM tppgprintlabeltypes INNER JOIN tppglabelnames INNER JOIN  tppglabelnametext ON tppglabelnames.id = tppglabelnametext.labelnameid ON tppgprintlabeltypes.labelnameid =  tppglabelnames.id INNER JOIN tppgproductmapping ON tppgprintlabeltypes.Id = tppgproductmapping.printlabeltypeid INNER JOIN  tppgproducts ON tppgproductmapping.productid = tppgproducts.id " + $" WHERE(tppgproducts.materialnumber = {materialNumber}) AND (tppgproductmapping.labeltypeid = {labelTypeId})";
    DataTable table = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString).Tables[0];
    Dictionary<string, string> labelData = new Dictionary<string, string>();
    int num = 1;
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      labelData.Add($"Line_{num}", row["text"] != DBNull.Value ? row["text"].ToString() : string.Empty);
      ++num;
    }
    foreach (KeyValuePair<string, string> keyValuePair in new ExcelTools().GetProductData(materialNumber, "DK", links))
      labelData.Add(keyValuePair.Key, keyValuePair.Value);
    return labelData;
  }

  private Dictionary<string, string> GetProductData(long materialNumber, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    Dictionary<string, string> productData = new Dictionary<string, string>();
    foreach (KeyValuePair<string, string> keyValuePair in new ExcelTools().GetProductData(materialNumber, "DK", links))
      productData.Add(keyValuePair.Key, keyValuePair.Value);
    return productData;
  }

  public List<DocumentInfo> GetDocumentInfo(
    long lineNo,
    long materialNumber,
    LineType labelType,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("SELECT tppglabeltypes.labeltype AS LabelType,tppglabeltypes.description AS LabelTypeName, tppgribbontypes.name AS RibbonName, tppgcolors.name AS ");
    stringBuilder.Append(" RibbonColorName, tprinters.itemname AS PrinterName, tppglabelnames.labelname AS LabelName,tppglabelnames.printable AS IsPrintable, ");
    stringBuilder.Append(" tppgcolors_1.name AS LabelNameColorName, tppgcolors_1.code AS LabelNameColorCode, tppgcolors.code");
    stringBuilder.Append(" AS RibbonColorCode,tppglabeltypes.id AS LabelTypeId,turn");
    stringBuilder.Append(" FROM tppglineprinters INNER JOIN ");
    stringBuilder.Append(" tppglines ON tppglineprinters.lineid = tppglines.Id INNER JOIN ");
    stringBuilder.Append(" tppgproducts INNER JOIN ");
    stringBuilder.Append(" tppgproductmapping ON tppgproducts.id = tppgproductmapping.productid INNER JOIN ");
    stringBuilder.Append(" tppgprintlabeltypes ON tppgproductmapping.printlabeltypeid = tppgprintlabeltypes.Id INNER JOIN ");
    stringBuilder.Append("tppgribbontypes ON tppgprintlabeltypes.ribbontypeid = tppgribbontypes.id INNER JOIN ");
    stringBuilder.Append("tppglabeltypes ON tppgproductmapping.labeltypeid = tppglabeltypes.id ON tppglineprinters.labeltypeid = ");
    stringBuilder.Append(" tppgproductmapping.labeltypeid INNER JOIN ");
    stringBuilder.Append("tppgcolors ON tppgribbontypes.color = tppgcolors.Id INNER JOIN ");
    stringBuilder.Append("tprinters ON tppglineprinters.printeraliasid = tprinters.printerid INNER JOIN ");
    stringBuilder.Append("tppglabelnames ON tppgprintlabeltypes.labelnameid = tppglabelnames.id INNER JOIN ");
    stringBuilder.Append("tppgcolors AS tppgcolors_1 ON tppglabelnames.colorid = tppgcolors_1.Id ");
    stringBuilder.Append($" WHERE(tppglines.linenumber = {lineNo}) AND(tppglines.type = {(int) labelType}) ");
    stringBuilder.Append($" AND(tppgproducts.materialnumber = {materialNumber}) ");
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(stringBuilder.ToString()).Tables[0].Rows.Cast<DataRow>().Select<DataRow, DocumentInfo>((System.Func<DataRow, DocumentInfo>) (row => new DocumentInfo()
    {
      LabelName = row["LabelName"].ToString(),
      LabelTypeId = int.Parse(row["LabelTypeId"].ToString()),
      PrinterName = row["PrinterName"].ToString(),
      LabelType = row["LabelType"].ToString(),
      LabelNameColorName = row["LabelNameColorName"].ToString(),
      LabelTypeName = row["LabelTypeName"].ToString(),
      LabelNameColorCode = row["LabelNameColorCode"].ToString(),
      RibbonName = row["RibbonName"].ToString(),
      RibbonColorCode = row["RibbonColorCode"].ToString(),
      RibbonColorName = row["RibbonColorName"].ToString(),
      IsPrintable = row["IsPrintable"] != DBNull.Value && bool.Parse(row["IsPrintable"].ToString()),
      Direction = row["turn"] != DBNull.Value ? int.Parse(row["turn"].ToString()) : 0
    })).ToList<DocumentInfo>();
  }

  public DataSet GetPrintCodes(int printerId, string tableName, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string str = tableName == "tppgprintercodes" ? "printerfamilyid" : "printerid";
    string sqlString = $"SELECT * FROM {tableName} WHERE {str} = {printerId}";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString);
  }

  public static void SavePrintCodes(DataSet data, string tableName, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT * FROM " + tableName;
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).SaveData(data, sqlString);
  }

  public static DataSet GetPrinterSettings(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT * FROM tppgprinters ORDER BY id";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString);
  }

  public static DataTable GetPcsysPrinters(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT printerid,itemname FROM tprinters ORDER BY itemname";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString).Tables[0];
  }

  public static DataTable GetPcsysPrinterFamilies(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT id,familyname FROM tprinterfamilies ORDER BY familyname";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString).Tables[0];
  }

  public static void SavePrintSettingsCodes(DataSet data, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT * FROM tppgprinters";
    new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).SaveData(data, sqlString);
  }

  private string GetPrinterCode(
    int printFamilyId,
    int printerId,
    PrinterCode codeFunction,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    try
    {
      string printerSpecificCode = this.GetPrinterSpecificCode(printerId, codeFunction, links);
      DataTable table = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData($"SELECT printercode FROM tppgprintercodes WHERE printfunction = {(int) codeFunction} AND printerfamilyid = {printFamilyId}").Tables[0];
      return printerSpecificCode.Length > 0 ? printerSpecificCode : table.Rows.Cast<DataRow>().Aggregate<DataRow, string>(string.Empty, (Func<string, DataRow, string>) ((current, row) => current + $"{row[0]}$"));
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message, ex);
    }
  }

  private LabelInfo GetLabelInfo(int printerId, PrinterCode codeFunction, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    try
    {
      DataTable table = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData($"SELECT * FROM tppgprintersettings WHERE installedprinterid = {printerId}").Tables[0];
      if (table.Rows.Count == 0)
        return new LabelInfo() { Rotation = -1, Heat = -1 };
      return new LabelInfo()
      {
        Heat = string.IsNullOrEmpty(table.Rows[0]["heat"].ToString()) ? 0 : int.Parse(table.Rows[0]["heat"].ToString()),
        Rotation = string.IsNullOrEmpty(table.Rows[0]["rotate"].ToString()) ? 0 : int.Parse(table.Rows[0]["rotate"].ToString())
      };
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message, ex);
    }
  }

  private int GetOrderNumber(int lineNumber, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = $"SELECT saporderId FROM tppglinedata WHERE lineid  = (SELECT id FROM tppglines WHERE linenumber = {lineNumber})";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarInt(sqlString);
  }

  private long GetPartNumber(int orderId, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT tppgproducts.materialnumber FROM tppgsaporders INNER JOIN " + $"tppgproducts ON tppgsaporders.productid = dbo.tppgproducts.id WHERE tppgsaporders.id = {orderId}";
    return long.Parse(new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarString(sqlString));
  }

  public void StartProduction(int lineNumber, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    int orderNumber = this.GetOrderNumber(lineNumber, links);
    if (orderNumber < 0)
      return;
    try
    {
      long partNumber = this.GetPartNumber(orderNumber, links);
      List<DocumentInfo> documentInfo = this.GetDocumentInfo((long) lineNumber, partNumber, LineType.Product, links);
      foreach (string str in this.GetPrinterOnLine(lineNumber, links))
      {
        string printer = str;
        if (documentInfo.All<DocumentInfo>((System.Func<DocumentInfo, bool>) (a => a.PrinterName != printer)))
          this.SendResetCode(printer, lineNumber, links);
      }
    }
    catch (Exception ex)
    {
      string message = ex.Message;
    }
  }

  private static long MaterialItemNumber(int lineNumber, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string sqlString = "SELECT tppgproducts.materialnumber FROM dbo.tppgsaporders INNER JOIN tppglinedata ON tppgsaporders.id = tppglinedata.saporderId INNER JOINtppgproducts ON tppgsaporders.productid = tppgproducts.id INNER JOIN tppglines ON tppglinedata.lineid = tppglines.Id " + $"WHERE(tppglines.linenumber = {lineNumber})";
    return (long) new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarInt(sqlString);
  }

  public void StopProduction(int lineNumber, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    int orderNumber = this.GetOrderNumber(lineNumber, links);
    if (orderNumber < 0)
      return;
    try
    {
      new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteNonQuery($"UPDATE tppgorderreport SET stopped = getdate() WHERE orderid = {orderNumber} AND linenumber = {lineNumber}");
    }
    catch (Exception ex)
    {
    }
  }

  public string GetPrinterSpecificCode(
    int printerId,
    PrinterCode codeFunction,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    string str = codeFunction == PrinterCode.Reset ? " AND printercode like '<EMPTYDOCUMENT>%'" : string.Empty;
    string sqlString = $"SELECT printercode FROM tppgprinterspecificcodes WHERE printerid= {printerId} {str}";
    return new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).GetData(sqlString).Tables[0].Rows.Cast<DataRow>().Aggregate<DataRow, string>(string.Empty, (Func<string, DataRow, string>) ((current, row) => current + $"{row[0]}$"));
  }

  public static PCSYS_PPG_LPS_ProxyConnector.InfoService GetUrlConnection(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    try
    {
      string hostName = System.Net.Dns.GetHostName();
      List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
      {
        new IBSExternalParameter()
        {
          Name = "@servername",
          Value = (object) hostName
        }
      };
      string str = new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) links).ExecuteScalarStringWithParameters("SELECT informationserverport FROM tservers WHERE servername = @servername", externalParameterList.ToArray());
      return new PCSYS_PPG_LPS_ProxyConnector.InfoService()
      {
        Port = str == string.Empty ? "8020" : str,
        ServerName = hostName,
        Version = "PCSYS"
      };
    }
    catch (Exception ex)
    {
    }
    return (PCSYS_PPG_LPS_ProxyConnector.InfoService) null;
  }

  public string GetPrintStatus(string printerName, List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links, PCSYS_PPG_LPS_ProxyConnector.InfoService url)
  {
    try
    {
      return DataTools.GetConnection(url).GetPrinterStatusByIp(printerName, links.Select<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink, PCSYS_PPG_Info.PrintServiceInfo.IbsDataLink>((System.Func<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink, PCSYS_PPG_Info.PrintServiceInfo.IbsDataLink>) (link => new PCSYS_PPG_Info.PrintServiceInfo.IbsDataLink()
      {
        HttpAddress = link.HttpAddress,
        Port = link.Port,
        Version = link.Version,
        IsActive = link.IsActive
      })).ToArray<PCSYS_PPG_Info.PrintServiceInfo.IbsDataLink>());
    }
    catch (Exception ex)
    {
      return ex.Message;
    }
  }

  private static XmlDictionaryReaderQuotas SetQuotas()
  {
    return new XmlDictionaryReaderQuotas()
    {
      MaxArrayLength = int.MaxValue,
      MaxBytesPerRead = int.MaxValue,
      MaxDepth = int.MaxValue,
      MaxNameTableCharCount = int.MaxValue,
      MaxStringContentLength = int.MaxValue
    };
  }

  public static GenericDataProviderClient GetConnection(PCSYS_PPG_LPS_ProxyConnector.InfoService info)
  {
    string uriString = $"http://{info.ServerName}:{info.Port}/{info.Version}/PRINTSERVICEINFO";
    try
    {
      BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
      basicHttpBinding.ReaderQuotas = DataTools.SetQuotas();
      basicHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
      return new GenericDataProviderClient((Binding) basicHttpBinding, new EndpointAddress(new Uri(uriString), new AddressHeader[0]));
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message, ex);
    }
  }
}
