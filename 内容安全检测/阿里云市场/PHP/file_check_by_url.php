<?php

function env_or_default($key, $default) {
    $value = getenv($key);
    return $value === false || $value === '' ? $default : $value;
}

$url = env_or_default('FILE_CHECK_BY_URL_URL', 'https://filecheck.market.alicloudapi.com/wordscheckFileCheckByUrl');
$appcode = env_or_default('ALIYUN_APPCODE', 'YOUR_ALIYUN_APPCODE');
$fileName = env_or_default('FILE_NAME', 'demo.docx');
$fileUrl = env_or_default('FILE_URL', 'https://example.com/demo.docx');
$body = http_build_query(array(
    'fileName' => $fileName,
    'url' => $fileUrl
));

$ch = curl_init($url);
curl_setopt_array($ch, array(
    CURLOPT_POST => true,
    CURLOPT_POSTFIELDS => $body,
    CURLOPT_RETURNTRANSFER => true,
    CURLOPT_HTTPHEADER => array(
        'Authorization: APPCODE ' . $appcode,
        'Content-Type: application/x-www-form-urlencoded'
    )
));

$response = curl_exec($ch);
if ($response === false) {
    throw new RuntimeException(curl_error($ch));
}
curl_close($ch);

echo $response . PHP_EOL;

