# 内容安全检测

该目录提供阿里云市场商品 `cmapi00073537` 的客户端示例。

商品名称：

- `文本和文档内容安全合规敏感词检测 API｜支持 Word/PDF/图片/Excel/PPT`

## 当前包含的 3 个接口

### 1. 文本检测

接口名称：

- `wordscheckTextCheck`

适用于评论审核、聊天审核、表单内容审核、用户输入校验等场景。

### 2. 远程文件 URL 检测

接口名称：

- `wordscheckFileCheckByUrl`

适用于文件已经存储在 OSS、CDN 或业务文件服务器上的场景。

### 3. 文件上传检测

接口名称：

- `wordscheckFileCheck`

适用于直接上传本地文件进行检测的场景。

## 鉴权方式

阿里云市场示例统一使用：

- `Authorization: APPCODE your_appcode`

## 默认域名

根据阿里云市场页面公开数据，该商品绑定的默认域名为：

- `https://filecheck.market.alicloudapi.com`

## 已确认的接口地址

- 文本检测：`POST /openapi/v1/text/check`
- 远程文件 URL 检测：`POST /openapi/v1/file/check-by-url`
- 文件上传检测：`POST /openapi/v1/file/check`

## 环境变量

所有示例统一使用以下环境变量中的一部分：

- `ALIYUN_APPCODE`
- `TEXT_CHECK_URL`
- `FILE_CHECK_BY_URL_URL`
- `FILE_CHECK_UPLOAD_URL`
- `CHECK_TEXT`
- `FILE_NAME`
- `FILE_URL`
- `UPLOAD_FILE_PATH`

## 说明

- 由于阿里云市场公开页面没有直接返回完整调试文档，这里已将域名、接口名和常见调用方式整理为可直接改造的示例
- 目前 3 个接口的 URL 路径已根据调试页面截图确认
- 如果后续参数名和返回结构还有细节调整，只需要在这些示例中替换对应字段即可
