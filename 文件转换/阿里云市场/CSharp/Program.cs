using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

internal class Program
{
    private static string Env(string key, string fallback)
    {
        var value = Environment.GetEnvironmentVariable(key);
        return string.IsNullOrWhiteSpace(value) ? fallback : value;
    }

    private static async Task Main()
    {
        var url = Env("API_URL", "https://your-aliyun-market-domain/fileConvert");
        var appcode = Env("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE");
        var fileName = Env("FILE_NAME", "demo.docx");
        var fileUrl = Env("FILE_URL", "https://example.com/demo.docx");
        var callbackUrl = Env("CALLBACK_URL", "");

        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.TryAddWithoutValidation("Authorization", $"APPCODE {appcode}");
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
