// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.UserControls.UserControlLogs
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PCSYS_PLC_Server.UserControls;

public class UserControlLogs : UserControl
{
  private IContainer components = (IContainer) null;
  private ToolStrip toolStrip1;

  public UserControlLogs() => this.InitializeComponent();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.toolStrip1 = new ToolStrip();
    this.SuspendLayout();
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(760, 25);
    this.toolStrip1.TabIndex = 0;
    this.toolStrip1.Text = "toolStrip1";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.toolStrip1);
    this.Name = "UserControlLogs";
    this.Size = new Size(760, 560);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
