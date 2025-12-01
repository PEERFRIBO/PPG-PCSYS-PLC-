// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Service.ProjectInstaller
// Assembly: PCSYS PLC Service, Version=1.0.7.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 3ECB826A-1CFB-42EB-9747-AB0DACAA30C1
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Service.exe

using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

#nullable disable
namespace PCSYS_PLC_Service;

[RunInstaller(true)]
public class ProjectInstaller : Installer
{
  private IContainer components = (IContainer) null;
  private ServiceProcessInstaller serviceProcessInstaller1;
  private ServiceInstaller serviceInstallerServer;

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
    this.serviceInstallerServer = new ServiceInstaller();
    this.serviceProcessInstaller1.Account = ServiceAccount.LocalSystem;
    this.serviceProcessInstaller1.Password = (string) null;
    this.serviceProcessInstaller1.Username = (string) null;
    this.serviceInstallerServer.ServiceName = "PCSYS PLC Service";
    this.Installers.AddRange(new Installer[2]
    {
      (Installer) this.serviceProcessInstaller1,
      (Installer) this.serviceInstallerServer
    });
  }
}
