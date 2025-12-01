// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Printer_Service.ProjectInstaller
// Assembly: PCSYS PLC Printer Service, Version=1.0.0.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 857E55AE-E165-426D-B46A-8A20B764DAF5
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Printer Service.exe

using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

#nullable disable
namespace PCSYS_PLC_Printer_Service;

[RunInstaller(true)]
public class ProjectInstaller : Installer
{
  private IContainer components = (IContainer) null;
  private ServiceProcessInstaller serviceProcessInstaller1;
  private ServiceInstaller serviceInstaller1;

  public ProjectInstaller() => this.InitializeComponent();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.serviceProcessInstaller1 = new ServiceProcessInstaller();
    this.serviceInstaller1 = new ServiceInstaller();
    this.serviceProcessInstaller1.Account = ServiceAccount.LocalService;
    this.serviceProcessInstaller1.Password = (string) null;
    this.serviceProcessInstaller1.Username = (string) null;
    this.serviceInstaller1.ServiceName = "PCSYS PLC Printer Service";
    this.Installers.AddRange(new Installer[2]
    {
      (Installer) this.serviceProcessInstaller1,
      (Installer) this.serviceInstaller1
    });
  }
}
