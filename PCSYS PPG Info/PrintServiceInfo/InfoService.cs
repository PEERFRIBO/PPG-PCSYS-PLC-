// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.PrintServiceInfo.InfoService
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
[DataContract(Name = "InfoService", Namespace = "http://schemas.datacontract.org/2004/07/PCSYSAppSettings")]
[Serializable]
public class InfoService : IExtensibleDataObject, INotifyPropertyChanged
{
  [NonSerialized]
  private ExtensionDataObject extensionDataField;
  [OptionalField]
  private string PortField;
  [OptionalField]
  private string ServerNameField;
  [OptionalField]
  private string VersionField;

  [Browsable(false)]
  public ExtensionDataObject ExtensionData
  {
    get => this.extensionDataField;
    set => this.extensionDataField = value;
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
  public string ServerName
  {
    get => this.ServerNameField;
    set
    {
      if ((object) this.ServerNameField == (object) value)
        return;
      this.ServerNameField = value;
      this.RaisePropertyChanged(nameof (ServerName));
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
