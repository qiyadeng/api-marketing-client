<?php

function env_or_default($key, $default) {
    $value = getenv($key);
    return $value === false || $value === '' ? $default : $value;
}

$url = env_or_default('API_URL', 'https://your-aliyun-market-domain/getInvocieInfo');
$appcode = env_or_default('ALIYUN_APPCODE', 'YOUR_ALIYUN_APPCODE');
$invoiceName = env_or_default('INVOICE_NAME', '上海奇伢信息科技有限公司');
$body = http_build_query(array('name' => $invoiceName));

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

