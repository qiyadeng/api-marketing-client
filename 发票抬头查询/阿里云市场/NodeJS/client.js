function env(name, fallback) {
  return process.env[name] || fallback;
}

async function main() {
  const url = env("API_URL", "https://your-aliyun-market-domain/getInvocieInfo");
  const appcode = env("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE");
  const invoiceName = env("INVOICE_NAME", "上海奇伢信息科技有限公司");
  const body = new URLSearchParams({ name: invoiceName });

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

