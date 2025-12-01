// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.UserControls.UserControlRegister
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PCSYS_Plc_AppSettings;
using PCSYS_PPG_LPS_ProxyConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server.UserControls;

public class UserControlRegister : UserControl
{
  public Register Reg;
  private DataTable _data;
  private IContainer components = (IContainer) null;
  private Label label1;
  private UltraGrid ultraGridValues;
  private Timer timerLogs;
  private BackgroundWorker backgroundWorkerLogs;

  public UserControlRegister() => this.InitializeComponent();

  private void timerLogs_Tick(object sender, EventArgs e)
  {
    if (this.backgroundWorkerLogs.IsBusy)
      return;
    this.backgroundWorkerLogs.RunWorkerAsync();
  }

  private void backgroundWorkerLogs_DoWork(object sender, DoWorkEventArgs e)
  {
    this._data = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).GetData("SELECT tppgregisterfields.* FROM tppgregisters INNER JOIN " + $"tppgregisterfields ON tppgregisters.id = tppgregisterfields.registerid WHERE regname = {(int) this.Reg.RegName}").Tables[0];
  }

  private void backgroundWorkerLogs_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    if (this._data == null)
      return;
    this.ultraGridValues.DataSource = (object) this._data;
    this.ultraGridValues.DisplayLayout.Bands[0].Columns["id"].Hidden = true;
    this.ultraGridValues.DisplayLayout.Bands[0].Columns["registerid"].Hidden = true;
    this.ultraGridValues.DisplayLayout.Bands[0].Columns["address"].Width = 30;
    this.ultraGridValues.DisplayLayout.Bands[0].Columns["address"].Header.Caption = "Adr.";
    this.ultraGridValues.DisplayLayout.Bands[0].Columns["registertype"].Hidden = this.Reg.Type == RegisterType.Data;
    this.ultraGridValues.DisplayLayout.Bands[0].Columns["type"].Hidden = this.Reg.Type == RegisterType.Controller;
    this.ultraGridValues.DisplayLayout.Bands[0].Columns["datatype"].Hidden = true;
    this.ultraGridValues.DisplayLayout.Bands[0].Columns["access"].Hidden = true;
    this.ultraGridValues.DisplayLayout.Bands[0].Columns["registertype"].ValueList = (IValueList) this.GetFieldRegisterTypes();
    this.ultraGridValues.DisplayLayout.Bands[0].Columns["type"].ValueList = (IValueList) this.GetFieldTypes();
    if (this.Reg.Type == RegisterType.Controller)
      this.ultraGridValues.DisplayLayout.Bands[0].Columns["value"].Editor = (EmbeddableEditorBase) new CheckEditor();
    this.ultraGridValues.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.VisibleRows);
  }

  private ValueList GetFieldRegisterTypes()
  {
    ValueList fieldRegisterTypes = new ValueList();
    foreach (object dataValue in Enum.GetValues(typeof (FieldRegisterType)))
      fieldRegisterTypes.ValueListItems.Add(dataValue);
    return fieldRegisterTypes;
  }

  private void UserControlRegister_Load(object sender, EventArgs e)
  {
    this.label1.Text = this.Reg.Db.ToString();
  }

  private ValueList GetFieldTypes()
  {
    ValueList fieldTypes = new ValueList();
    foreach (object dataValue in Enum.GetValues(typeof (FieldType)))
      fieldTypes.ValueListItems.Add(dataValue);
    return fieldTypes;
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
    this.label1 = new Label();
    this.ultraGridValues = new UltraGrid();
    this.timerLogs = new Timer(this.components);
    this.backgroundWorkerLogs = new BackgroundWorker();
    ((ISupportInitialize) this.ultraGridValues).BeginInit();
    this.SuspendLayout();
    this.label1.AutoSize = true;
    this.label1.Dock = DockStyle.Top;
    this.label1.Location = new Point(0, 0);
    this.label1.Margin = new Padding(4, 0, 4, 0);
    this.label1.Name = "label1";
    this.label1.Size = new Size(45, 17);
    this.label1.TabIndex = 0;
    this.label1.Text = "Name";
    appearance1.BackColor = SystemColors.Window;
    appearance1.BorderColor = SystemColors.InactiveCaption;
    this.ultraGridValues.DisplayLayout.Appearance = (AppearanceBase) appearance1;
    this.ultraGridValues.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
    this.ultraGridValues.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridValues.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
    appearance2.BackColor = SystemColors.ActiveBorder;
    appearance2.BackColor2 = SystemColors.ControlDark;
    appearance2.BackGradientStyle = GradientStyle.Vertical;
    appearance2.BorderColor = SystemColors.Window;
    this.ultraGridValues.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance2;
    appearance3.ForeColor = SystemColors.GrayText;
    this.ultraGridValues.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance3;
    this.ultraGridValues.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridValues.DisplayLayout.GroupByBox.Hidden = true;
    appearance4.BackColor = SystemColors.ControlLightLight;
    appearance4.BackColor2 = SystemColors.Control;
    appearance4.BackGradientStyle = GradientStyle.Horizontal;
    appearance4.ForeColor = SystemColors.GrayText;
    this.ultraGridValues.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance4;
    this.ultraGridValues.DisplayLayout.MaxColScrollRegions = 1;
    this.ultraGridValues.DisplayLayout.MaxRowScrollRegions = 1;
    appearance5.BackColor = SystemColors.Window;
    appearance5.ForeColor = SystemColors.ControlText;
    this.ultraGridValues.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance5;
    appearance6.BackColor = SystemColors.Highlight;
    appearance6.ForeColor = SystemColors.HighlightText;
    this.ultraGridValues.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance6;
    this.ultraGridValues.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
    this.ultraGridValues.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
    this.ultraGridValues.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
    this.ultraGridValues.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
    this.ultraGridValues.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
    appearance7.BackColor = SystemColors.Window;
    this.ultraGridValues.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance7;
    appearance8.BorderColor = Color.Silver;
    appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
    this.ultraGridValues.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance8;
    this.ultraGridValues.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
    this.ultraGridValues.DisplayLayout.Override.CellPadding = 0;
    appearance9.BackColor = SystemColors.Control;
    appearance9.BackColor2 = SystemColors.ControlDark;
    appearance9.BackGradientAlignment = GradientAlignment.Element;
    appearance9.BackGradientStyle = GradientStyle.Horizontal;
    appearance9.BorderColor = SystemColors.Window;
    this.ultraGridValues.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance9;
    appearance10.TextHAlignAsString = "Left";
    this.ultraGridValues.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance10;
    this.ultraGridValues.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
    this.ultraGridValues.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
    appearance11.BackColor = SystemColors.Window;
    appearance11.BorderColor = Color.Silver;
    this.ultraGridValues.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance11;
    this.ultraGridValues.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
    appearance12.BackColor = SystemColors.ControlLight;
    this.ultraGridValues.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance12;
    this.ultraGridValues.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
    this.ultraGridValues.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
    this.ultraGridValues.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
    this.ultraGridValues.Dock = DockStyle.Fill;
    this.ultraGridValues.Location = new Point(0, 17);
    this.ultraGridValues.Margin = new Padding(4, 4, 4, 4);
    this.ultraGridValues.Name = "ultraGridValues";
    this.ultraGridValues.Size = new Size(293, 325);
    this.ultraGridValues.TabIndex = 1;
    this.ultraGridValues.Text = "ultraGrid1";
    this.timerLogs.Enabled = true;
    this.timerLogs.Interval = 1000;
    this.timerLogs.Tick += new EventHandler(this.timerLogs_Tick);
    this.backgroundWorkerLogs.DoWork += new DoWorkEventHandler(this.backgroundWorkerLogs_DoWork);
    this.backgroundWorkerLogs.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorkerLogs_RunWorkerCompleted);
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.ultraGridValues);
    this.Controls.Add((Control) this.label1);
    this.Margin = new Padding(4, 4, 4, 4);
    this.Name = "UserControlRegister";
    this.Size = new Size(293, 342);
    this.Load += new EventHandler(this.UserControlRegister_Load);
    ((ISupportInitialize) this.ultraGridValues).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
