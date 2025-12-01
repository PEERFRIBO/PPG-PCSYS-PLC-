// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.Code.RawPrintApi
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;

#nullable disable
namespace PCSYS_PLC_Server.Code;

internal static class RawPrintApi
{
  public const int PRINTER_ACCESS_ADMINISTER = 4;
  public const int PRINTER_STATUS_OFFLINE = 128 /*0x80*/;
  public const int PRINTER_CONTROL_PAUSE = 1;
  public const int PRINTER_CONTROL_RESUME = 2;
  public const int PRINTER_CONTROL_PURGE = 3;
  public const int PRINTER_CONTROL_SET_STATUS = 4;

  public static string[] GetPrinters()
  {
    NativeMethods.PrinterEnumFlags flags = NativeMethods.PrinterEnumFlags.PRINTER_ENUM_LOCAL | NativeMethods.PrinterEnumFlags.PRINTER_ENUM_CONNECTIONS;
    uint pcbNeeded = 0;
    uint pcReturned = 0;
    if (NativeMethods.EnumPrinters(flags, (string) null, 2U, IntPtr.Zero, 0U, ref pcbNeeded, ref pcReturned))
      return new string[0];
    int lastWin32Error = Marshal.GetLastWin32Error();
    if (lastWin32Error != 122)
      throw new RawPrintApi.Win32Exception(lastWin32Error);
    GCHandle gcHandle = GCHandle.Alloc((object) new byte[(int) pcbNeeded], GCHandleType.Pinned);
    try
    {
      IntPtr pPrinterEnum = gcHandle.AddrOfPinnedObject();
      if (!NativeMethods.EnumPrinters(flags, (string) null, 2U, pPrinterEnum, pcbNeeded, ref pcbNeeded, ref pcReturned))
        throw new RawPrintApi.Win32Exception(Marshal.GetLastWin32Error());
      NativeMethods.PRINTER_INFO_2[] source = new NativeMethods.PRINTER_INFO_2[(int) pcReturned];
      int num = Marshal.SizeOf(typeof (NativeMethods.PRINTER_INFO_2));
      for (int index = 0; (long) index < (long) pcReturned; ++index)
      {
        IntPtr ptr = Marshal.SizeOf(typeof (IntPtr)) <= 4 ? new IntPtr(pPrinterEnum.ToInt32() + index * num) : new IntPtr(pPrinterEnum.ToInt64() + (long) (index * num));
        NativeMethods.PRINTER_INFO_2 structure = new NativeMethods.PRINTER_INFO_2();
        Marshal.PtrToStructure(ptr, (object) structure);
        source[index] = structure;
      }
      return ((IEnumerable<NativeMethods.PRINTER_INFO_2>) source).Select<NativeMethods.PRINTER_INFO_2, string>((Func<NativeMethods.PRINTER_INFO_2, string>) (printerInfo => printerInfo.pPrinterName)).ToArray<string>();
    }
    finally
    {
      gcHandle.Free();
    }
  }

  private static void PausePrinter(string printerName)
  {
    IntPtr phPrinter;
    if (!NativeMethods.OpenPrinter(printerName, out phPrinter, IntPtr.Zero) || NativeMethods.SetPrinter(phPrinter, 0, IntPtr.Zero, 1))
      ;
  }

  private static void ResumePrinter(string printerName)
  {
    IntPtr phPrinter;
    if (!NativeMethods.OpenPrinter(printerName, out phPrinter, IntPtr.Zero) || NativeMethods.SetPrinter(phPrinter, 0, IntPtr.Zero, 2))
      ;
  }

  public static string GetPrinterStatus(string printerName)
  {
    NativeMethods.PrinterEnumFlags flags = NativeMethods.PrinterEnumFlags.PRINTER_ENUM_LOCAL | NativeMethods.PrinterEnumFlags.PRINTER_ENUM_CONNECTIONS;
    uint pcbNeeded = 0;
    uint pcReturned = 0;
    if (NativeMethods.EnumPrinters(flags, (string) null, 2U, IntPtr.Zero, 0U, ref pcbNeeded, ref pcReturned))
      return string.Empty;
    int lastWin32Error = Marshal.GetLastWin32Error();
    if (lastWin32Error != 122)
      throw new RawPrintApi.Win32Exception(lastWin32Error);
    GCHandle gcHandle = GCHandle.Alloc((object) new byte[(int) pcbNeeded], GCHandleType.Pinned);
    try
    {
      IntPtr pPrinterEnum = gcHandle.AddrOfPinnedObject();
      if (!NativeMethods.EnumPrinters(flags, (string) null, 2U, pPrinterEnum, pcbNeeded, ref pcbNeeded, ref pcReturned))
        throw new RawPrintApi.Win32Exception(Marshal.GetLastWin32Error());
      NativeMethods.PRINTER_INFO_2[] printerInfo2Array = new NativeMethods.PRINTER_INFO_2[(int) pcReturned];
      int num = Marshal.SizeOf(typeof (NativeMethods.PRINTER_INFO_2));
      for (int index = 0; (long) index < (long) pcReturned; ++index)
      {
        IntPtr ptr = Marshal.SizeOf(typeof (IntPtr)) <= 4 ? new IntPtr(pPrinterEnum.ToInt32() + index * num) : new IntPtr(pPrinterEnum.ToInt64() + (long) (index * num));
        NativeMethods.PRINTER_INFO_2 structure = new NativeMethods.PRINTER_INFO_2();
        Marshal.PtrToStructure(ptr, (object) structure);
        if (printerName == structure.pPrinterName)
          return Enum.GetName(typeof (RawPrintApi.Printer_Status), (object) Convert.ToInt32(structure.Status));
      }
      return string.Empty;
    }
    finally
    {
      gcHandle.Free();
    }
  }

  [SecuritySafeCritical]
  public static void SendBytesToPrinter(string printerName, byte[] pBytes, string description)
  {
    IntPtr phPrinter;
    if (!NativeMethods.OpenPrinter(printerName, out phPrinter, IntPtr.Zero))
      throw new RawPrintApi.Win32Exception(Marshal.GetLastWin32Error());
    try
    {
      NativeMethods.DOC_INFO di = new NativeMethods.DOC_INFO()
      {
        pDocName = description,
        pDataType = "RAW"
      };
      if (!NativeMethods.StartDocPrinter(phPrinter, 1, ref di))
        throw new RawPrintApi.Win32Exception(Marshal.GetLastWin32Error());
      try
      {
        if (!NativeMethods.StartPagePrinter(phPrinter))
          throw new RawPrintApi.Win32Exception(Marshal.GetLastWin32Error());
        try
        {
          NativeMethods.WritePrinter(phPrinter, pBytes, (uint) pBytes.Length, out uint _);
        }
        finally
        {
          NativeMethods.EndPagePrinter(phPrinter);
        }
      }
      finally
      {
        NativeMethods.EndDocPrinter(phPrinter);
      }
    }
    finally
    {
      NativeMethods.ClosePrinter(phPrinter);
    }
  }

  public static void SendStringToPrinter(string szPrinterName, string pDoc)
  {
    Windows1252Encoding windows1252Encoding = new Windows1252Encoding();
    pDoc = RawPrintApi.TweakZPLString(pDoc);
    byte[] bytes = windows1252Encoding.GetBytes(pDoc);
    RawPrintApi.SendBytesToPrinter(szPrinterName, bytes, "LPS Print");
  }

  private static string TweakZPLString(string ZPLString)
  {
    return ZPLString.Replace("^CI0", "^CI27").Replace("^XA", "^XA^CFD,24");
  }

  public static string GetDriverName(string printerName)
  {
    NativeMethods.PrinterEnumFlags flags = NativeMethods.PrinterEnumFlags.PRINTER_ENUM_LOCAL | NativeMethods.PrinterEnumFlags.PRINTER_ENUM_CONNECTIONS;
    uint pcbNeeded = 0;
    uint pcReturned = 0;
    if (NativeMethods.EnumPrinters(flags, (string) null, 2U, IntPtr.Zero, 0U, ref pcbNeeded, ref pcReturned))
      return string.Empty;
    int lastWin32Error = Marshal.GetLastWin32Error();
    if (lastWin32Error != 122)
      throw new RawPrintApi.Win32Exception(lastWin32Error);
    GCHandle gcHandle = GCHandle.Alloc((object) new byte[(int) pcbNeeded], GCHandleType.Pinned);
    try
    {
      IntPtr pPrinterEnum = gcHandle.AddrOfPinnedObject();
      if (!NativeMethods.EnumPrinters(flags, (string) null, 2U, pPrinterEnum, pcbNeeded, ref pcbNeeded, ref pcReturned))
        return Marshal.GetLastWin32Error().ToString();
      NativeMethods.PRINTER_INFO_2[] printerInfo2Array = new NativeMethods.PRINTER_INFO_2[(int) pcReturned];
      int num = Marshal.SizeOf(typeof (NativeMethods.PRINTER_INFO_2));
      for (int index = 0; (long) index < (long) pcReturned; ++index)
      {
        IntPtr ptr = Marshal.SizeOf(typeof (IntPtr)) <= 4 ? new IntPtr(pPrinterEnum.ToInt32() + index * num) : new IntPtr(pPrinterEnum.ToInt64() + (long) (index * num));
        NativeMethods.PRINTER_INFO_2 structure = new NativeMethods.PRINTER_INFO_2();
        Marshal.PtrToStructure(ptr, (object) structure);
        printerInfo2Array[index] = structure;
        if (structure.pPrinterName == printerName)
          return structure.pDriverName;
      }
    }
    finally
    {
      gcHandle.Free();
    }
    return string.Empty;
  }

  public enum Printer_Status
  {
    PRINTER_STATUS_READY = 0,
    PRINTER_STATUS_PAUSED = 1,
    PRINTER_STATUS_ERROR = 2,
    PRINTER_STATUS_PENDING_DELETION = 4,
    PRINTER_STATUS_PAPER_JAM = 8,
    PRINTER_STATUS_PAPER_OUT = 16, // 0x00000010
    PRINTER_STATUS_MANUAL_FEED = 32, // 0x00000020
    PRINTER_STATUS_PAPER_PROBLEM = 64, // 0x00000040
    PRINTER_STATUS_OFFLINE = 128, // 0x00000080
    PRINTER_STATUS_OFFLINE_ = 129, // 0x00000081
    PRINTER_STATUS_IO_ACTIVE = 256, // 0x00000100
    PRINTER_STATUS_BUSY = 512, // 0x00000200
    PRINTER_STATUS_PRINTING = 1024, // 0x00000400
    PRINTER_STATUS_OUTPUT_BIN_FULL = 2048, // 0x00000800
    PRINTER_STATUS_NOT_AVAILABLE = 4096, // 0x00001000
    PRINTER_STATUS_WAITING = 8192, // 0x00002000
    PRINTER_STATUS_PROCESSING = 16384, // 0x00004000
    PRINTER_STATUS_INITIALIZING = 32768, // 0x00008000
    PRINTER_STATUS_WARMING_UP = 65536, // 0x00010000
    PRINTER_STATUS_TONER_LOW = 131072, // 0x00020000
    PRINTER_STATUS_NO_TONER = 262144, // 0x00040000
    PRINTER_STATUS_PAGE_PUNT = 524288, // 0x00080000
    PRINTER_STATUS_USER_INTERVENTION = 1048576, // 0x00100000
    PRINTER_STATUS_OUT_OF_MEMORY = 2097152, // 0x00200000
    PRINTER_STATUS_DOOR_OPEN = 4194304, // 0x00400000
    PRINTER_STATUS_SERVER_UNKNOWN = 8388608, // 0x00800000
    PRINTER_STATUS_POWER_SAVE = 16777216, // 0x01000000
  }

  public class Win32Exception(int error) : Exception("Error code: " + (object) error)
  {
  }
}
