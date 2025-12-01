// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormPrinterSettings
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
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server;

public class FormPrinterSettings : Form
{
  private DataSet _printers;
  private IContainer components = (IContainer) null;
  private UltraGrid ultraGridPrinters;
  private Button buttonOk;
  private Button buttonCancel;
  private ContextMenuStrip contextMenuStripMenu;
  private ToolStripMenuItem printerCodesToolStripMenuItem;

  public FormPrinterSettings() => this.InitializeComponent();

  private DataSet Printers()
  {
    return new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).GetData("SELECT printerid,itemname FROM tprinters ORDER BY itemname");
  }

  private void FormPrinterSettings_Load(object sender, EventArgs e)
  {
    this._printers = this.Printers();
    this.ultraGridPrinters.DataSource = (object) this._printers.Tables[0];
    UltraGridBand band = this.ultraGridPrinters.DisplayLayout.Bands[0];
    band.Columns[0].Hidden = true;
    band.Columns[1].Header.Caption = "Name";
    band.Override.AllowUpdate = DefaultableBoolean.False;
  }

  private void Save(bool askBeforeSave)
  {
    this.ultraGridPrinters.ActiveRow?.Update();
    if (!this._printers.HasChanges() || askBeforeSave && MessageBox.Show(nameof (Save), "Save changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
      return;
    DataTools.SavePrintCodes(this._printers, "tppgprinterspecificcodes", SettingsTools.MainSettings.DataLinks);
  }

  private void printerCodesToolStripMenuItem_Click(object sender, EventArgs e)
  {
    object obj = this.ultraGridPrinters.ActiveRow?.Cells[0].Value;
    if (obj == null)
      return;
    using (FormPrinterCodes formPrinterCodes = new FormPrinterCodes(int.Parse(obj.ToString()), "tppgprinterspecificcodes"))
    {
      int num = (int) formPrinterCodes.ShowDialog();
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
    this.ultraGridPrinters = new UltraGrid();
    this.buttonOk = new Button();
    this.buttonCancel = new Button();
    this.contextMenuStripMenu = new ContextMenuStrip(this.components);
    this.printerCodesToolStripMenuItem = new ToolStripMenuItem();
    ((ISupportInitialize) this.ultraGridPrinters).BeginInit();
    this.contextMenuStripMenu.SuspendLayout();
    this.SuspendLayout();
    this.ultraGridPrinters.ContextMenuStrip = this.contextMenuStripMenu;
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
    this.ultraGridPrinters.Dock = DockStyle.Top;
    this.ultraGridPrinters.Location = new Point(0, 0);
    this.ultraGridPrinters.Margin = new Padding(4);
    this.ultraGridPrinters.Name = "ultraGridPrinters";
    this.ultraGridPrinters.Size = new Size(537, 384);
    this.ultraGridPrinters.TabIndex = 15;
    this.ultraGridPrinters.Text = "ultraGrid2";
    this.buttonOk.DialogResult = DialogResult.OK;
    this.buttonOk.Location = new Point(359, 390);
    this.buttonOk.Margin = new Padding(3, 2, 3, 2);
    this.buttonOk.Name = "buttonOk";
    this.buttonOk.Size = new Size(75, 30);
    this.buttonOk.TabIndex = 13;
    this.buttonOk.Text = "&Save";
    this.buttonOk.UseVisualStyleBackColor = true;
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(439, 390);
    this.buttonCancel.Margin = new Padding(3, 2, 3, 2);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(75, 30);
    this.buttonCancel.TabIndex = 12;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.contextMenuStripMenu.ImageScalingSize = new Size(20, 20);
    this.contextMenuStripMenu.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.printerCodesToolStripMenuItem
    });
    this.contextMenuStripMenu.Name = "contextMenuStripMenu";
    this.contextMenuStripMenu.Size = new Size(211, 56);
    this.printerCodesToolStripMenuItem.Name = "printerCodesToolStripMenuItem";
    this.printerCodesToolStripMenuItem.Size = new Size(210, 24);
    this.printerCodesToolStripMenuItem.Text = "Printer Codes";
    this.printerCodesToolStripMenuItem.Click += new EventHandler(this.printerCodesToolStripMenuItem_Click);
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(537, 427);
    this.Controls.Add((Control) this.ultraGridPrinters);
    this.Controls.Add((Control) this.buttonOk);
    this.Controls.Add((Control) this.buttonCancel);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = "FormPrinterSettings";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Printer Settings";
    this.Load += new EventHandler(this.FormPrinterSettings_Load);
    ((ISupportInitialize) this.ultraGridPrinters).EndInit();
    this.contextMenuStripMenu.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
