# IniFile.NET
IniFile.NET is a .NET library to open, modify and save .INI files.

The `IniFile.Ini` class maintains the structure of an .INI file as an in-memory object model, with objects for sections, properties (key-value pairs), comments and blank lines, allowing it to model the exact structure of a .INI file.

The `IniFile.Ini` class is a collection of `Section` objects (`IList<Section>`). Each `Section` is additionally a collection of `Property` objects (`IList<Property>`).

Both `Section` and `Property` objects contain a collection of minor objects, namely `Comment` and `BlankLine` objects, which are the comments and blank lines that appear before the respective sections and properties.

## Loading an existing .INI
The `Ini` class provides several constructor overloads to load .INI data from streams, text readers and files.
```cs
// Load INI data from a file
var ini = new Ini(@"Settings.ini");
```

The class also provides a static factory method `Load` to create an `Ini` object from a string.
```cs
const iniStr = File.ReadAllText(@"Settings.ini");
Ini ini = Ini.Load(iniStr);
```

### `IniLoadSettings`
All `Ini` constructors and the `Load` static factory method accept an optional `IniLoadSettings` object to control how the .INI data is loaded and handled.

|Property|Description|Default|
|--------|-----------|-------|
|`Encoding`|The character encoding to use when reading or writing data to the INI file.|`UTF-8`|
|`DetectEncoding`|A `bool` indicating whether the character encoding should be automatically detected when the INI file is loaded.|`false`|
|`CaseSensitive`|A `bool` indicating whether the section names and property key names in the INI file are case sensitive.|`false`|

```cs
var loadSettings = new IniLoadSettings
{
    Encoding = Encoding.Unicode,
    DetectEncoding = true,
    CaseSensitive = true
};
var ini = new Ini(stream, loadSettings);
```

## Creating a INI file
This code:
```cs
var ini = new Ini(@"Players.ini")
{
    new Section("Players", "This section defines the players")
    {
        new Property("Player1", "The Flash"),
        new Property("Player2", "Superman")
    },
    new Section("The Flash", string.Empty)
    {
        ["Level"] = "9",
        ["Power"] = "Superspeed"
    },
    new Section("Superman", string.Empty)
    {
        ["Level"] = "9",
        ["Power"] = "Superstrength,heat vision"
    }
};
```
produces the following INI data:
```ini
; This section defines the players
[Players]
Player1 = The Flash
Player2 = Superman

[The Flash]
Level = 9
Power = Superspeed

[Superman]
Level = 9
Power = Superstrength,heat vision
```

## Saving the INI content
The `Ini` class provides several overloads to save the INI content to streams, text writers and files. All these overloads have synchronous and async versions.
```cs
// Synchronous call
ini.SaveTo(@"Setting.ini");

// Asynchronous call
await ini.SaveToAsync(stream);
```

## Formatting the INI content
The `Ini` class retains the exact formatting from the source .INI file content. It provides a `Format` method to correctly format the contents.

By default, the `Format` method resets the padding for all lines in the INI file.
```cs
var ini = new Ini(iniFilePath);
ini.Format();
ini.SaveTo(iniFilePath);
```

In addition, the `Format` method takes an optional `IniFormatOptions` parameter that can specify additional formatting options:

|Option|Description|Default|
|------|-----------|-------|
|`EnsureBlankLinesBetweenSections`|If true, a blank line is inserted between each section.|`false`|
|`EnsureBlankLinesBetweenProperties`|If true, a blank line is inserted between each property.|`false`|
|`RemoveSuccessiveBlankLines`|If true, any successive blank lines are removed.|`false`|

```cs
var ini = new Ini(iniFilePath);
ini.Format(new IniFormatOptions
{
    EnsureBlankLinesBetweenSections = true,
    RemoveSuccessiveBlankLines = true
});
ini.SaveTo(iniFilePath);
```
