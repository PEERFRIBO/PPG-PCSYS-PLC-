// Decompiled with JetBrains decompiler
// Type: PCSYS_Plc_AppSettings.LineData
// Assembly: PCSYS Plc AppSettings, Version=1.0.0.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: E34941EA-E4DC-4942-8469-EEE8D175299F
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS Plc AppSettings.dll

#nullable disable
namespace PCSYS_Plc_AppSettings;

public class LineData
{
  public int Material { get; set; }

  public int FillingOrder { get; set; }

  public int Sequence { get; set; }

  public int Destination { get; set; }

  public int QtyOnPallet { get; set; }

  public LineStatus Status { get; set; }

  public int ErrorCode { get; set; }

  public string ErrorMessage { get; set; }
}
