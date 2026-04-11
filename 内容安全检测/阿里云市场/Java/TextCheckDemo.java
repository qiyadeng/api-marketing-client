import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URLEncoder;
import java.net.URL;
import java.nio.charset.StandardCharsets;

public class TextCheckDemo {
    private static String env(String key, String fallback) {
        String value = System.getenv(key);
        return value == null || value.trim().isEmpty() ? fallback : value;
    }

    public static void main(String[] args) throws Exception {
        String url = env("TEXT_CHECK_URL", "https://filecheck.market.alicloudapi.com/wordscheckTextCheck");
        String appcode = env("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE");
        String checkText = env("CHECK_TEXT", "This text will be checked for risky content.");
        String body = "text=" + URLEncoder.encode(checkText, StandardCharsets.UTF_8.name());

        HttpURLConnection connection = (HttpURLConnection) new URL(url).openConnection();
        connection.setRequestMethod("POST");
        connection.setConnectTimeout(30000);
        connection.setReadTimeout(30000);
        connection.setDoOutput(true);
        connection.setRequestProperty("Authorization", "APPCODE " + appcode);
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
