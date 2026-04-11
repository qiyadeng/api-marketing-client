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
        var url = Env("API_URL", "https://your-aliyun-market-domain/getInvocieInfo");
        var appcode = Env("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE");
        var invoiceName = Env("INVOICE_NAME", "上海奇伢信息科技有限公司");

        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.TryAddWithoutValidation("Authorization", $"APPCODE {appcode}");
        request.Content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("name", invoiceName)
        });

        var response = await client.SendAsync(request);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
