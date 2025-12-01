// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormMain
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PCSYS_Event_Log;
using PCSYS_Plc_AppSettings;
using PCSYS_PLC_Server.UserControls;
using PCSYS_PLC_Service.Worker;
using PCSYS_PPG_Info;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.ServiceProcess;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server;

public class FormMain : Form
{
  private ServiceControllerStatus _status;
  private ServiceControllerStatus _printerstatus;
  private UserControlSettings _ctrlsettings;
  private DataTable _logs;
  private IContainer components = (IContainer) null;
  private ToolStrip toolStrip1;
  private ToolStripLabel toolStripLabel1;
  private ToolStripButton toolStripButtonPLCServer;
  private TabControl tabControlMain;
  private TabPage tabPageLogs;
  private SplitContainer splitContainerLogs;
  private StatusStrip statusStrip1;
  private TabPage tabPageSystem;
  private ImageList imageListServiceMode;
  private ServiceController serviceControllerPLC;
  private Timer timerPlc;
  private BackgroundWorker backgroundWorkerPlcController;
  private UltraGrid ultraGridLogs;
  private Timer timerLogs;
  private BackgroundWorker backgroundWorkerLogs;
  private ToolStripDropDownButton toolStripDropDownButton1;
  private ToolStripMenuItem resetToolStripMenuItem;
  private ToolStripMenuItem registerPalletToolStripMenuItem;
  private ToolStripMenuItem preparePalletToolStripMenuItem;
  private ToolStripMenuItem releasePalletToolStripMenuItem;
  private ToolStripMenuItem falseToolStripMenuItem;
  private ToolStripMenuItem trueToolStripMenuItem;
  private ToolStripMenuItem falseToolStripMenuItem1;
  private ToolStripMenuItem trueToolStripMenuItem1;
  private ToolStripMenuItem falseToolStripMenuItem2;
  private ToolStripMenuItem trueToolStripMenuItem2;
  private ToolStripMenuItem falseToolStripMenuItem3;
  private ToolStripMenuItem trueToolStripMenuItem3;
  private ToolStripLabel toolStripLabel2;
  private ToolStripButton toolStripButtonPLCPrinters;
  private ServiceController serviceControllerPrinterService;
  private ToolStripButton toolStripButtonReprint;
  private ToolStripButton toolStripButtonTestPrint;
  private CheckBox checkBoxLog;
  private ToolStripButton toolStripButtonSscc;
  private ToolStripButton toolStripButton1;
  private TextBox textBoxItemNo;

  public FormMain() => this.InitializeComponent();

  private void FormMain_Load(object sender, EventArgs e)
  {
    SettingsTools.MainSettings = new SettingsTools().GetSettings("TECSYS PLC Server");
    SettingsTools.Registers = PCSYS_PLC_Server.Code.DataTools.InitlizeRegisters();
    if (SettingsTools.MainSettings.DataLinks != null && SettingsTools.MainSettings.DataLinks.Count > 1)
      LogTools.WriteToEventLog("test", EventLogEntryType.SuccessAudit, 0, LogTools.Operation.System, SettingsTools.MainSettings.DataLinks);
    this._ctrlsettings = new UserControlSettings();
    this.tabPageSystem.Controls.Add((Control) this._ctrlsettings);
    int num = 0;
    foreach (Register register in SettingsTools.Registers)
    {
      UserControlRegister userControlRegister1 = new UserControlRegister();
      userControlRegister1.Reg = register;
      userControlRegister1.Left = num;
      UserControlRegister userControlRegister2 = userControlRegister1;
      num += userControlRegister2.Width + 10;
      this.splitContainerLogs.Panel1.Controls.Add((Control) userControlRegister2);
    }
    this.backgroundWorkerPlcController.RunWorkerAsync();
  }

  private void timerPlc_Tick(object sender, EventArgs e)
  {
    if (this.backgroundWorkerPlcController.IsBusy)
      return;
    this.backgroundWorkerPlcController.RunWorkerAsync();
  }

  private void backgroundWorkerPlcController_DoWork(object sender, DoWorkEventArgs e)
  {
    this.serviceControllerPLC.Refresh();
    this._status = this.serviceControllerPLC.Status;
    this.serviceControllerPrinterService.Refresh();
    //this._printerstatus = this.serviceControllerPrinterService.Status;
  }

  private void backgroundWorkerPlcController_RunWorkerCompleted(
    object sender,
    RunWorkerCompletedEventArgs e)
  {
    if (this._status == ServiceControllerStatus.Running)
    {
      this.toolStripButtonPLCServer.Image = this.imageListServiceMode.Images[1];
      this.toolStripButtonPLCServer.Enabled = true;
      this.toolStripButtonPLCServer.ToolTipText = "Stop PCSYS PLC Service";
    }
    if (this._status == ServiceControllerStatus.Stopped)
    {
      this.toolStripButtonPLCServer.Image = this.imageListServiceMode.Images[0];
      this.toolStripButtonPLCServer.Enabled = true;
      this.toolStripButtonPLCServer.ToolTipText = "Start PCSYS PLC Service";
    }
    if (this._status != ServiceControllerStatus.Running && this._status != ServiceControllerStatus.Stopped)
    {
      this.toolStripButtonPLCServer.Image = this.imageListServiceMode.Images[2];
      this.toolStripButtonPLCServer.Enabled = false;
      this.toolStripButtonPLCServer.ToolTipText = "Stop PCSYS PLC Service is pending..";
    }
    if (this._printerstatus == ServiceControllerStatus.Running)
    {
      this.toolStripButtonPLCPrinters.Image = this.imageListServiceMode.Images[1];
      this.toolStripButtonPLCPrinters.Enabled = true;
      this.toolStripButtonPLCPrinters.ToolTipText = "Stop PCSYS PLC Printer Service";
    }
    else if (this._printerstatus == ServiceControllerStatus.Stopped)
    {
      this.toolStripButtonPLCPrinters.Image = this.imageListServiceMode.Images[0];
      this.toolStripButtonPLCPrinters.Enabled = true;
      this.toolStripButtonPLCPrinters.ToolTipText = "Start PCSYS PLC Printer Service";
    }
    else
    {
      this.toolStripButtonPLCPrinters.Image = this.imageListServiceMode.Images[2];
      this.toolStripButtonPLCPrinters.Enabled = false;
      this.toolStripButtonPLCPrinters.ToolTipText = "Stop PCSYS PLC Printer Service is pending..";
    }
  }

  private void toolStripButtonPLCServer_Click(object sender, EventArgs e)
  {
    this.toolStripButtonPLCServer.Enabled = false;
    try
    {
      if (this._status == ServiceControllerStatus.Running)
        this.serviceControllerPLC.Stop();
      if (this._status != ServiceControllerStatus.Stopped)
        return;
      this.serviceControllerPLC.Start();
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message);
    }
  }

  private void backgroundWorkerLogs_DoWork(object sender, DoWorkEventArgs e)
  {
    this._logs = PCSYS_PLC_Server.Code.DataTools.GetLogs();
  }

  private ValueList GetLogTypes()
  {
    ValueList logTypes = new ValueList();
    foreach (object dataValue in Enum.GetValues(typeof (EventLogEntryType)))
      logTypes.ValueListItems.Add(dataValue);
    return logTypes;
  }

  private void backgroundWorkerLogs_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    try
    {
      if (this._logs == null)
        return;
      this.ultraGridLogs.DataSource = (object) this._logs;
      this.ultraGridLogs.DisplayLayout.Bands[0].Columns["Type"].ValueList = (IValueList) this.GetLogTypes();
      this.ultraGridLogs.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.VisibleRows);
    }
    catch (Exception ex)
    {
    }
  }

  private void timerLogs_Tick(object sender, EventArgs e)
  {
    if (this.backgroundWorkerLogs.IsBusy)
      return;
    this.backgroundWorkerLogs.RunWorkerAsync();
  }

  private void checkBoxOnLine_CheckedChanged(object sender, EventArgs e)
  {
    this.timerLogs.Enabled = ((CheckBox) sender).Checked;
  }

  private void button2_Click(object sender, EventArgs e)
  {
    List<ExcelTools.ExcelImportResult> excelImportResultList = new ExcelTools().ImportExcel(File.ReadAllBytes("C:\\temp\\Excel.xls"), SettingsTools.MainSettings.DataLinks);
    if (excelImportResultList.Count <= 0)
      return;
    foreach (ExcelTools.ExcelImportResult excelImportResult in excelImportResultList)
    {
      int num = (int) MessageBox.Show(excelImportResult.Error);
    }
  }

  private void resetToolStripMenuItem_Click(object sender, EventArgs e)
  {
  }

  private void registerPalletToolStripMenuItem_Click(object sender, EventArgs e)
  {
  }

  private void preparePalletToolStripMenuItem_Click(object sender, EventArgs e)
  {
  }

  private void releasePalletToolStripMenuItem_Click(object sender, EventArgs e)
  {
  }

  private void falseToolStripMenuItem_Click(object sender, EventArgs e)
  {
    PlcTools.WriteIntOffLine(100, 0, 0, SettingsTools.MainSettings.DataLinks);
  }

  private void trueToolStripMenuItem_Click(object sender, EventArgs e)
  {
    PlcTools.WriteIntOffLine(100, 0, 1, SettingsTools.MainSettings.DataLinks);
    PlcTools.WriteIntOffLine(100, 8, 0, SettingsTools.MainSettings.DataLinks);
    PlcTools.WriteIntOffLine(100, 16 /*0x10*/, 0, SettingsTools.MainSettings.DataLinks);
    PlcTools.WriteIntOffLine(100, 22, 0, SettingsTools.MainSettings.DataLinks);
  }

  private void falseToolStripMenuItem1_Click(object sender, EventArgs e)
  {
    PlcTools.WriteIntOffLine(100, 8, 0, SettingsTools.MainSettings.DataLinks);
  }

  private void trueToolStripMenuItem1_Click(object sender, EventArgs e)
  {
    PlcTools.WriteIntOffLine(100, 8, 1, SettingsTools.MainSettings.DataLinks);
    PlcTools.WriteIntOffLine(100, 0, 0, SettingsTools.MainSettings.DataLinks);
    PlcTools.WriteIntOffLine(100, 16 /*0x10*/, 0, SettingsTools.MainSettings.DataLinks);
    PlcTools.WriteIntOffLine(100, 22, 0, SettingsTools.MainSettings.DataLinks);
  }

  private void falseToolStripMenuItem2_Click(object sender, EventArgs e)
  {
    PlcTools.WriteIntOffLine(100, 16 /*0x10*/, 0, SettingsTools.MainSettings.DataLinks);
  }

  private void trueToolStripMenuItem2_Click(object sender, EventArgs e)
  {
    PlcTools.WriteIntOffLine(100, 16 /*0x10*/, 1, SettingsTools.MainSettings.DataLinks);
    PlcTools.WriteIntOffLine(100, 0, 0, SettingsTools.MainSettings.DataLinks);
    PlcTools.WriteIntOffLine(100, 8, 0, SettingsTools.MainSettings.DataLinks);
    PlcTools.WriteIntOffLine(100, 22, 0, SettingsTools.MainSettings.DataLinks);
  }

  private void falseToolStripMenuItem3_Click(object sender, EventArgs e)
  {
    PlcTools.WriteIntOffLine(100, 22, 0, SettingsTools.MainSettings.DataLinks);
  }

  private void trueToolStripMenuItem3_Click(object sender, EventArgs e)
  {
    PlcTools.WriteIntOffLine(100, 22, 1, SettingsTools.MainSettings.DataLinks);
    PlcTools.WriteIntOffLine(100, 0, 0, SettingsTools.MainSettings.DataLinks);
    PlcTools.WriteIntOffLine(100, 8, 0, SettingsTools.MainSettings.DataLinks);
    PlcTools.WriteIntOffLine(100, 16 /*0x10*/, 0, SettingsTools.MainSettings.DataLinks);
  }

  private void button4_Click(object sender, EventArgs e)
  {
  }

  private void toolStripButtonPLCPrinters_Click(object sender, EventArgs e)
  {
    this.toolStripButtonPLCPrinters.Enabled = false;
    try
    {
      if (this._printerstatus == ServiceControllerStatus.Running)
        this.serviceControllerPrinterService.Stop();
      if (this._printerstatus != ServiceControllerStatus.Stopped)
        return;
      this.serviceControllerPrinterService.Start();
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message);
    }
  }

  private void button1_Click_1(object sender, EventArgs e)
  {
  }

  private void toolStripButtonReprint_Click(object sender, EventArgs e)
  {
    using (FormDirectPrint formDirectPrint = new FormDirectPrint())
    {
      int num = (int) formDirectPrint.ShowDialog();
    }
  }

  private void toolStripButtonTestPrint_Click(object sender, EventArgs e)
  {
    string empty = string.Empty;
  }

  private void checkBoxLog_CheckedChanged(object sender, EventArgs e)
  {
    this.timerLogs.Enabled = this.checkBoxLog.Checked;
  }

  private void toolStripButtonSscc_Click(object sender, EventArgs e)
  {
    using (FormInsertToConstore insertToConstore = new FormInsertToConstore())
    {
      int num = (int) insertToConstore.ShowDialog();
    }
  }

  private void toolStripButton1_Click(object sender, EventArgs e)
  {
    using (FormSendFiledToSap formSendFiledToSap = new FormSendFiledToSap())
    {
      int num = (int) formSendFiledToSap.ShowDialog();
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormMain));
    Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
    this.toolStrip1 = new ToolStrip();
    this.toolStripLabel1 = new ToolStripLabel();
    this.toolStripButtonPLCServer = new ToolStripButton();
    this.toolStripDropDownButton1 = new ToolStripDropDownButton();
    this.resetToolStripMenuItem = new ToolStripMenuItem();
    this.falseToolStripMenuItem = new ToolStripMenuItem();
    this.trueToolStripMenuItem = new ToolStripMenuItem();
    this.registerPalletToolStripMenuItem = new ToolStripMenuItem();
    this.falseToolStripMenuItem1 = new ToolStripMenuItem();
    this.trueToolStripMenuItem1 = new ToolStripMenuItem();
    this.preparePalletToolStripMenuItem = new ToolStripMenuItem();
    this.falseToolStripMenuItem2 = new ToolStripMenuItem();
    this.trueToolStripMenuItem2 = new ToolStripMenuItem();
    this.releasePalletToolStripMenuItem = new ToolStripMenuItem();
    this.falseToolStripMenuItem3 = new ToolStripMenuItem();
    this.trueToolStripMenuItem3 = new ToolStripMenuItem();
    this.toolStripLabel2 = new ToolStripLabel();
    this.toolStripButtonPLCPrinters = new ToolStripButton();
    this.toolStripButtonReprint = new ToolStripButton();
    this.toolStripButtonTestPrint = new ToolStripButton();
    this.toolStripButtonSscc = new ToolStripButton();
    this.toolStripButton1 = new ToolStripButton();
    this.tabControlMain = new TabControl();
    this.tabPageLogs = new TabPage();
    this.splitContainerLogs = new SplitContainer();
    this.ultraGridLogs = new UltraGrid();
    this.statusStrip1 = new StatusStrip();
    this.tabPageSystem = new TabPage();
    this.imageListServiceMode = new ImageList(this.components);
    this.serviceControllerPLC = new ServiceController();
    this.timerPlc = new Timer(this.components);
    this.backgroundWorkerPlcController = new BackgroundWorker();
    this.timerLogs = new Timer(this.components);
    this.backgroundWorkerLogs = new BackgroundWorker();
    this.serviceControllerPrinterService = new ServiceController();
    this.checkBoxLog = new CheckBox();
    this.textBoxItemNo = new TextBox();
    this.toolStrip1.SuspendLayout();
    this.tabControlMain.SuspendLayout();
    this.tabPageLogs.SuspendLayout();
    this.splitContainerLogs.BeginInit();
    this.splitContainerLogs.Panel2.SuspendLayout();
    this.splitContainerLogs.SuspendLayout();
    ((ISupportInitialize) this.ultraGridLogs).BeginInit();
    this.SuspendLayout();
    this.toolStrip1.ImageScalingSize = new Size(20, 20);
    this.toolStrip1.Items.AddRange(new ToolStripItem[9]
    {
      (ToolStripItem) this.toolStripLabel1,
      (ToolStripItem) this.toolStripButtonPLCServer,
      (ToolStripItem) this.toolStripDropDownButton1,
      (ToolStripItem) this.toolStripLabel2,
      (ToolStripItem) this.toolStripButtonPLCPrinters,
      (ToolStripItem) this.toolStripButtonReprint,
      (ToolStripItem) this.toolStripButtonTestPrint,
      (ToolStripItem) this.toolStripButtonSscc,
      (ToolStripItem) this.toolStripButton1
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(1540, 27);
    this.toolStrip1.TabIndex = 0;
    this.toolStrip1.Text = "toolStrip1";
    this.toolStripLabel1.Name = "toolStripLabel1";
    this.toolStripLabel1.Size = new Size(122, 24);
    this.toolStripLabel1.Text = "PCSYS PLC Server";
    this.toolStripButtonPLCServer.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonPLCServer.Image = (Image) PCSYS_PLC_Server.Properties.Resources.bullet_triangle_green;
    this.toolStripButtonPLCServer.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonPLCServer.Name = "toolStripButtonPLCServer";
    this.toolStripButtonPLCServer.Size = new Size(24, 24);
    this.toolStripButtonPLCServer.Text = "toolStripButton1";
    this.toolStripButtonPLCServer.ToolTipText = "Start PLC Service";
    this.toolStripButtonPLCServer.Click += new EventHandler(this.toolStripButtonPLCServer_Click);
    this.toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this.toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.resetToolStripMenuItem,
      (ToolStripItem) this.registerPalletToolStripMenuItem,
      (ToolStripItem) this.preparePalletToolStripMenuItem,
      (ToolStripItem) this.releasePalletToolStripMenuItem
    });
    this.toolStripDropDownButton1.Image = (Image) componentResourceManager.GetObject("toolStripDropDownButton1.Image");
    this.toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
    this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
    this.toolStripDropDownButton1.Size = new Size(115, 24);
    this.toolStripDropDownButton1.Text = "Set PLC Status";
    this.resetToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.falseToolStripMenuItem,
      (ToolStripItem) this.trueToolStripMenuItem
    });
    this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
    this.resetToolStripMenuItem.Size = new Size(178, 26);
    this.resetToolStripMenuItem.Text = "Reset";
    this.resetToolStripMenuItem.Click += new EventHandler(this.resetToolStripMenuItem_Click);
    this.falseToolStripMenuItem.Name = "falseToolStripMenuItem";
    this.falseToolStripMenuItem.Size = new Size(116, 26);
    this.falseToolStripMenuItem.Text = "False";
    this.falseToolStripMenuItem.Click += new EventHandler(this.falseToolStripMenuItem_Click);
    this.trueToolStripMenuItem.Name = "trueToolStripMenuItem";
    this.trueToolStripMenuItem.Size = new Size(116, 26);
    this.trueToolStripMenuItem.Text = "True";
    this.trueToolStripMenuItem.Click += new EventHandler(this.trueToolStripMenuItem_Click);
    this.registerPalletToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.falseToolStripMenuItem1,
      (ToolStripItem) this.trueToolStripMenuItem1
    });
    this.registerPalletToolStripMenuItem.Name = "registerPalletToolStripMenuItem";
    this.registerPalletToolStripMenuItem.Size = new Size(178, 26);
    this.registerPalletToolStripMenuItem.Text = "Register Pallet";
    this.registerPalletToolStripMenuItem.Click += new EventHandler(this.registerPalletToolStripMenuItem_Click);
    this.falseToolStripMenuItem1.Name = "falseToolStripMenuItem1";
    this.falseToolStripMenuItem1.Size = new Size(116, 26);
    this.falseToolStripMenuItem1.Text = "False";
    this.falseToolStripMenuItem1.Click += new EventHandler(this.falseToolStripMenuItem1_Click);
    this.trueToolStripMenuItem1.Name = "trueToolStripMenuItem1";
    this.trueToolStripMenuItem1.Size = new Size(116, 26);
    this.trueToolStripMenuItem1.Text = "True";
    this.trueToolStripMenuItem1.Click += new EventHandler(this.trueToolStripMenuItem1_Click);
    this.preparePalletToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.falseToolStripMenuItem2,
      (ToolStripItem) this.trueToolStripMenuItem2
    });
    this.preparePalletToolStripMenuItem.Name = "preparePalletToolStripMenuItem";
    this.preparePalletToolStripMenuItem.Size = new Size(178, 26);
    this.preparePalletToolStripMenuItem.Text = "Prepare Pallet";
    this.preparePalletToolStripMenuItem.Click += new EventHandler(this.preparePalletToolStripMenuItem_Click);
    this.falseToolStripMenuItem2.Name = "falseToolStripMenuItem2";
    this.falseToolStripMenuItem2.Size = new Size(116, 26);
    this.falseToolStripMenuItem2.Text = "False";
    this.falseToolStripMenuItem2.Click += new EventHandler(this.falseToolStripMenuItem2_Click);
    this.trueToolStripMenuItem2.Name = "trueToolStripMenuItem2";
    this.trueToolStripMenuItem2.Size = new Size(116, 26);
    this.trueToolStripMenuItem2.Text = "True";
    this.trueToolStripMenuItem2.Click += new EventHandler(this.trueToolStripMenuItem2_Click);
    this.releasePalletToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.falseToolStripMenuItem3,
      (ToolStripItem) this.trueToolStripMenuItem3
    });
    this.releasePalletToolStripMenuItem.Name = "releasePalletToolStripMenuItem";
    this.releasePalletToolStripMenuItem.Size = new Size(178, 26);
    this.releasePalletToolStripMenuItem.Text = "Release Pallet";
    this.releasePalletToolStripMenuItem.Click += new EventHandler(this.releasePalletToolStripMenuItem_Click);
    this.falseToolStripMenuItem3.Name = "falseToolStripMenuItem3";
    this.falseToolStripMenuItem3.Size = new Size(116, 26);
    this.falseToolStripMenuItem3.Text = "False";
    this.falseToolStripMenuItem3.Click += new EventHandler(this.falseToolStripMenuItem3_Click);
    this.trueToolStripMenuItem3.Name = "trueToolStripMenuItem3";
    this.trueToolStripMenuItem3.Size = new Size(116, 26);
    this.trueToolStripMenuItem3.Text = "True";
    this.trueToolStripMenuItem3.Click += new EventHandler(this.trueToolStripMenuItem3_Click);
    this.toolStripLabel2.Name = "toolStripLabel2";
    this.toolStripLabel2.Size = new Size(130, 24);
    this.toolStripLabel2.Text = "PCSYS PLC Printers";
    this.toolStripButtonPLCPrinters.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonPLCPrinters.Image = (Image) PCSYS_PLC_Server.Properties.Resources.bullet_triangle_green;
    this.toolStripButtonPLCPrinters.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonPLCPrinters.Name = "toolStripButtonPLCPrinters";
    this.toolStripButtonPLCPrinters.Size = new Size(24, 24);
    this.toolStripButtonPLCPrinters.Text = "toolStripButton1";
    this.toolStripButtonPLCPrinters.Click += new EventHandler(this.toolStripButtonPLCPrinters_Click);
    this.toolStripButtonReprint.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonReprint.Image = (Image) componentResourceManager.GetObject("toolStripButtonReprint.Image");
    this.toolStripButtonReprint.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonReprint.Name = "toolStripButtonReprint";
    this.toolStripButtonReprint.Size = new Size(24, 24);
    this.toolStripButtonReprint.Text = "Reprint";
    this.toolStripButtonReprint.ToolTipText = "Reprint SSCC";
    this.toolStripButtonReprint.Click += new EventHandler(this.toolStripButtonReprint_Click);
    this.toolStripButtonTestPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonTestPrint.Image = (Image) componentResourceManager.GetObject("toolStripButtonTestPrint.Image");
    this.toolStripButtonTestPrint.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonTestPrint.Name = "toolStripButtonTestPrint";
    this.toolStripButtonTestPrint.Size = new Size(24, 24);
    this.toolStripButtonTestPrint.Text = "Reset pallet printer";
    this.toolStripButtonTestPrint.Click += new EventHandler(this.toolStripButtonTestPrint_Click);
    this.toolStripButtonSscc.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonSscc.Image = (Image) componentResourceManager.GetObject("toolStripButtonSscc.Image");
    this.toolStripButtonSscc.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonSscc.Name = "toolStripButtonSscc";
    this.toolStripButtonSscc.Size = new Size(24, 24);
    this.toolStripButtonSscc.Text = "Insert SSCC to Constore";
    this.toolStripButtonSscc.Click += new EventHandler(this.toolStripButtonSscc_Click);
    this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButton1.Image = (Image) componentResourceManager.GetObject("toolStripButton1.Image");
    this.toolStripButton1.ImageTransparentColor = Color.Magenta;
    this.toolStripButton1.Name = "toolStripButton1";
    this.toolStripButton1.Size = new Size(24, 24);
    this.toolStripButton1.Text = "User Input";
    this.toolStripButton1.Click += new EventHandler(this.toolStripButton1_Click);
    this.tabControlMain.Controls.Add((Control) this.tabPageLogs);
    this.tabControlMain.Controls.Add((Control) this.tabPageSystem);
    this.tabControlMain.Dock = DockStyle.Fill;
    this.tabControlMain.Location = new Point(0, 27);
    this.tabControlMain.Margin = new Padding(4, 4, 4, 4);
    this.tabControlMain.Name = "tabControlMain";
    this.tabControlMain.SelectedIndex = 0;
    this.tabControlMain.Size = new Size(1540, 660);
    this.tabControlMain.TabIndex = 1;
    this.tabPageLogs.Controls.Add((Control) this.splitContainerLogs);
    this.tabPageLogs.Controls.Add((Control) this.statusStrip1);
    this.tabPageLogs.Location = new Point(4, 25);
    this.tabPageLogs.Margin = new Padding(4, 4, 4, 4);
    this.tabPageLogs.Name = "tabPageLogs";
    this.tabPageLogs.Padding = new Padding(4, 4, 4, 4);
    this.tabPageLogs.Size = new Size(1532, 631);
    this.tabPageLogs.TabIndex = 0;
    this.tabPageLogs.Text = "Logs";
    this.tabPageLogs.UseVisualStyleBackColor = true;
    this.splitContainerLogs.Dock = DockStyle.Fill;
    this.splitContainerLogs.Location = new Point(4, 4);
    this.splitContainerLogs.Margin = new Padding(4, 4, 4, 4);
    this.splitContainerLogs.Name = "splitContainerLogs";
    this.splitContainerLogs.Orientation = Orientation.Horizontal;
    this.splitContainerLogs.Panel2.Controls.Add((Control) this.ultraGridLogs);
    this.splitContainerLogs.Size = new Size(1524, 601);
    this.splitContainerLogs.SplitterDistance = 349;
    this.splitContainerLogs.SplitterWidth = 5;
    this.splitContainerLogs.TabIndex = 1;
    appearance1.BackColor = SystemColors.Window;
    appearance1.BorderColor = SystemColors.InactiveCaption;
    this.ultraGridLogs.DisplayLayout.Appearance = (AppearanceBase) appearance1;
    this.ultraGridLogs.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
    this.ultraGridLogs.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridLogs.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
    appearance2.BackColor = SystemColors.ActiveBorder;
    appearance2.BackColor2 = SystemColors.ControlDark;
    appearance2.BackGradientStyle = GradientStyle.Vertical;
    appearance2.BorderColor = SystemColors.Window;
    this.ultraGridLogs.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance2;
    appearance3.ForeColor = SystemColors.GrayText;
    this.ultraGridLogs.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance3;
    this.ultraGridLogs.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridLogs.DisplayLayout.GroupByBox.Hidden = true;
    appearance4.BackColor = SystemColors.ControlLightLight;
    appearance4.BackColor2 = SystemColors.Control;
    appearance4.BackGradientStyle = GradientStyle.Horizontal;
    appearance4.ForeColor = SystemColors.GrayText;
    this.ultraGridLogs.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance4;
    this.ultraGridLogs.DisplayLayout.MaxColScrollRegions = 1;
    this.ultraGridLogs.DisplayLayout.MaxRowScrollRegions = 1;
    appearance5.BackColor = SystemColors.Window;
    appearance5.ForeColor = SystemColors.ControlText;
    this.ultraGridLogs.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance5;
    appearance6.BackColor = SystemColors.Highlight;
    appearance6.ForeColor = SystemColors.HighlightText;
    this.ultraGridLogs.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance6;
    this.ultraGridLogs.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
    this.ultraGridLogs.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
    this.ultraGridLogs.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
    this.ultraGridLogs.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
    this.ultraGridLogs.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
    appearance7.BackColor = SystemColors.Window;
    this.ultraGridLogs.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance7;
    appearance8.BorderColor = Color.Silver;
    appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
    this.ultraGridLogs.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance8;
    this.ultraGridLogs.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
    this.ultraGridLogs.DisplayLayout.Override.CellPadding = 0;
    appearance9.BackColor = SystemColors.Control;
    appearance9.BackColor2 = SystemColors.ControlDark;
    appearance9.BackGradientAlignment = GradientAlignment.Element;
    appearance9.BackGradientStyle = GradientStyle.Horizontal;
    appearance9.BorderColor = SystemColors.Window;
    this.ultraGridLogs.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance9;
    appearance10.TextHAlignAsString = "Left";
    this.ultraGridLogs.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance10;
    this.ultraGridLogs.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
    this.ultraGridLogs.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
    appearance11.BackColor = SystemColors.Window;
    appearance11.BorderColor = Color.Silver;
    this.ultraGridLogs.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance11;
    this.ultraGridLogs.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
    appearance12.BackColor = SystemColors.ControlLight;
    this.ultraGridLogs.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance12;
    this.ultraGridLogs.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
    this.ultraGridLogs.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
    this.ultraGridLogs.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
    this.ultraGridLogs.Dock = DockStyle.Fill;
    this.ultraGridLogs.Location = new Point(0, 0);
    this.ultraGridLogs.Margin = new Padding(4, 4, 4, 4);
    this.ultraGridLogs.Name = "ultraGridLogs";
    this.ultraGridLogs.Size = new Size(1524, 247);
    this.ultraGridLogs.TabIndex = 4;
    this.ultraGridLogs.Text = "ultraGrid2";
    this.statusStrip1.ImageScalingSize = new Size(20, 20);
    this.statusStrip1.Location = new Point(4, 605);
    this.statusStrip1.Name = "statusStrip1";
    this.statusStrip1.Padding = new Padding(1, 0, 19, 0);
    this.statusStrip1.Size = new Size(1524, 22);
    this.statusStrip1.TabIndex = 0;
    this.statusStrip1.Text = "statusStrip1";
    this.tabPageSystem.Location = new Point(4, 25);
    this.tabPageSystem.Margin = new Padding(4, 4, 4, 4);
    this.tabPageSystem.Name = "tabPageSystem";
    this.tabPageSystem.Padding = new Padding(4, 4, 4, 4);
    this.tabPageSystem.Size = new Size(1532, 625);
    this.tabPageSystem.TabIndex = 1;
    this.tabPageSystem.Text = "Settings";
    this.tabPageSystem.UseVisualStyleBackColor = true;
    this.imageListServiceMode.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageListServiceMode.ImageStream");
    this.imageListServiceMode.TransparentColor = Color.Transparent;
    this.imageListServiceMode.Images.SetKeyName(0, "bullet_triangle_green.png");
    this.imageListServiceMode.Images.SetKeyName(1, "bullet_square_red.png");
    this.imageListServiceMode.Images.SetKeyName(2, "bullet_triangle_glass_yellow.png");
    this.serviceControllerPLC.ServiceName = "PCSYS PLC Service";
    this.timerPlc.Enabled = true;
    this.timerPlc.Interval = 2000;
    this.timerPlc.Tick += new EventHandler(this.timerPlc_Tick);
    this.backgroundWorkerPlcController.DoWork += new DoWorkEventHandler(this.backgroundWorkerPlcController_DoWork);
    this.backgroundWorkerPlcController.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorkerPlcController_RunWorkerCompleted);
    this.timerLogs.Enabled = true;
    this.timerLogs.Interval = 5000;
    this.timerLogs.Tick += new EventHandler(this.timerLogs_Tick);
    this.backgroundWorkerLogs.DoWork += new DoWorkEventHandler(this.backgroundWorkerLogs_DoWork);
    this.backgroundWorkerLogs.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorkerLogs_RunWorkerCompleted);
    this.serviceControllerPrinterService.ServiceName = "PCSYS PLC Printer Service";
    this.checkBoxLog.AutoSize = true;
    this.checkBoxLog.Checked = true;
    this.checkBoxLog.CheckState = CheckState.Checked;
    this.checkBoxLog.Location = new Point(780, 4);
    this.checkBoxLog.Margin = new Padding(3, 2, 3, 2);
    this.checkBoxLog.Name = "checkBoxLog";
    this.checkBoxLog.Size = new Size(82, 21);
    this.checkBoxLog.TabIndex = 2;
    this.checkBoxLog.Text = "Auto log";
    this.checkBoxLog.UseVisualStyleBackColor = true;
    this.checkBoxLog.CheckedChanged += new EventHandler(this.checkBoxLog_CheckedChanged);
    this.textBoxItemNo.Location = new Point(891, 4);
    this.textBoxItemNo.Name = "textBoxItemNo";
    this.textBoxItemNo.Size = new Size(100, 22);
    this.textBoxItemNo.TabIndex = 3;
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(1540, 687);
    this.Controls.Add((Control) this.textBoxItemNo);
    this.Controls.Add((Control) this.checkBoxLog);
    this.Controls.Add((Control) this.tabControlMain);
    this.Controls.Add((Control) this.toolStrip1);
    this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
    this.Margin = new Padding(4, 4, 4, 4);
    this.Name = "FormMain";
    this.Text = "PCSYS PLC Server";
    this.Load += new EventHandler(this.FormMain_Load);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.tabControlMain.ResumeLayout(false);
    this.tabPageLogs.ResumeLayout(false);
    this.tabPageLogs.PerformLayout();
    this.splitContainerLogs.Panel2.ResumeLayout(false);
    this.splitContainerLogs.EndInit();
    this.splitContainerLogs.ResumeLayout(false);
    ((ISupportInitialize) this.ultraGridLogs).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
