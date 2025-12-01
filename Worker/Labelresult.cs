// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Service.Worker.Labelresult
// Assembly: PCSYS PLC Service, Version=1.0.7.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 3ECB826A-1CFB-42EB-9747-AB0DACAA30C1
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Service.exe

#nullable disable
namespace PCSYS_PLC_Service.Worker;

public class Labelresult
{
  public bool HasAnError { get; set; }

  public string ErrorMessage { get; set; }

  public string PrinterAlias { get; set; }

  public string DocumentName { get; set; }

  public int ErrorCode { get; set; }
}
