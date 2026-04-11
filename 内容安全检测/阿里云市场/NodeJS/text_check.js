function env(name, fallback) {
  return process.env[name] || fallback;
}

async function main() {
  const url = env("TEXT_CHECK_URL", "https://filecheck.market.alicloudapi.com/openapi/v1/text/check");
  const appcode = env("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE");
  const checkText = env("CHECK_TEXT", "This text will be checked for risky content.");
  const body = new URLSearchParams({ text: checkText });

  const response = await fetch(url, {
    method: "POST",
    headers: {
      Authorization: `APPCODE ${appcode}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ text: checkText }),
  });

  console.log(await response.text());
}

main().catch((error) => {
  console.error(error);
  process.exit(1);
});
