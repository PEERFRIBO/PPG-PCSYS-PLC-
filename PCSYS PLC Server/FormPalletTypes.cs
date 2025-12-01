// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormPalletTypes
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

public class FormPalletTypes : Form
{
  private DataSet _palletlabels;
  private IContainer components = (IContainer) null;
  private UltraGrid ultraGridPalletTypes;
  private ToolStrip toolStrip1;
  private ToolStripButton toolStripButtonAddRegister;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripButton toolStripButtonSave;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripButton toolStripButtonDeleteRegister;
  private Button buttonCancel;
  private Button buttonSave;

  public FormPalletTypes() => this.InitializeComponent();

  private void FormPalletTypes_Load(object sender, EventArgs e)
  {
    this._palletlabels = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).GetData("SELECT * FROM tppgpalletdocuments");
    this.ultraGridPalletTypes.DataSource = (object) this._palletlabels.Tables[0];
    this.ultraGridPalletTypes.DisplayLayout.Bands[0].Columns["id"].Hidden = true;
    this.ultraGridPalletTypes.DisplayLayout.Bands[0].Columns["pallettype"].Header.Caption = "Pallet Type";
    this.ultraGridPalletTypes.DisplayLayout.Bands[0].Columns["documentid"].Header.Caption = "Document";
    this.ultraGridPalletTypes.DisplayLayout.Bands[0].Columns["documentid"].ValueList = (IValueList) this.GetDocuments();
    this.ultraGridPalletTypes.DisplayLayout.Bands[0].Columns["isdefault"].Header.Caption = "Default";
  }

  private ValueList GetDocuments()
  {
    ValueList documents = new ValueList();
    foreach (DataRow row in (InternalDataCollectionBase) new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).GetData("SELECT documentid,itemname FROM tdocuments WHERE documenttype = 1 ORDER BY itemname").Tables[0].Rows)
      documents.ValueListItems.Add((object) int.Parse(row[0].ToString()), row[1].ToString());
    return documents;
  }

  private void toolStripButtonAddRegister_Click(object sender, EventArgs e)
  {
    this.ultraGridPalletTypes.DisplayLayout.Bands[0].AddNew();
  }

  private void Save()
  {
    SettingsTools.SaveSettings(SettingsTools.MainSettings);
    if (!this._palletlabels.HasChanges())
      return;
    if (!this.CheckForDefault())
    {
      int num = (int) MessageBox.Show("Set Default printer before save");
    }
    else
    {
      new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).SaveData(this._palletlabels.GetChanges(), "SELECT * FROM tppgpalletdocuments");
      this._palletlabels.AcceptChanges();
    }
  }

  private void toolStripButtonDeleteRegister_Click(object sender, EventArgs e)
  {
    this.ultraGridPalletTypes.DeleteSelectedRows(true);
  }

  private void toolStripButtonSave_Click(object sender, EventArgs e) => this.Save();

  private void buttonSave_Click(object sender, EventArgs e) => this.Save();

  private bool CheckForDefault()
  {
    foreach (UltraGridRow row in this.ultraGridPalletTypes.Rows)
    {
      if (row.Cells["isdefault"].Text != string.Empty && bool.Parse(row.Cells["isdefault"].Text))
        return true;
    }
    return false;
  }

  private void ultraGridPalletTypes_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
  {
  }

  private void ultraGridPalletTypes_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormPalletTypes));
    this.ultraGridPalletTypes = new UltraGrid();
    this.toolStrip1 = new ToolStrip();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this.buttonCancel = new Button();
    this.buttonSave = new Button();
    this.toolStripButtonAddRegister = new ToolStripButton();
    this.toolStripButtonSave = new ToolStripButton();
    this.toolStripButtonDeleteRegister = new ToolStripButton();
    ((ISupportInitialize) this.ultraGridPalletTypes).BeginInit();
    this.toolStrip1.SuspendLayout();
    this.SuspendLayout();
    appearance1.BackColor = SystemColors.Window;
    appearance1.BorderColor = SystemColors.InactiveCaption;
    this.ultraGridPalletTypes.DisplayLayout.Appearance = (AppearanceBase) appearance1;
    this.ultraGridPalletTypes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
    this.ultraGridPalletTypes.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridPalletTypes.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
    appearance2.BackColor = SystemColors.ActiveBorder;
    appearance2.BackColor2 = SystemColors.ControlDark;
    appearance2.BackGradientStyle = GradientStyle.Vertical;
    appearance2.BorderColor = SystemColors.Window;
    this.ultraGridPalletTypes.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance2;
    appearance3.ForeColor = SystemColors.GrayText;
    this.ultraGridPalletTypes.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance3;
    this.ultraGridPalletTypes.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridPalletTypes.DisplayLayout.GroupByBox.Hidden = true;
    appearance4.BackColor = SystemColors.ControlLightLight;
    appearance4.BackColor2 = SystemColors.Control;
    appearance4.BackGradientStyle = GradientStyle.Horizontal;
    appearance4.ForeColor = SystemColors.GrayText;
    this.ultraGridPalletTypes.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance4;
    this.ultraGridPalletTypes.DisplayLayout.MaxColScrollRegions = 1;
    this.ultraGridPalletTypes.DisplayLayout.MaxRowScrollRegions = 1;
    appearance5.BackColor = SystemColors.Window;
    appearance5.ForeColor = SystemColors.ControlText;
    this.ultraGridPalletTypes.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance5;
    appearance6.BackColor = SystemColors.Highlight;
    appearance6.ForeColor = SystemColors.HighlightText;
    this.ultraGridPalletTypes.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance6;
    this.ultraGridPalletTypes.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
    this.ultraGridPalletTypes.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
    this.ultraGridPalletTypes.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
    this.ultraGridPalletTypes.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
    this.ultraGridPalletTypes.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
    appearance7.BackColor = SystemColors.Window;
    this.ultraGridPalletTypes.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance7;
    appearance8.BorderColor = Color.Silver;
    appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
    this.ultraGridPalletTypes.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance8;
    this.ultraGridPalletTypes.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
    this.ultraGridPalletTypes.DisplayLayout.Override.CellPadding = 0;
    appearance9.BackColor = SystemColors.Control;
    appearance9.BackColor2 = SystemColors.ControlDark;
    appearance9.BackGradientAlignment = GradientAlignment.Element;
    appearance9.BackGradientStyle = GradientStyle.Horizontal;
    appearance9.BorderColor = SystemColors.Window;
    this.ultraGridPalletTypes.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance9;
    appearance10.TextHAlignAsString = "Left";
    this.ultraGridPalletTypes.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance10;
    this.ultraGridPalletTypes.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
    this.ultraGridPalletTypes.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
    appearance11.BackColor = SystemColors.Window;
    appearance11.BorderColor = Color.Silver;
    this.ultraGridPalletTypes.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance11;
    this.ultraGridPalletTypes.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
    appearance12.BackColor = SystemColors.ControlLight;
    this.ultraGridPalletTypes.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance12;
    this.ultraGridPalletTypes.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
    this.ultraGridPalletTypes.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
    this.ultraGridPalletTypes.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
    this.ultraGridPalletTypes.Dock = DockStyle.Top;
    this.ultraGridPalletTypes.Location = new Point(0, 27);
    this.ultraGridPalletTypes.Margin = new Padding(4);
    this.ultraGridPalletTypes.Name = "ultraGridPalletTypes";
    this.ultraGridPalletTypes.Size = new Size(435, 293);
    this.ultraGridPalletTypes.TabIndex = 6;
    this.ultraGridPalletTypes.Text = "ultraGrid2";
    this.ultraGridPalletTypes.BeforeRowUpdate += new CancelableRowEventHandler(this.ultraGridPalletTypes_BeforeRowUpdate);
    this.ultraGridPalletTypes.BeforeCellUpdate += new BeforeCellUpdateEventHandler(this.ultraGridPalletTypes_BeforeCellUpdate);
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
    this.toolStrip1.Size = new Size(435, 27);
    this.toolStrip1.TabIndex = 5;
    this.toolStrip1.Text = "toolStrip1";
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(6, 27);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    this.toolStripSeparator2.Size = new Size(6, 27);
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(347, 330);
    this.buttonCancel.Margin = new Padding(3, 2, 3, 2);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(75, 27);
    this.buttonCancel.TabIndex = 7;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.buttonSave.DialogResult = DialogResult.OK;
    this.buttonSave.Location = new Point(267, 330);
    this.buttonSave.Margin = new Padding(3, 2, 3, 2);
    this.buttonSave.Name = "buttonSave";
    this.buttonSave.Size = new Size(75, 27);
    this.buttonSave.TabIndex = 8;
    this.buttonSave.Text = "&Save";
    this.buttonSave.UseVisualStyleBackColor = true;
    this.buttonSave.Click += new EventHandler(this.buttonSave_Click);
    this.toolStripButtonAddRegister.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonAddRegister.Image = (Image) PCSYS_PLC_Server.Properties.Resources.document_add;
    this.toolStripButtonAddRegister.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonAddRegister.Name = "toolStripButtonAddRegister";
    this.toolStripButtonAddRegister.Size = new Size(24, 24);
    this.toolStripButtonAddRegister.Text = "toolStripButton1";
    this.toolStripButtonAddRegister.ToolTipText = "Add Register";
    this.toolStripButtonAddRegister.Click += new EventHandler(this.toolStripButtonAddRegister_Click);
    this.toolStripButtonSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonSave.Image = (Image) componentResourceManager.GetObject("toolStripButtonSave.Image");
    this.toolStripButtonSave.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonSave.Name = "toolStripButtonSave";
    this.toolStripButtonSave.Size = new Size(24, 24);
    this.toolStripButtonSave.Text = "toolStripButton1";
    this.toolStripButtonSave.ToolTipText = "Save";
    this.toolStripButtonSave.Click += new EventHandler(this.toolStripButtonSave_Click);
    this.toolStripButtonDeleteRegister.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonDeleteRegister.Image = (Image) PCSYS_PLC_Server.Properties.Resources.document_delete;
    this.toolStripButtonDeleteRegister.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonDeleteRegister.Name = "toolStripButtonDeleteRegister";
    this.toolStripButtonDeleteRegister.Size = new Size(24, 24);
    this.toolStripButtonDeleteRegister.Text = "toolStripButton1";
    this.toolStripButtonDeleteRegister.ToolTipText = "Delete register";
    this.toolStripButtonDeleteRegister.Click += new EventHandler(this.toolStripButtonDeleteRegister_Click);
    this.AcceptButton = (IButtonControl) this.buttonSave;
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.ClientSize = new Size(435, 366);
    this.Controls.Add((Control) this.buttonSave);
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.ultraGridPalletTypes);
    this.Controls.Add((Control) this.toolStrip1);
    this.Margin = new Padding(3, 2, 3, 2);
    this.Name = "FormPalletTypes";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Pallet Labels";
    this.Load += new EventHandler(this.FormPalletTypes_Load);
    ((ISupportInitialize) this.ultraGridPalletTypes).EndInit();
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
