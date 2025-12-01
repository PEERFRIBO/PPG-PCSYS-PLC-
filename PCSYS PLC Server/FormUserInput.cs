// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormUserInput
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PCSYS_Plc_AppSettings;
using PCSYS_PPG_Info;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server;

public class FormUserInput : Form
{
  private IContainer components = (IContainer) null;
  private UltraGrid ultraGridUserInputs;
  private ContextMenuStrip contextMenuStripMenu;
  private ToolStripMenuItem toolStripMenuItem1;

  public FormUserInput() => this.InitializeComponent();

  private void FormUserInput_Load(object sender, EventArgs e)
  {
    this.ultraGridUserInputs.DataSource = (object) new DataTools().GetUserInput(SettingsTools.MainSettings.DataLinks);
  }

  private void contextMenuStripMenu_Opening(object sender, CancelEventArgs e)
  {
  }

  private void ToolStripMenuItem1OnClick(object sender, EventArgs e)
  {
    UltraGridRow activeRow = this.ultraGridUserInputs.ActiveRow;
    DataTools.ExecuteUserResponse(Convert.ToInt32(activeRow.Cells["sscccode"].Value), Convert.ToInt32(activeRow.Cells["value"].Value), string.Empty, SettingsTools.MainSettings.DataLinks);
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
    this.ultraGridUserInputs = new UltraGrid();
    this.contextMenuStripMenu = new ContextMenuStrip(this.components);
    this.toolStripMenuItem1 = new ToolStripMenuItem();
    ((ISupportInitialize) this.ultraGridUserInputs).BeginInit();
    this.contextMenuStripMenu.SuspendLayout();
    this.SuspendLayout();
    this.ultraGridUserInputs.ContextMenuStrip = this.contextMenuStripMenu;
    appearance1.BackColor = SystemColors.Window;
    appearance1.BorderColor = SystemColors.InactiveCaption;
    this.ultraGridUserInputs.DisplayLayout.Appearance = (AppearanceBase) appearance1;
    this.ultraGridUserInputs.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
    this.ultraGridUserInputs.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridUserInputs.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
    appearance2.BackColor = SystemColors.ActiveBorder;
    appearance2.BackColor2 = SystemColors.ControlDark;
    appearance2.BackGradientStyle = GradientStyle.Vertical;
    appearance2.BorderColor = SystemColors.Window;
    this.ultraGridUserInputs.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance2;
    appearance3.ForeColor = SystemColors.GrayText;
    this.ultraGridUserInputs.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance3;
    this.ultraGridUserInputs.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
    this.ultraGridUserInputs.DisplayLayout.GroupByBox.Hidden = true;
    appearance4.BackColor = SystemColors.ControlLightLight;
    appearance4.BackColor2 = SystemColors.Control;
    appearance4.BackGradientStyle = GradientStyle.Horizontal;
    appearance4.ForeColor = SystemColors.GrayText;
    this.ultraGridUserInputs.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance4;
    this.ultraGridUserInputs.DisplayLayout.MaxColScrollRegions = 1;
    this.ultraGridUserInputs.DisplayLayout.MaxRowScrollRegions = 1;
    appearance5.BackColor = SystemColors.Window;
    appearance5.ForeColor = SystemColors.ControlText;
    this.ultraGridUserInputs.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance5;
    appearance6.BackColor = SystemColors.Highlight;
    appearance6.ForeColor = SystemColors.HighlightText;
    this.ultraGridUserInputs.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance6;
    this.ultraGridUserInputs.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
    this.ultraGridUserInputs.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
    this.ultraGridUserInputs.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
    this.ultraGridUserInputs.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
    this.ultraGridUserInputs.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
    appearance7.BackColor = SystemColors.Window;
    this.ultraGridUserInputs.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance7;
    appearance8.BorderColor = Color.Silver;
    appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
    this.ultraGridUserInputs.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance8;
    this.ultraGridUserInputs.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
    this.ultraGridUserInputs.DisplayLayout.Override.CellPadding = 0;
    appearance9.BackColor = SystemColors.Control;
    appearance9.BackColor2 = SystemColors.ControlDark;
    appearance9.BackGradientAlignment = GradientAlignment.Element;
    appearance9.BackGradientStyle = GradientStyle.Horizontal;
    appearance9.BorderColor = SystemColors.Window;
    this.ultraGridUserInputs.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance9;
    appearance10.TextHAlignAsString = "Left";
    this.ultraGridUserInputs.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance10;
    this.ultraGridUserInputs.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
    this.ultraGridUserInputs.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
    appearance11.BackColor = SystemColors.Window;
    appearance11.BorderColor = Color.Silver;
    this.ultraGridUserInputs.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance11;
    this.ultraGridUserInputs.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
    appearance12.BackColor = SystemColors.ControlLight;
    this.ultraGridUserInputs.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance12;
    this.ultraGridUserInputs.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
    this.ultraGridUserInputs.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
    this.ultraGridUserInputs.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
    this.ultraGridUserInputs.Dock = DockStyle.Top;
    this.ultraGridUserInputs.Location = new Point(0, 0);
    this.ultraGridUserInputs.Name = "ultraGridUserInputs";
    this.ultraGridUserInputs.Size = new Size(518, 378);
    this.ultraGridUserInputs.TabIndex = 12;
    this.ultraGridUserInputs.Text = "ultraGrid2";
    this.contextMenuStripMenu.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.toolStripMenuItem1
    });
    this.contextMenuStripMenu.Name = "contextMenuStripMenu";
    this.contextMenuStripMenu.Size = new Size(100, 26);
    this.contextMenuStripMenu.Opening += new CancelEventHandler(this.contextMenuStripMenu_Opening);
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    this.toolStripMenuItem1.Size = new Size(99, 22);
    this.toolStripMenuItem1.Text = "Print";
    this.toolStripMenuItem1.Click += new EventHandler(this.ToolStripMenuItem1OnClick);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(518, 380);
    this.Controls.Add((Control) this.ultraGridUserInputs);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = "FormUserInput";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "User Input";
    this.Load += new EventHandler(this.FormUserInput_Load);
    ((ISupportInitialize) this.ultraGridUserInputs).EndInit();
    this.contextMenuStripMenu.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
