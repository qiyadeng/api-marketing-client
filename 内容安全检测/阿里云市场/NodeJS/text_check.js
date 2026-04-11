function env(name, fallback) {
  return process.env[name] || fallback;
}

async function main() {
  const url = env("TEXT_CHECK_URL", "https://filecheck.market.alicloudapi.com/wordscheckTextCheck");
  const appcode = env("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE");
  const checkText = env("CHECK_TEXT", "This text will be checked for risky content.");
  const body = new URLSearchParams({ text: checkText });

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
