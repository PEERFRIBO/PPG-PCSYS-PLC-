// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_LPS_ProxyConnector.PcsysAppSettings
// Assembly: PCSYS PPG LPS ProxyConnector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 0E676491-4AF1-4945-9FF3-238675E09664
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG LPS ProxyConnector.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

#nullable disable
namespace PCSYS_PPG_LPS_ProxyConnector;

public class PcsysAppSettings
{
  public const string SettingFileName = "AppSettings.xml";

  public static IBSSettings GetSettings(string path, bool allowRoaming, string appName)
  {
    try
    {
      if (path == string.Empty)
        path = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
      string pathName = new DirectoryInfo(path).Name;
      if (pathName == "Debug" || pathName == "Release")
        pathName = appName;
      return PcsysAppSettings.GetMainSettings(Path.Combine(PcsysAppSettings.AllUsersDataFolder(pathName), "AppSettings.xml"), allowRoaming);
    }
    catch (Exception ex)
    {
      return new IBSSettings()
      {
        ErrorMassages = ex.Message
      };
    }
  }

  private static bool HasNoAccess(string fileName)
  {
    try
    {
      File.Open(fileName, FileMode.Append, FileAccess.Write, FileShare.None).Close();
    }
    catch (Exception ex)
    {
      return true;
    }
    return false;
  }

  private static IBSSettings GetFileInfo(string filename)
  {
    if (!File.Exists(filename))
      return PcsysAppSettings.InitilizeNewSettings();
    using (TextReader textReader = (TextReader) new StreamReader(filename))
      return (IBSSettings) new XmlSerializer(typeof (IBSSettings)).Deserialize(textReader);
  }

  public static IBSSettings InitilizeNewSettings()
  {
    List<IbsDataLink> ibsDataLinkList = new List<IbsDataLink>();
    IBSSettings ibsSettings = new IBSSettings()
    {
      Engine = IBSSettings.EngineType.DirectPrint
    };
    ibsSettings.IBSDataLinks = ibsDataLinkList;
    ibsSettings.InfoServiceVersion = "PCSYS";
    ibsSettings.InfoServicesConnections = new List<InfoService>()
    {
      new InfoService()
      {
        Port = "2020",
        ServerName = "localhost",
        Version = "PCSYS"
      }
    };
    return ibsSettings;
  }

  private static IBSSettings GetMainSettings(string filename, bool allowRoaming = false)
  {
    try
    {
      return PcsysAppSettings.GetFileInfo(filename);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message, ex);
    }
  }

  private static string SaveSettings(string path, IBSSettings settings)
  {
    string path1 = Path.Combine(path, "AppSettings.xml");
    XmlSerializer xmlSerializer = new XmlSerializer(settings.GetType());
    TextWriter textWriter = (TextWriter) new StreamWriter(path1);
    xmlSerializer.Serialize(textWriter, (object) settings);
    textWriter.Close();
    return path1;
  }

  public static void SaveSettings(IBSSettings settings, string appName)
  {
    try
    {
      string pathName = new DirectoryInfo(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName).Name;
      if (pathName == "Debug" || pathName == "Release")
        pathName = appName;
      string path = PcsysAppSettings.AllUsersDataFolder(pathName);
      if (settings.UseRoaming)
      {
        PcsysAppSettings.SaveSettings(path, settings);
      }
      else
      {
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
        PcsysAppSettings.SaveSettings(path, settings);
      }
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message, ex);
    }
  }

  public static Guid AssemblyGuid
  {
    get
    {
      return new Guid((Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (GuidAttribute), true)[0] as GuidAttribute).Value);
    }
  }

  public static string AllUsersDataFolder(string pathName)
  {
    string str = string.Empty;
    try
    {
      str = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\PCSYS\\{pathName}\\";
      return PcsysAppSettings.CheckDir(str);
    }
    catch (Exception ex)
    {
      throw new Exception(str, ex);
    }
  }

  private static string CheckDir(string dir)
  {
    if (!Directory.Exists(dir))
      Directory.CreateDirectory(dir);
    return dir;
  }
}
