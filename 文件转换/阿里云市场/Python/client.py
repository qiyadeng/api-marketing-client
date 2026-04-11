import os
from urllib.parse import urlencode
from urllib.request import Request, urlopen


def main() -> None:
    url = os.getenv("API_URL", "https://your-aliyun-market-domain/fileConvert")
    appcode = os.getenv("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE")
    file_name = os.getenv("FILE_NAME", "demo.docx")
    file_url = os.getenv("FILE_URL", "https://example.com/demo.docx")
    callback_url = os.getenv("CALLBACK_URL", "")
    payload = urlencode(
        {
            "fileName": file_name,
            "url": file_url,
            "callbackUrl": callback_url,
        }
    ).encode("utf-8")

    request = Request(
        url,
        data=payload,
        headers={
            "Authorization": f"APPCODE {appcode}",
            "Content-Type": "application/x-www-form-urlencoded",
        },
        method="POST",
    )

    with urlopen(request, timeout=60) as response:
        print(response.read().decode("utf-8"))


if __name__ == "__main__":
    main()

