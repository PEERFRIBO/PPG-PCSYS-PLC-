// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormSendFiledToSap
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server;

public class FormSendFiledToSap : Form
{
  private IContainer components = (IContainer) null;
  private Label label1;
  private TextBox textBoxFillingOrder;
  private Button button1;
  private TextBox textBoxSscc;
  private Label label2;
  private TextBox textBoxQty;
  private Label label3;
  private TextBox textBoxEan;
  private Label label4;

  public FormSendFiledToSap() => this.InitializeComponent();

  private void button1_Click(object sender, EventArgs e)
  {
    File.AppendAllText("C:\\temp\\sap.txt", this.textBoxFillingOrder.Text.PadLeft(12, '0') + this.textBoxSscc.Text.PadLeft(20, '0') + this.textBoxQty.Text.PadLeft(13, '0') + this.textBoxEan.Text.PadLeft(13, '0') + "\r\n");
    this.textBoxFillingOrder.Text = string.Empty;
    this.textBoxQty.Text = string.Empty;
    this.textBoxEan.Text = string.Empty;
    this.textBoxSscc.Text = string.Empty;
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
    this.textBoxFillingOrder = new TextBox();
    this.button1 = new Button();
    this.textBoxSscc = new TextBox();
    this.label2 = new Label();
    this.textBoxQty = new TextBox();
    this.label3 = new Label();
    this.textBoxEan = new TextBox();
    this.label4 = new Label();
    this.SuspendLayout();
    this.label1.AutoSize = true;
    this.label1.Location = new Point(31 /*0x1F*/, 39);
    this.label1.Name = "label1";
    this.label1.Size = new Size(45, 17);
    this.label1.TabIndex = 0;
    this.label1.Text = "Order";
    this.textBoxFillingOrder.Location = new Point(34, 59);
    this.textBoxFillingOrder.Name = "textBoxFillingOrder";
    this.textBoxFillingOrder.Size = new Size(174, 22);
    this.textBoxFillingOrder.TabIndex = 1;
    this.button1.Location = new Point(276, 263);
    this.button1.Name = "button1";
    this.button1.Size = new Size(108, 34);
    this.button1.TabIndex = 2;
    this.button1.Text = "button1";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    this.textBoxSscc.Location = new Point(214, 59);
    this.textBoxSscc.Name = "textBoxSscc";
    this.textBoxSscc.Size = new Size(174, 22);
    this.textBoxSscc.TabIndex = 4;
    this.label2.AutoSize = true;
    this.label2.Location = new Point(211, 39);
    this.label2.Name = "label2";
    this.label2.Size = new Size(38, 17);
    this.label2.TabIndex = 3;
    this.label2.Text = "Sscc";
    this.textBoxQty.Location = new Point(394, 59);
    this.textBoxQty.Name = "textBoxQty";
    this.textBoxQty.Size = new Size(174, 22);
    this.textBoxQty.TabIndex = 6;
    this.label3.AutoSize = true;
    this.label3.Location = new Point(391, 39);
    this.label3.Name = "label3";
    this.label3.Size = new Size(30, 17);
    this.label3.TabIndex = 5;
    this.label3.Text = "Qty";
    this.textBoxEan.Location = new Point(577, 59);
    this.textBoxEan.Name = "textBoxEan";
    this.textBoxEan.Size = new Size(174, 22);
    this.textBoxEan.TabIndex = 8;
    this.label4.AutoSize = true;
    this.label4.Location = new Point(574, 39);
    this.label4.Name = "label4";
    this.label4.Size = new Size(33, 17);
    this.label4.TabIndex = 7;
    this.label4.Text = "Ean";
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(800, 450);
    this.Controls.Add((Control) this.textBoxEan);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.textBoxQty);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.textBoxSscc);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.textBoxFillingOrder);
    this.Controls.Add((Control) this.label1);
    this.Name = "FormSendFiledToSap";
    this.Text = "FormSendFiledToSap";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
