// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormInsertToConstore
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

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

public class FormInsertToConstore : Form
{
  private DataTable _ssccinfo;
  private IContainer components = (IContainer) null;
  private Label label1;
  private ComboBox comboBoxSsccCode;
  private Label label2;
  private Label labelOrdreNumber;
  private Label labelSequence;
  private Label label4;
  private Label labelDestination;
  private Label label5;
  private Label labelqtyonpallet;
  private Label label7;
  private Label labelLineNo;
  private Label label6;
  private Button buttonInsertToDatabase;
  private Label labelMaterialNumber;
  private Label label8;

  public FormInsertToConstore() => this.InitializeComponent();

  private void FormInsertToConstore_Load(object sender, EventArgs e)
  {
    this._ssccinfo = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) SettingsTools.MainSettings.DataLinks).GetData("SELECT * FROM tppgprintedsscc ORDER BY id DESC").Tables[0];
    foreach (DataRow row in (InternalDataCollectionBase) this._ssccinfo.Rows)
      this.comboBoxSsccCode.Items.Add((object) row["sscc"].ToString());
  }

  private void comboBoxSsccCode_SelectedIndexChanged(object sender, EventArgs e)
  {
    DataRow[] dataRowArray = this._ssccinfo.Select($"sscc = '{this.comboBoxSsccCode.Text}'");
    if (dataRowArray.Length == 0)
    {
      int num = (int) MessageBox.Show("SSCC not found");
    }
    else
    {
      this.labelDestination.Text = dataRowArray[0]["destination"].ToString();
      this.labelLineNo.Text = dataRowArray[0]["lineno"].ToString();
      this.labelMaterialNumber.Text = dataRowArray[0]["materialnumber"].ToString();
      this.labelSequence.Text = dataRowArray[0]["sequence"].ToString();
      this.labelqtyonpallet.Text = dataRowArray[0]["qtyonpallet"].ToString();
      this.labelOrdreNumber.Text = dataRowArray[0]["fillingorder"].ToString();
    }
  }

  private void buttonInsertToDatabase_Click(object sender, EventArgs e)
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
    this.label1 = new Label();
    this.comboBoxSsccCode = new ComboBox();
    this.label2 = new Label();
    this.labelOrdreNumber = new Label();
    this.labelSequence = new Label();
    this.label4 = new Label();
    this.labelDestination = new Label();
    this.label5 = new Label();
    this.labelqtyonpallet = new Label();
    this.label7 = new Label();
    this.labelLineNo = new Label();
    this.label6 = new Label();
    this.buttonInsertToDatabase = new Button();
    this.labelMaterialNumber = new Label();
    this.label8 = new Label();
    this.SuspendLayout();
    this.label1.AutoSize = true;
    this.label1.Location = new Point(24, 29);
    this.label1.Name = "label1";
    this.label1.Size = new Size(81, 17);
    this.label1.TabIndex = 0;
    this.label1.Text = "SSCC Code";
    this.comboBoxSsccCode.FormattingEnabled = true;
    this.comboBoxSsccCode.Location = new Point(24, 49);
    this.comboBoxSsccCode.Name = "comboBoxSsccCode";
    this.comboBoxSsccCode.Size = new Size(369, 24);
    this.comboBoxSsccCode.TabIndex = 1;
    this.comboBoxSsccCode.SelectedIndexChanged += new EventHandler(this.comboBoxSsccCode_SelectedIndexChanged);
    this.label2.AutoSize = true;
    this.label2.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
    this.label2.Location = new Point(24, 111);
    this.label2.Name = "label2";
    this.label2.Size = new Size(111, 17);
    this.label2.TabIndex = 2;
    this.label2.Text = "Ordre Number";
    this.labelOrdreNumber.AutoSize = true;
    this.labelOrdreNumber.Location = new Point(24, 139);
    this.labelOrdreNumber.Name = "labelOrdreNumber";
    this.labelOrdreNumber.Size = new Size(0, 17);
    this.labelOrdreNumber.TabIndex = 3;
    this.labelSequence.AutoSize = true;
    this.labelSequence.Location = new Point(165, 198);
    this.labelSequence.Name = "labelSequence";
    this.labelSequence.Size = new Size(72, 17);
    this.labelSequence.TabIndex = 5;
    this.labelSequence.Text = "Sequence";
    this.label4.AutoSize = true;
    this.label4.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
    this.label4.Location = new Point(165, 170);
    this.label4.Name = "label4";
    this.label4.Size = new Size(80 /*0x50*/, 17);
    this.label4.TabIndex = 4;
    this.label4.Text = "Sequence";
    this.labelDestination.AutoSize = true;
    this.labelDestination.Location = new Point(21, 198);
    this.labelDestination.Name = "labelDestination";
    this.labelDestination.Size = new Size(0, 17);
    this.labelDestination.TabIndex = 7;
    this.label5.AutoSize = true;
    this.label5.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
    this.label5.Location = new Point(21, 170);
    this.label5.Name = "label5";
    this.label5.Size = new Size(90, 17);
    this.label5.TabIndex = 6;
    this.label5.Text = "Destination";
    this.labelqtyonpallet.AutoSize = true;
    this.labelqtyonpallet.Location = new Point(165, 249);
    this.labelqtyonpallet.Name = "labelqtyonpallet";
    this.labelqtyonpallet.Size = new Size(0, 17);
    this.labelqtyonpallet.TabIndex = 9;
    this.label7.AutoSize = true;
    this.label7.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
    this.label7.Location = new Point(165, 221);
    this.label7.Name = "label7";
    this.label7.Size = new Size(102, 17);
    this.label7.TabIndex = 8;
    this.label7.Text = "Qty on Pallet";
    this.labelLineNo.AutoSize = true;
    this.labelLineNo.Location = new Point(23, 249);
    this.labelLineNo.Name = "labelLineNo";
    this.labelLineNo.Size = new Size(0, 17);
    this.labelLineNo.TabIndex = 11;
    this.label6.AutoSize = true;
    this.label6.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
    this.label6.Location = new Point(23, 221);
    this.label6.Name = "label6";
    this.label6.Size = new Size(62, 17);
    this.label6.TabIndex = 10;
    this.label6.Text = "Line no";
    this.buttonInsertToDatabase.Location = new Point(12, 304);
    this.buttonInsertToDatabase.Name = "buttonInsertToDatabase";
    this.buttonInsertToDatabase.Size = new Size(147, 32 /*0x20*/);
    this.buttonInsertToDatabase.TabIndex = 12;
    this.buttonInsertToDatabase.Text = "Insert to database";
    this.buttonInsertToDatabase.UseVisualStyleBackColor = true;
    this.buttonInsertToDatabase.Click += new EventHandler(this.buttonInsertToDatabase_Click);
    this.labelMaterialNumber.AutoSize = true;
    this.labelMaterialNumber.Location = new Point(165, 139);
    this.labelMaterialNumber.Name = "labelMaterialNumber";
    this.labelMaterialNumber.Size = new Size(0, 17);
    this.labelMaterialNumber.TabIndex = 14;
    this.label8.AutoSize = true;
    this.label8.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
    this.label8.Location = new Point(165, 111);
    this.label8.Name = "label8";
    this.label8.Size = new Size((int) sbyte.MaxValue, 17);
    this.label8.TabIndex = 13;
    this.label8.Text = "Material Number";
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(442, 370);
    this.Controls.Add((Control) this.labelMaterialNumber);
    this.Controls.Add((Control) this.label8);
    this.Controls.Add((Control) this.buttonInsertToDatabase);
    this.Controls.Add((Control) this.labelLineNo);
    this.Controls.Add((Control) this.label6);
    this.Controls.Add((Control) this.labelqtyonpallet);
    this.Controls.Add((Control) this.label7);
    this.Controls.Add((Control) this.labelDestination);
    this.Controls.Add((Control) this.label5);
    this.Controls.Add((Control) this.labelSequence);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.labelOrdreNumber);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.comboBoxSsccCode);
    this.Controls.Add((Control) this.label1);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = "FormInsertToConstore";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Insert To Constore";
    this.Load += new EventHandler(this.FormInsertToConstore_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
