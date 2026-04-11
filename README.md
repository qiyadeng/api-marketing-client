# API 工具服务客户端示例

这个仓库面向最终使用者，提供上海奇伢信息科技有限公司 API 工具服务的多语言调用示例，帮助你快速完成接入、调试和二次集成。

需要强调的是，这些产品本质上是 API 工具类服务，不是“营销产品”。之所以同时放在阿里云市场和腾讯云市场销售，主要是因为云市场具备更高的搜索流量和更成熟的采购入口，方便有明确需求的开发者和企业用户直接试用、购买和接入。

如果你的业务场景涉及文件转换、发票抬头查询、文档或文本内容安全检测，也可以直接通过云市场购买对应服务，或联系我们沟通定制化接入方案。

## 当前包含的 3 类 API 服务

### 1. 发票抬头查询

输入企业名称后，返回常见开票所需信息，适合开票系统、财税工具、ERP、CRM、商城或内部业务系统接入。

常见返回字段包括：

- 纳税人识别号
- 地址
- 电话
- 开户行
- 银行账号

### 2. 文件转换

将 Word、Excel、PPT、图片等文件转换为 PDF，适合文档归档、在线预览、合同流转、资料上传和办公自动化场景。

### 3. 内容安全检测

针对文本、远程文件 URL 或本地上传文件进行内容合规检测，适合表单提交审核、文档上传审核、知识库入库前检查、业务资料风控等场景。

当前示例包含 3 个接口：

- 文本检测
- 远程文件 URL 检测
- 文件上传检测

## 云市场产品链接

### 腾讯云市场

- 发票抬头、纳税人识别号查询：
  [https://market.cloud.tencent.com/products/37283](https://market.cloud.tencent.com/products/37283)
- Office 文件转换为 PDF API：
  [https://market.cloud.tencent.com/products/18525](https://market.cloud.tencent.com/products/18525)

### 阿里云市场

- 发票抬头、纳税人识别号查询：
  [https://market.aliyun.com/apimarket/detail/cmapi026162](https://market.aliyun.com/apimarket/detail/cmapi026162)
- Office 文件转换为 PDF API：
  [https://market.aliyun.com/apimarket/detail/cmapi00037375](https://market.aliyun.com/apimarket/detail/cmapi00037375)
- 内容安全检测：
  [https://market.aliyun.com/detail/cmapi00073537](https://market.aliyun.com/detail/cmapi00073537)

说明：

- 上述链接已按当前公开商品页核对。
- 目前仓库中的“内容安全检测”示例对应阿里云市场商品。

## 仓库结构

目录按照“服务能力 -> 云市场平台 -> 编程语言”的结构组织，便于直接定位示例代码。

```text
.
├─ 发票抬头查询
│  ├─ 腾讯云市场
│  └─ 阿里云市场
├─ 文件转换
│  ├─ 腾讯云市场
│  └─ 阿里云市场
├─ 内容安全检测
│  └─ 阿里云市场
└─ _legacy
```

当前示例语言包括：

- Java
- Python
- PHP
- NodeJS
- CSharp

## 鉴权方式

### 腾讯云市场

腾讯云市场示例统一使用 HMAC 鉴权，常用环境变量如下：

- `TENCENT_SECRET_ID`
- `TENCENT_SECRET_KEY`
- `TENCENT_SOURCE`

默认可使用：

- `TENCENT_SOURCE=market`

### 阿里云市场

阿里云市场示例统一使用 `APPCODE` 鉴权，常用环境变量如下：

- `ALIYUN_APPCODE`

请求头格式通常为：

```text
Authorization: APPCODE your_appcode
```

## 常见环境变量

不同示例会按需使用以下环境变量：

- `API_URL`
- `INVOICE_NAME`
- `FILE_NAME`
- `FILE_URL`
- `CALLBACK_URL`
- `TEXT_CHECK_URL`
- `FILE_CHECK_BY_URL_URL`
- `FILE_CHECK_UPLOAD_URL`
- `CHECK_TEXT`
- `UPLOAD_FILE_PATH`

## 使用方式

1. 进入对应服务目录。
2. 选择对应云市场平台目录。
3. 选择对应语言目录。
4. 按示例中的环境变量配置你的接口地址和凭证。
5. 运行示例代码并根据返回结果完成联调。

示例路径：

- `发票抬头查询/腾讯云市场/Python/client.py`
- `文件转换/阿里云市场/Java/ClientDemo.java`
- `内容安全检测/阿里云市场/Python/text_check.py`

## 各服务补充说明

### 发票抬头查询

- 腾讯云市场示例默认保留了当前可用的示例地址。
- 阿里云市场示例中的 `API_URL` 需要替换为你购买商品后获得的真实调用地址。

### 文件转换

- 支持通过文件 URL 发起转换。
- `callbackUrl` 为可选参数，适合异步处理场景。
- 阿里云市场示例中的 `API_URL` 需要替换为真实网关地址。

### 内容安全检测

- 当前仓库示例基于阿里云市场商品 `cmapi00073537`。
- 已整理出 3 个可直接改造的检测接口示例。
- 当前公开信息显示默认域名为 `https://filecheck.market.alicloudapi.com`。

## 关于历史代码

历史示例代码已保留在 `_legacy` 目录，方便回溯或对比，不建议作为当前主入口继续使用。

## 适合谁使用

这个仓库主要适合以下用户：

- 需要快速验证 API 能力的开发者
- 正在做企业系统集成的技术团队
- 有文档处理、开票信息查询、内容审核需求的产品或项目负责人
- 希望先通过云市场采购，再接入正式业务系统的企业客户

如果你正在搜索以下能力，也可以优先查看本仓库对应目录或直接前往云市场购买：

- 文件转换服务
- 发票抬头查询服务
- 文档安全检测服务
- 文本内容安全检测服务

## 联系与采购建议

- 想先验证能力：建议先查看对应语言示例并在云市场下单试用。
- 已有明确业务需求：建议直接购买对应商品并完成联调。
- 需要企业级方案或定制接入：可以直接联系我们沟通具体场景。
