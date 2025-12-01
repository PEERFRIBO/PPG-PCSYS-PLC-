// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.PrintServiceInfo.UpdatePrinterInfo
// Assembly: PCSYS PPG Info, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 1DA98507-3547-4F13-89C5-8F8721C43394
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG Info.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

#nullable disable
namespace PCSYS_PPG_Info.PrintServiceInfo;

[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "UpdatePrinterInfo", Namespace = "http://schemas.datacontract.org/2004/07/IBSPrinterManager")]
[Serializable]
public class UpdatePrinterInfo : IExtensibleDataObject, INotifyPropertyChanged
{
  [NonSerialized]
  private ExtensionDataObject extensionDataField;
  [OptionalField]
  private PrinterInfo PrinterField;
  [OptionalField]
  private bool UpdateField;

  [Browsable(false)]
  public ExtensionDataObject ExtensionData
  {
    get => this.extensionDataField;
    set => this.extensionDataField = value;
  }

  [DataMember]
  public PrinterInfo Printer
  {
    get => this.PrinterField;
    set
    {
      if (this.PrinterField == value)
        return;
      this.PrinterField = value;
      this.RaisePropertyChanged(nameof (Printer));
    }
  }

  [DataMember]
  public bool Update
  {
    get => this.UpdateField;
    set
    {
      if (this.UpdateField.Equals(value))
        return;
      this.UpdateField = value;
      this.RaisePropertyChanged(nameof (Update));
    }
  }

  public event PropertyChangedEventHandler PropertyChanged;

  protected void RaisePropertyChanged(string propertyName)
  {
    PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
    if (propertyChanged == null)
      return;
    propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
  }
}
