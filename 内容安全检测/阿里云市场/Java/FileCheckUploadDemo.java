import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.nio.charset.StandardCharsets;
import java.util.UUID;

public class FileCheckUploadDemo {
    private static String env(String key, String fallback) {
        String value = System.getenv(key);
        return value == null || value.trim().isEmpty() ? fallback : value;
    }

    public static void main(String[] args) throws Exception {
        String url = env("FILE_CHECK_UPLOAD_URL", "https://filecheck.market.alicloudapi.com/wordscheckFileCheck");
        String appcode = env("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE");
        String uploadFilePath = env("UPLOAD_FILE_PATH", "demo.docx");
        String boundary = "----CodexBoundary" + UUID.randomUUID().toString().replace("-", "");
        File file = new File(uploadFilePath);

        HttpURLConnection connection = (HttpURLConnection) new URL(url).openConnection();
        connection.setRequestMethod("POST");
        connection.setConnectTimeout(120000);
        connection.setReadTimeout(120000);
        connection.setDoOutput(true);
        connection.setRequestProperty("Authorization", "APPCODE " + appcode);
        connection.setRequestProperty("Content-Type", "multipart/form-data; boundary=" + boundary);

        try (DataOutputStream out = new DataOutputStream(connection.getOutputStream());
             FileInputStream fileInputStream = new FileInputStream(file)) {
            out.writeBytes("--" + boundary + "\r\n");
            out.writeBytes("Content-Disposition: form-data; name=\"file\"; filename=\"" + file.getName() + "\"\r\n");
            out.writeBytes("Content-Type: application/octet-stream\r\n\r\n");

            byte[] buffer = new byte[8192];
            int bytesRead;
            while ((bytesRead = fileInputStream.read(buffer)) != -1) {
                out.write(buffer, 0, bytesRead);
            }

            out.writeBytes("\r\n--" + boundary + "--\r\n");
            out.flush();
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
