# 文件转换

该目录提供“文件转换为 PDF”接口的多语言调用示例。

## 请求参数

- `fileName`：原始文件名
- `url`：待转换文件的访问地址
- `callbackUrl`：可选，异步回调地址

## 示例中使用的环境变量

- `FILE_NAME`
  默认值：`demo.docx`
- `FILE_URL`
- `CALLBACK_URL`
  默认值：空字符串
- `API_URL`

## 腾讯云市场默认地址

- `http://service-9jiloqcv-1258478321.sh.apigw.tencentcs.com/release/fileConvert`

## 说明

- 腾讯云市场示例使用 HMAC 鉴权
- 阿里云市场示例使用 `APPCODE` 鉴权
- 阿里云市场示例中的 `API_URL` 是占位地址，使用前请替换为实际接口地址
