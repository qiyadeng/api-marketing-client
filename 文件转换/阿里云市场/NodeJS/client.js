function env(name, fallback) {
  return process.env[name] || fallback;
}

async function main() {
  const url = env("API_URL", "https://your-aliyun-market-domain/fileConvert");
  const appcode = env("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE");
  const fileName = env("FILE_NAME", "demo.docx");
  const fileUrl = env("FILE_URL", "https://example.com/demo.docx");
  const callbackUrl = env("CALLBACK_URL", "");
  const body = new URLSearchParams({
    fileName,
    url: fileUrl,
    callbackUrl,
  });

  const response = await fetch(url, {
    method: "POST",
    headers: {
      Authorization: `APPCODE ${appcode}`,
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

