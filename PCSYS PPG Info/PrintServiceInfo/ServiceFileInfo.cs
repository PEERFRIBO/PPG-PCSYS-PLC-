// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.PrintServiceInfo.ServiceFileInfo
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
[DataContract(Name = "ServiceFileInfo", Namespace = "http://schemas.datacontract.org/2004/07/IBSInfoService")]
[Serializable]
public class ServiceFileInfo : IExtensibleDataObject, INotifyPropertyChanged
{
  [NonSerialized]
  private ExtensionDataObject extensionDataField;
  [OptionalField]
  private string CreatedField;
  [OptionalField]
  private string ErrorField;
  [OptionalField]
  private string InstalledField;
  [OptionalField]
  private string NameField;
  [OptionalField]
  private string PathField;
  [OptionalField]
  private string StartModeField;
  [OptionalField]
  private string VersionField;

  [Browsable(false)]
  public ExtensionDataObject ExtensionData
  {
    get => this.extensionDataField;
    set => this.extensionDataField = value;
  }

  [DataMember]
  public string Created
  {
    get => this.CreatedField;
    set
    {
      if ((object) this.CreatedField == (object) value)
        return;
      this.CreatedField = value;
      this.RaisePropertyChanged(nameof (Created));
    }
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
  public string Installed
  {
    get => this.InstalledField;
    set
    {
      if ((object) this.InstalledField == (object) value)
        return;
      this.InstalledField = value;
      this.RaisePropertyChanged(nameof (Installed));
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
  public string Path
  {
    get => this.PathField;
    set
    {
      if ((object) this.PathField == (object) value)
        return;
      this.PathField = value;
      this.RaisePropertyChanged(nameof (Path));
    }
  }

  [DataMember]
  public string StartMode
  {
    get => this.StartModeField;
    set
    {
      if ((object) this.StartModeField == (object) value)
        return;
      this.StartModeField = value;
      this.RaisePropertyChanged(nameof (StartMode));
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
