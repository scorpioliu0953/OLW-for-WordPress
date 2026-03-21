# OLW for WordPress

桌面版 WordPress 部落格編輯器，從 [Open Live Writer](https://github.com/OpenLiveWriter/OpenLiveWriter) 分支而來。

**核心改動：** 移除舊有的 XML-RPC 協定，改用更安全的 **WordPress REST API** 進行通訊。

## 與原版差異

| | Open Live Writer | OLW for WordPress |
|---|---|---|
| 通訊協定 | XML-RPC | WordPress REST API (v2) |
| 驗證方式 | XML-RPC 帳密 | 應用程式密碼 (WP 5.6+) |
| 支援平台 | WordPress、Blogger、MovableType、LiveJournal、SharePoint 等 | **僅支援 WordPress** |
| 安全性 | XML-RPC 常被安全外掛停用 | REST API 是官方標準，永遠可用 |

## 系統需求

- **WordPress 4.7+**（內建 REST API）
- **WordPress 5.6+** 建議（內建應用程式密碼功能）
- Windows 7 以上，.NET Framework 4.6.1

## 使用設定

1. 進入 WordPress 後台，前往 **使用者 > 個人資料 > 應用程式密碼**
2. 輸入名稱（例如「OLW」），點擊 **新增應用程式密碼**
3. 複製產生的密碼
4. 在 OLW for WordPress 中輸入網站網址、WordPress 使用者名稱，以及應用程式密碼

## 從原始碼編譯

### 前置需求

- Visual Studio 2015+ 或 MSBuild 14.0+
- NuGet CLI

### 本機編譯（Windows）

```cmd
build.cmd
```

### 透過 GitHub Actions 編譯（推薦）

每次 push 到 `main` 分支都會自動在 GitHub Actions 的 Windows 環境上編譯。不需要自己有 Windows 電腦。

1. Push 程式碼
2. 到本 Repository 的 **Actions** 頁籤查看
3. 編譯完成後下載產出檔案

push 到 `main` 時也會自動建立安裝檔。

## 專案架構

```
src/managed/
  OpenLiveWriter.BlogClient/
    Clients/WordPressRestClient.cs    # 核心 REST API 用戶端
    Detection/BlogServiceDetector.cs  # 自動偵測 WordPress REST API
  OpenLiveWriter.PostEditor/          # 文章編輯器介面
  OpenLiveWriter.CoreServices/        # 共用工具程式庫
  OpenLiveWriter/                     # 應用程式進入點
  writer.sln                          # 方案檔
```

### REST API 用戶端

`WordPressRestClient` 透過 `/wp-json/wp/v2/` 端點與 WordPress 溝通：

- **文章與頁面** - 完整的新增、讀取、更新、刪除，支援標題、內容、摘要、代稱、狀態、分類、標籤
- **分類與標籤** - 列出、建立、指派
- **媒體** - 透過 multipart POST 上傳圖片與檔案
- **作者** - 列出可用的作者

驗證採用 HTTP Basic Auth 搭配應用程式密碼，透過 HTTPS 傳輸。

## 歷史

Live Writer 最初由 JJ Allaire、Joe Cheng、Charles Teague 和 Spike Washburn 等工程師團隊開發。
該團隊於 2006 年被 Microsoft 收購。2015 年 12 月，Microsoft 將程式碼捐贈給 .NET Foundation，成為 Open Live Writer。

本分支移除了所有舊有部落格平台支援與 XML-RPC，專注於透過現代 REST API 連接 WordPress。

## 授權條款

採用 [MIT 授權條款](license.txt)。

## .NET Foundation

原始 Open Live Writer 專案由 [.NET Foundation](http://www.dotnetfoundation.org) 支持。
