// Decompiled with JetBrains decompiler
// Type: PCSYS_Plc_AppSettings.Register
// Assembly: PCSYS Plc AppSettings, Version=1.0.0.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: E34941EA-E4DC-4942-8469-EEE8D175299F
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS Plc AppSettings.dll

using System.Collections.Generic;

#nullable disable
namespace PCSYS_Plc_AppSettings;

public class Register
{
  public int Id { get; set; }

  public int Db { get; set; }

  public DataBlockName RegName { get; set; }

  public RegisterType Type { get; set; }

  public List<RegisterField> Fields { get; set; }
}
