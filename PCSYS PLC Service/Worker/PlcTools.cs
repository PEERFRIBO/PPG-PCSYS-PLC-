// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Service.Worker.PlcTools
// Assembly: PCSYS PLC Service, Version=1.0.7.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 3ECB826A-1CFB-42EB-9747-AB0DACAA30C1
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Service.exe

using PCSYS_Event_Log;
using PCSYS_Plc_AppSettings;
using PCSYS_PLC_Service.PLCConnection;
using PCSYS_PPG_LPS_ProxyConnector;
using Sharp7Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PCSYS_PLC_Service.Worker;

public static class PlcTools
{
  public static RegisterStatus GetStatus()
  {
    Register register = PlcConfig.Registers.FirstOrDefault<Register>((Func<Register, bool>) (a => a.Type == RegisterType.Controller));
    return register == null ? RegisterStatus.None : register.Fields.Where<RegisterField>((Func<RegisterField, bool>) (a => a.Access == FieldAccess.Read)).Where<RegisterField>((Func<RegisterField, bool>) (field => PlcTools.ReadBoolPlc(register.Db, field.Address, PlcTools.GetByte(field.DataType)))).Select<RegisterField, RegisterStatus>((Func<RegisterField, RegisterStatus>) (field => PlcTools.GetStatus(field.RegisterType))).FirstOrDefault<RegisterStatus>();
  }

  private static int ReadIntPlc(int db, int address, int size)
  {
    if (PlcConfig.AppSettings.IsOffLine)
      return PlcTools.ReadIntOffLine(db, address);
    byte[] numArray = new byte[4];
    PlcConn.Client.DBRead(db, address, 4, numArray);
    PlcTools.WriteIntOffLine(db, address, (long) numArray[0]);
    return S7.GetDIntAt(numArray, 0);
  }

  public static PlcAction WriteStatusValue(DataBlockName db, FieldRegisterType type, int value)
  {
    try
    {
      Register register = PlcConfig.Registers.FirstOrDefault<Register>((Func<Register, bool>) (a => a.RegName == db));
      if (register == null)
      {
        LogTools.WriteToEventLog($"Register {db}  is missing", EventLogEntryType.Error, 0, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
        return new PlcAction()
        {
          ErrorMessage = $"Register {db}  is missing",
          Status = PlcActionStatus.Failed
        };
      }
      RegisterField registerField = register.Fields.FirstOrDefault<RegisterField>((Func<RegisterField, bool>) (a => a.RegisterType == type));
      if (registerField == null)
      {
        LogTools.WriteToEventLog($"{db}  {type} field is missing", EventLogEntryType.Error, 0, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
        return new PlcAction()
        {
          ErrorMessage = $"{db}  {type} field is missing",
          Status = PlcActionStatus.Failed
        };
      }
      PlcTools.WriteIntPlc(register.Db, registerField.Address, value);
      return new PlcAction()
      {
        Status = PlcActionStatus.Succeeded
      };
    }
    catch (Exception ex)
    {
      return new PlcAction()
      {
        Status = PlcActionStatus.Failed,
        ErrorMessage = ex.Message
      };
    }
  }

  public static PlcAction WriteValue(DataBlockName db, FieldType type, long value)
  {
    Register register = PlcConfig.Registers.FirstOrDefault<Register>((Func<Register, bool>) (a => a.RegName == db));
    if (register == null)
    {
      LogTools.WriteToEventLog($"Register {db}  is missing", EventLogEntryType.Error, 0, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
      return new PlcAction()
      {
        ErrorMessage = $"Register {db}  is missing",
        Status = PlcActionStatus.Failed
      };
    }
    RegisterField registerField = register.Fields.FirstOrDefault<RegisterField>((Func<RegisterField, bool>) (a => a.Type == type));
    if (registerField == null)
    {
      LogTools.WriteToEventLog($"{db}  {type} field is missing", EventLogEntryType.Error, 0, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
      return new PlcAction()
      {
        ErrorMessage = $"{db}  {type} field is missing",
        Status = PlcActionStatus.Failed
      };
    }
    PlcTools.WriteIntPlc(register.Db, registerField.Address, Convert.ToInt32(value));
    return new PlcAction()
    {
      Status = PlcActionStatus.Succeeded
    };
  }

  public static PlcAction ReadValue(DataBlockName db, FieldType field)
  {
    Register register = PlcConfig.Registers.First<Register>((Func<Register, bool>) (a => a.RegName == db));
    List<RegisterField> fields = register.Fields;
    if (fields == null)
    {
      DataBlockName dataBlockName = DataBlockName.DB101;
      LogTools.WriteToEventLog($"Register {dataBlockName.ToString()} is missing", EventLogEntryType.Error, 0, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
      PlcAction plcAction = new PlcAction();
      dataBlockName = DataBlockName.DB101;
      plcAction.ErrorMessage = $"Register {dataBlockName.ToString()} is missing";
      plcAction.Status = PlcActionStatus.Failed;
      return plcAction;
    }
    RegisterField registerField = fields.First<RegisterField>((Func<RegisterField, bool>) (a => a.Type == field));
    if (registerField == null)
    {
      LogTools.WriteToEventLog($"Field {field} is missing", EventLogEntryType.Error, 0, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
      return new PlcAction()
      {
        ErrorMessage = $"Field {field} is missing",
        Status = PlcActionStatus.Failed
      };
    }
    int num = PlcTools.ReadIntPlc(register.Db, registerField.Address, PlcTools.GetByte(registerField.DataType));
    PlcTools.WriteIntOffLine(register.Db, registerField.Address, (long) num);
    return new PlcAction()
    {
      Status = PlcActionStatus.Succeeded,
      Value = (object) num
    };
  }

  public static void SetReqRegister()
  {
    Register register = PlcConfig.Registers.First<Register>((Func<Register, bool>) (a => a.RegName == DataBlockName.DB100));
    if (register == null)
      return;
    if (!PlcTools.GetField(register, FieldRegisterType.ResetReq) && PlcTools.GetField(register, FieldRegisterType.ResetAck))
      PlcTools.WriteStatusValue(register.RegName, FieldRegisterType.ResetAck, 0);
    if (!PlcTools.GetField(register, FieldRegisterType.RegisterPalletReq) && PlcTools.GetField(register, FieldRegisterType.RegisterPalletAck))
      PlcTools.WriteStatusValue(register.RegName, FieldRegisterType.RegisterPalletAck, 0);
    if (!PlcTools.GetField(register, FieldRegisterType.PreparePalletReq) && PlcTools.GetField(register, FieldRegisterType.PreparePalletAck))
      PlcTools.WriteStatusValue(register.RegName, FieldRegisterType.PreparePalletAck, 0);
    if (!PlcTools.GetField(register, FieldRegisterType.ReleasePalletReq) && PlcTools.GetField(register, FieldRegisterType.ReleasePalletAck))
      PlcTools.WriteStatusValue(register.RegName, FieldRegisterType.ReleasePalletAck, 0);
  }

  private static bool GetField(Register register, FieldRegisterType type)
  {
    RegisterField registerField = register.Fields.FirstOrDefault<RegisterField>((Func<RegisterField, bool>) (a => a.RegisterType == type));
    return registerField != null && PlcTools.ReadBoolPlc(register.Db, registerField.Address, 1);
  }

  private static bool ReadBoolPlc(int db, int address, int size)
  {
    try
    {
      if (PlcConfig.AppSettings.IsOffLine)
        return PlcTools.ReadBoolOffLine(db, address);
      byte[] numArray = new byte[4];
      PlcConn.Client.DBRead(db, address, 4, numArray);
      PlcTools.WriteIntOffLine(db, address, (long) numArray[0]);
      return S7.GetDIntAt(numArray, 0) == 1;
    }
    catch (Exception ex)
    {
      LogTools.WriteToEventLog(ex.Message, EventLogEntryType.SuccessAudit, -1, LogTools.Operation.Plc, PlcConfig.AppSettings.DataLinks);
    }
    return false;
  }

  private static void WriteIntPlc(int db, int address, int value)
  {
    PlcTools.WriteIntOffLine(db, address, (long) value);
    if (PlcConfig.AppSettings.IsOffLine)
      return;
    byte[] numArray = new byte[4];
    S7.SetDIntAt(numArray, 0, value);
    PlcConn.Client.DBWrite(db, address, 4, numArray);
  }

  private static int ReadIntOffLine(int db, int address)
  {
    return new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) PlcConfig.AppSettings.DataLinks).ExecuteScalarInt("SELECT tppgregisterfields.value FROM tppgregisters INNER JOIN tppgregisterfields ON tppgregisters.id = tppgregisterfields.registerid " + $" WHERE tppgregisters.datablock = {db}  AND tppgregisterfields.address = {address}");
  }

  private static int WriteIntOffLine(int db, int address, long value)
  {
    try
    {
      return new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) PlcConfig.AppSettings.DataLinks).ExecuteScalarInt($"UPDATE field SET field.value = {value} FROM tppgregisterfields field INNER JOIN tppgregisters reg ON  field.registerid  = reg.id " + $" WHERE reg.datablock = {db} AND field.address = {address}");
    }
    catch (Exception ex)
    {
      return -1;
    }
  }

  public static int WriteIntOffLine(int db, int address, int value, List<LpsDataLink> links)
  {
    try
    {
      return new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).ExecuteScalarInt($"UPDATE field SET field.value = {value} FROM tppgregisterfields field INNER JOIN tppgregisters reg ON  field.registerid  = reg.id " + $" WHERE reg.datablock = {db} AND field.address = {address}");
    }
    catch (Exception ex)
    {
      return -1;
    }
  }

  private static bool ReadBoolOffLine(int db, int address)
  {
    return PlcTools.ReadIntOffLine(db, address) == 1;
  }

  private static RegisterStatus GetStatus(FieldRegisterType registerType)
  {
    switch (registerType)
    {
      case FieldRegisterType.ResetReq:
        return RegisterStatus.Reset;
      case FieldRegisterType.RegisterPalletReq:
        return RegisterStatus.RegisterPallet;
      case FieldRegisterType.PreparePalletReq:
        return RegisterStatus.PreparePallet;
      case FieldRegisterType.ReleasePalletReq:
        return RegisterStatus.ReleasePallet;
      default:
        return RegisterStatus.None;
    }
  }

  public static int GetByte(DataType type)
  {
    switch (type)
    {
      case DataType.Byte:
        return 1;
      case DataType.Int32:
        return 4;
      case DataType.Int64:
        return 8;
      default:
        return 1;
    }
  }
}
