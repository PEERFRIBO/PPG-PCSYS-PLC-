// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormLabelVariabelsMapping
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
namespace PCSYS_PLC_Server;

public class FormLabelVariabelsMapping : Form
{
  private DataSet _mappings = new DataSet();
  private IContainer components = (IContainer) null;
  private UltraGrid ultraGridFields;
  private ToolStrip toolStrip1;
  private ToolStripButton toolStripButtonNew;
  private ToolStripButton toolStripButtonSave;
  private ToolStripButton toolStripButtonDelete;
  private Button buttonOk;
  private Button buttonCancel;

  public FormLabelVariabelsMapping() => this.InitializeComponent();

  private void FormLabelVariabelsMapping_Load(object sender, EventArgs e)
  {
    this._mappings = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).GetData("SELECT * FROM tppgmappings ORDER BY fieldname");
    this.ultraGridFields.DataSource = (object) this._mappings.Tables[0];
    this.ultraGridFields.DisplayLayout.Bands[0].Columns[0].Hidden = true;
    this.ultraGridFields.DisplayLayout.Bands[0].Columns[1].Header.Caption = "Field Name";
    this.ultraGridFields.DisplayLayout.Bands[0].Columns[2].Header.Caption = "Variable Name";
    this.ultraGridFields.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.VisibleRows);
  }

  private void toolStripButtonNew_Click(object sender, EventArgs e)
  {
    this.ultraGridFields.DisplayLayout.Bands[0].AddNew();
  }

  private void ultraGridFields_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
  {
  }

  private void buttonOk_Click(object sender, EventArgs e)
  {
    if (!this._mappings.HasChanges())
      return;
    new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).SaveData(this._mappings, "SELECT * FROM tppgmappings");
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormLabelVariabelsMapping));
    this.ultraGridFields = new UltraGrid();
    this.toolStrip1 = new ToolStrip();
    this.toolStripButtonNew = new ToolStripButton();
    this.toolStripButtonSave = new ToolStripButton();
    this.toolStripButtonDelete = new ToolStripButton();
    this.buttonOk = new Button();
    this.buttonCancel = new Button();
    ((ISupportInitialize) this.ultraGridFields).BeginInit();
    this.toolStrip1.SuspendLayout();
    this.SuspendLayout();
    appearance1.BackColor = SystemColors.Window;
    appearance1.BorderColor = SystemColors.InactiveCaption;
    this.ultraGridFields.DisplayLayout.Appearance = (AppearanceBase) appearance1;
    this.ultraGridFields.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
    this.ultraGridFields.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridFields.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
    appearance2.BackColor = SystemColors.ActiveBorder;
    appearance2.BackColor2 = SystemColors.ControlDark;
    appearance2.BackGradientStyle = GradientStyle.Vertical;
    appearance2.BorderColor = SystemColors.Window;
    this.ultraGridFields.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance2;
    appearance3.ForeColor = SystemColors.GrayText;
    this.ultraGridFields.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance3;
    this.ultraGridFields.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridFields.DisplayLayout.GroupByBox.Hidden = true;
    appearance4.BackColor = SystemColors.ControlLightLight;
    appearance4.BackColor2 = SystemColors.Control;
    appearance4.BackGradientStyle = GradientStyle.Horizontal;
    appearance4.ForeColor = SystemColors.GrayText;
    this.ultraGridFields.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance4;
    this.ultraGridFields.DisplayLayout.MaxColScrollRegions = 1;
    this.ultraGridFields.DisplayLayout.MaxRowScrollRegions = 1;
    appearance5.BackColor = SystemColors.Window;
    appearance5.ForeColor = SystemColors.ControlText;
    this.ultraGridFields.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance5;
    appearance6.BackColor = SystemColors.Highlight;
    appearance6.ForeColor = SystemColors.HighlightText;
    this.ultraGridFields.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance6;
    this.ultraGridFields.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
    this.ultraGridFields.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
    this.ultraGridFields.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
    this.ultraGridFields.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
    this.ultraGridFields.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
    appearance7.BackColor = SystemColors.Window;
    this.ultraGridFields.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance7;
    appearance8.BorderColor = Color.Silver;
    appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
    this.ultraGridFields.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance8;
    this.ultraGridFields.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
    this.ultraGridFields.DisplayLayout.Override.CellPadding = 0;
    appearance9.BackColor = SystemColors.Control;
    appearance9.BackColor2 = SystemColors.ControlDark;
    appearance9.BackGradientAlignment = GradientAlignment.Element;
    appearance9.BackGradientStyle = GradientStyle.Horizontal;
    appearance9.BorderColor = SystemColors.Window;
    this.ultraGridFields.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance9;
    appearance10.TextHAlignAsString = "Left";
    this.ultraGridFields.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance10;
    this.ultraGridFields.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
    this.ultraGridFields.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
    appearance11.BackColor = SystemColors.Window;
    appearance11.BorderColor = Color.Silver;
    this.ultraGridFields.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance11;
    this.ultraGridFields.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
    appearance12.BackColor = SystemColors.ControlLight;
    this.ultraGridFields.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance12;
    this.ultraGridFields.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
    this.ultraGridFields.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
    this.ultraGridFields.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
    this.ultraGridFields.Dock = DockStyle.Top;
    this.ultraGridFields.Location = new Point(0, 27);
    this.ultraGridFields.Margin = new Padding(4);
    this.ultraGridFields.Name = "ultraGridFields";
    this.ultraGridFields.Size = new Size(532, 341);
    this.ultraGridFields.TabIndex = 15;
    this.ultraGridFields.Text = "ultraGrid2";
    this.ultraGridFields.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(this.ultraGridFields_BeforeRowsDeleted);
    this.toolStrip1.ImageScalingSize = new Size(20, 20);
    this.toolStrip1.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.toolStripButtonNew,
      (ToolStripItem) this.toolStripButtonSave,
      (ToolStripItem) this.toolStripButtonDelete
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(532, 27);
    this.toolStrip1.TabIndex = 14;
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
    this.toolStripButtonDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonDelete.Image = (Image) componentResourceManager.GetObject("toolStripButtonDelete.Image");
    this.toolStripButtonDelete.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonDelete.Name = "toolStripButtonDelete";
    this.toolStripButtonDelete.Size = new Size(24, 24);
    this.toolStripButtonDelete.Text = "toolStripButton1";
    this.toolStripButtonDelete.ToolTipText = "Delete";
    this.buttonOk.DialogResult = DialogResult.OK;
    this.buttonOk.Location = new Point(358, 374);
    this.buttonOk.Margin = new Padding(3, 2, 3, 2);
    this.buttonOk.Name = "buttonOk";
    this.buttonOk.Size = new Size(75, 30);
    this.buttonOk.TabIndex = 13;
    this.buttonOk.Text = "&Save";
    this.buttonOk.UseVisualStyleBackColor = true;
    this.buttonOk.Click += new EventHandler(this.buttonOk_Click);
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(439, 374);
    this.buttonCancel.Margin = new Padding(3, 2, 3, 2);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(75, 30);
    this.buttonCancel.TabIndex = 12;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.buttonOk;
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.ClientSize = new Size(532, 412);
    this.Controls.Add((Control) this.ultraGridFields);
    this.Controls.Add((Control) this.toolStrip1);
    this.Controls.Add((Control) this.buttonOk);
    this.Controls.Add((Control) this.buttonCancel);
    this.Name = "FormLabelVariabelsMapping";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Label Variabels Mapping";
    this.Load += new EventHandler(this.FormLabelVariabelsMapping_Load);
    ((ISupportInitialize) this.ultraGridFields).EndInit();
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
