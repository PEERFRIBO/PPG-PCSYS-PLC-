// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.ValidationResult
// Assembly: PCSYS PPG Info, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 1DA98507-3547-4F13-89C5-8F8721C43394
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG Info.dll

using System.Collections.Generic;

#nullable disable
namespace PCSYS_PPG_Info;

public class ValidationResult
{
  public bool HasError { get; set; }

  public List<DocumentInfo> Info { get; set; }
}
