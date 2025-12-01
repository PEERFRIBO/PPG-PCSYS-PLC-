// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_LPS_ProxyConnector.IbsDataLink
// Assembly: PCSYS PPG LPS ProxyConnector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 0E676491-4AF1-4945-9FF3-238675E09664
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG LPS ProxyConnector.dll

using System;


namespace PCSYS_PPG_LPS_ProxyConnector
{

    public class LpsDataLink
    {
        public string HttpAddress { get; set; }

        public string Version { get; set; }

        public int Port { get; set; }

        public bool IsActive { get; set; }

        public string Error { get; set; }

        public DateTime LastErrorTime { get; set; }
    }
}
