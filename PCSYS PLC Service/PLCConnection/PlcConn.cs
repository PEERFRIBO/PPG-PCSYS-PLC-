// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Service.PLCConnection.PlcConn
// Assembly: PCSYS PLC Service, Version=1.0.7.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 3ECB826A-1CFB-42EB-9747-AB0DACAA30C1
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Service.exe

using Sharp7Library;
using System;
using System.Threading;

#nullable disable
namespace PCSYS_PLC_Service.PLCConnection;

internal class PlcConn
{
  public static S7Client Client { get; set; }

  public static int InitializePlcTcpConnection()
  {
    try
    {
      PlcConn.Client = new S7Client();
      return PlcConn.Client.ConnectTo(PlcConfig.AppSettings.PlcIpAddress, PlcConfig.AppSettings.Rack, PlcConfig.AppSettings.Slot);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message, ex);
    }
  }

  public static void ReconnectPlcTcpConnection()
  {
    if (PlcConn.Client == null || PlcConn.Client.Connected)
      return;
    PlcConn.Client.ConnectTo(PlcConfig.AppSettings.PlcIpAddress, PlcConfig.AppSettings.Rack, PlcConfig.AppSettings.Slot);
    if (PlcConn.Client.Connected)
      return;
    Thread.Sleep(5000);
  }

  public static void DisconnectPlcTcpConnection()
  {
    if (PlcConn.Client != null && PlcConn.Client.Connected)
      PlcConn.Client.Disconnect();
    PlcConn.Client = (S7Client) null;
  }
}
