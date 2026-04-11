import os
from urllib.parse import urlencode
from urllib.request import Request, urlopen


def main() -> None:
    url = os.getenv(
        "TEXT_CHECK_URL",
        "https://filecheck.market.alicloudapi.com/wordscheckTextCheck",
    )
    appcode = os.getenv("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE")
    check_text = os.getenv("CHECK_TEXT", "This text will be checked for risky content.")
    payload = urlencode({"text": check_text}).encode("utf-8")

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
