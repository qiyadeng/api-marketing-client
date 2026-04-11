<?php

function calcAuthorization($source, $secretId, $secretKey, $datetime) {
    $signStr = "x-date: " . $datetime . "\n" . "x-source: " . $source;
    $hash = hash_hmac('sha1', $signStr, $secretKey, true);
    $sig = base64_encode($hash);

    $auth = 'hmac id="' . $secretId . '", algorithm="hmac-sha1", headers="x-date x-source", signature="' . $sig . '"';
    return $auth;
}

function urlencodeParams($params) {
    return http_build_query($params);
}

function main() {
    // 云市场分配的密钥Id
    $secretId = "AKIDl41lY7xX14aJiVQ4fs36UZ5hVCLEMju488IK";
    // 云市场分配的密钥Key
    $secretKey = "ItwffQ9dgd9iz1J1x0NmjLBWv088eRjrf9y9tnck";
    $source = "market";

    $datetime = gmdate('D, d M Y H:i:s T');
    // 签名
    $auth = calcAuthorization($source, $secretId, $secretKey, $datetime);
    // 请求方法
    $method = "POST";

    // 请求头
    $headers = [
        "X-Source: $source",
        "X-Date: $datetime",
        "Authorization: $auth",
        "Content-Type: application/x-www-form-urlencoded"
    ];

    // 查询参数
    $queryParams = [];

    // body参数
    $bodyParams = [
        "callbackUrl" => "",
        "fileName" => "test.docx",
        "url" => "http://bianliwenku.ctest.top/%E6%96%87%E5%BA%93%E6%A8%A1%E7%89%88/%E6%88%BF%E5%B1%8B%E7%A7%9F%E8%B5%81%E5%90%88%E5%90%8C%E6%A8%A1%E6%9D%BF/%E6%88%BF%E5%B1%8B%E7%A7%9F%E8%B5%81%E5%90%88%E5%90%8C%E6%88%BF%E4%B8%9C%E6%AC%BE-1.docx?OSSAccessKeyId=LTAI5tNGEwi5psyB6DttxhJt&Expires=1698512005&Signature=pzHvmEWS%2F2wWb4Mf9mUgfoS0gnU%3D"
    ];

    // URL
    $url = "http://service-9jiloqcv-1258478321.sh.apigw.tencentcs.com/release/fileConvert";
    if (!empty($queryParams)) {
        $url .= '?' . urlencodeParams($queryParams);
    }

    // 初始化 cURL
    $ch = curl_init($url);

    // 设置 cURL 选项
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch, CURLOPT_TIMEOUT, 50);
    curl_setopt($ch, CURLOPT_CONNECTTIMEOUT, 50);
    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, $method);
    curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);

    // 如果是 POST 请求，发送 body
    if (in_array($method, ['POST', 'PUT', 'PATCH'])) {
        curl_setopt($ch, CURLOPT_POSTFIELDS, urlencodeParams($bodyParams));
    }

    // 执行 cURL 请求
    $response = curl_exec($ch);
    if (curl_errno($ch)) {
        echo 'Error:' . curl_error($ch);
    } else {
        echo $response;
    }

    // 关闭 cURL
    curl_close($ch);
}

main();

?>