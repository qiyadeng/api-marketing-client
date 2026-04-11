using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

internal class FileCheckByUrlProgram
{
    private static string Env(string key, string fallback)
    {
        var value = Environment.GetEnvironmentVariable(key);
        return string.IsNullOrWhiteSpace(value) ? fallback : value;
    }

    private static async Task Main()
    {
        var url = Env("FILE_CHECK_BY_URL_URL", "https://filecheck.market.alicloudapi.com/openapi/v1/file/check-by-url");
        var appcode = Env("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE");
        var fileName = Env("FILE_NAME", "demo.docx");
        var fileUrl = Env("FILE_URL", "https://example.com/demo.docx");

        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.TryAddWithoutValidation("Authorization", $"APPCODE {appcode}");
        var json = "{\"fileName\":\"" + fileName.Replace("\\", "\\\\").Replace("\"", "\\\"")
            + "\",\"url\":\"" + fileUrl.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"}";
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.SendAsync(request);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
