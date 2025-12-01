// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.PrintServiceInfo.IGenericDataProvider
// Assembly: PCSYS PPG Info, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 1DA98507-3547-4F13-89C5-8F8721C43394
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG Info.dll

using System;
using System.CodeDom.Compiler;
using System.Data;
using System.ServiceModel;
using System.Threading.Tasks;

#nullable disable
namespace PCSYS_PPG_Info.PrintServiceInfo;

[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "PrintServiceInfo.IGenericDataProvider")]
public interface IGenericDataProvider
{
  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetServiceStatus", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetServiceStatusResponse")]
  IBSPrintServiceInfoServiceStatus GetServiceStatus(string serviceName);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetServiceStatus", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetServiceStatusResponse")]
  Task<IBSPrintServiceInfoServiceStatus> GetServiceStatusAsync(string serviceName);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/ActivateService", ReplyAction = "http://tempuri.org/IGenericDataProvider/ActivateServiceResponse")]
  string ActivateService(string serviceName, ProcessToolsServiceAction action);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/ActivateService", ReplyAction = "http://tempuri.org/IGenericDataProvider/ActivateServiceResponse")]
  Task<string> ActivateServiceAsync(string serviceName, ProcessToolsServiceAction action);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetPrinterJobs", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetPrinterJobsResponse")]
  DataSet GetPrinterJobs(string printerName);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetPrinterJobs", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetPrinterJobsResponse")]
  Task<DataSet> GetPrinterJobsAsync(string printerName);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetPrinterStatus", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetPrinterStatusResponse")]
  string GetPrinterStatus(string printerName);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetPrinterStatus", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetPrinterStatusResponse")]
  Task<string> GetPrinterStatusAsync(string printerName);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetPrinterStatusByIp", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetPrinterStatusByIpResponse")]
  string GetPrinterStatusByIp(string printerName, IbsDataLink[] links);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetPrinterStatusByIp", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetPrinterStatusByIpResponse")]
  Task<string> GetPrinterStatusByIpAsync(string printerName, IbsDataLink[] links);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/ActivatePrinter", ReplyAction = "http://tempuri.org/IGenericDataProvider/ActivatePrinterResponse")]
  bool ActivatePrinter(string printerName, string printCommand);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/ActivatePrinter", ReplyAction = "http://tempuri.org/IGenericDataProvider/ActivatePrinterResponse")]
  Task<bool> ActivatePrinterAsync(string printerName, string printCommand);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/ActivatePrintJob", ReplyAction = "http://tempuri.org/IGenericDataProvider/ActivatePrintJobResponse")]
  void ActivatePrintJob(string printJobId, string printerName, string printCommand);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/ActivatePrintJob", ReplyAction = "http://tempuri.org/IGenericDataProvider/ActivatePrintJobResponse")]
  Task ActivatePrintJobAsync(string printJobId, string printerName, string printCommand);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetData", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetDataResponse")]
  DataSet GetData(string sqlString);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetData", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetDataResponse")]
  Task<DataSet> GetDataAsync(string sqlString);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetEventLog", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetEventLogResponse")]
  EventLogInfo[] GetEventLog(string logName, DateTime date);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetEventLog", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetEventLogResponse")]
  Task<EventLogInfo[]> GetEventLogAsync(string logName, DateTime date);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/ExecuteQuery", ReplyAction = "http://tempuri.org/IGenericDataProvider/ExecuteQueryResponse")]
  int ExecuteQuery(string sqlString);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/ExecuteQuery", ReplyAction = "http://tempuri.org/IGenericDataProvider/ExecuteQueryResponse")]
  Task<int> ExecuteQueryAsync(string sqlString);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/ExecuteScalar", ReplyAction = "http://tempuri.org/IGenericDataProvider/ExecuteScalarResponse")]
  string ExecuteScalar(string sqlString);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/ExecuteScalar", ReplyAction = "http://tempuri.org/IGenericDataProvider/ExecuteScalarResponse")]
  Task<string> ExecuteScalarAsync(string sqlString);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/SeagullLicenseServiceStatus", ReplyAction = "http://tempuri.org/IGenericDataProvider/SeagullLicenseServiceStatusResponse")]
  string SeagullLicenseServiceStatus();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/SeagullLicenseServiceStatus", ReplyAction = "http://tempuri.org/IGenericDataProvider/SeagullLicenseServiceStatusResponse")]
  Task<string> SeagullLicenseServiceStatusAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/RestartIss", ReplyAction = "http://tempuri.org/IGenericDataProvider/RestartIssResponse")]
  void RestartIss();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/RestartIss", ReplyAction = "http://tempuri.org/IGenericDataProvider/RestartIssResponse")]
  Task RestartIssAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/AddPrinter", ReplyAction = "http://tempuri.org/IGenericDataProvider/AddPrinterResponse")]
  void AddPrinter(string printerName, string portName);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/AddPrinter", ReplyAction = "http://tempuri.org/IGenericDataProvider/AddPrinterResponse")]
  Task AddPrinterAsync(string printerName, string portName);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetPrinterModels", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetPrinterModelsResponse")]
  PrintFamilie[] GetPrinterModels();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetPrinterModels", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetPrinterModelsResponse")]
  Task<PrintFamilie[]> GetPrinterModelsAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/CheckInfoServiceConnection", ReplyAction = "http://tempuri.org/IGenericDataProvider/CheckInfoServiceConnectionResponse")]
  bool CheckInfoServiceConnection();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/CheckInfoServiceConnection", ReplyAction = "http://tempuri.org/IGenericDataProvider/CheckInfoServiceConnectionResponse")]
  Task<bool> CheckInfoServiceConnectionAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/AddPrinterPort", ReplyAction = "http://tempuri.org/IGenericDataProvider/AddPrinterPortResponse")]
  string AddPrinterPort(string address, string port);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/AddPrinterPort", ReplyAction = "http://tempuri.org/IGenericDataProvider/AddPrinterPortResponse")]
  Task<string> AddPrinterPortAsync(string address, string port);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/ControlService", ReplyAction = "http://tempuri.org/IGenericDataProvider/ControlServiceResponse")]
  void ControlService(string serviceName, ProcessToolsServiceAction ation, string path);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/ControlService", ReplyAction = "http://tempuri.org/IGenericDataProvider/ControlServiceResponse")]
  Task ControlServiceAsync(string serviceName, ProcessToolsServiceAction ation, string path);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetPrinterPorts", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetPrinterPortsResponse")]
  string[] GetPrinterPorts();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetPrinterPorts", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetPrinterPortsResponse")]
  Task<string[]> GetPrinterPortsAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/SetPort", ReplyAction = "http://tempuri.org/IGenericDataProvider/SetPortResponse")]
  string SetPort(string printerName, string portName, IbsDataLink[] links);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/SetPort", ReplyAction = "http://tempuri.org/IGenericDataProvider/SetPortResponse")]
  Task<string> SetPortAsync(string printerName, string portName, IbsDataLink[] links);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetPaperSources", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetPaperSourcesResponse")]
  PaperSources[] GetPaperSources(string printerName);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetPaperSources", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetPaperSourcesResponse")]
  Task<PaperSources[]> GetPaperSourcesAsync(string printerName);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/UpdateInstallPrinters", ReplyAction = "http://tempuri.org/IGenericDataProvider/UpdateInstallPrintersResponse")]
  string[] UpdateInstallPrinters();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/UpdateInstallPrinters", ReplyAction = "http://tempuri.org/IGenericDataProvider/UpdateInstallPrintersResponse")]
  Task<string[]> UpdateInstallPrintersAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetInstallPrinterInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetInstallPrinterInfoResponse")]
  UpdatePrinterInfos GetInstallPrinterInfo();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetInstallPrinterInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetInstallPrinterInfoResponse")]
  Task<UpdatePrinterInfos> GetInstallPrinterInfoAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/PerformanceInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/PerformanceInfoResponse")]
  PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo PerformanceInfo();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/PerformanceInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/PerformanceInfoResponse")]
  Task<PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo> PerformanceInfoAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetHeaderInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetHeaderInfoResponse")]
  PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo GetHeaderInfo();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetHeaderInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetHeaderInfoResponse")]
  Task<PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo> GetHeaderInfoAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetProcessInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetProcessInfoResponse")]
  PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo GetProcessInfo();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetProcessInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetProcessInfoResponse")]
  Task<PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo> GetProcessInfoAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetMemoryInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetMemoryInfoResponse")]
  PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo GetMemoryInfo();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetMemoryInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetMemoryInfoResponse")]
  Task<PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo> GetMemoryInfoAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetDiskInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetDiskInfoResponse")]
  PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo GetDiskInfo();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetDiskInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetDiskInfoResponse")]
  Task<PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo> GetDiskInfoAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetNetInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetNetInfoResponse")]
  PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo GetNetInfo();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetNetInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetNetInfoResponse")]
  Task<PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo> GetNetInfoAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/LogicalDisk", ReplyAction = "http://tempuri.org/IGenericDataProvider/LogicalDiskResponse")]
  string LogicalDisk();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/LogicalDisk", ReplyAction = "http://tempuri.org/IGenericDataProvider/LogicalDiskResponse")]
  Task<string> LogicalDiskAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/RestartLicense", ReplyAction = "http://tempuri.org/IGenericDataProvider/RestartLicenseResponse")]
  string RestartLicense(IBSSettingsEngineType engine);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/RestartLicense", ReplyAction = "http://tempuri.org/IGenericDataProvider/RestartLicenseResponse")]
  Task<string> RestartLicenseAsync(IBSSettingsEngineType engine);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetLicenseStatus", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetLicenseStatusResponse")]
  bool GetLicenseStatus(IBSSettingsEngineType engine);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetLicenseStatus", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetLicenseStatusResponse")]
  Task<bool> GetLicenseStatusAsync(IBSSettingsEngineType engine);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetServiceInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetServiceInfoResponse")]
  ServiceFileInfo GetServiceInfo(string serviceName);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetServiceInfo", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetServiceInfoResponse")]
  Task<ServiceFileInfo> GetServiceInfoAsync(string serviceName);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetAllServices", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetAllServicesResponse")]
  string[] GetAllServices();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/GetAllServices", ReplyAction = "http://tempuri.org/IGenericDataProvider/GetAllServicesResponse")]
  Task<string[]> GetAllServicesAsync();

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/UpdateSelectedInstallPrinters", ReplyAction = "http://tempuri.org/IGenericDataProvider/UpdateSelectedInstallPrintersResponse")]
  PrinterStatusMassage[] UpdateSelectedInstallPrinters(UpdatePrinterInfos info);

  [OperationContract(Action = "http://tempuri.org/IGenericDataProvider/UpdateSelectedInstallPrinters", ReplyAction = "http://tempuri.org/IGenericDataProvider/UpdateSelectedInstallPrintersResponse")]
  Task<PrinterStatusMassage[]> UpdateSelectedInstallPrintersAsync(UpdatePrinterInfos info);
}
