function env(name, fallback) {
  return process.env[name] || fallback;
}

async function main() {
  const url = env("FILE_CHECK_BY_URL_URL", "https://filecheck.market.alicloudapi.com/openapi/v1/file/check-by-url");
  const appcode = env("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE");
  const fileName = env("FILE_NAME", "demo.docx");
  const fileUrl = env("FILE_URL", "https://example.com/demo.docx");
  const response = await fetch(url, {
    method: "POST",
    headers: {
      Authorization: `APPCODE ${appcode}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      fileName,
      url: fileUrl,
    }),
  });

  console.log(await response.text());
}

main().catch((error) => {
  console.error(error);
  process.exit(1);
});
