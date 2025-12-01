// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormDirectPrint
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using PCSYS_PLC_Server.Code;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server;

public class FormDirectPrint : Form
{
  private IContainer components = (IContainer) null;
  private Label label1;
  private TextBox textBoxCode;
  private Label label2;
  private ComboBox comboBoxPrinter;
  private Button buttonPrint;

  public FormDirectPrint() => this.InitializeComponent();

  private void FormDirectPrint_Load(object sender, EventArgs e)
  {
    foreach (object installedPrinter in PrinterSettings.InstalledPrinters)
      this.comboBoxPrinter.Items.Add(installedPrinter);
  }

  private void buttonPrint_Click(object sender, EventArgs e)
  {
    Encoding.Default.GetBytes(this.textBoxCode.Text + "\r\n");
    RawPrintApi.SendStringToPrinter(this.comboBoxPrinter.Text, this.textBoxCode.Text);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.label1 = new Label();
    this.textBoxCode = new TextBox();
    this.label2 = new Label();
    this.comboBoxPrinter = new ComboBox();
    this.buttonPrint = new Button();
    this.SuspendLayout();
    this.label1.AutoSize = true;
    this.label1.Location = new Point(27, 36);
    this.label1.Name = "label1";
    this.label1.Size = new Size(41, 17);
    this.label1.TabIndex = 0;
    this.label1.Text = "Code";
    this.textBoxCode.Location = new Point(30, 56);
    this.textBoxCode.Name = "textBoxCode";
    this.textBoxCode.Size = new Size(310, 22);
    this.textBoxCode.TabIndex = 1;
    this.label2.AutoSize = true;
    this.label2.Location = new Point(27, 97);
    this.label2.Name = "label2";
    this.label2.Size = new Size(50, 17);
    this.label2.TabIndex = 2;
    this.label2.Text = "Printer";
    this.comboBoxPrinter.FormattingEnabled = true;
    this.comboBoxPrinter.Location = new Point(30, 117);
    this.comboBoxPrinter.Name = "comboBoxPrinter";
    this.comboBoxPrinter.Size = new Size(310, 24);
    this.comboBoxPrinter.TabIndex = 3;
    this.buttonPrint.Location = new Point(265, 172);
    this.buttonPrint.Name = "buttonPrint";
    this.buttonPrint.Size = new Size(75, 23);
    this.buttonPrint.TabIndex = 4;
    this.buttonPrint.Text = "Send";
    this.buttonPrint.UseVisualStyleBackColor = true;
    this.buttonPrint.Click += new EventHandler(this.buttonPrint_Click);
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(373, 225);
    this.Controls.Add((Control) this.buttonPrint);
    this.Controls.Add((Control) this.comboBoxPrinter);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.textBoxCode);
    this.Controls.Add((Control) this.label1);
    this.Name = "FormDirectPrint";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Direct Print";
    this.Load += new EventHandler(this.FormDirectPrint_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
