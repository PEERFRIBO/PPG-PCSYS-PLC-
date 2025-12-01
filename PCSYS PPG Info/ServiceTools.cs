// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.ServiceTools
// Assembly: PCSYS PPG Info, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 1DA98507-3547-4F13-89C5-8F8721C43394
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG Info.dll

using PCSYS_PPG_Info.PrintServiceInfo;
using System;
using System.Collections.Generic;

#nullable disable
namespace PCSYS_PPG_Info;

public class ServiceTools
{
  public static IBSPrintServiceInfoServiceStatus GetServiceStatus(
    string serviceName,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    GenericDataProviderClient connection = DataTools.GetConnection(DataTools.GetUrlConnection(links));
    try
    {
      return connection.GetServiceStatus(serviceName);
    }
    catch (Exception ex)
    {
    }
    return IBSPrintServiceInfoServiceStatus.NoConnection;
  }

  public static void ActivateService(
    string serviceName,
    IBSPrintServiceInfoServiceStatus status,
    List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    DataTools.GetConnection(DataTools.GetUrlConnection(links)).ActivateService(serviceName, status == IBSPrintServiceInfoServiceStatus.Running ? ProcessToolsServiceAction.Start : ProcessToolsServiceAction.Stop);
  }

  public static void RestartAllServices(List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink> links)
  {
    GenericDataProviderClient connection = DataTools.GetConnection(DataTools.GetUrlConnection(links));
    connection.ActivateService("PCSYS Data Service", ProcessToolsServiceAction.Restart);
    connection.ActivateService("PCSYS BTF Service", ProcessToolsServiceAction.Restart);
    connection.ActivateService("PCSYS SAP Service", ProcessToolsServiceAction.Restart);
    connection.ActivateService("PCSYS PLC Service", ProcessToolsServiceAction.Restart);
    connection.ActivateService("PCSYS PLC Printer Service", ProcessToolsServiceAction.Restart);
  }
}
