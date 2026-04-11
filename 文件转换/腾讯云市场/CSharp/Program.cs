using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

internal class Program
{
    private static string Env(string key, string fallback)
    {
        var value = Environment.GetEnvironmentVariable(key);
        return string.IsNullOrWhiteSpace(value) ? fallback : value;
    }

    private static string CalcAuthorization(string source, string secretId, string secretKey, string xDate)
    {
        var signText = $"x-date: {xDate}\nx-source: {source}";
        using var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(secretKey));
        var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(signText)));
        return $"hmac id=\"{secretId}\", algorithm=\"hmac-sha1\", headers=\"x-date x-source\", signature=\"{signature}\"";
    }

    private static async Task Main()
    {
        var url = Env("API_URL", "http://service-9jiloqcv-1258478321.sh.apigw.tencentcs.com/release/fileConvert");
        var secretId = Env("TENCENT_SECRET_ID", "YOUR_TENCENT_SECRET_ID");
        var secretKey = Env("TENCENT_SECRET_KEY", "YOUR_TENCENT_SECRET_KEY");
        var source = Env("TENCENT_SOURCE", "market");
        var fileName = Env("FILE_NAME", "demo.docx");
        var fileUrl = Env("FILE_URL", "https://example.com/demo.docx");
        var callbackUrl = Env("CALLBACK_URL", "");

        var xDate = DateTime.UtcNow.ToString("r");
        var authorization = CalcAuthorization(source, secretId, secretKey, xDate);
        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Post, url);

        request.Headers.TryAddWithoutValidation("Authorization", authorization);
        request.Headers.TryAddWithoutValidation("X-Source", source);
        request.Headers.TryAddWithoutValidation("X-Date", xDate);
        request.Content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("fileName", fileName),
            new KeyValuePair<string, string>("url", fileUrl),
            new KeyValuePair<string, string>("callbackUrl", callbackUrl)
        });

        var response = await client.SendAsync(request);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
