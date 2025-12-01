// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.Code.DataTools
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using PCSYS_Plc_AppSettings;
using PCSYS_PPG_LPS_ProxyConnector;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace PCSYS_PLC_Server.Code;

internal class DataTools
{
  public static void SaveRegisters()
  {
    new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).ExecuteNonQuery("DELETE FROM tppgregisters");
    foreach (Register register in SettingsTools.Registers)
    {
      string sqlString1 = $"INSERT INTO tppgregisters (datablock,registertype,regname) VALUES ({register.Db},{(int) register.Type},{(int) register.RegName}) SELECT SCOPE_IDENTITY()";
      int num = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).ExecuteScalarInt(sqlString1);
      foreach (RegisterField field in register.Fields)
      {
        string sqlString2 = "INSERT INTO tppgregisterfields (registerid,address,type,registertype,datatype,access) VALUES (" + $"{num},{field.Address},{(int) field.Type},{(int) field.RegisterType},{(int) field.DataType},{(int) field.Access})";
        new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).ExecuteNonQuery(sqlString2);
      }
    }
  }

  public static List<Register> InitlizeRegisters()
  {
    List<Register> registerList = new List<Register>();
    foreach (DataRow row in (InternalDataCollectionBase) new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).GetData("SELECT * FROM tppgregisters").Tables[0].Rows)
      registerList.Add(new Register()
      {
        Id = int.Parse(row["id"].ToString()),
        Db = int.Parse(row["datablock"].ToString()),
        Type = (RegisterType) int.Parse(row["registertype"].ToString()),
        Fields = DataTools.GetFields(int.Parse(row["id"].ToString())),
        RegName = (DataBlockName) int.Parse(row["regname"].ToString())
      });
    return registerList;
  }

  private static List<RegisterField> GetFields(int id)
  {
    List<RegisterField> fields = new List<RegisterField>();
    DataTable table = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).GetData($"SELECT * FROM tppgregisterfields WHERE registerid = {id}").Tables[0];
    if (table.Rows.Count == 0)
      return fields;
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      fields.Add(new RegisterField()
      {
        Id = int.Parse(row[nameof (id)].ToString()),
        Access = (FieldAccess) int.Parse(row["access"].ToString()),
        Address = int.Parse(row["address"].ToString()),
        DataType = (DataType) int.Parse(row["datatype"].ToString()),
        RegisterType = (FieldRegisterType) int.Parse(row["registertype"].ToString()),
        Type = (FieldType) int.Parse(row["type"].ToString())
      });
    return fields;
  }

  public void SetControllRegisterFields(FieldRegisterType type, bool value)
  {
  }

  public static DataTable GetLogs()
  {
    return new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).GetData($"SELECT TOP(100) message AS Message,eventlogentrytype AS Type,FORMAT(logdatetime,'hh:mm:ss') AS Time FROM tppgeventlog WHERE datediff(day, logdatetime, '{DateTime.Now.Date:yyyy-MM-dd}') = 0 AND applicationname = 'PCSYS_PLC_Service' ORDER BY id DESC").Tables[0];
  }
}
