// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Printer_Service.ServiceMain
// Assembly: PCSYS PLC Printer Service, Version=1.0.0.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 857E55AE-E165-426D-B46A-8A20B764DAF5
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Printer Service.exe

using PCSYS_Event_Log;
using PCSYS_Plc_AppSettings;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

#nullable disable
namespace PCSYS_PLC_Printer_Service;

public class ServiceMain : ServiceBase
{
  public static Settings AppSettings;
  public static RegisterStatus PlcStatus = RegisterStatus.None;
  public const string Appname = "PCSYS PLC Server";
  private IContainer components = (IContainer) null;

  public Timer Statetimer { get; set; }

  public ServiceMain() => this.InitializeComponent();

  protected override void OnStart(string[] args)
  {
    ServiceMain.AppSettings = new SettingsTools().GetSettings("PCSYS PLC Server");
    if (ServiceMain.AppSettings?.DataLinks == null || ServiceMain.AppSettings.DataLinks.Count == 0)
    {
      LogTools.WriteToEventLog("The link is missing", EventLogEntryType.Error, "PCSYS PLC Server", -1, LogTools.Operation.Plc);
    }
    else
    {
      this.Statetimer = new Timer(new TimerCallback(new StartManager().Start), (object) null, 5000, 5000);
      LogTools.WriteToEventLog("PCSYS PLC Printer Service is started", EventLogEntryType.SuccessAudit, "PCSYS PLC Server", -1, LogTools.Operation.Plc);
    }
  }

  protected override void OnStop()
  {
    LogTools.WriteToEventLog("PCSYS PLC Printer Service has stopped", EventLogEntryType.SuccessAudit, "PCSYS PLC Server", -1, LogTools.Operation.Plc);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.ServiceName = "Service1";
  }
}
