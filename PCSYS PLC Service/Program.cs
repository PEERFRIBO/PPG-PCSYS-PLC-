// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Service.Program
// Assembly: PCSYS PLC Service, Version=1.0.7.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 3ECB826A-1CFB-42EB-9747-AB0DACAA30C1
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Service.exe

using System.ServiceProcess;

#nullable disable
namespace PCSYS_PLC_Service;

internal static class Program
{
  private static void Main()
  {
    ServiceBase.Run(new ServiceBase[1]
    {
      (ServiceBase) new PCSYS_PLC_Service.Main()
    });
  }
}
