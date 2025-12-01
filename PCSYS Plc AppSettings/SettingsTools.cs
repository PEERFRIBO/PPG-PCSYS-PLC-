// Decompiled with JetBrains decompiler
// Type: PCSYS_Plc_AppSettings.SettingsTools
// Assembly: PCSYS Plc AppSettings, Version=1.0.0.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: E34941EA-E4DC-4942-8469-EEE8D175299F
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS Plc AppSettings.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

#nullable disable
namespace PCSYS_Plc_AppSettings;

public class SettingsTools
{
  public const string SettingFileName = "AppSettings.xml";
  public static Settings MainSettings;
  public static List<Register> Registers;
  public static bool WaitForUserInput;

  public Settings GetSettings(string appName)
  {
    string path = Path.Combine(SettingsTools.GetApplicationDataPath(), "AppSettings.xml");
    if (!File.Exists(path))
      return new Settings();
    using (TextReader textReader = (TextReader) new StreamReader(path))
      return (Settings) new XmlSerializer(typeof (Settings)).Deserialize(textReader);
  }

  public static string GetApplicationDataPath()
  {
    string pathName = new DirectoryInfo(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName).Name;
    if (pathName == "Debug" || pathName == "Release")
      pathName = "TECSYS PLC Server";
    return SettingsTools.AllUsersDataFolder(pathName);
  }

  public static string SaveSettings(Settings settings)
  {
    string applicationDataPath = SettingsTools.GetApplicationDataPath();
    string path = Path.Combine(applicationDataPath, "AppSettings.xml");
    string str = Path.Combine(applicationDataPath, "AppSettings.xml");
    XmlSerializer xmlSerializer = new XmlSerializer(settings.GetType());
    TextWriter textWriter = (TextWriter) new StreamWriter(path);
    xmlSerializer.Serialize(textWriter, (object) settings);
    textWriter.Close();
    return str;
  }

  private static string CheckDir(string dir)
  {
    if (!Directory.Exists(dir))
      Directory.CreateDirectory(dir);
    return dir;
  }

  public static string AllUsersDataFolder(string pathName)
  {
    string str = string.Empty;
    try
    {
      str = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\TECSYS\\{pathName}\\";
      return SettingsTools.CheckDir(str);
    }
    catch (Exception ex)
    {
      throw new Exception(str, ex);
    }
  }
}
