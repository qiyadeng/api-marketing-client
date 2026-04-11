<?php

function env_or_default($key, $default) {
    $value = getenv($key);
    return $value === false || $value === '' ? $default : $value;
}

$url = env_or_default('TEXT_CHECK_URL', 'https://filecheck.market.alicloudapi.com/wordscheckTextCheck');
$appcode = env_or_default('ALIYUN_APPCODE', 'YOUR_ALIYUN_APPCODE');
$checkText = env_or_default('CHECK_TEXT', 'This text will be checked for risky content.');
$body = http_build_query(array('text' => $checkText));

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
