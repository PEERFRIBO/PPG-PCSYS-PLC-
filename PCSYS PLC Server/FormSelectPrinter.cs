// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormSelectPrinter
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using PCSYS_Plc_AppSettings;
using PCSYS_PPG_LPS_ProxyConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server;

public class FormSelectPrinter : Form
{
  private IContainer components = (IContainer) null;
  private Label label1;
  private Button buttonCancel;
  private Label label2;
  private Button buttonPrint;
  public ComboBox comboBoxPrinters;
  public NumericUpDown numericUpDownQty;

  public FormSelectPrinter() => this.InitializeComponent();

  private void FormSelectPrinter_Load(object sender, EventArgs e)
  {
    string sqlString = "SELECT itemname FROM tprinters ORDER BY itemname";
    this.comboBoxPrinters.DataSource = (object) new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).GetData(sqlString).Tables[0];
    this.comboBoxPrinters.DisplayMember = "itemname";
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
    this.comboBoxPrinters = new ComboBox();
    this.buttonCancel = new Button();
    this.label2 = new Label();
    this.numericUpDownQty = new NumericUpDown();
    this.buttonPrint = new Button();
    this.numericUpDownQty.BeginInit();
    this.SuspendLayout();
    this.label1.AutoSize = true;
    this.label1.Location = new Point(12, 23);
    this.label1.Name = "label1";
    this.label1.Size = new Size(50, 17);
    this.label1.TabIndex = 0;
    this.label1.Text = "Printer";
    this.comboBoxPrinters.FormattingEnabled = true;
    this.comboBoxPrinters.Location = new Point(15, 43);
    this.comboBoxPrinters.Name = "comboBoxPrinters";
    this.comboBoxPrinters.Size = new Size(326, 24);
    this.comboBoxPrinters.TabIndex = 1;
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(266, 105);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(75, 32 /*0x20*/);
    this.buttonCancel.TabIndex = 3;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.label2.AutoSize = true;
    this.label2.Location = new Point(12, 86);
    this.label2.Name = "label2";
    this.label2.Size = new Size(30, 17);
    this.label2.TabIndex = 4;
    this.label2.Text = "Qty";
    this.numericUpDownQty.Location = new Point(15, 106);
    this.numericUpDownQty.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numericUpDownQty.Name = "numericUpDownQty";
    this.numericUpDownQty.Size = new Size(61, 22);
    this.numericUpDownQty.TabIndex = 5;
    this.numericUpDownQty.TextAlign = HorizontalAlignment.Right;
    this.numericUpDownQty.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.buttonPrint.DialogResult = DialogResult.OK;
    this.buttonPrint.Location = new Point(185, 106);
    this.buttonPrint.Name = "buttonPrint";
    this.buttonPrint.Size = new Size(75, 31 /*0x1F*/);
    this.buttonPrint.TabIndex = 6;
    this.buttonPrint.Text = "&Print";
    this.buttonPrint.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.buttonPrint;
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.ClientSize = new Size(353, 149);
    this.Controls.Add((Control) this.buttonPrint);
    this.Controls.Add((Control) this.numericUpDownQty);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.comboBoxPrinters);
    this.Controls.Add((Control) this.label1);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = "FormSelectPrinter";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Select Printer";
    this.Load += new EventHandler(this.FormSelectPrinter_Load);
    this.numericUpDownQty.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
