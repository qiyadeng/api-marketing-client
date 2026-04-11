import requests
import hmac
import hashlib
import base64
from urllib.parse import urlencode
from datetime import datetime
import pytz

def calc_authorization(source, secret_id, secret_key, datetime):
    sign_str = f"x-date: {datetime}\nx-source: {source}"
    secret_key_bytes = bytes(secret_key, 'utf-8')
    sign_str_bytes = bytes(sign_str, 'utf-8')
    hashed = hmac.new(secret_key_bytes, sign_str_bytes, hashlib.sha1)
    sig = base64.b64encode(hashed.digest()).decode('utf-8')
    auth = f'hmac id="{secret_id}", algorithm="hmac-sha1", headers="x-date x-source", signature="{sig}"'
    return auth

def main():
    secret_id = "AKIDl41lY7xX14aJiVQ4fs36UZ5hVCLEMju488IK"
    secret_key = "ItwffQ9dgd9iz1J1x0NmjLBWv088eRjrf9y9tnck"
    source = "market"

    datetime_format = "%a, %d %b %Y %H:%M:%S GMT"
    datetime_now = datetime.now(pytz.timezone('GMT')).strftime(datetime_format)

    auth = calc_authorization(source, secret_id, secret_key, datetime_now)

    headers = {
        "X-Source": source,
        "X-Date": datetime_now,
        "Authorization": auth,
        "Content-Type": "application/x-www-form-urlencoded"
    }

    body_params = {
        "name": "上海奇伢信息科技有限公司"
    }

    url = "https://service-rf059r1j-1258478321.sh.apigw.tencentcs.com/release/getInvocieInfo"
    
    try:
        response = requests.post(url, headers=headers, data=urlencode(body_params), timeout=5)
        response.raise_for_status()
        print(response.text)
    except requests.exceptions.RequestException as e:
        print(e)

if __name__ == "__main__":
    main()