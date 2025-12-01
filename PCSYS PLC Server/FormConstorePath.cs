// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormConstorePath
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using PCSYS_Plc_AppSettings;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server;

public class FormConstorePath : Form
{
  private IContainer components = (IContainer) null;
  private TextBox textBoxPtfPath;
  private Label label8;
  private Button buttonSave;
  private Button buttonCancel;

  public FormConstorePath() => this.InitializeComponent();

  private void FormConstorePath_Load(object sender, EventArgs e)
  {
    this.textBoxPtfPath.Text = SettingsTools.MainSettings.ConnectionString;
  }

  private void buttonSave_Click(object sender, EventArgs e)
  {
    SettingsTools.MainSettings.ConnectionString = this.textBoxPtfPath.Text;
  }

  private void buttonBrowse_Click(object sender, EventArgs e)
  {
    using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
    {
      folderBrowserDialog.SelectedPath = this.textBoxPtfPath.Text;
      if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
      this.textBoxPtfPath.Text = folderBrowserDialog.SelectedPath;
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
    this.textBoxPtfPath = new TextBox();
    this.label8 = new Label();
    this.buttonSave = new Button();
    this.buttonCancel = new Button();
    this.SuspendLayout();
    this.textBoxPtfPath.Location = new Point(15, 31 /*0x1F*/);
    this.textBoxPtfPath.Margin = new Padding(3, 2, 3, 2);
    this.textBoxPtfPath.Name = "textBoxPtfPath";
    this.textBoxPtfPath.Size = new Size(623, 22);
    this.textBoxPtfPath.TabIndex = 6;
    this.label8.AutoSize = true;
    this.label8.Location = new Point(12, 11);
    this.label8.Name = "label8";
    this.label8.Size = new Size(118, 17);
    this.label8.TabIndex = 5;
    this.label8.Text = "Connection string";
    this.buttonSave.DialogResult = DialogResult.OK;
    this.buttonSave.Location = new Point(476, 57);
    this.buttonSave.Margin = new Padding(3, 2, 3, 2);
    this.buttonSave.Name = "buttonSave";
    this.buttonSave.Size = new Size(77, 27);
    this.buttonSave.TabIndex = 10;
    this.buttonSave.Text = "&Save";
    this.buttonSave.UseVisualStyleBackColor = true;
    this.buttonSave.Click += new EventHandler(this.buttonSave_Click);
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(559, 57);
    this.buttonCancel.Margin = new Padding(3, 2, 3, 2);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(77, 27);
    this.buttonCancel.TabIndex = 9;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.buttonSave;
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.ClientSize = new Size(659, 99);
    this.Controls.Add((Control) this.buttonSave);
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.textBoxPtfPath);
    this.Controls.Add((Control) this.label8);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Margin = new Padding(4, 4, 4, 4);
    this.Name = "FormConstorePath";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Constore Path";
    this.Load += new EventHandler(this.FormConstorePath_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
