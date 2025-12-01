// Decompiled with JetBrains decompiler
// Type: PCSYS_Event_Log.LogTools
// Assembly: PCSYS Event Log, Version=1.0.0.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 49CEB70B-1C23-4C42-A5AE-DE3C1C126E6E
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS Event Log.dll

using PCSYS_PPG_LPS_ProxyConnector;
using PCSYS_PPG_LPS_ProxyConnector.DataService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;


namespace PCSYS_Event_Log
{
    //Change C# language version to 7.3 to support this code
    public static class LogTools
    {
        private static readonly object Templock = new object();

        public static void WriteToEventLog(
          string text,
          EventLogEntryType logEntryType,
          int lineId,
          LogTools.Operation operation,
          List<LpsDataLink> links)
        {
            lock (LogTools.Templock)
            {
                MethodBase method = new StackTrace().GetFrame(1).GetMethod();
                string str1 = method.DeclaringType != (Type)null ? method.DeclaringType.FullName : string.Empty;
                string str2 = str1;
                string str3 = string.Empty;
                if (str2 != null && str2.IndexOf(".", StringComparison.Ordinal) > 0)
                {
                    str2 = str2.IndexOf(".", StringComparison.Ordinal) > 0 ? str1.Substring(0, str2.IndexOf(".", StringComparison.Ordinal)) : str1;
                    str3 = str1.Length - 1 > str2.Length ? str1.Substring(str2.Length + 1, str1.Length - 1 - str2.Length) : string.Empty;
                }
                List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
      {
        new IBSExternalParameter()
        {
          Name = "@applicationname",
          Value = (object) str2
        },
        new IBSExternalParameter()
        {
          Name = "@source",
          Value = (object) str3
        },
        new IBSExternalParameter()
        {
          Name = "@message",
          Value = (object) text
        },
        new IBSExternalParameter()
        {
          Name = "@logdatetime",
          Value = (object) DateTime.Now
        }
      };
                new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>)links).ExecuteNonQueryWithParameters("INSERT INTO tppgeventlog (applicationname,eventlogentrytype, " + $"source,message,operation,lineid,logdatetime) VALUES (@applicationname,{(int)logEntryType},@source,@message,{(int)operation},{lineId},@logdatetime)", externalParameterList.ToArray());
            }
        }

        public static void WriteToEventLog(
          string text,
          EventLogEntryType logEntryType,
          string applicationName,
          int lineId,
          LogTools.Operation operation)
        {
            lock (LogTools.Templock)
            {
                string str = Path.Combine(LogTools.AllUsersDataFolder(applicationName), "Logs");
                if (!Directory.Exists(str))
                    Directory.CreateDirectory(str);
                using (StreamWriter streamWriter = File.AppendText(Path.Combine(str, $"Log{DateTime.Now:dd-MM-yyyy}.txt")))
                    streamWriter.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4:HH:mm:ss}", (object)text, (object)Enum.GetName(typeof(EventLogEntryType), (object)logEntryType), (object)(int)operation, (object)lineId, (object)DateTime.Now));
            }
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
                str = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\PCSYS\\{pathName}\\";
                return LogTools.CheckDir(str);
            }
            catch (Exception ex)
            {
                throw new Exception(str, ex);
            }
        }

        public enum Operation
        {
            Printing,
            Document,
            System,
            Plc,
            IDoc,
        }
    }
}