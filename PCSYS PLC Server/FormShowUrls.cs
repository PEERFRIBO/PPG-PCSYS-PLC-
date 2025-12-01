// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormShowUrls
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server;

public class FormShowUrls : Form
{
  private IContainer components = (IContainer) null;
  private Button buttonOk;
  private Button buttonCancel;
  public CheckedListBox checkedListBoxUris;

  public FormShowUrls() => this.InitializeComponent();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.buttonOk = new Button();
    this.buttonCancel = new Button();
    this.checkedListBoxUris = new CheckedListBox();
    this.SuspendLayout();
    this.buttonOk.DialogResult = DialogResult.OK;
    this.buttonOk.Location = new Point(149, 161);
    this.buttonOk.Margin = new Padding(2);
    this.buttonOk.Name = "buttonOk";
    this.buttonOk.Size = new Size(56, 24);
    this.buttonOk.TabIndex = 5;
    this.buttonOk.Text = "Ok";
    this.buttonOk.UseVisualStyleBackColor = true;
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(210, 161);
    this.buttonCancel.Margin = new Padding(2);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(56, 24);
    this.buttonCancel.TabIndex = 4;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.checkedListBoxUris.Dock = DockStyle.Top;
    this.checkedListBoxUris.FormattingEnabled = true;
    this.checkedListBoxUris.Location = new Point(0, 0);
    this.checkedListBoxUris.Margin = new Padding(2);
    this.checkedListBoxUris.Name = "checkedListBoxUris";
    this.checkedListBoxUris.Size = new Size(284, 154);
    this.checkedListBoxUris.TabIndex = 3;
    this.AcceptButton = (IButtonControl) this.buttonOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.ClientSize = new Size(284, 195);
    this.Controls.Add((Control) this.buttonOk);
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.checkedListBoxUris);
    this.Name = "FormShowUrls";
    this.Text = "FormShowUrls";
    this.ResumeLayout(false);
  }
}
