<?php

function env_or_default($key, $default) {
    $value = getenv($key);
    return $value === false || $value === '' ? $default : $value;
}

function calc_authorization($source, $secretId, $secretKey, $xDate) {
    $signStr = "x-date: {$xDate}\n" . "x-source: {$source}";
    $signature = base64_encode(hash_hmac('sha1', $signStr, $secretKey, true));
    return 'hmac id="' . $secretId . '", algorithm="hmac-sha1", headers="x-date x-source", signature="' . $signature . '"';
}

$url = env_or_default('API_URL', 'https://service-rf059r1j-1258478321.sh.apigw.tencentcs.com/release/getInvocieInfo');
$secretId = env_or_default('TENCENT_SECRET_ID', 'YOUR_TENCENT_SECRET_ID');
$secretKey = env_or_default('TENCENT_SECRET_KEY', 'YOUR_TENCENT_SECRET_KEY');
$source = env_or_default('TENCENT_SOURCE', 'market');
$invoiceName = env_or_default('INVOICE_NAME', '上海奇伢信息科技有限公司');

$xDate = gmdate('D, d M Y H:i:s') . ' GMT';
$authorization = calc_authorization($source, $secretId, $secretKey, $xDate);
$body = http_build_query(array('name' => $invoiceName));

$ch = curl_init($url);
curl_setopt_array($ch, array(
    CURLOPT_POST => true,
    CURLOPT_POSTFIELDS => $body,
    CURLOPT_RETURNTRANSFER => true,
    CURLOPT_HTTPHEADER => array(
        'Authorization: ' . $authorization,
        'X-Source: ' . $source,
        'X-Date: ' . $xDate,
        'Content-Type: application/x-www-form-urlencoded'
    )
));

$response = curl_exec($ch);
if ($response === false) {
    throw new RuntimeException(curl_error($ch));
}
curl_close($ch);

echo $response . PHP_EOL;

