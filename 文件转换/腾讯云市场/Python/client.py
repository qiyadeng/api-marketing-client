import base64
import hashlib
import hmac
import os
from datetime import datetime, timezone
from urllib.parse import urlencode
from urllib.request import Request, urlopen


def calc_authorization(source: str, secret_id: str, secret_key: str, x_date: str) -> str:
    sign_str = f"x-date: {x_date}\nx-source: {source}"
    signature = base64.b64encode(
        hmac.new(secret_key.encode("utf-8"), sign_str.encode("utf-8"), hashlib.sha1).digest()
    ).decode("utf-8")
    return (
        f'hmac id="{secret_id}", algorithm="hmac-sha1", '
        f'headers="x-date x-source", signature="{signature}"'
    )


def main() -> None:
    url = os.getenv(
        "API_URL",
        "http://service-9jiloqcv-1258478321.sh.apigw.tencentcs.com/release/fileConvert",
    )
    secret_id = os.getenv("TENCENT_SECRET_ID", "YOUR_TENCENT_SECRET_ID")
    secret_key = os.getenv("TENCENT_SECRET_KEY", "YOUR_TENCENT_SECRET_KEY")
    source = os.getenv("TENCENT_SOURCE", "market")
    file_name = os.getenv("FILE_NAME", "demo.docx")
    file_url = os.getenv("FILE_URL", "https://example.com/demo.docx")
    callback_url = os.getenv("CALLBACK_URL", "")

    x_date = datetime.now(timezone.utc).strftime("%a, %d %b %Y %H:%M:%S GMT")
    authorization = calc_authorization(source, secret_id, secret_key, x_date)
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
            "Authorization": authorization,
            "X-Source": source,
            "X-Date": x_date,
            "Content-Type": "application/x-www-form-urlencoded",
        },
        method="POST",
    )

    with urlopen(request, timeout=60) as response:
        print(response.read().decode("utf-8"))


if __name__ == "__main__":
    main()

