// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.PrintServiceInfo.PrinterInfo
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
[DataContract(Name = "PrinterInfo", Namespace = "http://schemas.datacontract.org/2004/07/PCSYSPrintManager.Code")]
[Serializable]
public class PrinterInfo : IExtensibleDataObject, INotifyPropertyChanged
{
  [NonSerialized]
  private ExtensionDataObject extensionDataField;
  [OptionalField]
  private int IdField;
  [OptionalField]
  private string IpAddressField;
  [OptionalField]
  private bool IsCodeSoftField;
  [OptionalField]
  private bool IsDirectField;
  [OptionalField]
  private string ModelField;
  [OptionalField]
  private string ModelTypeField;
  [OptionalField]
  private string NameField;
  [OptionalField]
  private string PortField;
  [OptionalField]
  private string PortNameField;
  [OptionalField]
  private string VersionField;

  [Browsable(false)]
  public ExtensionDataObject ExtensionData
  {
    get => this.extensionDataField;
    set => this.extensionDataField = value;
  }

  [DataMember]
  public int Id
  {
    get => this.IdField;
    set
    {
      if (this.IdField.Equals(value))
        return;
      this.IdField = value;
      this.RaisePropertyChanged(nameof (Id));
    }
  }

  [DataMember]
  public string IpAddress
  {
    get => this.IpAddressField;
    set
    {
      if ((object) this.IpAddressField == (object) value)
        return;
      this.IpAddressField = value;
      this.RaisePropertyChanged(nameof (IpAddress));
    }
  }

  [DataMember]
  public bool IsCodeSoft
  {
    get => this.IsCodeSoftField;
    set
    {
      if (this.IsCodeSoftField.Equals(value))
        return;
      this.IsCodeSoftField = value;
      this.RaisePropertyChanged(nameof (IsCodeSoft));
    }
  }

  [DataMember]
  public bool IsDirect
  {
    get => this.IsDirectField;
    set
    {
      if (this.IsDirectField.Equals(value))
        return;
      this.IsDirectField = value;
      this.RaisePropertyChanged(nameof (IsDirect));
    }
  }

  [DataMember]
  public string Model
  {
    get => this.ModelField;
    set
    {
      if ((object) this.ModelField == (object) value)
        return;
      this.ModelField = value;
      this.RaisePropertyChanged(nameof (Model));
    }
  }

  [DataMember]
  public string ModelType
  {
    get => this.ModelTypeField;
    set
    {
      if ((object) this.ModelTypeField == (object) value)
        return;
      this.ModelTypeField = value;
      this.RaisePropertyChanged(nameof (ModelType));
    }
  }

  [DataMember]
  public string Name
  {
    get => this.NameField;
    set
    {
      if ((object) this.NameField == (object) value)
        return;
      this.NameField = value;
      this.RaisePropertyChanged(nameof (Name));
    }
  }

  [DataMember]
  public string Port
  {
    get => this.PortField;
    set
    {
      if ((object) this.PortField == (object) value)
        return;
      this.PortField = value;
      this.RaisePropertyChanged(nameof (Port));
    }
  }

  [DataMember]
  public string PortName
  {
    get => this.PortNameField;
    set
    {
      if ((object) this.PortNameField == (object) value)
        return;
      this.PortNameField = value;
      this.RaisePropertyChanged(nameof (PortName));
    }
  }

  [DataMember]
  public string Version
  {
    get => this.VersionField;
    set
    {
      if ((object) this.VersionField == (object) value)
        return;
      this.VersionField = value;
      this.RaisePropertyChanged(nameof (Version));
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
