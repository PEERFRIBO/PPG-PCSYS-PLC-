// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.PrintServiceInfo.PaperSources
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
[DataContract(Name = "PaperSources", Namespace = "http://schemas.datacontract.org/2004/07/IBSPrinterManager")]
[Serializable]
public class PaperSources : IExtensibleDataObject, INotifyPropertyChanged
{
  [NonSerialized]
  private ExtensionDataObject extensionDataField;
  [OptionalField]
  private string PrinterNameField;
  [OptionalField]
  private string[] TraysField;

  [Browsable(false)]
  public ExtensionDataObject ExtensionData
  {
    get => this.extensionDataField;
    set => this.extensionDataField = value;
  }

  [DataMember]
  public string PrinterName
  {
    get => this.PrinterNameField;
    set
    {
      if ((object) this.PrinterNameField == (object) value)
        return;
      this.PrinterNameField = value;
      this.RaisePropertyChanged(nameof (PrinterName));
    }
  }

  [DataMember]
  public string[] Trays
  {
    get => this.TraysField;
    set
    {
      if (this.TraysField == value)
        return;
      this.TraysField = value;
      this.RaisePropertyChanged(nameof (Trays));
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
