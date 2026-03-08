# 气象业务监控软件

这是一个基于 Windows Forms 的气象业务监控客户端，用于按时次检查多类气象资料到报情况，并在缺报超过阈值时进行界面告警、事件落库、短信提醒和语音通知。

## 项目概览

- 解决方案：`WindowsFormsApplication2.sln`
- 主项目：`WindowsFormsApplication2/业务监控软件.csproj`
- 技术栈：`.NET Framework 4.5`、`WinForms`、`MySQL`
- 主要依赖：`MySql.Data 6.7.9`、`Newtonsoft.Json`、`RestSharp`

程序入口位于 `WindowsFormsApplication2/Program.cs`，启动后直接进入主界面 `Form1`。

## 主要功能

- 定时巡检常规资料和不规则资料的到报情况
- 同时调用国家局公共接口和本省接口进行缺报比对
- 在主界面中展示各类资料的状态、时次、缺报数和缺报站点
- 缺报超过阈值时写入 `Monitor.EVENT_LOG`
- 支持短信提醒
- 支持腾讯云语音外呼通知
- 提供配置查看和部分配置更新能力

## 核心模块

- `Form1.cs`
  - 主监控界面，负责启动监控、展示状态、触发告警
- `DataFlow.cs`
  - 核心监控流程，遍历配置并生成每类资料的状态结果
- `PublicInterface.cs`
  - 国家局公共接口调用
- `PrivateInterface.cs`
  - 本省接口调用
- `SMS.cs`
  - 短信发送
- `TencentVmsInterface.cs`
  - 腾讯云语音通知
- `InsterMysql.cs`
  - 事件日志、语音日志写库
- `getSubsystem_ALLConfig.cs`
  - 读取常规资料监控配置表 `subsystem_allconfig`
- `getIrregularSubsystem_config.cs`
  - 读取不规则资料配置表 `subsystem_allconfig_irregular`
- `getStationInfo.cs`
  - 读取站点和联系方式信息
- `UpdateSubsystem_ALLConfig.cs`
  - 更新部分监控配置

## 监控流程

1. 读取 MySQL 中的常规和不规则监控配置。
2. 根据当前 UTC 时次、业务频次和监控分钟判断当前是否需要检查。
3. 调用公共接口获取到报数量和到报站点。
4. 调用本省接口获取到报站点。
5. 以配置中的 `all_station_info` 为基准，计算双接口共同缺报的站点列表。
6. 将实际缺报数与阈值 `absent_sum` 比较，生成正常或异常状态。
7. 异常时更新主界面、写入事件日志，并按配置触发短信或语音通知。

## 数据库与配置

项目当前将数据库连接信息直接写在代码中，接手维护时应优先检查以下文件：

- `WindowsFormsApplication2/Form1.cs`
- `WindowsFormsApplication2/login.cs`
- `WindowsFormsApplication2/getStationInfo.cs`
- `WindowsFormsApplication2/getSubsystem_ALLConfig.cs`
- `WindowsFormsApplication2/getIrregularSubsystem_config.cs`
- `WindowsFormsApplication2/InsterMysql.cs`
- `WindowsFormsApplication2/SmsAssistant.cs`
- `WindowsFormsApplication2/TencentVmsInterface.cs`

从代码可见，至少使用到了以下表：

- `Monitor.subsystem_allconfig`
- `Monitor.subsystem_allconfig_irregular`
- `Monitor.EVENT_LOG`
- `USER`

另外，仓库根目录中的 `STATION_INFO_copy1.xlsx` 很可能是站点信息维护用的辅助文件。

## 构建与运行

### 环境要求

- Windows
- Visual Studio 2019 或更高版本
- .NET Framework 4.5 Developer Pack
- 可访问目标 MySQL 数据库
- 可访问国家局公共接口、本省接口、短信接口、腾讯云语音接口

### 打开方式

1. 用 Visual Studio 打开 `WindowsFormsApplication2.sln`
2. 还原 NuGet 包
3. 修正缺失的外部 DLL 引用
4. 检查数据库连接、接口地址、账号密钥
5. 编译并运行

## 当前仓库的已知问题

### 1. 外部 DLL 引用不可直接复用

`业务监控软件.csproj` 中的 `Newtonsoft.Json` 和 `RestSharp` 仍然指向开发机本地绝对路径：

- `F:\c#\winForm\基于ES库的语音告警一体化系统\...`

这意味着项目在新机器上通常会直接编译失败。建议改为：

- 通过 NuGet 正式引用 `Newtonsoft.Json`
- 通过 NuGet 正式引用 `RestSharp`
- 或将依赖 DLL 统一放入仓库内的 `lib` 目录并改为相对路径

### 2. 配置硬编码较多

数据库地址、账号、接口地址、短信内容、语音参数等信息大量写死在源码中，不利于部署和维护。建议逐步迁移到：

- `App.config`
- 独立配置文件
- 环境区分配置

### 3. 存在安全风险

部分 SQL 语句使用字符串拼接，登录逻辑和写库逻辑都存在 SQL 注入风险，建议尽快改为参数化查询。

## 建议的整理方向

- 把所有连接串和第三方接口配置集中管理
- 移除开发机绝对路径依赖
- 为数据库访问统一封装参数化查询
- 为接口调用增加超时、重试和错误日志
- 补充部署文档和数据库初始化脚本

## 目录结构

```text
.
|-- README.md
|-- STATION_INFO_copy1.xlsx
|-- WindowsFormsApplication2.sln
`-- WindowsFormsApplication2
    |-- Program.cs
    |-- Form1.cs
    |-- DataFlow.cs
    |-- PublicInterface.cs
    |-- PrivateInterface.cs
    |-- SMS.cs
    |-- TencentVmsInterface.cs
    |-- InsterMysql.cs
    |-- getStationInfo.cs
    |-- getSubsystem_ALLConfig.cs
    |-- getIrregularSubsystem_config.cs
    `-- 业务监控软件.csproj
```

## 维护说明

如果后续要继续维护这个项目，建议先完成三件事：

1. 修复 `csproj` 中的绝对路径依赖，确保项目能在新环境稳定编译。
2. 抽离数据库与第三方接口配置，避免再次出现环境绑定。
3. 梳理数据库表结构和各类资料编码含义，补齐正式运维文档。
