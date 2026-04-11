using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

internal class FileCheckUploadProgram
{
    private static string Env(string key, string fallback)
    {
        var value = Environment.GetEnvironmentVariable(key);
        return string.IsNullOrWhiteSpace(value) ? fallback : value;
    }

    private static async Task Main()
    {
        var url = Env("FILE_CHECK_UPLOAD_URL", "https://filecheck.market.alicloudapi.com/openapi/v1/file/check");
        var appcode = Env("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE");
        var uploadFilePath = Env("UPLOAD_FILE_PATH", "demo.docx");

        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        using var form = new MultipartFormDataContent();
        using var fileStream = File.OpenRead(uploadFilePath);
        using var streamContent = new StreamContent(fileStream);

        request.Headers.TryAddWithoutValidation("Authorization", $"APPCODE {appcode}");
        form.Add(streamContent, "file", Path.GetFileName(uploadFilePath));
        request.Content = form;

        var response = await client.SendAsync(request);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
