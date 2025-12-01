// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.UserControls.UserControlSettings
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PCSYS_Plc_AppSettings;
using PCSYS_PLC_Server.Code;
using PCSYS_PPG_LPS_ProxyConnector;
using PcsysLinkTools;
using PCSYSScanDataServers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server.UserControls;

public class UserControlSettings : UserControl
{
  private int id = 0;
  private string _address;
  private int _port;
  private List<string> _uris;
  private int _index;
  private Button _searchbutton;
  private PCSYS_PPG_LPS_ProxyConnector.LpsDataLink _searchlink;
  private bool _registerhaschanges;
  private bool _fieldshaschanges;
  private IContainer components = (IContainer) null;
  private GroupBox groupBox1;
  private GroupBox groupBoxPLC;
  private GroupBox groupBox4;
  private GroupBox groupBox3;
  private GroupBox groupBox2;
  private Button buttonTest_1;
  private Button buttonSearch_1;
  private RadioButton radioButtonIsActive_1;
  private NumericUpDown numericUpDownPort_1;
  private Label label6;
  private TextBox textBoxDataLink_1;
  private Label label7;
  private Button buttonTest_0;
  private Button buttonSearch_0;
  private RadioButton radioButtonIsActive_0;
  private NumericUpDown numericUpDownPort_0;
  private Label label5;
  private TextBox textBoxDataLink_0;
  private Label label4;
  private NumericUpDown numericUpDownSlot;
  private Label label3;
  private NumericUpDown numericUpDownRack;
  private Label label2;
  private TextBox textBoxIp;
  private Label label1;
  private Button buttonSave;
  private PictureBox pictureBoxWait_1;
  private PictureBox pictureBoxWait_0;
  private BackgroundWorker backgroundWorkerSearch;
  private Button buttonTestPlcConnection;
  private Button buttonSsccCounters;
  private CheckBox checkBoxOffLine;
  private Button buttonPalletType;
  private Button buttonBrowse;
  private TextBox textBoxPtfPath;
  private Label label8;
  private Button buttonPrinterCodes;
  private SplitContainer splitContainer1;
  private UltraGrid ultraGridRegisters;
  private ToolStrip toolStrip1;
  private ToolStripButton toolStripButtonAddRegister;
  private ToolStripButton toolStripButtonDeleteRegister;
  private ToolStripLabel toolStripLabel1;
  private UltraGrid ultraGridFields;
  private ToolStrip toolStrip2;
  private ToolStripButton toolStripButton1;
  private ToolStripButton toolStripButton2;
  private ToolStripLabel toolStripLabel2;
  private Button buttonSAP;
  private Button buttonConstore;
  private Button buttonDestinationCodes;
  private Button buttonReportPathBrowse;
  private TextBox textBoxReportPath;
  private Label label9;

  public UserControlSettings()
  {
    this.InitializeComponent();
    if (SettingsTools.Registers == null)
      SettingsTools.Registers = new List<Register>();
    if (SettingsTools.MainSettings.DataLinks != null)
    {
      if (SettingsTools.MainSettings.DataLinks.Count > 0)
      {
        this.textBoxDataLink_0.Text = SettingsTools.MainSettings.DataLinks[0].HttpAddress;
        this.numericUpDownPort_0.Value = (Decimal) SettingsTools.MainSettings.DataLinks[0].Port;
        this.radioButtonIsActive_0.Checked = SettingsTools.MainSettings.DataLinks[0].IsActive;
      }
      if (SettingsTools.MainSettings.DataLinks.Count > 1)
      {
        this.textBoxDataLink_1.Text = SettingsTools.MainSettings.DataLinks[1].HttpAddress;
        this.numericUpDownPort_1.Value = (Decimal) SettingsTools.MainSettings.DataLinks[1].Port;
        this.radioButtonIsActive_1.Checked = SettingsTools.MainSettings.DataLinks[1].IsActive;
      }
    }
    this.textBoxIp.Text = SettingsTools.MainSettings.PlcIpAddress;
    this.numericUpDownRack.Value = (Decimal) SettingsTools.MainSettings.Rack;
    this.numericUpDownSlot.Value = (Decimal) SettingsTools.MainSettings.Slot;
    this.checkBoxOffLine.Checked = SettingsTools.MainSettings.IsOffLine;
    this.ultraGridRegisters.DataSource = (object) SettingsTools.Registers.ToList<Register>();
    this.ultraGridRegisters.DisplayLayout.Bands[0].Override.ExpansionIndicator = ShowExpansionIndicator.Never;
    this.ultraGridRegisters.DisplayLayout.Bands[1].Hidden = true;
    ColumnsCollection columns = this.ultraGridRegisters.DisplayLayout.Bands[0].Columns;
    columns["Type"].ValueList = (IValueList) this.GetRegisterTypes();
    columns["Id"].Hidden = true;
    columns["RegName"].ValueList = (IValueList) this.GetRegisterName();
    this.textBoxPtfPath.Text = SettingsTools.MainSettings.PtfPath;
    this.textBoxReportPath.Text = SettingsTools.MainSettings.ReportPath;
  }

  private void toolStripButtonAddRegister_Click(object sender, EventArgs e)
  {
    SettingsTools.Registers.Add(new Register()
    {
      Id = this.id,
      Fields = new List<RegisterField>()
    });
    this.ultraGridRegisters.DataSource = (object) SettingsTools.Registers.ToList<Register>();
    ++this.id;
    this._registerhaschanges = true;
  }

  private void ultraGridRegisters_InitializeLayout(object sender, InitializeLayoutEventArgs e)
  {
  }

  private void ultraGridRegisters_AfterRowActivate(object sender, EventArgs e)
  {
    UltraGridRow activeRow = this.ultraGridRegisters.ActiveRow;
    if (activeRow == null)
      return;
    this.SetFields((int) activeRow.Cells["Id"].Value);
  }

  private void toolStripButton1_Click(object sender, EventArgs e)
  {
    UltraGridRow activeRow = this.ultraGridRegisters.ActiveRow;
    if (activeRow == null)
      return;
    int id = (int) activeRow.Cells["Id"].Value;
    if (SettingsTools.Registers.All<Register>((Func<Register, bool>) (a => a.Id != id)))
      return;
    int num = SettingsTools.Registers.FirstOrDefault<Register>((Func<Register, bool>) (a => a.Id == id)).Fields.LastOrDefault<RegisterField>() != null ? SettingsTools.Registers.FirstOrDefault<Register>((Func<Register, bool>) (a => a.Id == id)).Fields.LastOrDefault<RegisterField>().Id + 1 : 1;
    SettingsTools.Registers.FirstOrDefault<Register>((Func<Register, bool>) (a => a.Id == id)).Fields.Add(new RegisterField()
    {
      Id = num,
      ParentId = id
    });
    this.SetFields(id);
    this._fieldshaschanges = true;
  }

  private void SetFields(int id)
  {
    if (SettingsTools.Registers.All<Register>((Func<Register, bool>) (a => a.Id != id)))
      return;
    this.ultraGridFields.DataSource = (object) SettingsTools.Registers.FirstOrDefault<Register>((Func<Register, bool>) (a => a.Id == id)).Fields.ToList<RegisterField>();
    ColumnsCollection columns = this.ultraGridFields.DisplayLayout.Bands[0].Columns;
    columns["DataType"].ValueList = (IValueList) this.GetDataTypes();
    columns["RegisterType"].ValueList = (IValueList) this.GetFieldRegisterTypes();
    columns["Type"].ValueList = (IValueList) this.GetFieldTypes();
    columns["Id"].Hidden = true;
    columns["ParentId"].Hidden = true;
    columns["Access"].ValueList = (IValueList) this.GetAccessTypes();
    columns["Name"].Hidden = true;
    RegisterType registerType = (RegisterType) this.ultraGridRegisters.ActiveRow.Cells["Type"].Value;
    columns["RegisterType"].Hidden = registerType == RegisterType.Data;
    columns["Type"].Hidden = registerType == RegisterType.Controller;
  }

  private ValueList GetDataTypes()
  {
    ValueList dataTypes = new ValueList();
    foreach (object dataValue in System.Enum.GetValues(typeof (DataType)))
      dataTypes.ValueListItems.Add(dataValue);
    return dataTypes;
  }

  private ValueList GetRegisterName()
  {
    ValueList registerName = new ValueList();
    foreach (object dataValue in System.Enum.GetValues(typeof (DataBlockName)))
      registerName.ValueListItems.Add(dataValue);
    return registerName;
  }

  private ValueList GetFieldRegisterTypes()
  {
    ValueList fieldRegisterTypes = new ValueList();
    foreach (object dataValue in System.Enum.GetValues(typeof (FieldRegisterType)))
      fieldRegisterTypes.ValueListItems.Add(dataValue);
    return fieldRegisterTypes;
  }

  private ValueList GetFieldTypes()
  {
    ValueList fieldTypes = new ValueList();
    foreach (object dataValue in System.Enum.GetValues(typeof (FieldType)))
      fieldTypes.ValueListItems.Add(dataValue);
    return fieldTypes;
  }

  private ValueList GetAccessTypes()
  {
    ValueList accessTypes = new ValueList();
    foreach (object dataValue in System.Enum.GetValues(typeof (FieldAccess)))
      accessTypes.ValueListItems.Add(dataValue);
    return accessTypes;
  }

  private ValueList GetRegisterTypes()
  {
    ValueList registerTypes = new ValueList();
    foreach (object dataValue in System.Enum.GetValues(typeof (RegisterType)))
      registerTypes.ValueListItems.Add(dataValue);
    return registerTypes;
  }

  private void buttonSave_Click(object sender, EventArgs e)
  {
    SettingsTools.MainSettings.PlcIpAddress = this.textBoxIp.Text;
    SettingsTools.MainSettings.Rack = (int) this.numericUpDownRack.Value;
    SettingsTools.MainSettings.Slot = (int) this.numericUpDownSlot.Value;
    this.SetDataLink(0, this.textBoxDataLink_0.Text, (int) this.numericUpDownPort_0.Value, this.radioButtonIsActive_0.Checked);
    this.SetDataLink(1, this.textBoxDataLink_1.Text, (int) this.numericUpDownPort_1.Value, this.radioButtonIsActive_1.Checked);
    if (this._fieldshaschanges || this._registerhaschanges)
      DataTools.SaveRegisters();
    SettingsTools.MainSettings.IsOffLine = this.checkBoxOffLine.Checked;
    SettingsTools.MainSettings.PtfPath = this.textBoxPtfPath.Text;
    SettingsTools.MainSettings.ReportPath = this.textBoxReportPath.Text;
    SettingsTools.SaveSettings(SettingsTools.MainSettings);
  }

  private void SetDataLink(int index, string address, int port, bool isActive)
  {
    if (SettingsTools.MainSettings.DataLinks == null)
      return;
    if (SettingsTools.MainSettings.DataLinks.Count == index)
    {
      SettingsTools.MainSettings.DataLinks.Add(new PCSYS_PPG_LPS_ProxyConnector.LpsDataLink()
      {
        HttpAddress = address,
        Port = port,
        IsActive = isActive,
        Version = "PCSYS"
      });
    }
    else
    {
      SettingsTools.MainSettings.DataLinks[index].HttpAddress = address;
      SettingsTools.MainSettings.DataLinks[index].Port = port;
      SettingsTools.MainSettings.DataLinks[index].IsActive = isActive;
      SettingsTools.MainSettings.DataLinks[index].Version = "PCSYS";
    }
  }

  private void toolStripButtonDeleteRegister_Click(object sender, EventArgs e)
  {
    this.ultraGridRegisters.DeleteSelectedRows(false);
  }

  private void toolStripButton2_Click(object sender, EventArgs e)
  {
    this.ultraGridFields.DeleteSelectedRows(false);
  }

  private void ultraGridRegisters_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
  {
    if (MessageBox.Show("Delete selected Registers?", "Delete fields", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
    {
      e.Cancel = true;
    }
    else
    {
      foreach (UltraGridRow row in e.Rows)
      {
        int id = (int) row.Cells["Id"].Value;
        SettingsTools.Registers.Remove(SettingsTools.Registers.FirstOrDefault<Register>((Func<Register, bool>) (a => a.Id == id)));
      }
      this._registerhaschanges = true;
    }
  }

  private void ultraGridFields_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
  {
    if (MessageBox.Show("Delete selected fields?", "Delete fields", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
    {
      e.Cancel = true;
    }
    else
    {
      e.Cancel = true;
      foreach (UltraGridRow row in e.Rows)
      {
        int id = (int) row.Cells["Id"].Value;
        int parentid = (int) row.Cells["ParentId"].Value;
        Register register = SettingsTools.Registers.FirstOrDefault<Register>((Func<Register, bool>) (a => a.Id == parentid));
        register.Fields.Remove(register.Fields.FirstOrDefault<RegisterField>((Func<RegisterField, bool>) (a => a.Id == id)));
        this.SetFields(id);
      }
      this._fieldshaschanges = true;
    }
  }

  private void ultraGridFields_AfterRowsDeleted(object sender, EventArgs e)
  {

  }

  private void backgroundWorkerSearchSecond_DoWork(object sender, DoWorkEventArgs e)
  {
    List<PCSYSAppSettings.LpsDataLink> links = new List<PCSYSAppSettings.LpsDataLink>();
    foreach (PCSYS_PPG_LPS_ProxyConnector.LpsDataLink dataLink in SettingsTools.MainSettings.DataLinks)
      links.Add(new PCSYSAppSettings.LpsDataLink()
      {
        Error = dataLink.Error,
        HttpAddress = dataLink.HttpAddress,
        IsActive = dataLink.IsActive,
        Port = dataLink.Port,
        Version = PCSYSAppSettings.DataVersion.TECSYS
      });
    //this._uris = new ScanTools().GetAllDataSevers(Environment.UserDomainName, this._port.ToString(), links);
  }

  private void SetDataLink(string address, TextBox addressControl, NumericUpDown portControl)
  {
    addressControl.Text = LinkTools.GetServer(address);
    portControl.Value = (Decimal) int.Parse(LinkTools.GetPort(address));
  }

  private void backgroundWorkerSearch_RunWorkerCompleted(
    object sender,
    RunWorkerCompletedEventArgs e)
  {
    this.buttonSearch_0.Text = "Search";
    this.buttonSearch_1.Text = "Search";
    this.buttonSearch_0.Enabled = true;
    this.buttonSearch_1.Enabled = true;
    if (this._uris.Count == 0)
    {
      int num = (int) MessageBox.Show("Can't find any Data Links");
    }
    else
    {
      using (FormShowUrls formShowUrls = new FormShowUrls())
      {
        foreach (string uri in this._uris)
          formShowUrls.checkedListBoxUris.Items.Add((object) uri);
        if (formShowUrls.ShowDialog() != DialogResult.OK || formShowUrls.checkedListBoxUris.CheckedItems.Count <= 0 || formShowUrls.checkedListBoxUris.CheckedItems.Count != 1)
          return;
        if (this._index == 0)
          this.SetDataLink(formShowUrls.checkedListBoxUris.CheckedItems[0].ToString(), this.textBoxDataLink_0, this.numericUpDownPort_0);
        if (this._index == 1)
          this.SetDataLink(formShowUrls.checkedListBoxUris.CheckedItems[0].ToString(), this.textBoxDataLink_1, this.numericUpDownPort_1);
      }
    }
  }

  private void buttonSearch_0_Click(object sender, EventArgs e)
  {
    if (this.backgroundWorkerSearch.IsBusy)
      return;
    this._index = 0;
    this.buttonSearch_0.Enabled = false;
    this.buttonSearch_0.Text = "Wait..";
    this._port = (int) this.numericUpDownPort_0.Value;
    this.backgroundWorkerSearch.RunWorkerAsync();
  }

  private void buttonSearch_1_Click(object sender, EventArgs e)
  {
    if (this.backgroundWorkerSearch.IsBusy)
      return;
    this._index = 1;
    this.buttonSearch_1.Enabled = false;
    this.buttonSearch_1.Text = "Wait..";
    this._port = (int) this.numericUpDownPort_1.Value;
    this.backgroundWorkerSearch.RunWorkerAsync();
  }

  private async void StartTest()
  {
    await Task.Run((Action) (() =>
    {
      string text = this._searchbutton.Text;
      try
      {
        this.SetButtonTestLinkText("Searching...", false);
        int num = (int) MessageBox.Show(new IBSExchangeData().GetConnection((IEnumerable<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>) new List<PCSYS_PPG_LPS_ProxyConnector.LpsDataLink>()
        {
          this._searchlink
        }).CheckConnection(Dns.GetHostName()));
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message);
      }
      this.SetButtonTestLinkText(text, true);
    })).ConfigureAwait(false);
  }

  private void SetButtonTestLinkText(string text, bool activate)
  {
    if (this._searchbutton.InvokeRequired)
    {
      this.Invoke((Delegate) new UserControlSettings.SetTextCallback(this.SetButtonTestLinkText), (object) text, (object) activate);
    }
    else
    {
      this._searchbutton.Text = text;
      this._searchbutton.Enabled = activate;
    }
  }

  private void buttonTest_0_Click(object sender, EventArgs e)
  {
    this._searchlink = new PCSYS_PPG_LPS_ProxyConnector.LpsDataLink()
    {
      HttpAddress = this.textBoxDataLink_0.Text,
      Version = "PCSYS",
      Port = (int) this.numericUpDownPort_0.Value,
      IsActive = true
    };
    this._searchbutton = this.buttonTest_0;
    this.StartTest();
  }

  private void buttonTest_1_Click(object sender, EventArgs e)
  {
    this._searchlink = new PCSYS_PPG_LPS_ProxyConnector.LpsDataLink()
    {
      HttpAddress = this.textBoxDataLink_1.Text,
      Version = "PCSYS",
      Port = (int) this.numericUpDownPort_1.Value,
      IsActive = true
    };
    this._searchbutton = this.buttonTest_1;
    this.StartTest();
  }

  private void buttonSsccCounters_Click(object sender, EventArgs e)
  {
    using (FormSsccCounters formSsccCounters = new FormSsccCounters())
    {
      int num = (int) formSsccCounters.ShowDialog();
    }
  }

  private void ultraGridRegisters_AfterCellUpdate(object sender, CellEventArgs e)
  {
    this._registerhaschanges = true;
  }

  private void ultraGridFields_AfterCellUpdate(object sender, CellEventArgs e)
  {
    this._fieldshaschanges = true;
  }

  private void ultraGridRegisters_AfterRowUpdate(object sender, RowEventArgs e)
  {
    this._registerhaschanges = true;
  }

  private void ultraGridFields_AfterRowUpdate(object sender, RowEventArgs e)
  {
    this._fieldshaschanges = true;
  }

  private void buttonPalletType_Click(object sender, EventArgs e)
  {
    using (FormPalletTypes formPalletTypes = new FormPalletTypes())
    {
      int num = (int) formPalletTypes.ShowDialog();
    }
  }

  private void buttonBrowse_Click(object sender, EventArgs e)
  {
    using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
    {
      SelectedPath = this.textBoxPtfPath.Text
    })
    {
      if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
      this.textBoxPtfPath.Text = folderBrowserDialog.SelectedPath;
    }
  }

  private void buttonPrinterCodes_Click(object sender, EventArgs e)
  {
    using (FormPrinters formPrinters = new FormPrinters())
    {
      int num = (int) formPrinters.ShowDialog();
    }
  }

  private void buttonSAP_Click(object sender, EventArgs e)
  {
    using (FormSAPServer formSapServer = new FormSAPServer())
    {
      int num = (int) formSapServer.ShowDialog();
    }
  }

  private void buttonConstore_Click(object sender, EventArgs e)
  {
    using (FormConstorePath formConstorePath = new FormConstorePath())
    {
      int num = (int) formConstorePath.ShowDialog();
    }
  }

  private void buttonDestinationCodes_Click(object sender, EventArgs e)
  {
    using (FormDestinationCodes destinationCodes = new FormDestinationCodes())
    {
      int num = (int) destinationCodes.ShowDialog();
    }
  }

  private void buttonReportPathBrowse_Click(object sender, EventArgs e)
  {
    using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
    {
      SelectedPath = this.textBoxReportPath.Text
    })
    {
      if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
      this.textBoxReportPath.Text = folderBrowserDialog.SelectedPath;
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
    Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
    Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
    this.groupBox1 = new GroupBox();
    this.pictureBoxWait_1 = new PictureBox();
    this.pictureBoxWait_0 = new PictureBox();
    this.buttonTest_1 = new Button();
    this.buttonSearch_1 = new Button();
    this.radioButtonIsActive_1 = new RadioButton();
    this.numericUpDownPort_1 = new NumericUpDown();
    this.label6 = new Label();
    this.textBoxDataLink_1 = new TextBox();
    this.label7 = new Label();
    this.buttonTest_0 = new Button();
    this.buttonSearch_0 = new Button();
    this.radioButtonIsActive_0 = new RadioButton();
    this.numericUpDownPort_0 = new NumericUpDown();
    this.label5 = new Label();
    this.textBoxDataLink_0 = new TextBox();
    this.label4 = new Label();
    this.groupBoxPLC = new GroupBox();
    this.groupBox4 = new GroupBox();
    this.splitContainer1 = new SplitContainer();
    this.ultraGridRegisters = new UltraGrid();
    this.toolStrip1 = new ToolStrip();
    this.toolStripButtonAddRegister = new ToolStripButton();
    this.toolStripButtonDeleteRegister = new ToolStripButton();
    this.toolStripLabel1 = new ToolStripLabel();
    this.ultraGridFields = new UltraGrid();
    this.toolStrip2 = new ToolStrip();
    this.toolStripButton1 = new ToolStripButton();
    this.toolStripButton2 = new ToolStripButton();
    this.toolStripLabel2 = new ToolStripLabel();
    this.groupBox3 = new GroupBox();
    this.checkBoxOffLine = new CheckBox();
    this.buttonTestPlcConnection = new Button();
    this.numericUpDownSlot = new NumericUpDown();
    this.label3 = new Label();
    this.numericUpDownRack = new NumericUpDown();
    this.label2 = new Label();
    this.textBoxIp = new TextBox();
    this.label1 = new Label();
    this.groupBox2 = new GroupBox();
    this.buttonDestinationCodes = new Button();
    this.buttonConstore = new Button();
    this.buttonSAP = new Button();
    this.buttonPrinterCodes = new Button();
    this.buttonBrowse = new Button();
    this.textBoxPtfPath = new TextBox();
    this.label8 = new Label();
    this.buttonPalletType = new Button();
    this.buttonSsccCounters = new Button();
    this.buttonSave = new Button();
    this.backgroundWorkerSearch = new BackgroundWorker();
    this.textBoxReportPath = new TextBox();
    this.label9 = new Label();
    this.buttonReportPathBrowse = new Button();
    this.groupBox1.SuspendLayout();
    ((ISupportInitialize) this.pictureBoxWait_1).BeginInit();
    ((ISupportInitialize) this.pictureBoxWait_0).BeginInit();
    this.numericUpDownPort_1.BeginInit();
    this.numericUpDownPort_0.BeginInit();
    this.groupBoxPLC.SuspendLayout();
    this.groupBox4.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    ((ISupportInitialize) this.ultraGridRegisters).BeginInit();
    this.toolStrip1.SuspendLayout();
    ((ISupportInitialize) this.ultraGridFields).BeginInit();
    this.toolStrip2.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.numericUpDownSlot.BeginInit();
    this.numericUpDownRack.BeginInit();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.groupBox1.Controls.Add((Control) this.pictureBoxWait_1);
    this.groupBox1.Controls.Add((Control) this.pictureBoxWait_0);
    this.groupBox1.Controls.Add((Control) this.buttonTest_1);
    this.groupBox1.Controls.Add((Control) this.buttonSearch_1);
    this.groupBox1.Controls.Add((Control) this.radioButtonIsActive_1);
    this.groupBox1.Controls.Add((Control) this.numericUpDownPort_1);
    this.groupBox1.Controls.Add((Control) this.label6);
    this.groupBox1.Controls.Add((Control) this.textBoxDataLink_1);
    this.groupBox1.Controls.Add((Control) this.label7);
    this.groupBox1.Controls.Add((Control) this.buttonTest_0);
    this.groupBox1.Controls.Add((Control) this.buttonSearch_0);
    this.groupBox1.Controls.Add((Control) this.radioButtonIsActive_0);
    this.groupBox1.Controls.Add((Control) this.numericUpDownPort_0);
    this.groupBox1.Controls.Add((Control) this.label5);
    this.groupBox1.Controls.Add((Control) this.textBoxDataLink_0);
    this.groupBox1.Controls.Add((Control) this.label4);
    this.groupBox1.Location = new Point(19, 32 /*0x20*/);
    this.groupBox1.Margin = new Padding(4);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Padding = new Padding(4);
    this.groupBox1.Size = new Size(321, 308);
    this.groupBox1.TabIndex = 0;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Data Links";
    this.pictureBoxWait_1.Location = new Point(173, 233);
    this.pictureBoxWait_1.Margin = new Padding(4);
    this.pictureBoxWait_1.Name = "pictureBoxWait_1";
    this.pictureBoxWait_1.Size = new Size(32 /*0x20*/, 28);
    this.pictureBoxWait_1.TabIndex = 20;
    this.pictureBoxWait_1.TabStop = false;
    this.pictureBoxWait_1.Visible = false;
    this.pictureBoxWait_0.Location = new Point(173, 91);
    this.pictureBoxWait_0.Margin = new Padding(4);
    this.pictureBoxWait_0.Name = "pictureBoxWait_0";
    this.pictureBoxWait_0.Size = new Size(32 /*0x20*/, 28);
    this.pictureBoxWait_0.TabIndex = 19;
    this.pictureBoxWait_0.TabStop = false;
    this.pictureBoxWait_0.Visible = false;
    this.buttonTest_1.Location = new Point(213, 268);
    this.buttonTest_1.Margin = new Padding(4);
    this.buttonTest_1.Name = "buttonTest_1";
    this.buttonTest_1.Size = new Size(100, 28);
    this.buttonTest_1.TabIndex = 18;
    this.buttonTest_1.Text = "Test";
    this.buttonTest_1.UseVisualStyleBackColor = true;
    this.buttonTest_1.Click += new EventHandler(this.buttonTest_1_Click);
    this.buttonSearch_1.Location = new Point(213, 233);
    this.buttonSearch_1.Margin = new Padding(4);
    this.buttonSearch_1.Name = "buttonSearch_1";
    this.buttonSearch_1.Size = new Size(100, 28);
    this.buttonSearch_1.TabIndex = 17;
    this.buttonSearch_1.Text = "Search";
    this.buttonSearch_1.UseVisualStyleBackColor = true;
    this.buttonSearch_1.Click += new EventHandler(this.buttonSearch_1_Click);
    this.radioButtonIsActive_1.AutoSize = true;
    this.radioButtonIsActive_1.Location = new Point(240 /*0xF0*/, 175);
    this.radioButtonIsActive_1.Margin = new Padding(4);
    this.radioButtonIsActive_1.Name = "radioButtonIsActive_1";
    this.radioButtonIsActive_1.Size = new Size(67, 21);
    this.radioButtonIsActive_1.TabIndex = 16 /*0x10*/;
    this.radioButtonIsActive_1.TabStop = true;
    this.radioButtonIsActive_1.Text = "Active";
    this.radioButtonIsActive_1.UseVisualStyleBackColor = true;
    this.numericUpDownPort_1.Location = new Point(19, 252);
    this.numericUpDownPort_1.Margin = new Padding(4);
    this.numericUpDownPort_1.Maximum = new Decimal(new int[4]
    {
      99999,
      0,
      0,
      0
    });
    this.numericUpDownPort_1.Name = "numericUpDownPort_1";
    this.numericUpDownPort_1.Size = new Size(77, 22);
    this.numericUpDownPort_1.TabIndex = 15;
    this.numericUpDownPort_1.TextAlign = HorizontalAlignment.Right;
    this.label6.AutoSize = true;
    this.label6.Location = new Point(15, 233);
    this.label6.Margin = new Padding(4, 0, 4, 0);
    this.label6.Name = "label6";
    this.label6.Size = new Size(34, 17);
    this.label6.TabIndex = 14;
    this.label6.Text = "Port";
    this.textBoxDataLink_1.Location = new Point(19, 201);
    this.textBoxDataLink_1.Margin = new Padding(4);
    this.textBoxDataLink_1.Name = "textBoxDataLink_1";
    this.textBoxDataLink_1.Size = new Size(293, 22);
    this.textBoxDataLink_1.TabIndex = 13;
    this.label7.AutoSize = true;
    this.label7.Location = new Point(15, 180);
    this.label7.Margin = new Padding(4, 0, 4, 0);
    this.label7.Name = "label7";
    this.label7.Size = new Size(75, 17);
    this.label7.TabIndex = 12;
    this.label7.Text = "IP address";
    this.buttonTest_0.Location = new Point(213, (int) sbyte.MaxValue);
    this.buttonTest_0.Margin = new Padding(4);
    this.buttonTest_0.Name = "buttonTest_0";
    this.buttonTest_0.Size = new Size(100, 28);
    this.buttonTest_0.TabIndex = 11;
    this.buttonTest_0.Text = "Test";
    this.buttonTest_0.UseVisualStyleBackColor = true;
    this.buttonTest_0.Click += new EventHandler(this.buttonTest_0_Click);
    this.buttonSearch_0.Location = new Point(213, 91);
    this.buttonSearch_0.Margin = new Padding(4);
    this.buttonSearch_0.Name = "buttonSearch_0";
    this.buttonSearch_0.Size = new Size(100, 28);
    this.buttonSearch_0.TabIndex = 10;
    this.buttonSearch_0.Text = "Search";
    this.buttonSearch_0.UseVisualStyleBackColor = true;
    this.buttonSearch_0.Click += new EventHandler(this.buttonSearch_0_Click);
    this.radioButtonIsActive_0.AutoSize = true;
    this.radioButtonIsActive_0.Location = new Point(240 /*0xF0*/, 33);
    this.radioButtonIsActive_0.Margin = new Padding(4);
    this.radioButtonIsActive_0.Name = "radioButtonIsActive_0";
    this.radioButtonIsActive_0.Size = new Size(67, 21);
    this.radioButtonIsActive_0.TabIndex = 8;
    this.radioButtonIsActive_0.TabStop = true;
    this.radioButtonIsActive_0.Text = "Active";
    this.radioButtonIsActive_0.UseVisualStyleBackColor = true;
    this.numericUpDownPort_0.Location = new Point(19, 111);
    this.numericUpDownPort_0.Margin = new Padding(4);
    this.numericUpDownPort_0.Maximum = new Decimal(new int[4]
    {
      99999,
      0,
      0,
      0
    });
    this.numericUpDownPort_0.Name = "numericUpDownPort_0";
    this.numericUpDownPort_0.Size = new Size(77, 22);
    this.numericUpDownPort_0.TabIndex = 7;
    this.numericUpDownPort_0.TextAlign = HorizontalAlignment.Right;
    this.label5.AutoSize = true;
    this.label5.Location = new Point(15, 91);
    this.label5.Margin = new Padding(4, 0, 4, 0);
    this.label5.Name = "label5";
    this.label5.Size = new Size(34, 17);
    this.label5.TabIndex = 6;
    this.label5.Text = "Port";
    this.textBoxDataLink_0.Location = new Point(19, 59);
    this.textBoxDataLink_0.Margin = new Padding(4);
    this.textBoxDataLink_0.Name = "textBoxDataLink_0";
    this.textBoxDataLink_0.Size = new Size(293, 22);
    this.textBoxDataLink_0.TabIndex = 3;
    this.label4.AutoSize = true;
    this.label4.Location = new Point(15, 38);
    this.label4.Margin = new Padding(4, 0, 4, 0);
    this.label4.Name = "label4";
    this.label4.Size = new Size(75, 17);
    this.label4.TabIndex = 2;
    this.label4.Text = "IP address";
    this.groupBoxPLC.Controls.Add((Control) this.groupBox4);
    this.groupBoxPLC.Controls.Add((Control) this.groupBox3);
    this.groupBoxPLC.Location = new Point(348, 32 /*0x20*/);
    this.groupBoxPLC.Margin = new Padding(4);
    this.groupBoxPLC.Name = "groupBoxPLC";
    this.groupBoxPLC.Padding = new Padding(4);
    this.groupBoxPLC.Size = new Size(805, 496);
    this.groupBoxPLC.TabIndex = 1;
    this.groupBoxPLC.TabStop = false;
    this.groupBoxPLC.Text = "PLC";
    this.groupBox4.Controls.Add((Control) this.splitContainer1);
    this.groupBox4.Location = new Point(8, 116);
    this.groupBox4.Margin = new Padding(4);
    this.groupBox4.Name = "groupBox4";
    this.groupBox4.Padding = new Padding(4);
    this.groupBox4.Size = new Size(789, 409);
    this.groupBox4.TabIndex = 4;
    this.groupBox4.TabStop = false;
    this.groupBox4.Text = "Registers";
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.Location = new Point(4, 19);
    this.splitContainer1.Margin = new Padding(4);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.ultraGridRegisters);
    this.splitContainer1.Panel1.Controls.Add((Control) this.toolStrip1);
    this.splitContainer1.Panel2.Controls.Add((Control) this.ultraGridFields);
    this.splitContainer1.Panel2.Controls.Add((Control) this.toolStrip2);
    this.splitContainer1.Size = new Size(781, 386);
    this.splitContainer1.SplitterDistance = 282;
    this.splitContainer1.SplitterWidth = 5;
    this.splitContainer1.TabIndex = 2;
    appearance1.BackColor = SystemColors.Window;
    appearance1.BorderColor = SystemColors.InactiveCaption;
    this.ultraGridRegisters.DisplayLayout.Appearance = (AppearanceBase) appearance1;
    this.ultraGridRegisters.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
    this.ultraGridRegisters.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridRegisters.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
    appearance2.BackColor = SystemColors.ActiveBorder;
    appearance2.BackColor2 = SystemColors.ControlDark;
    appearance2.BackGradientStyle = GradientStyle.Vertical;
    appearance2.BorderColor = SystemColors.Window;
    this.ultraGridRegisters.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance2;
    appearance3.ForeColor = SystemColors.GrayText;
    this.ultraGridRegisters.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance3;
    this.ultraGridRegisters.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridRegisters.DisplayLayout.GroupByBox.Hidden = true;
    appearance4.BackColor = SystemColors.ControlLightLight;
    appearance4.BackColor2 = SystemColors.Control;
    appearance4.BackGradientStyle = GradientStyle.Horizontal;
    appearance4.ForeColor = SystemColors.GrayText;
    this.ultraGridRegisters.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance4;
    this.ultraGridRegisters.DisplayLayout.MaxColScrollRegions = 1;
    this.ultraGridRegisters.DisplayLayout.MaxRowScrollRegions = 1;
    appearance5.BackColor = SystemColors.Window;
    appearance5.ForeColor = SystemColors.ControlText;
    this.ultraGridRegisters.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance5;
    appearance6.BackColor = SystemColors.Highlight;
    appearance6.ForeColor = SystemColors.HighlightText;
    this.ultraGridRegisters.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance6;
    this.ultraGridRegisters.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
    this.ultraGridRegisters.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
    this.ultraGridRegisters.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
    this.ultraGridRegisters.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
    this.ultraGridRegisters.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
    appearance7.BackColor = SystemColors.Window;
    this.ultraGridRegisters.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance7;
    appearance8.BorderColor = Color.Silver;
    appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
    this.ultraGridRegisters.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance8;
    this.ultraGridRegisters.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
    this.ultraGridRegisters.DisplayLayout.Override.CellPadding = 0;
    appearance9.BackColor = SystemColors.Control;
    appearance9.BackColor2 = SystemColors.ControlDark;
    appearance9.BackGradientAlignment = GradientAlignment.Element;
    appearance9.BackGradientStyle = GradientStyle.Horizontal;
    appearance9.BorderColor = SystemColors.Window;
    this.ultraGridRegisters.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance9;
    appearance10.TextHAlignAsString = "Left";
    this.ultraGridRegisters.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance10;
    this.ultraGridRegisters.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
    this.ultraGridRegisters.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
    appearance11.BackColor = SystemColors.Window;
    appearance11.BorderColor = Color.Silver;
    this.ultraGridRegisters.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance11;
    this.ultraGridRegisters.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
    appearance12.BackColor = SystemColors.ControlLight;
    this.ultraGridRegisters.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance12;
    this.ultraGridRegisters.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
    this.ultraGridRegisters.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
    this.ultraGridRegisters.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
    this.ultraGridRegisters.Dock = DockStyle.Top;
    this.ultraGridRegisters.Location = new Point(0, 27);
    this.ultraGridRegisters.Margin = new Padding(4);
    this.ultraGridRegisters.Name = "ultraGridRegisters";
    this.ultraGridRegisters.Size = new Size(282, 327);
    this.ultraGridRegisters.TabIndex = 4;
    this.ultraGridRegisters.Text = "ultraGrid2";
    this.ultraGridRegisters.AfterCellUpdate += new CellEventHandler(this.ultraGridRegisters_AfterCellUpdate);
    this.ultraGridRegisters.AfterRowActivate += new EventHandler(this.ultraGridRegisters_AfterRowActivate);
    this.ultraGridRegisters.AfterRowUpdate += new RowEventHandler(this.ultraGridRegisters_AfterRowUpdate);
    this.ultraGridRegisters.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(this.ultraGridRegisters_BeforeRowsDeleted);
    this.toolStrip1.ImageScalingSize = new Size(20, 20);
    this.toolStrip1.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.toolStripButtonAddRegister,
      (ToolStripItem) this.toolStripButtonDeleteRegister,
      (ToolStripItem) this.toolStripLabel1
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(282, 27);
    this.toolStrip1.TabIndex = 0;
    this.toolStrip1.Text = "toolStrip1";
    this.toolStripButtonAddRegister.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonAddRegister.Image = (Image) PCSYS_PLC_Server.Properties.Resources.document_add;
    this.toolStripButtonAddRegister.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonAddRegister.Name = "toolStripButtonAddRegister";
    this.toolStripButtonAddRegister.Size = new Size(24, 24);
    this.toolStripButtonAddRegister.Text = "toolStripButton1";
    this.toolStripButtonAddRegister.ToolTipText = "Add Register";
    this.toolStripButtonAddRegister.Click += new EventHandler(this.toolStripButtonAddRegister_Click);
    this.toolStripButtonDeleteRegister.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonDeleteRegister.Image = (Image) PCSYS_PLC_Server.Properties.Resources.document_delete;
    this.toolStripButtonDeleteRegister.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonDeleteRegister.Name = "toolStripButtonDeleteRegister";
    this.toolStripButtonDeleteRegister.Size = new Size(24, 24);
    this.toolStripButtonDeleteRegister.Text = "toolStripButton1";
    this.toolStripButtonDeleteRegister.ToolTipText = "Delete register";
    this.toolStripLabel1.Name = "toolStripLabel1";
    this.toolStripLabel1.Size = new Size(69, 24);
    this.toolStripLabel1.Text = "Registers";
    appearance13.BackColor = SystemColors.Window;
    appearance13.BorderColor = SystemColors.InactiveCaption;
    this.ultraGridFields.DisplayLayout.Appearance = (AppearanceBase) appearance13;
    this.ultraGridFields.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
    this.ultraGridFields.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridFields.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
    appearance14.BackColor = SystemColors.ActiveBorder;
    appearance14.BackColor2 = SystemColors.ControlDark;
    appearance14.BackGradientStyle = GradientStyle.Vertical;
    appearance14.BorderColor = SystemColors.Window;
    this.ultraGridFields.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance14;
    appearance15.ForeColor = SystemColors.GrayText;
    this.ultraGridFields.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance15;
    this.ultraGridFields.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridFields.DisplayLayout.GroupByBox.Hidden = true;
    appearance16.BackColor = SystemColors.ControlLightLight;
    appearance16.BackColor2 = SystemColors.Control;
    appearance16.BackGradientStyle = GradientStyle.Horizontal;
    appearance16.ForeColor = SystemColors.GrayText;
    this.ultraGridFields.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance16;
    this.ultraGridFields.DisplayLayout.MaxColScrollRegions = 1;
    this.ultraGridFields.DisplayLayout.MaxRowScrollRegions = 1;
    appearance17.BackColor = SystemColors.Window;
    appearance17.ForeColor = SystemColors.ControlText;
    this.ultraGridFields.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance17;
    appearance18.BackColor = SystemColors.Highlight;
    appearance18.ForeColor = SystemColors.HighlightText;
    this.ultraGridFields.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance18;
    this.ultraGridFields.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
    this.ultraGridFields.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
    this.ultraGridFields.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
    this.ultraGridFields.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
    this.ultraGridFields.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
    appearance19.BackColor = SystemColors.Window;
    this.ultraGridFields.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance19;
    appearance20.BorderColor = Color.Silver;
    appearance20.TextTrimming = TextTrimming.EllipsisCharacter;
    this.ultraGridFields.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance20;
    this.ultraGridFields.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
    this.ultraGridFields.DisplayLayout.Override.CellPadding = 0;
    appearance21.BackColor = SystemColors.Control;
    appearance21.BackColor2 = SystemColors.ControlDark;
    appearance21.BackGradientAlignment = GradientAlignment.Element;
    appearance21.BackGradientStyle = GradientStyle.Horizontal;
    appearance21.BorderColor = SystemColors.Window;
    this.ultraGridFields.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance21;
    appearance22.TextHAlignAsString = "Left";
    this.ultraGridFields.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance22;
    this.ultraGridFields.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
    this.ultraGridFields.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
    appearance23.BackColor = SystemColors.Window;
    appearance23.BorderColor = Color.Silver;
    this.ultraGridFields.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance23;
    this.ultraGridFields.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
    appearance24.BackColor = SystemColors.ControlLight;
    this.ultraGridFields.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance24;
    this.ultraGridFields.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
    this.ultraGridFields.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
    this.ultraGridFields.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
    this.ultraGridFields.Dock = DockStyle.Top;
    this.ultraGridFields.Location = new Point(0, 27);
    this.ultraGridFields.Margin = new Padding(4);
    this.ultraGridFields.Name = "ultraGridFields";
    this.ultraGridFields.Size = new Size(494, 327);
    this.ultraGridFields.TabIndex = 3;
    this.ultraGridFields.Text = "ultraGrid2";
    this.ultraGridFields.AfterCellUpdate += new CellEventHandler(this.ultraGridFields_AfterCellUpdate);
    this.ultraGridFields.AfterRowUpdate += new RowEventHandler(this.ultraGridFields_AfterRowUpdate);
    this.ultraGridFields.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(this.ultraGridFields_BeforeRowsDeleted);
    this.toolStrip2.ImageScalingSize = new Size(20, 20);
    this.toolStrip2.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.toolStripButton1,
      (ToolStripItem) this.toolStripButton2,
      (ToolStripItem) this.toolStripLabel2
    });
    this.toolStrip2.Location = new Point(0, 0);
    this.toolStrip2.Name = "toolStrip2";
    this.toolStrip2.Size = new Size(494, 27);
    this.toolStrip2.TabIndex = 1;
    this.toolStrip2.Text = "toolStrip2";
    this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButton1.Image = (Image) PCSYS_PLC_Server.Properties.Resources.document_add;
    this.toolStripButton1.ImageTransparentColor = Color.Magenta;
    this.toolStripButton1.Name = "toolStripButton1";
    this.toolStripButton1.Size = new Size(24, 24);
    this.toolStripButton1.Text = "toolStripButton1";
    this.toolStripButton1.ToolTipText = "Add Field";
    this.toolStripButton1.Click += new EventHandler(this.toolStripButton1_Click);
    this.toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButton2.Image = (Image) PCSYS_PLC_Server.Properties.Resources.document_delete;
    this.toolStripButton2.ImageTransparentColor = Color.Magenta;
    this.toolStripButton2.Name = "toolStripButton2";
    this.toolStripButton2.Size = new Size(24, 24);
    this.toolStripButton2.Text = "toolStripButton1";
    this.toolStripButton2.ToolTipText = "Delete field";
    this.toolStripLabel2.Name = "toolStripLabel2";
    this.toolStripLabel2.Size = new Size(47, 24);
    this.toolStripLabel2.Text = "Fields";
    this.groupBox3.Controls.Add((Control) this.checkBoxOffLine);
    this.groupBox3.Controls.Add((Control) this.buttonTestPlcConnection);
    this.groupBox3.Controls.Add((Control) this.numericUpDownSlot);
    this.groupBox3.Controls.Add((Control) this.label3);
    this.groupBox3.Controls.Add((Control) this.numericUpDownRack);
    this.groupBox3.Controls.Add((Control) this.label2);
    this.groupBox3.Controls.Add((Control) this.textBoxIp);
    this.groupBox3.Controls.Add((Control) this.label1);
    this.groupBox3.Location = new Point(8, 34);
    this.groupBox3.Margin = new Padding(4);
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.Padding = new Padding(4);
    this.groupBox3.Size = new Size(785, 74);
    this.groupBox3.TabIndex = 3;
    this.groupBox3.TabStop = false;
    this.groupBox3.Text = "Server";
    this.checkBoxOffLine.AutoSize = true;
    this.checkBoxOffLine.Location = new Point(12, 46);
    this.checkBoxOffLine.Margin = new Padding(3, 2, 3, 2);
    this.checkBoxOffLine.Name = "checkBoxOffLine";
    this.checkBoxOffLine.Size = new Size(71, 21);
    this.checkBoxOffLine.TabIndex = 20;
    this.checkBoxOffLine.Text = "Offline";
    this.checkBoxOffLine.UseVisualStyleBackColor = true;
    this.buttonTestPlcConnection.Location = new Point(663, 21);
    this.buttonTestPlcConnection.Margin = new Padding(4);
    this.buttonTestPlcConnection.Name = "buttonTestPlcConnection";
    this.buttonTestPlcConnection.Size = new Size(100, 28);
    this.buttonTestPlcConnection.TabIndex = 19;
    this.buttonTestPlcConnection.Text = "Test";
    this.buttonTestPlcConnection.UseVisualStyleBackColor = true;
    this.numericUpDownSlot.Location = new Point(527, 22);
    this.numericUpDownSlot.Margin = new Padding(4);
    this.numericUpDownSlot.Name = "numericUpDownSlot";
    this.numericUpDownSlot.Size = new Size(77, 22);
    this.numericUpDownSlot.TabIndex = 5;
    this.numericUpDownSlot.TextAlign = HorizontalAlignment.Right;
    this.label3.AutoSize = true;
    this.label3.Location = new Point(485, 25);
    this.label3.Margin = new Padding(4, 0, 4, 0);
    this.label3.Name = "label3";
    this.label3.Size = new Size(32 /*0x20*/, 17);
    this.label3.TabIndex = 4;
    this.label3.Text = "Slot";
    this.numericUpDownRack.Location = new Point(379, 21);
    this.numericUpDownRack.Margin = new Padding(4);
    this.numericUpDownRack.Name = "numericUpDownRack";
    this.numericUpDownRack.Size = new Size(77, 22);
    this.numericUpDownRack.TabIndex = 3;
    this.numericUpDownRack.TextAlign = HorizontalAlignment.Right;
    this.label2.AutoSize = true;
    this.label2.Location = new Point(327, 25);
    this.label2.Margin = new Padding(4, 0, 4, 0);
    this.label2.Name = "label2";
    this.label2.Size = new Size(40, 17);
    this.label2.TabIndex = 2;
    this.label2.Text = "Rack";
    this.textBoxIp.Location = new Point(93, 21);
    this.textBoxIp.Margin = new Padding(4);
    this.textBoxIp.Name = "textBoxIp";
    this.textBoxIp.Size = new Size(197, 22);
    this.textBoxIp.TabIndex = 1;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(9, 25);
    this.label1.Margin = new Padding(4, 0, 4, 0);
    this.label1.Name = "label1";
    this.label1.Size = new Size(75, 17);
    this.label1.TabIndex = 0;
    this.label1.Text = "IP address";
    this.groupBox2.Controls.Add((Control) this.buttonReportPathBrowse);
    this.groupBox2.Controls.Add((Control) this.textBoxReportPath);
    this.groupBox2.Controls.Add((Control) this.label9);
    this.groupBox2.Controls.Add((Control) this.buttonDestinationCodes);
    this.groupBox2.Controls.Add((Control) this.buttonConstore);
    this.groupBox2.Controls.Add((Control) this.buttonSAP);
    this.groupBox2.Controls.Add((Control) this.buttonPrinterCodes);
    this.groupBox2.Controls.Add((Control) this.buttonBrowse);
    this.groupBox2.Controls.Add((Control) this.textBoxPtfPath);
    this.groupBox2.Controls.Add((Control) this.label8);
    this.groupBox2.Controls.Add((Control) this.buttonPalletType);
    this.groupBox2.Controls.Add((Control) this.buttonSsccCounters);
    this.groupBox2.Location = new Point(19, 347);
    this.groupBox2.Margin = new Padding(4);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Padding = new Padding(4);
    this.groupBox2.Size = new Size(321, 245);
    this.groupBox2.TabIndex = 2;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Settings";
    this.buttonDestinationCodes.Location = new Point(18, 59);
    this.buttonDestinationCodes.Margin = new Padding(4);
    this.buttonDestinationCodes.Name = "buttonDestinationCodes";
    this.buttonDestinationCodes.Size = new Size(147, 28);
    this.buttonDestinationCodes.TabIndex = 8;
    this.buttonDestinationCodes.Text = "Destination Codes";
    this.buttonDestinationCodes.UseVisualStyleBackColor = true;
    this.buttonDestinationCodes.Click += new EventHandler(this.buttonDestinationCodes_Click);
    this.buttonConstore.Location = new Point(21, 209);
    this.buttonConstore.Margin = new Padding(4);
    this.buttonConstore.Name = "buttonConstore";
    this.buttonConstore.Size = new Size(75, 28);
    this.buttonConstore.TabIndex = 7;
    this.buttonConstore.Text = "Constore";
    this.buttonConstore.UseVisualStyleBackColor = true;
    this.buttonConstore.Click += new EventHandler(this.buttonConstore_Click);
    this.buttonSAP.Location = new Point(104, 209);
    this.buttonSAP.Margin = new Padding(4);
    this.buttonSAP.Name = "buttonSAP";
    this.buttonSAP.Size = new Size(70, 28);
    this.buttonSAP.TabIndex = 6;
    this.buttonSAP.Text = "SAP";
    this.buttonSAP.UseVisualStyleBackColor = true;
    this.buttonSAP.Click += new EventHandler(this.buttonSAP_Click);
    this.buttonPrinterCodes.Location = new Point(174, 59);
    this.buttonPrinterCodes.Margin = new Padding(4);
    this.buttonPrinterCodes.Name = "buttonPrinterCodes";
    this.buttonPrinterCodes.Size = new Size(140, 28);
    this.buttonPrinterCodes.TabIndex = 5;
    this.buttonPrinterCodes.Text = "Printers";
    this.buttonPrinterCodes.UseVisualStyleBackColor = true;
    this.buttonPrinterCodes.Click += new EventHandler(this.buttonPrinterCodes_Click);
    this.buttonBrowse.Location = new Point(212, 138);
    this.buttonBrowse.Margin = new Padding(4);
    this.buttonBrowse.Name = "buttonBrowse";
    this.buttonBrowse.Size = new Size(100, 28);
    this.buttonBrowse.TabIndex = 4;
    this.buttonBrowse.Text = "Browse";
    this.buttonBrowse.UseVisualStyleBackColor = true;
    this.buttonBrowse.Click += new EventHandler(this.buttonBrowse_Click);
    this.textBoxPtfPath.Location = new Point(19, 110);
    this.textBoxPtfPath.Margin = new Padding(3, 2, 3, 2);
    this.textBoxPtfPath.Name = "textBoxPtfPath";
    this.textBoxPtfPath.Size = new Size(293, 22);
    this.textBoxPtfPath.TabIndex = 3;
    this.label8.AutoSize = true;
    this.label8.Location = new Point(16 /*0x10*/, 91);
    this.label8.Name = "label8";
    this.label8.Size = new Size(67, 17);
    this.label8.TabIndex = 2;
    this.label8.Text = "PTF Path";
    this.buttonPalletType.Location = new Point(174, 23);
    this.buttonPalletType.Margin = new Padding(4);
    this.buttonPalletType.Name = "buttonPalletType";
    this.buttonPalletType.Size = new Size(140, 28);
    this.buttonPalletType.TabIndex = 1;
    this.buttonPalletType.Text = "Pallet types";
    this.buttonPalletType.UseVisualStyleBackColor = true;
    this.buttonPalletType.Click += new EventHandler(this.buttonPalletType_Click);
    this.buttonSsccCounters.Location = new Point(19, 23);
    this.buttonSsccCounters.Margin = new Padding(4);
    this.buttonSsccCounters.Name = "buttonSsccCounters";
    this.buttonSsccCounters.Size = new Size(147, 28);
    this.buttonSsccCounters.TabIndex = 0;
    this.buttonSsccCounters.Text = "SSCC Counters";
    this.buttonSsccCounters.UseVisualStyleBackColor = true;
    this.buttonSsccCounters.Click += new EventHandler(this.buttonSsccCounters_Click);
    this.buttonSave.Location = new Point(1049, 564);
    this.buttonSave.Margin = new Padding(4);
    this.buttonSave.Name = "buttonSave";
    this.buttonSave.Size = new Size(96 /*0x60*/, 28);
    this.buttonSave.TabIndex = 3;
    this.buttonSave.Text = "&save";
    this.buttonSave.UseVisualStyleBackColor = true;
    this.buttonSave.Click += new EventHandler(this.buttonSave_Click);
    this.backgroundWorkerSearch.DoWork += new DoWorkEventHandler(this.backgroundWorkerSearchSecond_DoWork);
    this.backgroundWorkerSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorkerSearch_RunWorkerCompleted);
    this.textBoxReportPath.Location = new Point(21, 172);
    this.textBoxReportPath.Margin = new Padding(3, 2, 3, 2);
    this.textBoxReportPath.Name = "textBoxReportPath";
    this.textBoxReportPath.Size = new Size(291, 22);
    this.textBoxReportPath.TabIndex = 10;
    this.label9.AutoSize = true;
    this.label9.Location = new Point(18, 153);
    this.label9.Name = "label9";
    this.label9.Size = new Size(67, 17);
    this.label9.TabIndex = 9;
    this.label9.Text = "PTF Path";
    this.buttonReportPathBrowse.Location = new Point(212, 200);
    this.buttonReportPathBrowse.Margin = new Padding(4);
    this.buttonReportPathBrowse.Name = "buttonReportPathBrowse";
    this.buttonReportPathBrowse.Size = new Size(100, 28);
    this.buttonReportPathBrowse.TabIndex = 11;
    this.buttonReportPathBrowse.Text = "Browse";
    this.buttonReportPathBrowse.UseVisualStyleBackColor = true;
    this.buttonReportPathBrowse.Click += new EventHandler(this.buttonReportPathBrowse_Click);
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.buttonSave);
    this.Controls.Add((Control) this.groupBox2);
    this.Controls.Add((Control) this.groupBoxPLC);
    this.Controls.Add((Control) this.groupBox1);
    this.Margin = new Padding(4);
    this.Name = "UserControlSettings";
    this.Size = new Size(1183, 603);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    ((ISupportInitialize) this.pictureBoxWait_1).EndInit();
    ((ISupportInitialize) this.pictureBoxWait_0).EndInit();
    this.numericUpDownPort_1.EndInit();
    this.numericUpDownPort_0.EndInit();
    this.groupBoxPLC.ResumeLayout(false);
    this.groupBox4.ResumeLayout(false);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel1.PerformLayout();
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.Panel2.PerformLayout();
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    ((ISupportInitialize) this.ultraGridRegisters).EndInit();
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    ((ISupportInitialize) this.ultraGridFields).EndInit();
    this.toolStrip2.ResumeLayout(false);
    this.toolStrip2.PerformLayout();
    this.groupBox3.ResumeLayout(false);
    this.groupBox3.PerformLayout();
    this.numericUpDownSlot.EndInit();
    this.numericUpDownRack.EndInit();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.ResumeLayout(false);
  }

  private delegate void SetTextCallback(string text, bool activate);
}
