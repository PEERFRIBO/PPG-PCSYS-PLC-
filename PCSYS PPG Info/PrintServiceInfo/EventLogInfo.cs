// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.PrintServiceInfo.EventLogInfo
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
[DataContract(Name = "EventLogInfo", Namespace = "http://schemas.datacontract.org/2004/07/PCSYSEventLogs")]
[Serializable]
public class EventLogInfo : IExtensibleDataObject, INotifyPropertyChanged
{
  [NonSerialized]
  private ExtensionDataObject extensionDataField;
  [OptionalField]
  private string LogSourceField;
  [OptionalField]
  private EventLogEntryType LogTypeField;
  [OptionalField]
  private string MessagesField;
  [OptionalField]
  private DateTime TimeField;

  [Browsable(false)]
  public ExtensionDataObject ExtensionData
  {
    get => this.extensionDataField;
    set => this.extensionDataField = value;
  }

  [DataMember]
  public string LogSource
  {
    get => this.LogSourceField;
    set
    {
      if ((object) this.LogSourceField == (object) value)
        return;
      this.LogSourceField = value;
      this.RaisePropertyChanged(nameof (LogSource));
    }
  }

  [DataMember]
  public EventLogEntryType LogType
  {
    get => this.LogTypeField;
    set
    {
      if (this.LogTypeField.Equals((object) value))
        return;
      this.LogTypeField = value;
      this.RaisePropertyChanged(nameof (LogType));
    }
  }

  [DataMember]
  public string Messages
  {
    get => this.MessagesField;
    set
    {
      if ((object) this.MessagesField == (object) value)
        return;
      this.MessagesField = value;
      this.RaisePropertyChanged(nameof (Messages));
    }
  }

  [DataMember]
  public DateTime Time
  {
    get => this.TimeField;
    set
    {
      if (this.TimeField.Equals(value))
        return;
      this.TimeField = value;
      this.RaisePropertyChanged(nameof (Time));
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
