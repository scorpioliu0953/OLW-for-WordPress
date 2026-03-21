# OLW for WordPress

A desktop blog editor for WordPress, forked from [Open Live Writer](https://github.com/OpenLiveWriter/OpenLiveWriter).

**Key change:** Replaced the legacy XML-RPC protocol with the modern **WordPress REST API**, providing better security and reliability.

## What's Different

| | Open Live Writer | OLW for WordPress |
|---|---|---|
| Protocol | XML-RPC | WordPress REST API (v2) |
| Authentication | XML-RPC credentials | Application Passwords (WP 5.6+) |
| Supported platforms | WordPress, Blogger, MovableType, LiveJournal, SharePoint, etc. | **WordPress only** |
| Security | XML-RPC often disabled by security plugins | REST API is the standard, always available |

## Requirements

- **WordPress 4.7+** (REST API built-in)
- **WordPress 5.6+** recommended (Application Passwords built-in)
- Windows 7 or later, .NET Framework 4.6.1

## Setup

1. In your WordPress admin, go to **Users > Profile > Application Passwords**
2. Enter a name (e.g. "OLW") and click **Add New Application Password**
3. Copy the generated password
4. In OLW for WordPress, enter your site URL, WordPress username, and the application password

## Building from Source

### Prerequisites

- Visual Studio 2015+ or MSBuild 14.0+
- NuGet CLI

### Build locally (Windows)

```cmd
build.cmd
```

### Build via GitHub Actions (recommended)

Every push to `main` triggers an automatic build on GitHub Actions using a Windows runner. No local Windows machine needed.

1. Push your code
2. Go to **Actions** tab in this repository
3. Download build artifacts when the workflow completes

The workflow also creates an installer package on pushes to `main`.

## Architecture

```
src/managed/
  OpenLiveWriter.BlogClient/
    Clients/WordPressRestClient.cs    # Core REST API client
    Detection/BlogServiceDetector.cs  # Auto-detects WordPress REST API
  OpenLiveWriter.PostEditor/          # Post editing UI
  OpenLiveWriter.CoreServices/        # Shared utilities
  OpenLiveWriter/                     # Application entry point
  writer.sln                          # Solution file
```

### REST API Client

`WordPressRestClient` communicates with WordPress via `/wp-json/wp/v2/` endpoints:

- **Posts & Pages** - Full CRUD with title, content, excerpt, slug, status, categories, tags
- **Categories & Tags** - List, create, and assign
- **Media** - Upload images and files via multipart POST
- **Authors** - List available authors

Authentication uses HTTP Basic Auth with Application Passwords, sent over HTTPS.

## History

The product that became Live Writer was originally created by a small, super-talented team of engineers including
JJ Allaire, Joe Cheng, Charles Teague, and Spike Washburn. The team was acquired by Microsoft in 2006.
In December 2015, Microsoft donated the code to the .NET Foundation as Open Live Writer.

This fork removes all legacy blog platform support and XML-RPC, focusing exclusively on WordPress with the modern REST API.

## License

Licensed under the [MIT License](license.txt).

## .NET Foundation

The original Open Live Writer project is supported by the [.NET Foundation](http://www.dotnetfoundation.org).
