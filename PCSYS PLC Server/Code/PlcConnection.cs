// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.Code.PlcConnection
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using PCSYS_Plc_AppSettings;
using Sharp7Library;
using System.Threading;

#nullable disable
namespace PCSYS_PLC_Server.Code;

internal class PlcConnection
{
  public static S7Client Client { get; set; }

  public static void InitializePlcTcpConnection()
  {
    PlcConnection.Client = new S7Client();
    PlcConnection.Client.ConnectTo(SettingsTools.MainSettings.PlcIpAddress, SettingsTools.MainSettings.Rack, SettingsTools.MainSettings.Slot);
  }

  public static void ReconnectPlcTcpConnection()
  {
    if (PlcConnection.Client == null || PlcConnection.Client.Connected)
      return;
    PlcConnection.Client.ConnectTo(SettingsTools.MainSettings.PlcIpAddress, SettingsTools.MainSettings.Rack, SettingsTools.MainSettings.Slot);
    if (PlcConnection.Client.Connected)
      return;
    Thread.Sleep(5000);
  }

  public static void DisconnectPlcTcpConnection()
  {
    if (PlcConnection.Client != null && PlcConnection.Client.Connected)
      PlcConnection.Client.Disconnect();
    PlcConnection.Client = (S7Client) null;
  }
}
