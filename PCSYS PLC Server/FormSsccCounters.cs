// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormSsccCounters
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PCSYS_Plc_AppSettings;
using PCSYS_PLC_Service.Worker;
using PCSYS_PPG_Info;
using PCSYS_PPG_LPS_ProxyConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server;

public class FormSsccCounters : Form
{
  private DataSet _serialnumbers;
  private IContainer components = (IContainer) null;
  private SplitContainer splitContainer1;
  private ToolStrip toolStrip1;
  private ToolStripButton toolStripButtonAddRegister;
  private ToolStripButton toolStripButtonDeleteRegister;
  private UltraGrid ultraGridCounters;
  private NumericUpDown numericUpDownValue;
  private Label label1;
  private GroupBox groupBox1;
  private Button buttonTest;
  private RichTextBox richTextBoxFormat;
  private ContextMenuStrip contextMenuStripVariables;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripButton toolStripButtonSave;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripMenuItem counterToolStripMenuItem;
  private NumericUpDown numericUpDownMax;
  private Label label2;

  public FormSsccCounters() => this.InitializeComponent();

  private void FormSsccCounters_Load(object sender, EventArgs e)
  {
    foreach (string name in Enum.GetNames(typeof (OrderField)))
      this.contextMenuStripVariables.Items.Add(name, (Image) null, new EventHandler(this.eeeToolStripMenuItem_Click));
    foreach (string fieldName in new DataTools().GetFieldNames(SettingsTools.MainSettings.DataLinks))
      this.contextMenuStripVariables.Items.Add(fieldName, (Image) null, new EventHandler(this.eeeToolStripMenuItem_Click));
    this._serialnumbers = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).GetData("SELECT * FROM tppgssccformats");
    this.ultraGridCounters.DataSource = (object) this._serialnumbers.Tables[0];
    UltraGridBand band = this.ultraGridCounters.DisplayLayout.Bands[0];
    band.Columns[0].Hidden = true;
    band.Columns["countervalue"].Hidden = true;
    band.Columns["format"].Hidden = true;
    band.Columns["name"].Header.Caption = "Name";
    band.Columns["maxvalue"].Hidden = true;
  }

  private void eeeToolStripMenuItem_Click(object sender, EventArgs e)
  {
    string str1 = sender.ToString();
    if (str1 == "Counter")
      str1 = "{000000000}";
    int startIndex = this.richTextBoxFormat.SelectionStart + this.richTextBoxFormat.SelectionLength;
    string str2 = startIndex < this.richTextBoxFormat.Text.Length ? this.richTextBoxFormat.Text.Substring(startIndex, this.richTextBoxFormat.Text.Length - startIndex) : string.Empty;
    this.richTextBoxFormat.Text = $"{this.richTextBoxFormat.Text.Substring(0, this.richTextBoxFormat.SelectionStart)}[{str1}]{str2}";
  }

  private void toolStripButtonAddRegister_Click(object sender, EventArgs e)
  {
    this.ultraGridCounters.DisplayLayout.Bands[0].AddNew();
  }

  private void toolStripButtonDeleteRegister_Click(object sender, EventArgs e)
  {
    this.ultraGridCounters.DeleteSelectedRows(true);
  }

  private void ultraGridCounters_AfterRowActivate(object sender, EventArgs e)
  {
    this.richTextBoxFormat.Text = this.ultraGridCounters.ActiveRow.Cells["format"].Text;
    this.numericUpDownValue.Value = (Decimal) (this.ultraGridCounters.ActiveRow.Cells["countervalue"].Text != string.Empty ? (int) this.ultraGridCounters.ActiveRow.Cells["countervalue"].Value : 1);
    this.numericUpDownMax.Value = (Decimal) (this.ultraGridCounters.ActiveRow.Cells["maxvalue"].Text != string.Empty ? (int) this.ultraGridCounters.ActiveRow.Cells["maxvalue"].Value : 1);
  }

  private void richTextBoxFormat_TextChanged(object sender, EventArgs e)
  {
    UltraGridRow activeRow = this.ultraGridCounters.ActiveRow;
    if (activeRow == null)
      return;
    activeRow.Cells["format"].Value = (object) this.richTextBoxFormat.Text;
    activeRow.Update();
  }

  private void numericUpDownValue_ValueChanged(object sender, EventArgs e)
  {
    UltraGridRow activeRow = this.ultraGridCounters.ActiveRow;
    if (activeRow == null)
      return;
    activeRow.Cells["countervalue"].Value = (object) this.numericUpDownValue.Value;
    activeRow.Update();
  }

  private void toolStripButtonSave_Click(object sender, EventArgs e)
  {
    this.numericUpDownMax_ValueChanged(sender, e);
    this.numericUpDownValue_ValueChanged(sender, e);
    if (!this._serialnumbers.HasChanges())
      return;
    new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).SaveData(this._serialnumbers.GetChanges(), "SELECT * FROM tppgssccformats");
    this._serialnumbers.AcceptChanges();
  }

  private void numericUpDownMax_ValueChanged(object sender, EventArgs e)
  {
    UltraGridRow activeRow = this.ultraGridCounters.ActiveRow;
    if (activeRow == null)
      return;
    activeRow.Cells["maxvalue"].Value = (object) this.numericUpDownMax.Value;
    activeRow.Update();
  }

  private void buttonTest_Click(object sender, EventArgs e)
  {
    try
    {
      int num = (int) MessageBox.Show("SSCC: " + new BarcodeTools().TestBarCode(this.richTextBoxFormat.Text, (int) this.numericUpDownValue.Value));
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message);
    }
  }

  private void numericUpDownValue_Leave(object sender, EventArgs e)
  {
  }

  private void numericUpDownMax_Leave(object sender, EventArgs e)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormSsccCounters));
    this.splitContainer1 = new SplitContainer();
    this.ultraGridCounters = new UltraGrid();
    this.toolStrip1 = new ToolStrip();
    this.toolStripButtonAddRegister = new ToolStripButton();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.toolStripButtonSave = new ToolStripButton();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this.toolStripButtonDeleteRegister = new ToolStripButton();
    this.numericUpDownValue = new NumericUpDown();
    this.label1 = new Label();
    this.groupBox1 = new GroupBox();
    this.buttonTest = new Button();
    this.richTextBoxFormat = new RichTextBox();
    this.contextMenuStripVariables = new ContextMenuStrip(this.components);
    this.counterToolStripMenuItem = new ToolStripMenuItem();
    this.numericUpDownMax = new NumericUpDown();
    this.label2 = new Label();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    ((ISupportInitialize) this.ultraGridCounters).BeginInit();
    this.toolStrip1.SuspendLayout();
    this.numericUpDownValue.BeginInit();
    this.groupBox1.SuspendLayout();
    this.contextMenuStripVariables.SuspendLayout();
    this.numericUpDownMax.BeginInit();
    this.SuspendLayout();
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.Location = new Point(0, 0);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.ultraGridCounters);
    this.splitContainer1.Panel1.Controls.Add((Control) this.toolStrip1);
    this.splitContainer1.Panel2.Controls.Add((Control) this.numericUpDownMax);
    this.splitContainer1.Panel2.Controls.Add((Control) this.label2);
    this.splitContainer1.Panel2.Controls.Add((Control) this.numericUpDownValue);
    this.splitContainer1.Panel2.Controls.Add((Control) this.label1);
    this.splitContainer1.Panel2.Controls.Add((Control) this.groupBox1);
    this.splitContainer1.Size = new Size(674, 405);
    this.splitContainer1.SplitterDistance = 223;
    this.splitContainer1.TabIndex = 0;
    appearance1.BackColor = SystemColors.Window;
    appearance1.BorderColor = SystemColors.InactiveCaption;
    this.ultraGridCounters.DisplayLayout.Appearance = (AppearanceBase) appearance1;
    this.ultraGridCounters.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
    this.ultraGridCounters.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridCounters.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
    appearance2.BackColor = SystemColors.ActiveBorder;
    appearance2.BackColor2 = SystemColors.ControlDark;
    appearance2.BackGradientStyle = GradientStyle.Vertical;
    appearance2.BorderColor = SystemColors.Window;
    this.ultraGridCounters.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance2;
    appearance3.ForeColor = SystemColors.GrayText;
    this.ultraGridCounters.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance3;
    this.ultraGridCounters.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridCounters.DisplayLayout.GroupByBox.Hidden = true;
    appearance4.BackColor = SystemColors.ControlLightLight;
    appearance4.BackColor2 = SystemColors.Control;
    appearance4.BackGradientStyle = GradientStyle.Horizontal;
    appearance4.ForeColor = SystemColors.GrayText;
    this.ultraGridCounters.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance4;
    this.ultraGridCounters.DisplayLayout.MaxColScrollRegions = 1;
    this.ultraGridCounters.DisplayLayout.MaxRowScrollRegions = 1;
    appearance5.BackColor = SystemColors.Window;
    appearance5.ForeColor = SystemColors.ControlText;
    this.ultraGridCounters.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance5;
    appearance6.BackColor = SystemColors.Highlight;
    appearance6.ForeColor = SystemColors.HighlightText;
    this.ultraGridCounters.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance6;
    this.ultraGridCounters.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
    this.ultraGridCounters.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
    this.ultraGridCounters.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
    this.ultraGridCounters.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
    this.ultraGridCounters.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
    appearance7.BackColor = SystemColors.Window;
    this.ultraGridCounters.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance7;
    appearance8.BorderColor = Color.Silver;
    appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
    this.ultraGridCounters.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance8;
    this.ultraGridCounters.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
    this.ultraGridCounters.DisplayLayout.Override.CellPadding = 0;
    appearance9.BackColor = SystemColors.Control;
    appearance9.BackColor2 = SystemColors.ControlDark;
    appearance9.BackGradientAlignment = GradientAlignment.Element;
    appearance9.BackGradientStyle = GradientStyle.Horizontal;
    appearance9.BorderColor = SystemColors.Window;
    this.ultraGridCounters.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance9;
    appearance10.TextHAlignAsString = "Left";
    this.ultraGridCounters.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance10;
    this.ultraGridCounters.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
    this.ultraGridCounters.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
    appearance11.BackColor = SystemColors.Window;
    appearance11.BorderColor = Color.Silver;
    this.ultraGridCounters.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance11;
    this.ultraGridCounters.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
    appearance12.BackColor = SystemColors.ControlLight;
    this.ultraGridCounters.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance12;
    this.ultraGridCounters.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
    this.ultraGridCounters.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
    this.ultraGridCounters.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
    this.ultraGridCounters.Dock = DockStyle.Fill;
    this.ultraGridCounters.Location = new Point(0, 27);
    this.ultraGridCounters.Name = "ultraGridCounters";
    this.ultraGridCounters.Size = new Size(223, 378);
    this.ultraGridCounters.TabIndex = 4;
    this.ultraGridCounters.Text = "ultraGrid2";
    this.ultraGridCounters.AfterRowActivate += new EventHandler(this.ultraGridCounters_AfterRowActivate);
    this.toolStrip1.ImageScalingSize = new Size(20, 20);
    this.toolStrip1.Items.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this.toolStripButtonAddRegister,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.toolStripButtonSave,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this.toolStripButtonDeleteRegister
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(223, 27);
    this.toolStrip1.TabIndex = 1;
    this.toolStrip1.Text = "toolStrip1";
    this.toolStripButtonAddRegister.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonAddRegister.Image = (Image) PCSYS_PLC_Server.Properties.Resources.document_add;
    this.toolStripButtonAddRegister.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonAddRegister.Name = "toolStripButtonAddRegister";
    this.toolStripButtonAddRegister.Size = new Size(24, 24);
    this.toolStripButtonAddRegister.Text = "toolStripButton1";
    this.toolStripButtonAddRegister.ToolTipText = "Add Register";
    this.toolStripButtonAddRegister.Click += new EventHandler(this.toolStripButtonAddRegister_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(6, 27);
    this.toolStripButtonSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonSave.Image = (Image) componentResourceManager.GetObject("toolStripButtonSave.Image");
    this.toolStripButtonSave.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonSave.Name = "toolStripButtonSave";
    this.toolStripButtonSave.Size = new Size(24, 24);
    this.toolStripButtonSave.Text = "toolStripButton1";
    this.toolStripButtonSave.ToolTipText = "Save";
    this.toolStripButtonSave.Click += new EventHandler(this.toolStripButtonSave_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    this.toolStripSeparator2.Size = new Size(6, 27);
    this.toolStripButtonDeleteRegister.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonDeleteRegister.Image = (Image) PCSYS_PLC_Server.Properties.Resources.document_delete;
    this.toolStripButtonDeleteRegister.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonDeleteRegister.Name = "toolStripButtonDeleteRegister";
    this.toolStripButtonDeleteRegister.Size = new Size(24, 24);
    this.toolStripButtonDeleteRegister.Text = "toolStripButton1";
    this.toolStripButtonDeleteRegister.ToolTipText = "Delete register";
    this.toolStripButtonDeleteRegister.Click += new EventHandler(this.toolStripButtonDeleteRegister_Click);
    this.numericUpDownValue.Location = new Point(92, 164);
    this.numericUpDownValue.Maximum = new Decimal(new int[4]
    {
      1410065407,
      2,
      0,
      0
    });
    this.numericUpDownValue.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numericUpDownValue.Name = "numericUpDownValue";
    this.numericUpDownValue.Size = new Size(141, 20);
    this.numericUpDownValue.TabIndex = 2;
    this.numericUpDownValue.TextAlign = HorizontalAlignment.Right;
    this.numericUpDownValue.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numericUpDownValue.ValueChanged += new EventHandler(this.numericUpDownValue_ValueChanged);
    this.numericUpDownValue.Leave += new EventHandler(this.numericUpDownValue_Leave);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(12, 166);
    this.label1.Name = "label1";
    this.label1.Size = new Size(74, 13);
    this.label1.TabIndex = 1;
    this.label1.Text = "Counter Value";
    this.groupBox1.Controls.Add((Control) this.buttonTest);
    this.groupBox1.Controls.Add((Control) this.richTextBoxFormat);
    this.groupBox1.Dock = DockStyle.Top;
    this.groupBox1.Location = new Point(0, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(447, 149);
    this.groupBox1.TabIndex = 0;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Format";
    this.buttonTest.Location = new Point(359, 111);
    this.buttonTest.Name = "buttonTest";
    this.buttonTest.Size = new Size(75, 23);
    this.buttonTest.TabIndex = 2;
    this.buttonTest.Text = "Test";
    this.buttonTest.UseVisualStyleBackColor = true;
    this.buttonTest.Click += new EventHandler(this.buttonTest_Click);
    this.richTextBoxFormat.ContextMenuStrip = this.contextMenuStripVariables;
    this.richTextBoxFormat.Location = new Point(6, 19);
    this.richTextBoxFormat.Name = "richTextBoxFormat";
    this.richTextBoxFormat.Size = new Size(428, 86);
    this.richTextBoxFormat.TabIndex = 1;
    this.richTextBoxFormat.Text = "";
    this.richTextBoxFormat.TextChanged += new EventHandler(this.richTextBoxFormat_TextChanged);
    this.contextMenuStripVariables.ImageScalingSize = new Size(20, 20);
    this.contextMenuStripVariables.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.counterToolStripMenuItem
    });
    this.contextMenuStripVariables.Name = "contextMenuStripVariables";
    this.contextMenuStripVariables.Size = new Size(118, 26);
    this.counterToolStripMenuItem.Name = "counterToolStripMenuItem";
    this.counterToolStripMenuItem.Size = new Size(117, 22);
    this.counterToolStripMenuItem.Text = "Counter";
    this.counterToolStripMenuItem.Click += new EventHandler(this.eeeToolStripMenuItem_Click);
    this.numericUpDownMax.Location = new Point(92, 190);
    this.numericUpDownMax.Maximum = new Decimal(new int[4]
    {
      1410065407,
      2,
      0,
      0
    });
    this.numericUpDownMax.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numericUpDownMax.Name = "numericUpDownMax";
    this.numericUpDownMax.Size = new Size(141, 20);
    this.numericUpDownMax.TabIndex = 4;
    this.numericUpDownMax.TextAlign = HorizontalAlignment.Right;
    this.numericUpDownMax.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numericUpDownMax.ValueChanged += new EventHandler(this.numericUpDownMax_ValueChanged);
    this.numericUpDownMax.Leave += new EventHandler(this.numericUpDownMax_Leave);
    this.label2.AutoSize = true;
    this.label2.Location = new Point(12, 192 /*0xC0*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(60, 13);
    this.label2.TabIndex = 3;
    this.label2.Text = "Max. Value";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(674, 405);
    this.Controls.Add((Control) this.splitContainer1);
    this.Name = "FormSsccCounters";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "SSCC Counters";
    this.Load += new EventHandler(this.FormSsccCounters_Load);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel1.PerformLayout();
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.Panel2.PerformLayout();
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    ((ISupportInitialize) this.ultraGridCounters).EndInit();
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.numericUpDownValue.EndInit();
    this.groupBox1.ResumeLayout(false);
    this.contextMenuStripVariables.ResumeLayout(false);
    this.numericUpDownMax.EndInit();
    this.ResumeLayout(false);
  }
}
