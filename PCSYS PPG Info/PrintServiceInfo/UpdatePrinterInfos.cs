// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.PrintServiceInfo.UpdatePrinterInfos
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
[DataContract(Name = "UpdatePrinterInfos", Namespace = "http://schemas.datacontract.org/2004/07/IBSPrinterManager")]
[Serializable]
public class UpdatePrinterInfos : IExtensibleDataObject, INotifyPropertyChanged
{
  [NonSerialized]
  private ExtensionDataObject extensionDataField;
  [OptionalField]
  private UpdatePrinterInfo[] AddPrintersField;
  [OptionalField]
  private string ErrorMessagesField;
  [OptionalField]
  private UpdatePrinterInfo[] RemovePrintersField;
  [OptionalField]
  private UpdatePrinterInfo[] UpdatePrintersField;
  [OptionalField]
  private InfoService UrlField;

  [Browsable(false)]
  public ExtensionDataObject ExtensionData
  {
    get => this.extensionDataField;
    set => this.extensionDataField = value;
  }

  [DataMember]
  public UpdatePrinterInfo[] AddPrinters
  {
    get => this.AddPrintersField;
    set
    {
      if (this.AddPrintersField == value)
        return;
      this.AddPrintersField = value;
      this.RaisePropertyChanged(nameof (AddPrinters));
    }
  }

  [DataMember]
  public string ErrorMessages
  {
    get => this.ErrorMessagesField;
    set
    {
      if ((object) this.ErrorMessagesField == (object) value)
        return;
      this.ErrorMessagesField = value;
      this.RaisePropertyChanged(nameof (ErrorMessages));
    }
  }

  [DataMember]
  public UpdatePrinterInfo[] RemovePrinters
  {
    get => this.RemovePrintersField;
    set
    {
      if (this.RemovePrintersField == value)
        return;
      this.RemovePrintersField = value;
      this.RaisePropertyChanged(nameof (RemovePrinters));
    }
  }

  [DataMember]
  public UpdatePrinterInfo[] UpdatePrinters
  {
    get => this.UpdatePrintersField;
    set
    {
      if (this.UpdatePrintersField == value)
        return;
      this.UpdatePrintersField = value;
      this.RaisePropertyChanged(nameof (UpdatePrinters));
    }
  }

  [DataMember]
  public InfoService Url
  {
    get => this.UrlField;
    set
    {
      if (this.UrlField == value)
        return;
      this.UrlField = value;
      this.RaisePropertyChanged(nameof (Url));
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
