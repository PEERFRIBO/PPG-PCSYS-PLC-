// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.PrintServiceInfo.GenericDataProviderClient
// Assembly: PCSYS PPG Info, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 1DA98507-3547-4F13-89C5-8F8721C43394
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG Info.dll

using System;
using System.CodeDom.Compiler;
using System.Data;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

#nullable disable
namespace PCSYS_PPG_Info.PrintServiceInfo;

[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class GenericDataProviderClient : ClientBase<IGenericDataProvider>, IGenericDataProvider
{
  public GenericDataProviderClient()
  {
  }

  public GenericDataProviderClient(string endpointConfigurationName)
    : base(endpointConfigurationName)
  {
  }

  public GenericDataProviderClient(string endpointConfigurationName, string remoteAddress)
    : base(endpointConfigurationName, remoteAddress)
  {
  }

  public GenericDataProviderClient(string endpointConfigurationName, EndpointAddress remoteAddress)
    : base(endpointConfigurationName, remoteAddress)
  {
  }

  public GenericDataProviderClient(Binding binding, EndpointAddress remoteAddress)
    : base(binding, remoteAddress)
  {
  }

  public IBSPrintServiceInfoServiceStatus GetServiceStatus(string serviceName)
  {
    return this.Channel.GetServiceStatus(serviceName);
  }

  public Task<IBSPrintServiceInfoServiceStatus> GetServiceStatusAsync(string serviceName)
  {
    return this.Channel.GetServiceStatusAsync(serviceName);
  }

  public string ActivateService(string serviceName, ProcessToolsServiceAction action)
  {
    return this.Channel.ActivateService(serviceName, action);
  }

  public Task<string> ActivateServiceAsync(string serviceName, ProcessToolsServiceAction action)
  {
    return this.Channel.ActivateServiceAsync(serviceName, action);
  }

  public DataSet GetPrinterJobs(string printerName) => this.Channel.GetPrinterJobs(printerName);

  public Task<DataSet> GetPrinterJobsAsync(string printerName)
  {
    return this.Channel.GetPrinterJobsAsync(printerName);
  }

  public string GetPrinterStatus(string printerName) => this.Channel.GetPrinterStatus(printerName);

  public Task<string> GetPrinterStatusAsync(string printerName)
  {
    return this.Channel.GetPrinterStatusAsync(printerName);
  }

  public string GetPrinterStatusByIp(string printerName, IbsDataLink[] links)
  {
    return this.Channel.GetPrinterStatusByIp(printerName, links);
  }

  public Task<string> GetPrinterStatusByIpAsync(string printerName, IbsDataLink[] links)
  {
    return this.Channel.GetPrinterStatusByIpAsync(printerName, links);
  }

  public bool ActivatePrinter(string printerName, string printCommand)
  {
    return this.Channel.ActivatePrinter(printerName, printCommand);
  }

  public Task<bool> ActivatePrinterAsync(string printerName, string printCommand)
  {
    return this.Channel.ActivatePrinterAsync(printerName, printCommand);
  }

  public void ActivatePrintJob(string printJobId, string printerName, string printCommand)
  {
    this.Channel.ActivatePrintJob(printJobId, printerName, printCommand);
  }

  public Task ActivatePrintJobAsync(string printJobId, string printerName, string printCommand)
  {
    return this.Channel.ActivatePrintJobAsync(printJobId, printerName, printCommand);
  }

  public DataSet GetData(string sqlString) => this.Channel.GetData(sqlString);

  public Task<DataSet> GetDataAsync(string sqlString) => this.Channel.GetDataAsync(sqlString);

  public EventLogInfo[] GetEventLog(string logName, DateTime date)
  {
    return this.Channel.GetEventLog(logName, date);
  }

  public Task<EventLogInfo[]> GetEventLogAsync(string logName, DateTime date)
  {
    return this.Channel.GetEventLogAsync(logName, date);
  }

  public int ExecuteQuery(string sqlString) => this.Channel.ExecuteQuery(sqlString);

  public Task<int> ExecuteQueryAsync(string sqlString) => this.Channel.ExecuteQueryAsync(sqlString);

  public string ExecuteScalar(string sqlString) => this.Channel.ExecuteScalar(sqlString);

  public Task<string> ExecuteScalarAsync(string sqlString)
  {
    return this.Channel.ExecuteScalarAsync(sqlString);
  }

  public string SeagullLicenseServiceStatus() => this.Channel.SeagullLicenseServiceStatus();

  public Task<string> SeagullLicenseServiceStatusAsync()
  {
    return this.Channel.SeagullLicenseServiceStatusAsync();
  }

  public void RestartIss() => this.Channel.RestartIss();

  public Task RestartIssAsync() => this.Channel.RestartIssAsync();

  public void AddPrinter(string printerName, string portName)
  {
    this.Channel.AddPrinter(printerName, portName);
  }

  public Task AddPrinterAsync(string printerName, string portName)
  {
    return this.Channel.AddPrinterAsync(printerName, portName);
  }

  public PrintFamilie[] GetPrinterModels() => this.Channel.GetPrinterModels();

  public Task<PrintFamilie[]> GetPrinterModelsAsync() => this.Channel.GetPrinterModelsAsync();

  public bool CheckInfoServiceConnection() => this.Channel.CheckInfoServiceConnection();

  public Task<bool> CheckInfoServiceConnectionAsync()
  {
    return this.Channel.CheckInfoServiceConnectionAsync();
  }

  public string AddPrinterPort(string address, string port)
  {
    return this.Channel.AddPrinterPort(address, port);
  }

  public Task<string> AddPrinterPortAsync(string address, string port)
  {
    return this.Channel.AddPrinterPortAsync(address, port);
  }

  public void ControlService(string serviceName, ProcessToolsServiceAction ation, string path)
  {
    this.Channel.ControlService(serviceName, ation, path);
  }

  public Task ControlServiceAsync(string serviceName, ProcessToolsServiceAction ation, string path)
  {
    return this.Channel.ControlServiceAsync(serviceName, ation, path);
  }

  public string[] GetPrinterPorts() => this.Channel.GetPrinterPorts();

  public Task<string[]> GetPrinterPortsAsync() => this.Channel.GetPrinterPortsAsync();

  public string SetPort(string printerName, string portName, IbsDataLink[] links)
  {
    return this.Channel.SetPort(printerName, portName, links);
  }

  public Task<string> SetPortAsync(string printerName, string portName, IbsDataLink[] links)
  {
    return this.Channel.SetPortAsync(printerName, portName, links);
  }

  public PaperSources[] GetPaperSources(string printerName)
  {
    return this.Channel.GetPaperSources(printerName);
  }

  public Task<PaperSources[]> GetPaperSourcesAsync(string printerName)
  {
    return this.Channel.GetPaperSourcesAsync(printerName);
  }

  public string[] UpdateInstallPrinters() => this.Channel.UpdateInstallPrinters();

  public Task<string[]> UpdateInstallPrintersAsync() => this.Channel.UpdateInstallPrintersAsync();

  public UpdatePrinterInfos GetInstallPrinterInfo() => this.Channel.GetInstallPrinterInfo();

  public Task<UpdatePrinterInfos> GetInstallPrinterInfoAsync()
  {
    return this.Channel.GetInstallPrinterInfoAsync();
  }

  public PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo PerformanceInfo()
  {
    return this.Channel.PerformanceInfo();
  }

  public Task<PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo> PerformanceInfoAsync()
  {
    return this.Channel.PerformanceInfoAsync();
  }

  public PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo GetHeaderInfo()
  {
    return this.Channel.GetHeaderInfo();
  }

  public Task<PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo> GetHeaderInfoAsync()
  {
    return this.Channel.GetHeaderInfoAsync();
  }

  public PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo GetProcessInfo()
  {
    return this.Channel.GetProcessInfo();
  }

  public Task<PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo> GetProcessInfoAsync()
  {
    return this.Channel.GetProcessInfoAsync();
  }

  public PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo GetMemoryInfo()
  {
    return this.Channel.GetMemoryInfo();
  }

  public Task<PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo> GetMemoryInfoAsync()
  {
    return this.Channel.GetMemoryInfoAsync();
  }

  public PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo GetDiskInfo()
  {
    return this.Channel.GetDiskInfo();
  }

  public Task<PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo> GetDiskInfoAsync()
  {
    return this.Channel.GetDiskInfoAsync();
  }

  public PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo GetNetInfo() => this.Channel.GetNetInfo();

  public Task<PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo> GetNetInfoAsync()
  {
    return this.Channel.GetNetInfoAsync();
  }

  public string LogicalDisk() => this.Channel.LogicalDisk();

  public Task<string> LogicalDiskAsync() => this.Channel.LogicalDiskAsync();

  public string RestartLicense(IBSSettingsEngineType engine) => this.Channel.RestartLicense(engine);

  public Task<string> RestartLicenseAsync(IBSSettingsEngineType engine)
  {
    return this.Channel.RestartLicenseAsync(engine);
  }

  public bool GetLicenseStatus(IBSSettingsEngineType engine)
  {
    return this.Channel.GetLicenseStatus(engine);
  }

  public Task<bool> GetLicenseStatusAsync(IBSSettingsEngineType engine)
  {
    return this.Channel.GetLicenseStatusAsync(engine);
  }

  public ServiceFileInfo GetServiceInfo(string serviceName)
  {
    return this.Channel.GetServiceInfo(serviceName);
  }

  public Task<ServiceFileInfo> GetServiceInfoAsync(string serviceName)
  {
    return this.Channel.GetServiceInfoAsync(serviceName);
  }

  public string[] GetAllServices() => this.Channel.GetAllServices();

  public Task<string[]> GetAllServicesAsync() => this.Channel.GetAllServicesAsync();

  public PrinterStatusMassage[] UpdateSelectedInstallPrinters(UpdatePrinterInfos info)
  {
    return this.Channel.UpdateSelectedInstallPrinters(info);
  }

  public Task<PrinterStatusMassage[]> UpdateSelectedInstallPrintersAsync(UpdatePrinterInfos info)
  {
    return this.Channel.UpdateSelectedInstallPrintersAsync(info);
  }
}
