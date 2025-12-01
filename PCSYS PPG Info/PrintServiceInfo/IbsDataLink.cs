// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.PrintServiceInfo.IbsDataLink
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
[DataContract(Name = "IbsDataLink", Namespace = "http://schemas.datacontract.org/2004/07/PCSYSAppSettings")]
[Serializable]
public class IbsDataLink : IExtensibleDataObject, INotifyPropertyChanged
{
  [NonSerialized]
  private ExtensionDataObject extensionDataField;
  [OptionalField]
  private string ErrorField;
  [OptionalField]
  private string HttpAddressField;
  [OptionalField]
  private bool IsActiveField;
  [OptionalField]
  private DateTime LastErrorTimeField;
  [OptionalField]
  private int PortField;
  [OptionalField]
  private string VersionField;

  [Browsable(false)]
  public ExtensionDataObject ExtensionData
  {
    get => this.extensionDataField;
    set => this.extensionDataField = value;
  }

  [DataMember]
  public string Error
  {
    get => this.ErrorField;
    set
    {
      if ((object) this.ErrorField == (object) value)
        return;
      this.ErrorField = value;
      this.RaisePropertyChanged(nameof (Error));
    }
  }

  [DataMember]
  public string HttpAddress
  {
    get => this.HttpAddressField;
    set
    {
      if ((object) this.HttpAddressField == (object) value)
        return;
      this.HttpAddressField = value;
      this.RaisePropertyChanged(nameof (HttpAddress));
    }
  }

  [DataMember]
  public bool IsActive
  {
    get => this.IsActiveField;
    set
    {
      if (this.IsActiveField.Equals(value))
        return;
      this.IsActiveField = value;
      this.RaisePropertyChanged(nameof (IsActive));
    }
  }

  [DataMember]
  public DateTime LastErrorTime
  {
    get => this.LastErrorTimeField;
    set
    {
      if (this.LastErrorTimeField.Equals(value))
        return;
      this.LastErrorTimeField = value;
      this.RaisePropertyChanged(nameof (LastErrorTime));
    }
  }

  [DataMember]
  public int Port
  {
    get => this.PortField;
    set
    {
      if (this.PortField.Equals(value))
        return;
      this.PortField = value;
      this.RaisePropertyChanged(nameof (Port));
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
