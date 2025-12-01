// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.Code.NativeMethods
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace PCSYS_PLC_Server.Code;

internal static class NativeMethods
{
  public const int ERROR_INSUFFICIENT_BUFFER = 122;

  [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool OpenPrinter(
    string pPrinterName,
    out IntPtr phPrinter,
    ref NativeMethods.PRINTER_DEFAULTS pDefault);

  [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

  [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int ClosePrinter(IntPtr hPrinter);

  [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool StartDocPrinter(
    IntPtr hPrinter,
    int level,
    ref NativeMethods.DOC_INFO di);

  [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool EndDocPrinter(IntPtr hPrinter);

  [DllImport("winspool.drv")]
  public static extern bool StartPagePrinter(IntPtr hPrinter);

  [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool WritePrinter(
    IntPtr hPrinter,
    byte[] pBytes,
    uint bytesCount,
    out uint dwWritten);

  [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool EndPagePrinter(IntPtr hPrinter);

  [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool EnumPrinters(
    NativeMethods.PrinterEnumFlags flags,
    string name,
    uint level,
    IntPtr pPrinterEnum,
    uint cbBuf,
    ref uint pcbNeeded,
    ref uint pcReturned);

  [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool GetPrinter(
    IntPtr hPrinter,
    int dwLevel,
    IntPtr pPrinter,
    int dwBuf,
    out int dwNeeded);

  [DllImport("winspool.drv", CharSet = CharSet.Ansi, SetLastError = true)]
  public static extern bool SetPrinter(IntPtr hPrinter, int level, IntPtr pPrinter, int command);

  [Flags]
  public enum PrinterEnumFlags
  {
    PRINTER_ENUM_DEFAULT = 1,
    PRINTER_ENUM_LOCAL = 2,
    PRINTER_ENUM_CONNECTIONS = 4,
    PRINTER_ENUM_FAVORITE = PRINTER_ENUM_CONNECTIONS, // 0x00000004
    PRINTER_ENUM_NAME = 8,
    PRINTER_ENUM_REMOTE = 16, // 0x00000010
    PRINTER_ENUM_SHARED = 32, // 0x00000020
    PRINTER_ENUM_NETWORK = 64, // 0x00000040
    PRINTER_ENUM_EXPAND = 16384, // 0x00004000
    PRINTER_ENUM_CONTAINER = 32768, // 0x00008000
    PRINTER_ENUM_ICONMASK = 16711680, // 0x00FF0000
    PRINTER_ENUM_ICON1 = 65536, // 0x00010000
    PRINTER_ENUM_ICON2 = 131072, // 0x00020000
    PRINTER_ENUM_ICON3 = 262144, // 0x00040000
    PRINTER_ENUM_ICON4 = 524288, // 0x00080000
    PRINTER_ENUM_ICON5 = 1048576, // 0x00100000
    PRINTER_ENUM_ICON6 = 2097152, // 0x00200000
    PRINTER_ENUM_ICON7 = 4194304, // 0x00400000
    PRINTER_ENUM_ICON8 = 8388608, // 0x00800000
    PRINTER_ENUM_HIDE = 16777216, // 0x01000000
  }

  public struct DOC_INFO
  {
    [MarshalAs(UnmanagedType.LPTStr)]
    public string pDocName;
    [MarshalAs(UnmanagedType.LPTStr)]
    public string pOutputFile;
    [MarshalAs(UnmanagedType.LPTStr)]
    public string pDataType;
  }

  public struct PRINTER_DEFAULTS
  {
    public IntPtr pDatatype;
    public IntPtr pDevMode;
    public int DesiredAccess;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  public class PRINTER_INFO_1
  {
    public uint flags;
    public string description;
    public string name;
    public string comment;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  public class PRINTER_INFO_2
  {
    public string pServerName;
    public string pPrinterName;
    public string pShareName;
    public string pPortName;
    public string pDriverName;
    public string pComment;
    public string pLocation;
    public IntPtr pDevMode;
    public string pSepFile;
    public string pPrintProcessor;
    public string pDatatype;
    public string pParameters;
    public IntPtr pSecurityDescriptor;
    public uint Attributes;
    public uint Priority;
    public uint DefaultPriority;
    public uint StartTime;
    public uint UntilTime;
    public uint Status;
    public uint cJobs;
    public uint AveragePPM;
  }
}
