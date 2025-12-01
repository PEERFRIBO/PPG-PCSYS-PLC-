// Decompiled with JetBrains decompiler
// Type: PCSYS_Plc_AppSettings.Settings
// Assembly: PCSYS Plc AppSettings, Version=1.0.0.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: E34941EA-E4DC-4942-8469-EEE8D175299F
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS Plc AppSettings.dll

using PCSYS_PPG_LPS_ProxyConnector;
using System.Collections.Generic;

#nullable disable
namespace PCSYS_Plc_AppSettings;

public class Settings
{
  public string PlcIpAddress { get; set; }

  public int Rack { get; set; }

  public int Slot { get; set; }

  public int Timeout { get; set; }

  public List<LpsDataLink> DataLinks { get; set; }

  public bool IsOffLine { get; set; }

  public int UserResponseTimeout { get; set; }

  public string PtfPath { get; set; }

  public int DefaultPalletType { get; set; }

  public string ResetCode { get; set; }

  public string GateWayHost { get; set; }

  public string ProgramId { get; set; }

  public string GateWayService { get; set; }

  public string User { get; set; }

  public byte[] Password { get; set; }

  public int Log { get; set; }

  public int Encoding { get; set; }

  public bool Unicode { get; set; }

  public string FunctionInsideSap { get; set; }

  public int EanFieldId { get; set; }

  public string ConnectionString { get; set; }

  public string ReportPath { get; set; }
}
