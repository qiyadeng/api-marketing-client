using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

internal class TextCheckProgram
{
    private static string Env(string key, string fallback)
    {
        var value = Environment.GetEnvironmentVariable(key);
        return string.IsNullOrWhiteSpace(value) ? fallback : value;
    }

    private static async Task Main()
    {
        var url = Env("TEXT_CHECK_URL", "https://filecheck.market.alicloudapi.com/wordscheckTextCheck");
        var appcode = Env("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE");
        var checkText = Env("CHECK_TEXT", "This text will be checked for risky content.");

        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.TryAddWithoutValidation("Authorization", $"APPCODE {appcode}");
        request.Content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("text", checkText)
        });

        var response = await client.SendAsync(request);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
