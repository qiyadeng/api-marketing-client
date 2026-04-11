const crypto = require("crypto");

function env(name, fallback) {
  return process.env[name] || fallback;
}

function calcAuthorization(source, secretId, secretKey, xDate) {
  const signStr = `x-date: ${xDate}\nx-source: ${source}`;
  const signature = crypto.createHmac("sha1", secretKey).update(signStr).digest("base64");
  return `hmac id="${secretId}", algorithm="hmac-sha1", headers="x-date x-source", signature="${signature}"`;
}

async function main() {
  const url = env("API_URL", "http://service-9jiloqcv-1258478321.sh.apigw.tencentcs.com/release/fileConvert");
  const secretId = env("TENCENT_SECRET_ID", "YOUR_TENCENT_SECRET_ID");
  const secretKey = env("TENCENT_SECRET_KEY", "YOUR_TENCENT_SECRET_KEY");
  const source = env("TENCENT_SOURCE", "market");
  const fileName = env("FILE_NAME", "demo.docx");
  const fileUrl = env("FILE_URL", "https://example.com/demo.docx");
  const callbackUrl = env("CALLBACK_URL", "");

  const xDate = new Date().toUTCString();
  const authorization = calcAuthorization(source, secretId, secretKey, xDate);
  const body = new URLSearchParams({
    fileName,
    url: fileUrl,
    callbackUrl,
  });

  const response = await fetch(url, {
    method: "POST",
    headers: {
      Authorization: authorization,
      "X-Source": source,
      "X-Date": xDate,
      "Content-Type": "application/x-www-form-urlencoded",
    },
    body: body.toString(),
  });

  console.log(await response.text());
}

main().catch((error) => {
  console.error(error);
  process.exit(1);
});

