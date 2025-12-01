// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.PrintServiceInfo.PrintFamilie
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
[DataContract(Name = "PrintFamilie", Namespace = "http://schemas.datacontract.org/2004/07/PCSYSPrintManager.Code")]
[Serializable]
public class PrintFamilie : IExtensibleDataObject, INotifyPropertyChanged
{
  [NonSerialized]
  private ExtensionDataObject extensionDataField;
  private string Familiek__BackingFieldField;
  private string[] Modelsk__BackingFieldField;

  [Browsable(false)]
  public ExtensionDataObject ExtensionData
  {
    get => this.extensionDataField;
    set => this.extensionDataField = value;
  }

  [DataMember(Name = "<Familie>k__BackingField", IsRequired = true)]
  public string Familiek__BackingField
  {
    get => this.Familiek__BackingFieldField;
    set
    {
      if ((object) this.Familiek__BackingFieldField == (object) value)
        return;
      this.Familiek__BackingFieldField = value;
      this.RaisePropertyChanged(nameof (Familiek__BackingField));
    }
  }

  [DataMember(Name = "<Models>k__BackingField", IsRequired = true)]
  public string[] Modelsk__BackingField
  {
    get => this.Modelsk__BackingFieldField;
    set
    {
      if (this.Modelsk__BackingFieldField == value)
        return;
      this.Modelsk__BackingFieldField = value;
      this.RaisePropertyChanged(nameof (Modelsk__BackingField));
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
