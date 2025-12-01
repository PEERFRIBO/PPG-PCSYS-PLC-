// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.FormSAPServer
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using ERPConnect;
using PCSYS_Plc_AppSettings;
using PCSYS_PPG_Info;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server;

public class FormSAPServer : Form
{
  private IContainer components = (IContainer) null;
  private GroupBox groupBox3;
  private TextBox textBoxPort;
  private Label label14;
  private TextBox textBoxPassword;
  private Label label7;
  private TextBox textBoxUser;
  private Label label13;
  private Button buttonTest;
  private TextBox editBoxClient;
  private Label label3;
  private TextBox editBoxProgramID;
  private Label label1;
  private TextBox editBoxAsHost;
  private Label label10;
  private Button buttonCancel;
  private Button buttonSave;
  private CheckBox checkBoxUniCode;
  private TextBox textBoxFunction;
  private Label label2;
  private ComboBox comboBoxEanField;
  private Label label4;

  public FormSAPServer() => this.InitializeComponent();

  private void buttonTest_Click(object sender, EventArgs e)
  {
    string connectionString = $"USER={this.textBoxUser.Text} LANG=EN CLIENT={this.editBoxClient.Text} SYSNR=00 ASHOST={this.editBoxAsHost.Text} PASSWD={this.textBoxPassword.Text}";
    LIC.SetLic("25FP6CW8P5");
    R3Connection r3Connection = new R3Connection(connectionString);
    try
    {
      r3Connection.Open();
      int num = (int) MessageBox.Show(r3Connection.Ping() ? "The connection is accepted" : "The connection is not accepted");
      r3Connection.Close();
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message);
    }
  }

  private void buttonSave_Click(object sender, EventArgs e)
  {
    SettingsTools.MainSettings.GateWayHost = this.editBoxAsHost.Text;
    SettingsTools.MainSettings.ProgramId = this.editBoxProgramID.Text;
    SettingsTools.MainSettings.GateWayService = this.editBoxClient.Text;
    SettingsTools.MainSettings.Unicode = this.checkBoxUniCode.Checked;
    SettingsTools.MainSettings.User = this.textBoxUser.Text;
    SettingsTools.MainSettings.Password = this.textBoxPassword.Text.Length > 0 ? Encoding.BigEndianUnicode.GetBytes(this.textBoxPassword.Text) : new byte[0];
    SettingsTools.MainSettings.FunctionInsideSap = this.textBoxFunction.Text;
    if (this.comboBoxEanField.SelectedValue == null)
      return;
    SettingsTools.MainSettings.EanFieldId = (int) this.comboBoxEanField.SelectedValue;
  }

  private void FormSAPServer_Load(object sender, EventArgs e)
  {
    this.editBoxProgramID.Text = SettingsTools.MainSettings.ProgramId;
    this.editBoxAsHost.Text = SettingsTools.MainSettings.GateWayHost;
    this.editBoxClient.Text = SettingsTools.MainSettings.GateWayService;
    this.checkBoxUniCode.Checked = SettingsTools.MainSettings.Unicode;
    this.textBoxUser.Text = SettingsTools.MainSettings.User;
    this.textBoxPassword.Text = SettingsTools.MainSettings.Password != null ? Encoding.BigEndianUnicode.GetString(SettingsTools.MainSettings.Password) : string.Empty;
    this.textBoxFunction.Text = SettingsTools.MainSettings.FunctionInsideSap;
    this.comboBoxEanField.DataSource = (object) new DataTools().GetFieldNameList(SettingsTools.MainSettings.DataLinks).ToList<KeyValuePair<int, string>>();
    this.comboBoxEanField.DisplayMember = "Value";
    this.comboBoxEanField.ValueMember = "Key";
    this.comboBoxEanField.SelectedValue = (object) SettingsTools.MainSettings.EanFieldId;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.groupBox3 = new GroupBox();
    this.comboBoxEanField = new ComboBox();
    this.label4 = new Label();
    this.textBoxFunction = new TextBox();
    this.label2 = new Label();
    this.checkBoxUniCode = new CheckBox();
    this.textBoxPort = new TextBox();
    this.label14 = new Label();
    this.textBoxPassword = new TextBox();
    this.label7 = new Label();
    this.textBoxUser = new TextBox();
    this.label13 = new Label();
    this.buttonTest = new Button();
    this.editBoxClient = new TextBox();
    this.label3 = new Label();
    this.editBoxProgramID = new TextBox();
    this.label1 = new Label();
    this.editBoxAsHost = new TextBox();
    this.label10 = new Label();
    this.buttonCancel = new Button();
    this.buttonSave = new Button();
    this.groupBox3.SuspendLayout();
    this.SuspendLayout();
    this.groupBox3.Controls.Add((Control) this.comboBoxEanField);
    this.groupBox3.Controls.Add((Control) this.label4);
    this.groupBox3.Controls.Add((Control) this.textBoxFunction);
    this.groupBox3.Controls.Add((Control) this.label2);
    this.groupBox3.Controls.Add((Control) this.checkBoxUniCode);
    this.groupBox3.Controls.Add((Control) this.textBoxPort);
    this.groupBox3.Controls.Add((Control) this.label14);
    this.groupBox3.Controls.Add((Control) this.textBoxPassword);
    this.groupBox3.Controls.Add((Control) this.label7);
    this.groupBox3.Controls.Add((Control) this.textBoxUser);
    this.groupBox3.Controls.Add((Control) this.label13);
    this.groupBox3.Controls.Add((Control) this.buttonTest);
    this.groupBox3.Controls.Add((Control) this.editBoxClient);
    this.groupBox3.Controls.Add((Control) this.label3);
    this.groupBox3.Controls.Add((Control) this.editBoxProgramID);
    this.groupBox3.Controls.Add((Control) this.label1);
    this.groupBox3.Controls.Add((Control) this.editBoxAsHost);
    this.groupBox3.Controls.Add((Control) this.label10);
    this.groupBox3.Location = new Point(12, 12);
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.Size = new Size(281, 266);
    this.groupBox3.TabIndex = 25;
    this.groupBox3.TabStop = false;
    this.groupBox3.Text = "SAP Access";
    this.comboBoxEanField.FormattingEnabled = true;
    this.comboBoxEanField.Location = new Point(6, 235);
    this.comboBoxEanField.Margin = new Padding(2, 2, 2, 2);
    this.comboBoxEanField.Name = "comboBoxEanField";
    this.comboBoxEanField.Size = new Size(269, 21);
    this.comboBoxEanField.TabIndex = 30;
    this.label4.AutoSize = true;
    this.label4.Location = new Point(6, 219);
    this.label4.Name = "label4";
    this.label4.Size = new Size(51, 13);
    this.label4.TabIndex = 29;
    this.label4.Text = "EAN field";
    this.textBoxFunction.Location = new Point(6, 189);
    this.textBoxFunction.Name = "textBoxFunction";
    this.textBoxFunction.Size = new Size(268, 20);
    this.textBoxFunction.TabIndex = 28;
    this.textBoxFunction.Text = "Z_PCO_TO_IDOC";
    this.label2.AutoSize = true;
    this.label2.Location = new Point(6, 173);
    this.label2.Name = "label2";
    this.label2.Size = new Size(48 /*0x30*/, 13);
    this.label2.TabIndex = 27;
    this.label2.Text = "Function";
    this.checkBoxUniCode.AutoSize = true;
    this.checkBoxUniCode.Location = new Point(6, 149);
    this.checkBoxUniCode.Name = "checkBoxUniCode";
    this.checkBoxUniCode.Size = new Size(66, 17);
    this.checkBoxUniCode.TabIndex = 26;
    this.checkBoxUniCode.Text = "Unicode";
    this.checkBoxUniCode.UseVisualStyleBackColor = true;
    this.textBoxPort.Enabled = false;
    this.textBoxPort.Location = new Point(216, 79);
    this.textBoxPort.Name = "textBoxPort";
    this.textBoxPort.Size = new Size(58, 20);
    this.textBoxPort.TabIndex = 25;
    this.label14.AutoSize = true;
    this.label14.Location = new Point(214, 63 /*0x3F*/);
    this.label14.Name = "label14";
    this.label14.Size = new Size(26, 13);
    this.label14.TabIndex = 24;
    this.label14.Text = "Port";
    this.textBoxPassword.Location = new Point(154, 119);
    this.textBoxPassword.Name = "textBoxPassword";
    this.textBoxPassword.PasswordChar = '*';
    this.textBoxPassword.Size = new Size(120, 20);
    this.textBoxPassword.TabIndex = 23;
    this.textBoxPassword.Text = "PPGDenmark01..";
    this.label7.AutoSize = true;
    this.label7.Location = new Point(154, 103);
    this.label7.Name = "label7";
    this.label7.Size = new Size(53, 13);
    this.label7.TabIndex = 22;
    this.label7.Text = "Password";
    this.textBoxUser.Location = new Point(6, 119);
    this.textBoxUser.Name = "textBoxUser";
    this.textBoxUser.Size = new Size(144 /*0x90*/, 20);
    this.textBoxUser.TabIndex = 21;
    this.textBoxUser.Text = "RFC-PCSYS";
    this.label13.AutoSize = true;
    this.label13.Location = new Point(6, 103);
    this.label13.Name = "label13";
    this.label13.Size = new Size(29, 13);
    this.label13.TabIndex = 20;
    this.label13.Text = "User";
    this.buttonTest.Location = new Point(166, 145);
    this.buttonTest.Name = "buttonTest";
    this.buttonTest.Size = new Size(108, 23);
    this.buttonTest.TabIndex = 19;
    this.buttonTest.Text = "Test connection";
    this.buttonTest.UseVisualStyleBackColor = true;
    this.buttonTest.Click += new EventHandler(this.buttonTest_Click);
    this.editBoxClient.Location = new Point(156, 79);
    this.editBoxClient.Name = "editBoxClient";
    this.editBoxClient.Size = new Size(56, 20);
    this.editBoxClient.TabIndex = 18;
    this.editBoxClient.Text = "105";
    this.label3.AutoSize = true;
    this.label3.Location = new Point(154, 63 /*0x3F*/);
    this.label3.Name = "label3";
    this.label3.Size = new Size(56, 13);
    this.label3.TabIndex = 17;
    this.label3.Text = "SAP client";
    this.editBoxProgramID.Location = new Point(6, 79);
    this.editBoxProgramID.Name = "editBoxProgramID";
    this.editBoxProgramID.Size = new Size(144 /*0x90*/, 20);
    this.editBoxProgramID.TabIndex = 16 /*0x10*/;
    this.editBoxProgramID.Text = "SAPPCSYSI";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(6, 63 /*0x3F*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(88, 13);
    this.label1.TabIndex = 15;
    this.label1.Text = "Local program ID";
    this.editBoxAsHost.Location = new Point(6, 38);
    this.editBoxAsHost.Name = "editBoxAsHost";
    this.editBoxAsHost.Size = new Size(268, 20);
    this.editBoxAsHost.TabIndex = 14;
    this.editBoxAsHost.Text = "T15CLNT105";
    this.label10.AutoSize = true;
    this.label10.Location = new Point(6, 22);
    this.label10.Name = "label10";
    this.label10.Size = new Size(43, 13);
    this.label10.TabIndex = 13;
    this.label10.Text = "ASHost";
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(214, 285);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(75, 23);
    this.buttonCancel.TabIndex = 26;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.buttonSave.DialogResult = DialogResult.OK;
    this.buttonSave.Location = new Point(134, 285);
    this.buttonSave.Name = "buttonSave";
    this.buttonSave.Size = new Size(75, 23);
    this.buttonSave.TabIndex = 27;
    this.buttonSave.Text = "&Save";
    this.buttonSave.UseVisualStyleBackColor = true;
    this.buttonSave.Click += new EventHandler(this.buttonSave_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(309, 323);
    this.Controls.Add((Control) this.buttonSave);
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.groupBox3);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = "FormSAPServer";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "SAP Server";
    this.Load += new EventHandler(this.FormSAPServer_Load);
    this.groupBox3.ResumeLayout(false);
    this.groupBox3.PerformLayout();
    this.ResumeLayout(false);
  }
}
