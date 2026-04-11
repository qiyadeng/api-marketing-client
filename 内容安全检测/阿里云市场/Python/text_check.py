import os
import json
from urllib.request import Request, urlopen


def main() -> None:
    url = os.getenv(
        "TEXT_CHECK_URL",
        "https://filecheck.market.alicloudapi.com/openapi/v1/text/check",
    )
    appcode = os.getenv("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE")
    check_text = os.getenv("CHECK_TEXT", "This text will be checked for risky content.")
    payload = json.dumps({"text": check_text}, ensure_ascii=False).encode("utf-8")

    request = Request(
        url,
        data=payload,
        headers={
            "Authorization": f"APPCODE {appcode}",
            "Content-Type": "application/json",
        },
        method="POST",
    )

    with urlopen(request, timeout=30) as response:
        print(response.read().decode("utf-8"))


if __name__ == "__main__":
    main()
