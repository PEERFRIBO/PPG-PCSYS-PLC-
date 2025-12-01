// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormPrinters
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PCSYS_Plc_AppSettings;
using PCSYS_PPG_Info;
using PCSYS_PPG_LPS_ProxyConnector;
using PCSYSLpsTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server;

public class FormPrinters : Form
{
  private DataSet _printers;
  private IContainer components = (IContainer) null;
  private UltraGrid ultraGridPrinters;
  private ToolStrip toolStrip1;
  private ToolStripButton toolStripButtonNew;
  private ToolStripButton toolStripButtonSave;
  private ToolStripButton toolStripButtonDelete;
  private Button buttonOk;
  private Button buttonCancel;
  private ContextMenuStrip contextMenuStripCodes;
  private ToolStripMenuItem printerCodesToolStripMenuItem;
  private Button buttonPrinterSettings;
  private Button button1;

  public FormPrinters() => this.InitializeComponent();

  private void FormPrinters_Load(object sender, EventArgs e)
  {
    this._printers = new DataTools().GetPrinters(SettingsTools.MainSettings.DataLinks);
    this.ultraGridPrinters.DataSource = (object) this._printers.Tables[0];
    this.ultraGridPrinters.DisplayLayout.Bands[0].Columns[0].Hidden = true;
    this.ultraGridPrinters.DisplayLayout.Bands[0].Columns[1].Header.Caption = "Printer Family";
    this.ultraGridPrinters.DisplayLayout.Bands[0].Columns[2].Header.Caption = "Initializer Printer";
    this.ultraGridPrinters.DisplayLayout.Bands[0].Columns[1].ValueList = (IValueList) FormPrinters.GetPrinterFamilies();
    this.ultraGridPrinters.DisplayLayout.Bands[0].Columns[2].ValueList = (IValueList) this.GetPrinters();
  }

  private ValueList GetPrinters()
  {
    DataTable table = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).GetData("SELECT printerid,itemname FROM tprinters ORDER BY itemname").Tables[0];
    ValueList printers = new ValueList();
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      printers.ValueListItems.Add((object) int.Parse(row["printerid"].ToString()), row["itemname"].ToString());
    return printers;
  }

  private static ValueList GetPrinterFamilies()
  {
    ValueList printerFamilies = new ValueList();
    foreach (object dataValue in Enum.GetValues(typeof (ProfileTools.PrinterFamily)))
      printerFamilies.ValueListItems.Add(dataValue);
    return printerFamilies;
  }

  private void toolStripButtonNew_Click(object sender, EventArgs e)
  {
    this.ultraGridPrinters.DisplayLayout.Bands[0].AddNew();
  }

  private void toolStripButtonDelete_Click(object sender, EventArgs e)
  {
    this.ultraGridPrinters.DeleteSelectedRows(true);
  }

  private void toolStripButtonSave_Click(object sender, EventArgs e) => this.Save(false);

  private void Save(bool askBeforeSaving)
  {
    this.ultraGridPrinters.ActiveRow?.Update();
    if (this._printers.HasChanges() && askBeforeSaving && MessageBox.Show("Save changes?", nameof (Save), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
      return;
    new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).SaveData(this._printers, "SELECT * FROM tppgprinters");
  }

  private void buttonOk_Click(object sender, EventArgs e) => this.Save(false);

  private void printerCodesToolStripMenuItem_Click(object sender, EventArgs e)
  {
    object obj = this.ultraGridPrinters.ActiveRow?.Cells["printerfamilyid"].Value;
    if (obj == null)
      return;
    using (FormPrinterCodes formPrinterCodes = new FormPrinterCodes(int.Parse(obj.ToString()), "tppgprintercodes"))
    {
      int num = (int) formPrinterCodes.ShowDialog();
    }
  }

  private void ultraGridPrinters_InitializeLayout(object sender, InitializeLayoutEventArgs e)
  {
  }

  private void buttonPrinterSettings_Click(object sender, EventArgs e)
  {
    using (FormPrinterSettings formPrinterSettings = new FormPrinterSettings())
    {
      int num = (int) formPrinterSettings.ShowDialog();
    }
  }

  private void button1_Click(object sender, EventArgs e)
  {
    using (FormLabelVariabelsMapping variabelsMapping = new FormLabelVariabelsMapping())
    {
      int num = (int) variabelsMapping.ShowDialog();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormPrinters));
    this.ultraGridPrinters = new UltraGrid();
    this.contextMenuStripCodes = new ContextMenuStrip(this.components);
    this.printerCodesToolStripMenuItem = new ToolStripMenuItem();
    this.toolStrip1 = new ToolStrip();
    this.toolStripButtonNew = new ToolStripButton();
    this.toolStripButtonSave = new ToolStripButton();
    this.toolStripButtonDelete = new ToolStripButton();
    this.buttonOk = new Button();
    this.buttonCancel = new Button();
    this.buttonPrinterSettings = new Button();
    this.button1 = new Button();
    ((ISupportInitialize) this.ultraGridPrinters).BeginInit();
    this.contextMenuStripCodes.SuspendLayout();
    this.toolStrip1.SuspendLayout();
    this.SuspendLayout();
    this.ultraGridPrinters.ContextMenuStrip = this.contextMenuStripCodes;
    appearance1.BackColor = SystemColors.Window;
    appearance1.BorderColor = SystemColors.InactiveCaption;
    this.ultraGridPrinters.DisplayLayout.Appearance = (AppearanceBase) appearance1;
    this.ultraGridPrinters.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
    this.ultraGridPrinters.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridPrinters.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
    appearance2.BackColor = SystemColors.ActiveBorder;
    appearance2.BackColor2 = SystemColors.ControlDark;
    appearance2.BackGradientStyle = GradientStyle.Vertical;
    appearance2.BorderColor = SystemColors.Window;
    this.ultraGridPrinters.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance2;
    appearance3.ForeColor = SystemColors.GrayText;
    this.ultraGridPrinters.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance3;
    this.ultraGridPrinters.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridPrinters.DisplayLayout.GroupByBox.Hidden = true;
    appearance4.BackColor = SystemColors.ControlLightLight;
    appearance4.BackColor2 = SystemColors.Control;
    appearance4.BackGradientStyle = GradientStyle.Horizontal;
    appearance4.ForeColor = SystemColors.GrayText;
    this.ultraGridPrinters.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance4;
    this.ultraGridPrinters.DisplayLayout.MaxColScrollRegions = 1;
    this.ultraGridPrinters.DisplayLayout.MaxRowScrollRegions = 1;
    appearance5.BackColor = SystemColors.Window;
    appearance5.ForeColor = SystemColors.ControlText;
    this.ultraGridPrinters.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance5;
    appearance6.BackColor = SystemColors.Highlight;
    appearance6.ForeColor = SystemColors.HighlightText;
    this.ultraGridPrinters.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance6;
    this.ultraGridPrinters.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
    this.ultraGridPrinters.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
    this.ultraGridPrinters.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
    this.ultraGridPrinters.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
    this.ultraGridPrinters.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
    appearance7.BackColor = SystemColors.Window;
    this.ultraGridPrinters.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance7;
    appearance8.BorderColor = Color.Silver;
    appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
    this.ultraGridPrinters.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance8;
    this.ultraGridPrinters.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
    this.ultraGridPrinters.DisplayLayout.Override.CellPadding = 0;
    appearance9.BackColor = SystemColors.Control;
    appearance9.BackColor2 = SystemColors.ControlDark;
    appearance9.BackGradientAlignment = GradientAlignment.Element;
    appearance9.BackGradientStyle = GradientStyle.Horizontal;
    appearance9.BorderColor = SystemColors.Window;
    this.ultraGridPrinters.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance9;
    appearance10.TextHAlignAsString = "Left";
    this.ultraGridPrinters.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance10;
    this.ultraGridPrinters.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
    this.ultraGridPrinters.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
    appearance11.BackColor = SystemColors.Window;
    appearance11.BorderColor = Color.Silver;
    this.ultraGridPrinters.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance11;
    this.ultraGridPrinters.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
    appearance12.BackColor = SystemColors.ControlLight;
    this.ultraGridPrinters.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance12;
    this.ultraGridPrinters.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
    this.ultraGridPrinters.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
    this.ultraGridPrinters.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
    this.ultraGridPrinters.Location = new Point(0, 30);
    this.ultraGridPrinters.Name = "ultraGridPrinters";
    this.ultraGridPrinters.Size = new Size(396, 277);
    this.ultraGridPrinters.TabIndex = 11;
    this.ultraGridPrinters.Text = "ultraGrid2";
    this.ultraGridPrinters.InitializeLayout += new InitializeLayoutEventHandler(this.ultraGridPrinters_InitializeLayout);
    this.contextMenuStripCodes.ImageScalingSize = new Size(20, 20);
    this.contextMenuStripCodes.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.printerCodesToolStripMenuItem
    });
    this.contextMenuStripCodes.Name = "contextMenuStripCodes";
    this.contextMenuStripCodes.Size = new Size(146, 26);
    this.printerCodesToolStripMenuItem.Name = "printerCodesToolStripMenuItem";
    this.printerCodesToolStripMenuItem.Size = new Size(145, 22);
    this.printerCodesToolStripMenuItem.Text = "Printer Codes";
    this.printerCodesToolStripMenuItem.Click += new EventHandler(this.printerCodesToolStripMenuItem_Click);
    this.toolStrip1.ImageScalingSize = new Size(20, 20);
    this.toolStrip1.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.toolStripButtonNew,
      (ToolStripItem) this.toolStripButtonSave,
      (ToolStripItem) this.toolStripButtonDelete
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(396, 27);
    this.toolStrip1.TabIndex = 10;
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
    this.toolStripButtonDelete.Click += new EventHandler(this.toolStripButtonDelete_Click);
    this.buttonOk.DialogResult = DialogResult.OK;
    this.buttonOk.Location = new Point(269, 312);
    this.buttonOk.Margin = new Padding(2, 2, 2, 2);
    this.buttonOk.Name = "buttonOk";
    this.buttonOk.Size = new Size(56, 24);
    this.buttonOk.TabIndex = 9;
    this.buttonOk.Text = "&Save";
    this.buttonOk.UseVisualStyleBackColor = true;
    this.buttonOk.Click += new EventHandler(this.buttonOk_Click);
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(329, 312);
    this.buttonCancel.Margin = new Padding(2, 2, 2, 2);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(56, 24);
    this.buttonCancel.TabIndex = 8;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.buttonPrinterSettings.Location = new Point(9, 312);
    this.buttonPrinterSettings.Margin = new Padding(2, 2, 2, 2);
    this.buttonPrinterSettings.Name = "buttonPrinterSettings";
    this.buttonPrinterSettings.Size = new Size(88, 24);
    this.buttonPrinterSettings.TabIndex = 12;
    this.buttonPrinterSettings.Text = "Printer Settings";
    this.buttonPrinterSettings.UseVisualStyleBackColor = true;
    this.buttonPrinterSettings.Click += new EventHandler(this.buttonPrinterSettings_Click);
    this.button1.Location = new Point(102, 312);
    this.button1.Margin = new Padding(2, 2, 2, 2);
    this.button1.Name = "button1";
    this.button1.Size = new Size(111, 24);
    this.button1.TabIndex = 13;
    this.button1.Text = "Printer Mappings";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(396, 347);
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.buttonPrinterSettings);
    this.Controls.Add((Control) this.ultraGridPrinters);
    this.Controls.Add((Control) this.toolStrip1);
    this.Controls.Add((Control) this.buttonOk);
    this.Controls.Add((Control) this.buttonCancel);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = "FormPrinters";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Initialization Printers";
    this.Load += new EventHandler(this.FormPrinters_Load);
    ((ISupportInitialize) this.ultraGridPrinters).EndInit();
    this.contextMenuStripCodes.ResumeLayout(false);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
