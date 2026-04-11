import os
from urllib.parse import urlencode
from urllib.request import Request, urlopen


def main() -> None:
    url = os.getenv("API_URL", "https://your-aliyun-market-domain/getInvocieInfo")
    appcode = os.getenv("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE")
    invoice_name = os.getenv("INVOICE_NAME", "上海奇伢信息科技有限公司")
    payload = urlencode({"name": invoice_name}).encode("utf-8")

    request = Request(
        url,
        data=payload,
        headers={
            "Authorization": f"APPCODE {appcode}",
            "Content-Type": "application/x-www-form-urlencoded",
        },
        method="POST",
    )

    with urlopen(request, timeout=30) as response:
        print(response.read().decode("utf-8"))


if __name__ == "__main__":
    main()

