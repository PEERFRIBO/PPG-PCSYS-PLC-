// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Service.PLCConnection.PlcConfig
// Assembly: PCSYS PLC Service, Version=1.0.7.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 3ECB826A-1CFB-42EB-9747-AB0DACAA30C1
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Service.exe

using PCSYS_Event_Log;
using PCSYS_Plc_AppSettings;
using PCSYS_PPG_LPS_ProxyConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace PCSYS_PLC_Service.PLCConnection;

public static class PlcConfig
{
  public static List<Register> Registers;
  public static bool WaitForUserInput;

  public static Settings AppSettings { get; set; }

  public static void LoadConfiguration()
  {
    PlcConfig.AppSettings = new SettingsTools().GetSettings("PCSYS PLC Server");
    PlcConfig.Registers = PlcConfig.InitlizeRegisters();
  }

  public static List<Register> InitlizeRegisters()
  {
    try
    {
      List<Register> registerList = new List<Register>();
      foreach (DataRow row in (InternalDataCollectionBase) new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) PlcConfig.AppSettings.DataLinks).GetData("SELECT * FROM tppgregisters").Tables[0].Rows)
        registerList.Add(new Register()
        {
          Db = int.Parse(row["datablock"].ToString()),
          Type = (RegisterType) int.Parse(row["registertype"].ToString()),
          Fields = PlcConfig.GetFields(int.Parse(row["id"].ToString())),
          RegName = (DataBlockName) int.Parse(row["regname"].ToString())
        });
      return registerList;
    }
    catch (Exception ex)
    {
      LogTools.WriteToEventLog("SELECT * FROM tppgregisters", EventLogEntryType.Error, "PCSYS PLC Server", -1, LogTools.Operation.Plc);
    }
    return (List<Register>) null;
  }

  private static List<RegisterField> GetFields(int id)
  {
    List<RegisterField> fields = new List<RegisterField>();
    DataTable table = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) PlcConfig.AppSettings.DataLinks).GetData($"SELECT * FROM tppgregisterfields WHERE registerid = {id}").Tables[0];
    if (table.Rows.Count == 0)
      return fields;
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      fields.Add(new RegisterField()
      {
        Access = (FieldAccess) int.Parse(row["access"].ToString()),
        Address = int.Parse(row["address"].ToString()),
        DataType = (DataType) int.Parse(row["datatype"].ToString()),
        RegisterType = (FieldRegisterType) int.Parse(row["registertype"].ToString()),
        Type = (FieldType) int.Parse(row["type"].ToString())
      });
    return fields;
  }
}
