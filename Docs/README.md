# Depra.Serialization

<div>
    <strong><a href="README.md">English</a> | <a href="README.RU.md">Русский</a></strong>
</div>

<details>
<summary>Table of Contents</summary>

- [Introduction](#-introduction)
    - [Features](#-features)
    - [Capabilities](#-capabilities)
- [Installation](#-installation)
- [Contents](#-contents)
- [Usage Examples](#-usage-examples)
- [Extensions](#-extensions)
- [Collaboration](#-collaboration)
- [Support](#-support)
- [License](#-license)

</details>

## 🧾 Introduction

`Depra.Serialization` is a library that provides interfaces for abstracting serialization and deserialization of data.

### 💡 Features:

- **Standardization**: Unified API for all serialization/deserialization formats.
- **Extensibility**: Flexible architecture for extending functionality to suit your needs.
- **Testing**: Code coverage with tests to ensure stability and reliability.
- **Documentation**: Detailed documentation for ease of use.
- **License**: Distributed under the **Apache-2.0** license.
- **Free**: Use this library in any projects, including commercial ones.
- **Security**: The library does not contain code that can harm your project.
- **Support**: Library support will continue indefinitely.
- **Independence**: The library does not depend on other libraries and packages.
- **Lightweight**: The library does not contain unnecessary code and dependencies.
- **Simplicity**: The library has a simple and understandable architecture.
- **Debugging Convenience**: The library throws clear exceptions and error messages in debug mode (`DEBUG`).

### 🦾 Capabilities:

- Support for serialization and deserialization in the following formats:
    - [x] `Binary`
    - [x] `JSON`
    - [x] `XML`
    - [ ] `YAML`
    - [ ] `CSV`

## 📥 Installation

### 📦 Via **NuGet**:

1. Open the **NuGet Package Manager** window.
2. Select the **Packages** tab.
3. Search for **Depra.Serialization**.
4. Choose the **Depra.Serialization** package.
5. Select the project where you want to install the package.
6. Click **Install**.

### ⚙️ Manual:

1. Download the ***.dll*** file from the [Releases](https://github.com/Depra-Inc/Serialization/releases) section
   or download the source code.
2. Import it into your project.

## 📖 Contents

The key concepts used in this library are described in the following interfaces:

- `IRawSerializer` - Interface for serializing and deserializing data in `byte[]` format.
- `ITextSerializer` - Interface for serializing and deserializing data in `string` format.
- `IStreamSerializer` - Interface for serializing and deserializing data in `Stream` format.
- `IMemorySerializer` - Interface for serializing and deserializing data in `ReadOnlyMemory<byte>` format.

## 📋 Usage Examples

1. Create an instance of the serializer that supports the format you want to use.
   You can register it in the **DI container** if you are using it in your project.

```csharp
IRawSerializer serializer = new BinarySerializer();
```

2. Use the serializer to serialize and deserialize data.

```csharp
var serialized = serializer.Serialize<MyDataType>(data);
var deserialized = await serializer.DeserializeAsync(serialized, typeof(MyDataType));
```

## ➕ Extensions

- `Depra.Json.Newtonsoft` - Adds support for serialization and deserialization in JSON format
  using the `Newtonsoft.Json` library.
- `Depra.Json.Microsoft` - Adds support for serialization and deserialization in JSON format
  using the `System.Text.Json` library.

## 🤝 Collaboration

I welcome feature requests and bug reports
in the [issues](https://github.com/Depra-Inc/Serialization/issues) section,
and I also accept [pull requests](https://github.com/Depra-Inc/Serialization/pulls).

## 🫂 Support

I am an independent developer,
and most of the development of this project is done in my spare time.
If you are interested in collaborating or hiring me for a project,
please check out my [portfolio](https://github.com/Depra-Inc)
and [contact me](mailto:g0dzZz1lla@yandex.ru)!

## 🔐 License

This project is distributed under
the **[Apache-2.0](https://github.com/Depra-Inc/Serialization/blob/main/LICENSE.md)** license.

Copyright (c) 2023 Nikolay Melnikov
[n.melnikov@depra.org](mailto:n.melnikov@depra.org)