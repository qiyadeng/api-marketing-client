# API Client Samples

This repository is now organized as:

- Level 1: API capability, such as `发票抬头查询` and `文件转换`
- Level 2: marketplace platform, such as `腾讯云市场` and `阿里云市场`
- Level 3: language, such as `Java`, `Python`, `PHP`, `NodeJS`, and `CSharp`

Legacy samples were preserved under `./_legacy` and `./<功能>/_legacy`.

Each language folder contains one runnable sample file.

Recommended environment variables:

- Tencent marketplace:
  - `TENCENT_SECRET_ID`
  - `TENCENT_SECRET_KEY`
  - `TENCENT_SOURCE` default: `market`
- Aliyun marketplace:
  - `ALIYUN_APPCODE`
- Common request values:
  - `API_URL`
  - `INVOICE_NAME`
  - `FILE_NAME`
  - `FILE_URL`
  - `CALLBACK_URL`

