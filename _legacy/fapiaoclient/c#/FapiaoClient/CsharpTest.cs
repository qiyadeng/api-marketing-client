using System.IO;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Web;

public class CsharpTest
{
    public static String HMACSHA1Text(String EncryptText, String EncryptKey)
    {
        HMACSHA1 hmacsha1 = new HMACSHA1();
        hmacsha1.Key = System.Text.Encoding.UTF8.GetBytes(EncryptKey);

        byte[] dataBuffer = System.Text.Encoding.UTF8.GetBytes(EncryptText);
        byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);
        return Convert.ToBase64String(hashBytes);
    }

    public static void Main(String[] args)
    {
        String url = "https://service-rf059r1j-1258478321.sh.apigw.tencentcs.com/release/getInvocieInfo";
        String method = "POST";
        String querys = "";


        //根据你的具体情况填写
        String postData = "name=上海奇伢信息科技有限公司";

        //改成你的 云市场分配的密钥Id
        String secretId = "AKIDl41lY7xX14aJiVQ4fs36UZ5hVCLEMju488IK";
        //改成你的云市场分配的密钥Key
        String secretKey = "ItwffQ9dgd9iz1J1x0NmjLBWv088eRjrf9y9tnck";
        String source = "market";

        String dt = DateTime.UtcNow.GetDateTimeFormats('r')[0];
        url = url + "?" + querys;

        String signStr = "x-date: " + dt + "\n" + "x-source: " + source;
        String sign = HMACSHA1Text(signStr, secretKey);

        String auth = "hmac id=\"" + secretId + "\", algorithm=\"hmac-sha1\", headers=\"x-date x-source\", signature=\"";
        auth = auth + sign + "\"";

        HttpWebRequest httpRequest = null;
        HttpWebResponse httpResponse = null;

        if (url.Contains("https://"))
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
        }
        else
        {
            httpRequest = (HttpWebRequest)WebRequest.Create(url);
        }

        httpRequest.Method = method;
        httpRequest.ContentLength = postData.Length;
        httpRequest.ContentType = "application/x-www-form-urlencoded";
        httpRequest.Headers.Add("Authorization", auth);
        httpRequest.Headers.Add("X-Source", source);
        httpRequest.Headers.Add("X-Date", dt);
        byte[] postDataBytes = System.Text.Encoding.UTF8.GetBytes(postData);//注意这行，包含中文查询必须加入这行
        httpRequest.GetRequestStream().Write(postDataBytes, 0, postData.Length);

        try
        {
            httpResponse = (HttpWebResponse)httpRequest.GetResponse();
        }
        catch (WebException ex)
        {
            httpResponse = (HttpWebResponse)ex.Response;
        }

        Console.WriteLine(httpResponse.StatusCode);
        Console.WriteLine(httpResponse.Headers);
        Stream st = httpResponse.GetResponseStream();
        StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
        Console.WriteLine(reader.ReadToEnd());
        Console.WriteLine("\n");

    }

    public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
    {
        return true;
    }
}
