import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URLEncoder;
import java.net.URL;
import java.nio.charset.StandardCharsets;
import java.text.SimpleDateFormat;
import java.util.Base64;
import java.util.Calendar;
import java.util.Locale;
import java.util.TimeZone;
import javax.crypto.Mac;
import javax.crypto.spec.SecretKeySpec;

public class ClientDemo {
    private static String env(String key, String fallback) {
        String value = System.getenv(key);
        return value == null || value.trim().isEmpty() ? fallback : value;
    }

    private static String calcAuthorization(String source, String secretId, String secretKey, String xDate)
            throws Exception {
        String signStr = "x-date: " + xDate + "\n" + "x-source: " + source;
        Mac mac = Mac.getInstance("HmacSHA1");
        mac.init(new SecretKeySpec(secretKey.getBytes(StandardCharsets.UTF_8), "HmacSHA1"));
        String signature = Base64.getEncoder()
                .encodeToString(mac.doFinal(signStr.getBytes(StandardCharsets.UTF_8)));
        return "hmac id=\"" + secretId + "\", algorithm=\"hmac-sha1\", headers=\"x-date x-source\", signature=\""
                + signature + "\"";
    }

    public static void main(String[] args) throws Exception {
        String url = env("API_URL",
                "http://service-9jiloqcv-1258478321.sh.apigw.tencentcs.com/release/fileConvert");
        String secretId = env("TENCENT_SECRET_ID", "YOUR_TENCENT_SECRET_ID");
        String secretKey = env("TENCENT_SECRET_KEY", "YOUR_TENCENT_SECRET_KEY");
        String source = env("TENCENT_SOURCE", "market");
        String fileName = env("FILE_NAME", "demo.docx");
        String fileUrl = env("FILE_URL", "https://example.com/demo.docx");
        String callbackUrl = env("CALLBACK_URL", "");

        Calendar calendar = Calendar.getInstance(TimeZone.getTimeZone("GMT"));
        SimpleDateFormat sdf = new SimpleDateFormat("EEE, dd MMM yyyy HH:mm:ss 'GMT'", Locale.US);
        sdf.setTimeZone(TimeZone.getTimeZone("GMT"));
        String xDate = sdf.format(calendar.getTime());
        String authorization = calcAuthorization(source, secretId, secretKey, xDate);
        String body = "fileName=" + URLEncoder.encode(fileName, StandardCharsets.UTF_8.name())
                + "&url=" + URLEncoder.encode(fileUrl, StandardCharsets.UTF_8.name())
                + "&callbackUrl=" + URLEncoder.encode(callbackUrl, StandardCharsets.UTF_8.name());

        HttpURLConnection connection = (HttpURLConnection) new URL(url).openConnection();
        connection.setRequestMethod("POST");
        connection.setConnectTimeout(60000);
        connection.setReadTimeout(60000);
        connection.setDoOutput(true);
        connection.setRequestProperty("Authorization", authorization);
        connection.setRequestProperty("X-Source", source);
        connection.setRequestProperty("X-Date", xDate);
        connection.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");

        try (OutputStream os = connection.getOutputStream()) {
            os.write(body.getBytes(StandardCharsets.UTF_8));
        }

        try (BufferedReader reader = new BufferedReader(
                new InputStreamReader(connection.getInputStream(), StandardCharsets.UTF_8))) {
            String line;
            StringBuilder result = new StringBuilder();
            while ((line = reader.readLine()) != null) {
                result.append(line);
            }
            System.out.println(result.toString());
        }
    }
}
