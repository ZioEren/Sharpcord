using Leaf.xNet;
using System.Net.NetworkInformation;
using System;
public static class ProxyManager
{
    private static string theProxy;
    private static bool useProxy;
    private static ProxyType proxyType;
    public static void SetProxy(string proxy)
    {
        theProxy = proxy;
    }
    public static void SetProxy(string ip, string port)
    {
        theProxy = ip + ":" + port;
    }
    public static string GetProxy()
    {
        return theProxy;
    }
    public static bool IsUsingProxy()
    {
        return useProxy;
    }
    public static void SetUseProxy(bool use)
    {
        useProxy = use;
    }
    public static ProxyType GetProxyType()
    {
        return proxyType;
    }
    public static void SetProxyType(ProxyType type)
    {
        proxyType = type;
    }
    public static void Change(HttpRequest req, string proxy)
    {
        Change(req, proxy, GetProxyType());
    }
    public static void Change(HttpRequest req, string proxy, ProxyType proxyType)
    {
        if (proxy != null && proxy != "" && req != null)
        {
            if (proxyType == ProxyType.HTTPS)
            {
                req.Proxy = HttpProxyClient.Parse(proxy);
            }
            else if (proxyType == ProxyType.SOCKS4)
            {
                req.Proxy = Socks4ProxyClient.Parse(proxy);
            }
            else if (proxyType == ProxyType.SOCKS4A)
            {
                req.Proxy = Socks4AProxyClient.Parse(proxy);
            }
            else
            {
                req.Proxy = Socks5ProxyClient.Parse(proxy);
            }
        }
    }
    public static void SimpleChange(HttpRequest req, string proxy, ProxyType proxyType)
    {
        if (IsUsingProxy())
        {
            Change(req, proxy);
        }
        else
        {
            Change(req, proxy, proxyType);
        }
    }
    public static bool CheckProxy(string ProxyIp, string ProxyPort)
    {
        int Proxyp = Convert.ToInt32(ProxyPort);
        Ping ping = new Ping();
        try
        {
            PingReply reply = ping.Send(ProxyIp, Proxyp);
            if (reply == null) return false;
            return (reply.Status == IPStatus.Success);
        }
        catch (PingException e)
        {
            return false;
        }
    }
}