// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_LPS_ProxyConnector.IBSExchangeData
// Assembly: PCSYS PPG LPS ProxyConnector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 0E676491-4AF1-4945-9FF3-238675E09664
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG LPS ProxyConnector.dll

using PCSYS_PPG_LPS_ProxyConnector.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Xml;

namespace PCSYS_PPG_LPS_ProxyConnector
{

    public class IBSExchangeData
    {
        public static LpsDataLink GetActiveLink()
        {
            return IBSExchangeData.GetActiveLink((IEnumerable<LpsDataLink>)null);
        }

        public static LpsDataLink GetActiveLink(IEnumerable<LpsDataLink> links)
        {

            

            if (links == null)
                throw new Exception("No connection found");

            if (!(links is IList<LpsDataLink> ibsDataLinkList))
                ibsDataLinkList = (IList<LpsDataLink>)links.ToList<LpsDataLink>();
            IList<LpsDataLink> source = ibsDataLinkList;
            LpsDataLink link1 = source.FirstOrDefault<LpsDataLink>((Func<LpsDataLink, bool>)(link => link.IsActive));
            for (int index = 0; index < 5; ++index)
            {
                try
                {
                    IBSExchangeData.GetInnerConnection(link1).CheckConnection(string.Empty);
                    return link1;
                }
                catch (Exception ex)
                {
                    foreach (LpsDataLink ibsDataLink in source.Where<LpsDataLink>((Func<LpsDataLink, bool>)(link => link.IsActive)))
                    {
                        ibsDataLink.Error = ex.Message;
                        ibsDataLink.IsActive = false;
                        ibsDataLink.LastErrorTime = DateTime.Now;
                    }
                    link1 = source != null ? source.FirstOrDefault<LpsDataLink>((Func<LpsDataLink, bool>)(link => !link.IsActive)) : (LpsDataLink)null;
                    if (link1 != null)
                    {
                        link1.IsActive = true;
                        return link1;
                    }
                    Thread.Sleep(1000);
                    foreach (LpsDataLink ibsDataLink in (IEnumerable<LpsDataLink>)source)
                        ibsDataLink.IsActive = true;
                }
            }
            return (LpsDataLink)null;
        }

        public AppServiceClient GetConnection(IEnumerable<LpsDataLink> links)
        {
            LpsDataLink activeLink = IBSExchangeData.GetActiveLink(links);
            if (activeLink == null)
                throw new Exception("No data link is found");
            try
            {
                return IBSExchangeData.GetInnerConnection(activeLink);
            }
            catch (Exception ex)
            {
                Thread.Sleep(2000);
            }
            return (AppServiceClient)null;
        }

        public static AppServiceClient GetInnerConnection(LpsDataLink link)
        {
            try
            {
                if (link == null)
                    throw new Exception("No data link is found");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                basicHttpBinding.ReaderQuotas = IBSExchangeData.SetQuotas();
                basicHttpBinding.MaxReceivedMessageSize = (long)int.MaxValue;
                return new AppServiceClient((Binding)basicHttpBinding, new EndpointAddress(new Uri($"http://{link.HttpAddress}:{link.Port}/{link.Version}/DATASERVICE"), new AddressHeader[0]));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private static XmlDictionaryReaderQuotas SetQuotas()
        {
            return new XmlDictionaryReaderQuotas()
            {
                MaxArrayLength = int.MaxValue,
                MaxBytesPerRead = int.MaxValue,
                MaxDepth = int.MaxValue,
                MaxNameTableCharCount = int.MaxValue,
                MaxStringContentLength = int.MaxValue
            };
        }
    }
}