const fs = require("fs");

function env(name, fallback) {
  return process.env[name] || fallback;
}

async function main() {
  const url = env("FILE_CHECK_UPLOAD_URL", "https://filecheck.market.alicloudapi.com/openapi/v1/file/check");
  const appcode = env("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE");
  const uploadFilePath = env("UPLOAD_FILE_PATH", "demo.docx");

  const form = new FormData();
  form.append("file", new Blob([fs.readFileSync(uploadFilePath)]), require("path").basename(uploadFilePath));

  const response = await fetch(url, {
    method: "POST",
    headers: {
      Authorization: `APPCODE ${appcode}`,
    },
    body: form,
  });

  console.log(await response.text());
}

main().catch((error) => {
  console.error(error);
  process.exit(1);
});
