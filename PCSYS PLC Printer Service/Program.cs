// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Printer_Service.Program
// Assembly: PCSYS PLC Printer Service, Version=1.0.0.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 857E55AE-E165-426D-B46A-8A20B764DAF5
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Printer Service.exe

using System.ServiceProcess;

#nullable disable
namespace PCSYS_PLC_Printer_Service;

internal static class Program
{
  private static void Main()
  {
    ServiceBase.Run(new ServiceBase[1]
    {
      (ServiceBase) new ServiceMain()
    });
  }
}
