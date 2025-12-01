// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.PrintServiceInfo.PrinterStatus
// Assembly: PCSYS PPG Info, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 1DA98507-3547-4F13-89C5-8F8721C43394
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG Info.dll

using System.CodeDom.Compiler;
using System.Runtime.Serialization;

#nullable disable
namespace PCSYS_PPG_Info.PrintServiceInfo;

[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "PrinterStatus", Namespace = "http://schemas.datacontract.org/2004/07/IBSPrinterManager")]
public enum PrinterStatus
{
  [EnumMember] Added,
  [EnumMember] Updated,
  [EnumMember] Deleted,
  [EnumMember] Error,
}
