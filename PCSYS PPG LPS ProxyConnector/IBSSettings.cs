// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_LPS_ProxyConnector.IBSSettings
// Assembly: PCSYS PPG LPS ProxyConnector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 0E676491-4AF1-4945-9FF3-238675E09664
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG LPS ProxyConnector.dll

using System.Collections.Generic;


namespace PCSYS_PPG_LPS_ProxyConnector
{

    public class IBSSettings
    {
        public List<LpsDataLink> IBSDataLinks { get; set; }

        public int PrintServiceInfoPort { get; set; }

        public int PrintServiceWCFPort { get; set; }

        public string InfoServiceVersion { get; set; }

        public string PrintPath { get; set; }

        public IBSSettings.EngineType Engine { get; set; }

        public IBSSettings.LogLevel Log { get; set; }

        public int CodeSoftTaskCount { get; set; }

        public long CodeSoftMaxMemory { get; set; }

        public int BarTenderTaskCount { get; set; }

        public bool RestartProcess { get; set; }

        public bool UseRoaming { get; set; }

        public int PtfPolling { get; set; }

        public int PrintJobTimeOut { get; set; }

        public int LogStart { get; set; }

        public int LogFinish { get; set; }

        public int RestartAfter { get; set; }

        public bool UseCodeSoftDefalutImagePath { get; set; }

        public bool AllowPrintingWithNoLicense { get; set; }

        public int SolutionsMode { get; set; }

        public int ColorDeep { get; set; }

        public string MailServer { get; set; }

        public string MailUserName { get; set; }

        public string MailServerPassword { get; set; }

        public string MailFrom { get; set; }

        public string SmsPort { get; set; }

        public bool AllowPrinterServices { get; set; }

        public bool AllowIpServices { get; set; }

        public bool AllowInfoServices { get; set; }

        public bool OverWriteImages { get; set; }

        public string CodeSoftCsIni { get; set; }

        public string DefaultPrinter { get; set; }

        public int MaxTasks { get; set; }

        public int BarTenderInnerTimeout { get; set; }

        public int MaxBatchTasks { get; set; }

        public int BatchLimit { get; set; }

        public int TaskNumbers { get; set; }

        public bool DynamicEngineScaling { get; set; }

        public bool WaitForTaskFinished { get; set; }

        public bool IsAutomation { get; set; }

        public int TcpIpPort { get; set; }

        public string ConsignorUrl { get; set; }

        public List<string> ServerFilter { get; set; }

        public List<InfoService> InfoServicesConnections { get; set; }

        public bool OpenOnlyOneInstance { get; set; }

        public string PrintFileDirectoty { get; set; }

        public string SMSServerPort { get; set; }

        public string SMSServerBaud { get; set; }

        public List<string> StatisticProfileFilter { get; set; }

        public List<string> StatisticServerFilter { get; set; }

        public string LastGraphicsPath { get; set; }

        public string ViewServers { get; set; }

        public List<int> TaskFilter { get; set; }

        public string ErrorMassages { get; set; }

        public enum EngineType
        {
            BarTender,
            CodeSoft,
            DirectPrint,
        }

        public enum EngineExtention
        {
            Btw,
            Lab,
        }

        public enum LogLevel
        {
            Production,
            Test,
            All,
        }
    }
}