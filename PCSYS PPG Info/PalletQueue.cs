// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.PalletQueue
// Assembly: PCSYS PPG Info, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 1DA98507-3547-4F13-89C5-8F8721C43394
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG Info.dll

#nullable disable
namespace PCSYS_PPG_Info;

public class PalletQueue
{
  public long Counter { get; set; }

  public OrderInformation OrderInfo { get; set; }

  public int Qty { get; set; }

  public string Sscc { get; set; }

  public string Ean { get; set; }

  public int ItemPackCount { get; set; }

  public int Packages { get; set; }

  public int LineNumber { get; set; }
}
