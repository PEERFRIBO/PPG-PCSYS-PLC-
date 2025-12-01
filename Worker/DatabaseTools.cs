// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Service.Worker.DatabaseTools
// Assembly: PCSYS PLC Service, Version=1.0.7.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 3ECB826A-1CFB-42EB-9747-AB0DACAA30C1
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Service.exe

using PCSYS_Plc_AppSettings;
using PCSYS_PLC_Service.PLCConnection;
using PCSYS_PPG_Info;
using PCSYS_PPG_LPS_ProxyConnector;
using PCSYS_PPG_LPS_ProxyConnector.DataService;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace PCSYS_PLC_Service.Worker;

public static class DatabaseTools
{
  public static DatabaseAction GetQty(long? material)
  {
    try
    {
      string sqlString = "SELECT tppgproductdata.text FROM tppgproducts INNER JOIN tppgproductdata ON tppgproducts.id = tppgproductdata.productid INNER JOIN tppgfieldnames ON tppgproductdata.columnid = tppgfieldnames.id " + $"WHERE(tppgproducts.materialnumber = {material}) AND (tppgfieldnames.isfullpalletfield = 1)";
      string str = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) PlcConfig.AppSettings.DataLinks).ExecuteScalarString(sqlString);
      if (string.IsNullOrEmpty(str))
        return new DatabaseAction()
        {
          ErrorMessage = $"Can't find Qty for Materiale no {material}"
        };
      return new DatabaseAction()
      {
        Value = (object) Convert.ToInt32(str)
      };
    }
    catch (Exception ex)
    {
      return new DatabaseAction()
      {
        ErrorMessage = ex.Message
      };
    }
  }

  public static bool LineExists(int lineNo)
  {
    return new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) PlcConfig.AppSettings.DataLinks).ExecuteScalarInt($"SELECT COUNT(*) FROM tppglines WHERE linenumber = {lineNo}") > 0;
  }

  public static void InsertUserInput(
    int sscccode,
    int palletLineId,
    string message,
    int materialQty)
  {
    List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
    {
      new IBSExternalParameter()
      {
        Name = "@messages",
        Value = (object) message
      }
    };
    string sqlString = $"UPDATE tppglinedata SET value = {materialQty},qtyonpalletmaster = {materialQty},sscccode = {sscccode},  message = @messages,Status = {3} WHERE lineid =  {palletLineId}";
    new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) PlcConfig.AppSettings.DataLinks).ExecuteNonQueryWithParameters(sqlString, externalParameterList.ToArray());
  }

  public static UserInputResponse GetUserInput(int lineNo)
  {
    string sqlString = $"SELECT status,value FROM tppglinedata WHERE lineid = (SELECT id from tppglines WHERE linenumber = {lineNo})";
    DataTable table = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) PlcConfig.AppSettings.DataLinks).GetData(sqlString).Tables[0];
    if (table.Rows.Count == 0)
      return new UserInputResponse()
      {
        Status = UserResponseStatus.Canceled
      };
    UserResponseStatus userResponseStatus = UserResponseStatus.Waiting;
    int num = int.Parse(table.Rows[0]["status"].ToString());
    if (num == 3)
      userResponseStatus = UserResponseStatus.Waiting;
    if (num == 0)
      userResponseStatus = UserResponseStatus.Canceled;
    if (num == 1)
      userResponseStatus = UserResponseStatus.Accepted;
    return new UserInputResponse()
    {
      Status = userResponseStatus,
      Value = (object) table.Rows[0]["value"].ToString()
    };
  }

  public static int GetPalletLineId()
  {
    return new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) PlcConfig.AppSettings.DataLinks).ExecuteScalarInt("SELECT id FROM tppglines WHERE type = 1");
  }

  public static void UpdatePlcStatus(PlcStatus status, string text)
  {
    string sqlString = $"IF (SELECT COUNT(*) FROM tppgplcstatus) > 0 UPDATE tppgplcstatus SET plcstatus = {(int) status},plcstatustext = @plcstatustext" + $" ELSE INSERT INTO tppgplcstatus (plcstatus,plcstatustext) VALUES ({(int) status},@plcstatustext)";
    List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
    {
      new IBSExternalParameter()
      {
        Name = "@plcstatustext",
        Value = (object) text
      }
    };
    new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) PlcConfig.AppSettings.DataLinks).ExecuteNonQueryWithParameters(sqlString, externalParameterList.ToArray());
  }

  public static void UpdateSapOrdersReturn()
  {
  }

  public static void UpdateLineStatus(int lineId, string value, LineStatus status)
  {
    string sqlString = $"UPDATE tppglinedata SET status = {(int) status},value = @value WHERE lineid = {lineId} AND status <> {(int) status}";
    List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
    {
      new IBSExternalParameter()
      {
        Name = "@value",
        Value = (object) value
      }
    };
    new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) PlcConfig.AppSettings.DataLinks).ExecuteNonQueryWithParameters(sqlString, externalParameterList.ToArray());
  }

  public static OrderInformation GetLineData(int line)
  {
    string sqlString = "SELECT tppgsaporders.destination, tppgsaporders.sequence, tppgsaporders.fillingorder, tppgproducts.materialnumber,tppglinedata.status  FROM tppgsaporders INNER JOIN tppglinedata ON tppgsaporders.id = tppglinedata.saporderId INNER JOIN tppglines ON tppglinedata.lineid = tppglines.Id INNER JOIN " + $"tppgproducts ON tppgsaporders.productid = tppgproducts.id WHERE linenumber = {line}";
    DataTable table = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) PlcConfig.AppSettings.DataLinks).GetData(sqlString).Tables[0];
    if (table.Rows.Count == 0)
      return new OrderInformation()
      {
        ErrorCode = 1021010,
        ErrorMessage = $"Can't find data for line {line}",
        HasAnError = true
      };
    DataRow row = table.Rows[0];
    if (row["status"] == DBNull.Value)
      return new OrderInformation()
      {
        ErrorCode = 1021012,
        ErrorMessage = "The Status is missing",
        HasAnError = true
      };
    long num1 = row["materialnumber"] != DBNull.Value ? long.Parse(row["materialnumber"].ToString()) : -1L;
    if (num1 == -1L)
      return new OrderInformation()
      {
        ErrorCode = 1021016,
        ErrorMessage = "Material number is missing",
        HasAnError = true
      };
    long num2 = row["fillingorder"] != DBNull.Value ? long.Parse(row["fillingorder"].ToString()) : -1L;
    if (num2 == -1L)
      return new OrderInformation()
      {
        ErrorCode = 1021018,
        ErrorMessage = "Filling order is missing",
        HasAnError = true
      };
    long num3 = row["sequence"] != DBNull.Value ? long.Parse(row["sequence"].ToString()) : -1L;
    if (num3 == -1L)
      return new OrderInformation()
      {
        ErrorCode = 1021020,
        ErrorMessage = "Sequence is missing",
        HasAnError = true
      };
    long num4 = row["destination"] != DBNull.Value ? long.Parse(row["destination"].ToString()) : -1L;
    if (num4 == -1L)
      return new OrderInformation()
      {
        ErrorCode = 1021020,
        ErrorMessage = "Destination is missing",
        HasAnError = true
      };
    return new OrderInformation()
    {
      Destination = new long?(num4),
      FillingOrder = new long?(num2),
      MaterialNumber = new long?(num1),
      Sequence = new long?(num3)
    };
  }
}
