// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormPrinterCodes
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PCSYS_Plc_AppSettings;
using PCSYS_PPG_Info;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server;

public class FormPrinterCodes : Form
{
  private DataSet _codes;
  private readonly int _printerid;
  private readonly string _tablename;
  private IContainer components = (IContainer) null;
  private Button buttonCancel;
  private Button buttonOk;
  private ToolStrip toolStrip1;
  private ToolStripButton toolStripButtonNew;
  private ToolStripButton toolStripButtonSave;
  private ToolStripButton toolStripButtonDelete;
  private UltraGrid ultraGridPrinterCodes;

  public FormPrinterCodes(int printerId, string tableName)
  {
    this._printerid = printerId;
    this._tablename = tableName;
    this.InitializeComponent();
  }

  private void FormPrinterCodes_Load(object sender, EventArgs e)
  {
    this._codes = new DataTools().GetPrintCodes(this._printerid, this._tablename, SettingsTools.MainSettings.DataLinks);
    this.ultraGridPrinterCodes.DataSource = (object) this._codes;
    this.ultraGridPrinterCodes.DisplayLayout.Bands[0].Columns[0].Hidden = true;
    this.ultraGridPrinterCodes.DisplayLayout.Bands[0].Columns[1].Hidden = true;
    this.ultraGridPrinterCodes.DisplayLayout.Bands[0].Columns[2].ValueList = (IValueList) this.GetValues();
    this.ultraGridPrinterCodes.DisplayLayout.Bands[0].Columns[3].Header.Caption = "Function";
    this.ultraGridPrinterCodes.DisplayLayout.Bands[0].Columns[3].Header.Caption = "Value";
    this.ultraGridPrinterCodes.DisplayLayout.Bands[0].Columns[4].Hidden = true;
  }

  private ValueList GetValues()
  {
    ValueList values = new ValueList();
    foreach (object dataValue in Enum.GetValues(typeof (PrinterCode)))
      values.ValueListItems.Add(dataValue);
    return values;
  }

  private void toolStripButtonNew_Click(object sender, EventArgs e)
  {
    this.ultraGridPrinterCodes.DisplayLayout.Bands[0].AddNew();
    this.ultraGridPrinterCodes.ActiveRow.Cells[1].Value = (object) this._printerid;
  }

  private void toolStripButtonSave_Click(object sender, EventArgs e) => this.Save(false);

  private void Save(bool askBeforeSave)
  {
    this.ultraGridPrinterCodes.ActiveRow?.Update();
    if (!this._codes.HasChanges() || askBeforeSave && MessageBox.Show("Save changes?", nameof (Save), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
      return;
    DataTools.SavePrintCodes(this._codes, this._tablename, SettingsTools.MainSettings.DataLinks);
    this._codes.AcceptChanges();
  }

  private void buttonOk_Click(object sender, EventArgs e) => this.Save(false);

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormPrinterCodes));
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
    this.buttonCancel = new Button();
    this.buttonOk = new Button();
    this.toolStrip1 = new ToolStrip();
    this.toolStripButtonNew = new ToolStripButton();
    this.toolStripButtonSave = new ToolStripButton();
    this.toolStripButtonDelete = new ToolStripButton();
    this.ultraGridPrinterCodes = new UltraGrid();
    this.toolStrip1.SuspendLayout();
    ((ISupportInitialize) this.ultraGridPrinterCodes).BeginInit();
    this.SuspendLayout();
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(259, (int) byte.MaxValue);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(75, 30);
    this.buttonCancel.TabIndex = 4;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.buttonOk.DialogResult = DialogResult.OK;
    this.buttonOk.Location = new Point(177, (int) byte.MaxValue);
    this.buttonOk.Name = "buttonOk";
    this.buttonOk.Size = new Size(75, 30);
    this.buttonOk.TabIndex = 5;
    this.buttonOk.Text = "&Save";
    this.buttonOk.UseVisualStyleBackColor = true;
    this.buttonOk.Click += new EventHandler(this.buttonOk_Click);
    this.toolStrip1.ImageScalingSize = new Size(20, 20);
    this.toolStrip1.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.toolStripButtonNew,
      (ToolStripItem) this.toolStripButtonSave,
      (ToolStripItem) this.toolStripButtonDelete
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(345, 27);
    this.toolStrip1.TabIndex = 6;
    this.toolStrip1.Text = "toolStrip1";
    this.toolStripButtonNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonNew.Image = (Image) componentResourceManager.GetObject("toolStripButtonNew.Image");
    this.toolStripButtonNew.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonNew.Name = "toolStripButtonNew";
    this.toolStripButtonNew.Size = new Size(24, 24);
    this.toolStripButtonNew.Text = "Add new";
    this.toolStripButtonNew.Click += new EventHandler(this.toolStripButtonNew_Click);
    this.toolStripButtonSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonSave.Image = (Image) componentResourceManager.GetObject("toolStripButtonSave.Image");
    this.toolStripButtonSave.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonSave.Name = "toolStripButtonSave";
    this.toolStripButtonSave.Size = new Size(24, 24);
    this.toolStripButtonSave.Text = "Save";
    this.toolStripButtonSave.Click += new EventHandler(this.toolStripButtonSave_Click);
    this.toolStripButtonDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonDelete.Image = (Image) componentResourceManager.GetObject("toolStripButtonDelete.Image");
    this.toolStripButtonDelete.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonDelete.Name = "toolStripButtonDelete";
    this.toolStripButtonDelete.Size = new Size(24, 24);
    this.toolStripButtonDelete.Text = "toolStripButton1";
    this.toolStripButtonDelete.ToolTipText = "Delete";
    appearance1.BackColor = SystemColors.Window;
    appearance1.BorderColor = SystemColors.InactiveCaption;
    this.ultraGridPrinterCodes.DisplayLayout.Appearance = (AppearanceBase) appearance1;
    this.ultraGridPrinterCodes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
    this.ultraGridPrinterCodes.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridPrinterCodes.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
    appearance2.BackColor = SystemColors.ActiveBorder;
    appearance2.BackColor2 = SystemColors.ControlDark;
    appearance2.BackGradientStyle = GradientStyle.Vertical;
    appearance2.BorderColor = SystemColors.Window;
    this.ultraGridPrinterCodes.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance2;
    appearance3.ForeColor = SystemColors.GrayText;
    this.ultraGridPrinterCodes.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance3;
    this.ultraGridPrinterCodes.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridPrinterCodes.DisplayLayout.GroupByBox.Hidden = true;
    appearance4.BackColor = SystemColors.ControlLightLight;
    appearance4.BackColor2 = SystemColors.Control;
    appearance4.BackGradientStyle = GradientStyle.Horizontal;
    appearance4.ForeColor = SystemColors.GrayText;
    this.ultraGridPrinterCodes.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance4;
    this.ultraGridPrinterCodes.DisplayLayout.MaxColScrollRegions = 1;
    this.ultraGridPrinterCodes.DisplayLayout.MaxRowScrollRegions = 1;
    appearance5.BackColor = SystemColors.Window;
    appearance5.ForeColor = SystemColors.ControlText;
    this.ultraGridPrinterCodes.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance5;
    appearance6.BackColor = SystemColors.Highlight;
    appearance6.ForeColor = SystemColors.HighlightText;
    this.ultraGridPrinterCodes.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance6;
    this.ultraGridPrinterCodes.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
    this.ultraGridPrinterCodes.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
    this.ultraGridPrinterCodes.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
    this.ultraGridPrinterCodes.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
    this.ultraGridPrinterCodes.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
    appearance7.BackColor = SystemColors.Window;
    this.ultraGridPrinterCodes.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance7;
    appearance8.BorderColor = Color.Silver;
    appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
    this.ultraGridPrinterCodes.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance8;
    this.ultraGridPrinterCodes.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
    this.ultraGridPrinterCodes.DisplayLayout.Override.CellPadding = 0;
    appearance9.BackColor = SystemColors.Control;
    appearance9.BackColor2 = SystemColors.ControlDark;
    appearance9.BackGradientAlignment = GradientAlignment.Element;
    appearance9.BackGradientStyle = GradientStyle.Horizontal;
    appearance9.BorderColor = SystemColors.Window;
    this.ultraGridPrinterCodes.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance9;
    appearance10.TextHAlignAsString = "Left";
    this.ultraGridPrinterCodes.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance10;
    this.ultraGridPrinterCodes.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
    this.ultraGridPrinterCodes.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
    appearance11.BackColor = SystemColors.Window;
    appearance11.BorderColor = Color.Silver;
    this.ultraGridPrinterCodes.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance11;
    this.ultraGridPrinterCodes.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
    appearance12.BackColor = SystemColors.ControlLight;
    this.ultraGridPrinterCodes.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance12;
    this.ultraGridPrinterCodes.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
    this.ultraGridPrinterCodes.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
    this.ultraGridPrinterCodes.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
    this.ultraGridPrinterCodes.Location = new Point(0, 31 /*0x1F*/);
    this.ultraGridPrinterCodes.Margin = new Padding(4);
    this.ultraGridPrinterCodes.Name = "ultraGridPrinterCodes";
    this.ultraGridPrinterCodes.Size = new Size(345, 217);
    this.ultraGridPrinterCodes.TabIndex = 7;
    this.ultraGridPrinterCodes.Text = "ultraGrid2";
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(345, 297);
    this.Controls.Add((Control) this.ultraGridPrinterCodes);
    this.Controls.Add((Control) this.toolStrip1);
    this.Controls.Add((Control) this.buttonOk);
    this.Controls.Add((Control) this.buttonCancel);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = "FormPrinterCodes";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Printer Codes";
    this.Load += new EventHandler(this.FormPrinterCodes_Load);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    ((ISupportInitialize) this.ultraGridPrinterCodes).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
