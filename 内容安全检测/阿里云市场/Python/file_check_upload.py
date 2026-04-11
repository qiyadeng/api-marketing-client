import os
import uuid
from urllib.request import Request, urlopen


def main() -> None:
    url = os.getenv(
        "FILE_CHECK_UPLOAD_URL",
        "https://filecheck.market.alicloudapi.com/wordscheckFileCheck",
    )
    appcode = os.getenv("ALIYUN_APPCODE", "YOUR_ALIYUN_APPCODE")
    upload_file_path = os.getenv("UPLOAD_FILE_PATH", "demo.docx")

    with open(upload_file_path, "rb") as file_obj:
        file_bytes = file_obj.read()

    boundary = f"----CodexBoundary{uuid.uuid4().hex}"
    body = (
        f"--{boundary}\r\n"
        f'Content-Disposition: form-data; name="file"; filename="{os.path.basename(upload_file_path)}"\r\n'
        "Content-Type: application/octet-stream\r\n\r\n"
    ).encode("utf-8") + file_bytes + f"\r\n--{boundary}--\r\n".encode("utf-8")

    request = Request(
        url,
        data=body,
        headers={
            "Authorization": f"APPCODE {appcode}",
            "Content-Type": f"multipart/form-data; boundary={boundary}",
        },
        method="POST",
    )

    with urlopen(request, timeout=120) as response:
        print(response.read().decode("utf-8"))


if __name__ == "__main__":
    main()

