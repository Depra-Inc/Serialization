# Depra.Serialization

<div>
    <strong><a href="README.md">English</a> | <a href="README.RU.md">Русский</a></strong>
</div>

<details>
<summary>Оглавление</summary>

- [Введение](#-введение)
    - [Особенности](#-особенности)
    - [Возможности](#-возможности)
- [Установка](#-установка)
- [Содержание](#-содержание)
- [Примеры использования](#-примеры-использования)
- [Расширения](#-расширения)
- [Сотрудничество](#-сотрудничество)
- [Поддержка](#-поддержка)
- [Лицензия](#-лицензия)

</details>

## 🧾 Введение

`Depra.Serialization` - это библиотека, которая предоставляет интерфейсы для абстракции
от способов сериализации и десериализации данных.

### 💡 Особенности:

- **Стандартизация**: Единое API для всех форматов сериализации/десериализации.
- **Расширяемость**: Гибкая архитектура для расширения функциональности по вашим потребностям.
- **Тестирование**: Покрытие кода тестами для обеспечения стабильности и надежности.
- **Документация**: Подробная документация для удобства использования.
- **Лицензия**: Распространяется под лицензией **Apache-2.0**.
- **Бесплатно**: Используйте эту библиотеку в любых проектах, включая коммерческие.
- **Безопасность**: Библиотека не содержит кода, который может нанести вред вашему проекту.
- **Поддержка**: Поддержка библиотеки будет продолжаться в течение неопределенного периода времени.
- **Независимость**: Библиотека не зависит от других библиотек и пакетов.
- **Легковесность**: Библиотека не содержит ненужного кода и зависимостей.
- **Простота**: Библиотека имеет простую и понятную архитектуру.
- **Удобство отладки**: Библиотека бросает понятные исключения и сообщения об ошибках в режиме отладки (`DEBUG`).

### 🦾 Возможности:

- Поддержка сериализации и десериализации в следующих форматах:
    - [x] `Binary`
    - [x] `JSON`
    - [x] `XML`
    - [ ] `YAML`
    - [ ] `CSV`

## 📥 Установка

### 📦 Через **NuGet**:

1. Откройте окно **NuGet Package Manager**.
2. Выберите вкладку **Packages**.
3. Введите в поиске **Depra.Serialization**.
4. Выберите пакет **Depra.Serialization**.
5. Выберите проект, в который вы хотите установить пакет.
6. Нажмите **Install**.

### ⚙️ Ручная:

1. Загрузите ***.dll*** файл из раздела [Releases](https://github.com/Depra-Inc/Serialization/releases)
   или скачайте исходный код.
2. Импортируйте в свой проект.

## 📖 Содержание

**Ключевые концепции**, используемые в этой библиотеке, описаны в следующих интерфейсах:

- `IRawSerializer` - интерфейс для сериализации и десериализации данных в формате `byte[]`.
- `ITextSerializer` - интерфейс для сериализации и десериализации данных в формате `string`.
- `IStreamSerializer` - интерфейс для сериализации и десериализации данных в формате `Stream`.
- `IMemorySerializer` - интерфейс для сериализации и десериализации данных в формате `ReadOnlyMemory<byte>`.

## 📋 Примеры использования

1. Создайте экземпляр сериализатора, который поддерживает формат, который вы хотите использовать.
   Можете зарегистрировать его в **DI контейнере**, если вы используете его в своем проекте.

```csharp
IRawSerializer serializer = new BinarySerializer();
```

2. Используйте методы сериализатора для сериализации и десериализации данных.

```csharp
var serialized = serializer.Serialize<MyDataType>(data);
var deserialized = await serializer.DeserializeAsync(serialized, typeof(MyDataType));
```

## ➕ Расширения

- `Depra.Json.Newtonsoft` - добавляет поддержку сериализации и десериализации в формате `JSON` с помощью
  библиотеки `Newtonsoft.Json`.
- `Depra.Json.Microsoft` - добавляет поддержку сериализации и десериализации в формате `JSON` с помощью
  библиотеки `System.Text.Json`.

## 🤝 Сотрудничество

Я рад приветствовать запросы на добавление новых функций и сообщения об ошибках в
разделе [issues](https://github.com/Depra-Inc/Serialization/issues) и также
принимать [pull requests](https://github.com/Depra-Inc/Serialization/pulls).

## 🫂 Поддержка

Я независимый разработчик,
и большая часть разработки этого проекта выполняется в свободное время.
Если вы заинтересованы в сотрудничестве или найме меня для проекта,
ознакомьтесь с моим [портфолио](https://github.com/Depra-Inc)
и [свяжитесь со мной](mailto:g0dzZz1lla@yandex.ru)!

## 🔐 Лицензия

Этот проект распространяется под лицензией
**[Apache-2.0](https://github.com/Depra-Inc/Serialization/blob/main/LICENSE.md)**

Copyright (c) 2023 Николай Мельников
[n.melnikov@depra.org](mailto:n.melnikov@depra.org)