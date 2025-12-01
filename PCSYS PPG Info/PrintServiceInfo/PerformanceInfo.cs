// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.PrintServiceInfo.PerformanceInfo
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
[DataContract(Name = "PerformanceInfo", Namespace = "http://schemas.datacontract.org/2004/07/PCSYS_PerformanceInformations")]
[Serializable]
public class PerformanceInfo : IExtensibleDataObject, INotifyPropertyChanged
{
  [NonSerialized]
  private ExtensionDataObject extensionDataField;
  [OptionalField]
  private string CpuField;
  [OptionalField]
  private double CpuHistoryField;
  [OptionalField]
  private string CpuProcentField;
  [OptionalField]
  private string DiskInfoField;
  [OptionalField]
  private double DiskRField;
  [OptionalField]
  private double DiskWField;
  [OptionalField]
  private string ErrorField;
  [OptionalField]
  private int MemoryPField;
  [OptionalField]
  private string MemoryPProcentField;
  [OptionalField]
  private int MemoryVField;
  [OptionalField]
  private string MemoryVProcentField;
  [OptionalField]
  private double NetIField;
  [OptionalField]
  private double NetOField;
  [OptionalField]
  private string OsNameField;
  [OptionalField]
  private string ProcessorField;
  [OptionalField]
  private string UserNameField;

  [Browsable(false)]
  public ExtensionDataObject ExtensionData
  {
    get => this.extensionDataField;
    set => this.extensionDataField = value;
  }

  [DataMember]
  public string Cpu
  {
    get => this.CpuField;
    set
    {
      if ((object) this.CpuField == (object) value)
        return;
      this.CpuField = value;
      this.RaisePropertyChanged(nameof (Cpu));
    }
  }

  [DataMember]
  public double CpuHistory
  {
    get => this.CpuHistoryField;
    set
    {
      if (this.CpuHistoryField.Equals(value))
        return;
      this.CpuHistoryField = value;
      this.RaisePropertyChanged(nameof (CpuHistory));
    }
  }

  [DataMember]
  public string CpuProcent
  {
    get => this.CpuProcentField;
    set
    {
      if ((object) this.CpuProcentField == (object) value)
        return;
      this.CpuProcentField = value;
      this.RaisePropertyChanged(nameof (CpuProcent));
    }
  }

  [DataMember]
  public string DiskInfo
  {
    get => this.DiskInfoField;
    set
    {
      if ((object) this.DiskInfoField == (object) value)
        return;
      this.DiskInfoField = value;
      this.RaisePropertyChanged(nameof (DiskInfo));
    }
  }

  [DataMember]
  public double DiskR
  {
    get => this.DiskRField;
    set
    {
      if (this.DiskRField.Equals(value))
        return;
      this.DiskRField = value;
      this.RaisePropertyChanged(nameof (DiskR));
    }
  }

  [DataMember]
  public double DiskW
  {
    get => this.DiskWField;
    set
    {
      if (this.DiskWField.Equals(value))
        return;
      this.DiskWField = value;
      this.RaisePropertyChanged(nameof (DiskW));
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
  public int MemoryP
  {
    get => this.MemoryPField;
    set
    {
      if (this.MemoryPField.Equals(value))
        return;
      this.MemoryPField = value;
      this.RaisePropertyChanged(nameof (MemoryP));
    }
  }

  [DataMember]
  public string MemoryPProcent
  {
    get => this.MemoryPProcentField;
    set
    {
      if ((object) this.MemoryPProcentField == (object) value)
        return;
      this.MemoryPProcentField = value;
      this.RaisePropertyChanged(nameof (MemoryPProcent));
    }
  }

  [DataMember]
  public int MemoryV
  {
    get => this.MemoryVField;
    set
    {
      if (this.MemoryVField.Equals(value))
        return;
      this.MemoryVField = value;
      this.RaisePropertyChanged(nameof (MemoryV));
    }
  }

  [DataMember]
  public string MemoryVProcent
  {
    get => this.MemoryVProcentField;
    set
    {
      if ((object) this.MemoryVProcentField == (object) value)
        return;
      this.MemoryVProcentField = value;
      this.RaisePropertyChanged(nameof (MemoryVProcent));
    }
  }

  [DataMember]
  public double NetI
  {
    get => this.NetIField;
    set
    {
      if (this.NetIField.Equals(value))
        return;
      this.NetIField = value;
      this.RaisePropertyChanged(nameof (NetI));
    }
  }

  [DataMember]
  public double NetO
  {
    get => this.NetOField;
    set
    {
      if (this.NetOField.Equals(value))
        return;
      this.NetOField = value;
      this.RaisePropertyChanged(nameof (NetO));
    }
  }

  [DataMember]
  public string OsName
  {
    get => this.OsNameField;
    set
    {
      if ((object) this.OsNameField == (object) value)
        return;
      this.OsNameField = value;
      this.RaisePropertyChanged(nameof (OsName));
    }
  }

  [DataMember]
  public string Processor
  {
    get => this.ProcessorField;
    set
    {
      if ((object) this.ProcessorField == (object) value)
        return;
      this.ProcessorField = value;
      this.RaisePropertyChanged(nameof (Processor));
    }
  }

  [DataMember]
  public string UserName
  {
    get => this.UserNameField;
    set
    {
      if ((object) this.UserNameField == (object) value)
        return;
      this.UserNameField = value;
      this.RaisePropertyChanged(nameof (UserName));
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
