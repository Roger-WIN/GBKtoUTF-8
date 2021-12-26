# GBK to UTF-8

这是一个将文本文件由 GBK 转码为 UTF-8 的小工具。

![image-20211226121734405](https://img.rogerkung-win.top/undefinedimage-20211226121734405.png)

可以选择多个文件，或者选择一个文件夹（支持在子文件夹中查找），但二者不可同时选择。

## 下载

https://github.com/Roger-WIN/GBKtoUTF-8/releases

提供两种可执行文件：

- `GBK.to.UTF-8_with-runtime.exe`：已包含框架（.NET 桌面运行时），无需另外安装，可直接运行；
- `GBK.to.UTF-8.exe`：未包含框架，需自行安装，方可运行。

## 系统要求

- | 操作系统版本 |                           安装要求                           |
  | :----------: | :----------------------------------------------------------: |
  |  Windows 11  |                           所有版本                           |
  |  Windows 10  |                       1607 或更高版本                        |
  | Windows 8.1  | [VC++ 2015~2019 运行库](https://aka.ms/vs/16/release/vc_redist.x64.exe) |
  |  Windows 7   | [VC++ 2015~2019 运行库](https://aka.ms/vs/16/release/vc_redist.x64.exe) |

- [.NET 6 桌面运行时](https://dotnet.microsoft.com/zh-cn/download/dotnet/thank-you/runtime-desktop-6.0.1-windows-x64-installer)

## 开发

### 先决条件

- [Windows 10](https://www.microsoft.com/zh-cn/software-download/windows10)（1909 或更高版本），[Windows 11](https://www.microsoft.com/zh-cn/software-download/windows11)
- [Microsoft Visual Studio 2022](https://visualstudio.microsoft.com/zh-hans/vs/)
- [.NET 6 SDK](https://dotnet.microsoft.com/zh-cn/download/dotnet/thank-you/sdk-6.0.101-windows-x64-installer)
