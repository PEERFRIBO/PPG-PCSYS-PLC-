// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.DocumentInfo
// Assembly: PCSYS PPG Info, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 1DA98507-3547-4F13-89C5-8F8721C43394
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG Info.dll

using System.Collections.Generic;

#nullable disable
namespace PCSYS_PPG_Info;

public class DocumentInfo
{
  public string LabelTypeName { get; set; }

  public int LabelTypeId { get; set; }

  public string PrinterName { get; set; }

  public string LabelName { get; set; }

  public string RibbonName { get; set; }

  public string LabelNameColorName { get; set; }

  public string LabelNameColorCode { get; set; }

  public string RibbonColorName { get; set; }

  public string RibbonColorCode { get; set; }

  public string ErrorMessage { get; set; }

  public string LabelType { get; set; }

  public Dictionary<string, string> Data { get; set; }

  public bool Successful { get; set; }

  public bool IsPrintable { get; set; }

  public string PrintStatusText { get; set; }

  public int Direction { get; set; }
}
