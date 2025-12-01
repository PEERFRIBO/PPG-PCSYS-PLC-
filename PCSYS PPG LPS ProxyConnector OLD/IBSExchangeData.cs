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

#nullable disable
namespace PCSYS_PPG_LPS_ProxyConnector;

public class IBSExchangeData
{
  public static IbsDataLink GetActiveLink()
  {
    return IBSExchangeData.GetActiveLink((IEnumerable<IbsDataLink>) null);
  }

  public static IbsDataLink GetActiveLink(IEnumerable<IbsDataLink> links)
  {
    if (links == null)
      throw new Exception("No connection found");
    if (!(links is IList<IbsDataLink> ibsDataLinkList))
      ibsDataLinkList = (IList<IbsDataLink>) links.ToList<IbsDataLink>();
    IList<IbsDataLink> source = ibsDataLinkList;
    IbsDataLink link1 = source.FirstOrDefault<IbsDataLink>((Func<IbsDataLink, bool>) (link => link.IsActive));
    for (int index = 0; index < 5; ++index)
    {
      try
      {
        IBSExchangeData.GetInnerConnection(link1).CheckConnection(string.Empty);
        return link1;
      }
      catch (Exception ex)
      {
        foreach (IbsDataLink ibsDataLink in source.Where<IbsDataLink>((Func<IbsDataLink, bool>) (link => link.IsActive)))
        {
          ibsDataLink.Error = ex.Message;
          ibsDataLink.IsActive = false;
          ibsDataLink.LastErrorTime = DateTime.Now;
        }
        link1 = source != null ? source.FirstOrDefault<IbsDataLink>((Func<IbsDataLink, bool>) (link => !link.IsActive)) : (IbsDataLink) null;
        if (link1 != null)
        {
          link1.IsActive = true;
          return link1;
        }
        Thread.Sleep(1000);
        foreach (IbsDataLink ibsDataLink in (IEnumerable<IbsDataLink>) source)
          ibsDataLink.IsActive = true;
      }
    }
    return (IbsDataLink) null;
  }

  public AppServiceClient GetConnection(IEnumerable<IbsDataLink> links)
  {
    IbsDataLink activeLink = IBSExchangeData.GetActiveLink(links);
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
    return (AppServiceClient) null;
  }

  public static AppServiceClient GetInnerConnection(IbsDataLink link)
  {
    try
    {
      if (link == null)
        throw new Exception("No data link is found");
      BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
      basicHttpBinding.ReaderQuotas = IBSExchangeData.SetQuotas();
      basicHttpBinding.MaxReceivedMessageSize = (long) int.MaxValue;
      return new AppServiceClient((Binding) basicHttpBinding, new EndpointAddress(new Uri($"http://{link.HttpAddress}:{link.Port}/{link.Version}/DATASERVICE"), new AddressHeader[0]));
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
