// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormReprint
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PCSYS_Plc_AppSettings;
using PCSYS_PPG_Info;
using PCSYS_PPG_LPS_ProxyConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server;

public class FormReprint : Form
{
  private PrintResult _printresult;
  private PrintMode _printmode;
  public string _printeralias;
  public int _labelcount;
  private IContainer components = (IContainer) null;
  private ToolStrip toolStrip1;
  private ToolStripLabel toolStripLabel1;
  private DateTimePicker dateTimePickerDate;
  private UltraGrid ultraGridSSCC;
  private Button buttonRefresh;
  private ContextMenuStrip contextMenuStripPrint;
  private ToolStripMenuItem viewToolStripMenuItem;
  private ToolStripMenuItem printToolStripMenuItem;
  private BackgroundWorker backgroundWorkerprint;

  public FormReprint() => this.InitializeComponent();

  private void FormReprint_Load(object sender, EventArgs e) => this.GetPrinted();

  private void GetPrinted()
  {
    string str1 = $"SELECT * FROM tppgprintedsscc WHERE DATEPART(year,printtime) = {this.dateTimePickerDate.Value.Year} AND ";
    DateTime dateTime = this.dateTimePickerDate.Value;
    // ISSUE: variable of a boxed type
     int month =  dateTime.Month;
    dateTime = this.dateTimePickerDate.Value;
    // ISSUE: variable of a boxed type
    int day = dateTime.Day;
    string str2 = $"DATEPART(month,printtime) = {month} AND DATEPART(day,printtime) = {day}";
    string sqlString = $"{str1}{str2} ORDER BY id DESC";
    this.ultraGridSSCC.DataSource = (object) new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).GetData(sqlString).Tables[0];
  }

  private void dateTimePickerDate_ValueChanged(object sender, EventArgs e) => this.GetPrinted();

  private void buttonRefresh_Click(object sender, EventArgs e)
  {
    if (this.ultraGridSSCC.ActiveRow == null)
      return;
    this.GetPrinted();
  }

  private void Print()
  {
    using (FormSelectPrinter formSelectPrinter = new FormSelectPrinter())
    {
      if (formSelectPrinter.ShowDialog() != DialogResult.OK || formSelectPrinter.comboBoxPrinters.Text == string.Empty || this.backgroundWorkerprint.IsBusy)
        return;
      this._printeralias = formSelectPrinter.comboBoxPrinters.Text;
      this._labelcount = (int) formSelectPrinter.numericUpDownQty.Value;
    }
    this.backgroundWorkerprint.RunWorkerAsync();
  }

  private void viewToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this._printmode = PrintMode.View;
    this.Print();
  }

  private void backgroundWorkerprint_DoWork(object sender, DoWorkEventArgs e)
  {
    UltraGridRow activeRow = this.ultraGridSSCC.ActiveRow;
    this._printresult = new DataTools().PrintPallet(this._printmode, new OrderInformation()
    {
      Sscc = activeRow.Cells["sscc"].Text
    }, this._printeralias, string.Empty, this._labelcount, SettingsTools.MainSettings.DataLinks);
  }

  private void backgroundWorkerprint_RunWorkerCompleted(
    object sender,
    RunWorkerCompletedEventArgs e)
  {
    if (!this._printresult.HasError)
      return;
    int num = (int) MessageBox.Show(this._printresult.ErrorMessage);
  }

  private void printToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this._printmode = PrintMode.Print;
    this.Print();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormReprint));
    this.toolStrip1 = new ToolStrip();
    this.toolStripLabel1 = new ToolStripLabel();
    this.dateTimePickerDate = new DateTimePicker();
    this.ultraGridSSCC = new UltraGrid();
    this.buttonRefresh = new Button();
    this.contextMenuStripPrint = new ContextMenuStrip(this.components);
    this.viewToolStripMenuItem = new ToolStripMenuItem();
    this.printToolStripMenuItem = new ToolStripMenuItem();
    this.backgroundWorkerprint = new BackgroundWorker();
    this.toolStrip1.SuspendLayout();
    ((ISupportInitialize) this.ultraGridSSCC).BeginInit();
    this.contextMenuStripPrint.SuspendLayout();
    this.SuspendLayout();
    this.toolStrip1.ImageScalingSize = new Size(20, 20);
    this.toolStrip1.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.toolStripLabel1
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(776, 25);
    this.toolStrip1.TabIndex = 0;
    this.toolStrip1.Text = "toolStrip1";
    this.toolStripLabel1.Name = "toolStripLabel1";
    this.toolStripLabel1.Size = new Size(41, 22);
    this.toolStripLabel1.Text = "Date";
    this.dateTimePickerDate.CustomFormat = "dd-MM-yyyy";
    this.dateTimePickerDate.Format = DateTimePickerFormat.Custom;
    this.dateTimePickerDate.Location = new Point(55, 1);
    this.dateTimePickerDate.Name = "dateTimePickerDate";
    this.dateTimePickerDate.Size = new Size(99, 22);
    this.dateTimePickerDate.TabIndex = 1;
    this.dateTimePickerDate.ValueChanged += new EventHandler(this.dateTimePickerDate_ValueChanged);
    this.ultraGridSSCC.ContextMenuStrip = this.contextMenuStripPrint;
    appearance1.BackColor = SystemColors.Window;
    appearance1.BorderColor = SystemColors.InactiveCaption;
    this.ultraGridSSCC.DisplayLayout.Appearance = (AppearanceBase) appearance1;
    this.ultraGridSSCC.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
    this.ultraGridSSCC.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridSSCC.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
    appearance2.BackColor = SystemColors.ActiveBorder;
    appearance2.BackColor2 = SystemColors.ControlDark;
    appearance2.BackGradientStyle = GradientStyle.Vertical;
    appearance2.BorderColor = SystemColors.Window;
    this.ultraGridSSCC.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance2;
    appearance3.ForeColor = SystemColors.GrayText;
    this.ultraGridSSCC.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance3;
    this.ultraGridSSCC.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridSSCC.DisplayLayout.GroupByBox.Hidden = true;
    appearance4.BackColor = SystemColors.ControlLightLight;
    appearance4.BackColor2 = SystemColors.Control;
    appearance4.BackGradientStyle = GradientStyle.Horizontal;
    appearance4.ForeColor = SystemColors.GrayText;
    this.ultraGridSSCC.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance4;
    this.ultraGridSSCC.DisplayLayout.MaxColScrollRegions = 1;
    this.ultraGridSSCC.DisplayLayout.MaxRowScrollRegions = 1;
    appearance5.BackColor = SystemColors.Window;
    appearance5.ForeColor = SystemColors.ControlText;
    this.ultraGridSSCC.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance5;
    appearance6.BackColor = SystemColors.Highlight;
    appearance6.ForeColor = SystemColors.HighlightText;
    this.ultraGridSSCC.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance6;
    this.ultraGridSSCC.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
    this.ultraGridSSCC.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
    this.ultraGridSSCC.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
    this.ultraGridSSCC.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
    this.ultraGridSSCC.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
    appearance7.BackColor = SystemColors.Window;
    this.ultraGridSSCC.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance7;
    appearance8.BorderColor = Color.Silver;
    appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
    this.ultraGridSSCC.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance8;
    this.ultraGridSSCC.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
    this.ultraGridSSCC.DisplayLayout.Override.CellPadding = 0;
    appearance9.BackColor = SystemColors.Control;
    appearance9.BackColor2 = SystemColors.ControlDark;
    appearance9.BackGradientAlignment = GradientAlignment.Element;
    appearance9.BackGradientStyle = GradientStyle.Horizontal;
    appearance9.BorderColor = SystemColors.Window;
    this.ultraGridSSCC.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance9;
    appearance10.TextHAlignAsString = "Left";
    this.ultraGridSSCC.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance10;
    this.ultraGridSSCC.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
    this.ultraGridSSCC.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
    appearance11.BackColor = SystemColors.Window;
    appearance11.BorderColor = Color.Silver;
    this.ultraGridSSCC.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance11;
    this.ultraGridSSCC.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
    appearance12.BackColor = SystemColors.ControlLight;
    this.ultraGridSSCC.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance12;
    this.ultraGridSSCC.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
    this.ultraGridSSCC.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
    this.ultraGridSSCC.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
    this.ultraGridSSCC.Dock = DockStyle.Fill;
    this.ultraGridSSCC.Location = new Point(0, 25);
    this.ultraGridSSCC.Margin = new Padding(4);
    this.ultraGridSSCC.Name = "ultraGridSSCC";
    this.ultraGridSSCC.Size = new Size(776, 525);
    this.ultraGridSSCC.TabIndex = 5;
    this.ultraGridSSCC.Text = "ultraGrid2";
    this.buttonRefresh.Location = new Point(159, 0);
    this.buttonRefresh.Name = "buttonRefresh";
    this.buttonRefresh.Size = new Size(26, 23);
    this.buttonRefresh.TabIndex = 6;
    this.buttonRefresh.Text = "...";
    this.buttonRefresh.UseVisualStyleBackColor = true;
    this.buttonRefresh.Click += new EventHandler(this.buttonRefresh_Click);
    this.contextMenuStripPrint.ImageScalingSize = new Size(20, 20);
    this.contextMenuStripPrint.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.viewToolStripMenuItem,
      (ToolStripItem) this.printToolStripMenuItem
    });
    this.contextMenuStripPrint.Name = "contextMenuStripPrint";
    this.contextMenuStripPrint.Size = new Size(176 /*0xB0*/, 80 /*0x50*/);
    this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
    this.viewToolStripMenuItem.Size = new Size(175, 24);
    this.viewToolStripMenuItem.Text = "View";
    this.viewToolStripMenuItem.Click += new EventHandler(this.viewToolStripMenuItem_Click);
    this.printToolStripMenuItem.Name = "printToolStripMenuItem";
    this.printToolStripMenuItem.Size = new Size(175, 24);
    this.printToolStripMenuItem.Text = "Print";
    this.printToolStripMenuItem.Click += new EventHandler(this.printToolStripMenuItem_Click);
    this.backgroundWorkerprint.DoWork += new DoWorkEventHandler(this.backgroundWorkerprint_DoWork);
    this.backgroundWorkerprint.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorkerprint_RunWorkerCompleted);
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(776, 550);
    this.Controls.Add((Control) this.buttonRefresh);
    this.Controls.Add((Control) this.ultraGridSSCC);
    this.Controls.Add((Control) this.dateTimePickerDate);
    this.Controls.Add((Control) this.toolStrip1);
    this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
    this.Name = "FormReprint";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Reprint SSCC";
    this.Load += new EventHandler(this.FormReprint_Load);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    ((ISupportInitialize) this.ultraGridSSCC).EndInit();
    this.contextMenuStripPrint.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
