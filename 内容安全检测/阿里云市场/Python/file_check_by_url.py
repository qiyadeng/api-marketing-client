import os
import json
from urllib.request import Request, urlopen


def main() -> None:
    url = os.getenv(
        "FILE_CHECK_BY_URL_URL",
        "https://filecheck.market.alicloudapi.com/openapi/v1/file/check-by-url",
    )
    appcode = os.getenv("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE")
    file_name = os.getenv("FILE_NAME", "demo.docx")
    file_url = os.getenv("FILE_URL", "https://example.com/demo.docx")
    payload = json.dumps({"fileName": file_name, "url": file_url}, ensure_ascii=False).encode("utf-8")

    request = Request(
        url,
        data=payload,
        headers={
            "Authorization": f"APPCODE {appcode}",
            "Content-Type": "application/json",
        },
        method="POST",
    )

    with urlopen(request, timeout=60) as response:
        print(response.read().decode("utf-8"))


if __name__ == "__main__":
    main()
