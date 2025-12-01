// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormInput
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server;

public class FormInput : Form
{
  private IContainer components = (IContainer) null;
  private Label label1;
  public TextBox textBoxId;
  private Button buttonOk;

  public FormInput() => this.InitializeComponent();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.label1 = new Label();
    this.textBoxId = new TextBox();
    this.buttonOk = new Button();
    this.SuspendLayout();
    this.label1.AutoSize = true;
    this.label1.Location = new Point(12, 21);
    this.label1.Name = "label1";
    this.label1.Size = new Size(19, 17);
    this.label1.TabIndex = 0;
    this.label1.Text = "Id";
    this.textBoxId.Location = new Point(37, 21);
    this.textBoxId.Name = "textBoxId";
    this.textBoxId.Size = new Size(100, 22);
    this.textBoxId.TabIndex = 1;
    this.buttonOk.DialogResult = DialogResult.OK;
    this.buttonOk.Location = new Point(46, 74);
    this.buttonOk.Name = "buttonOk";
    this.buttonOk.Size = new Size(75, 23);
    this.buttonOk.TabIndex = 2;
    this.buttonOk.Text = "&Ok";
    this.buttonOk.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.buttonOk;
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(170, 126);
    this.Controls.Add((Control) this.buttonOk);
    this.Controls.Add((Control) this.textBoxId);
    this.Controls.Add((Control) this.label1);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = "FormInput";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Insert SSCC Id";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
