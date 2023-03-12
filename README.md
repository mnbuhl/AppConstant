# AppConstant
___
AppConstant is a simple type-safe constant* library for C# and ASP.NET Core.
It allows you to define type-safe constants that translates to web and database friendly values.

<sub><sub>* technically not a constant</sub></sub>

## Installation (coming soon)
___

[//]: # (AppConstant is available on NuGet. Install it using the following command:)

[//]: # ()
[//]: # (    dotnet add package AppConstant.AspNetCore)

[//]: # ()
[//]: # (Planning on using it together with Entity Framework Core? Install the following package as well:)

[//]: # ()
[//]: # (    dotnet add package AppConstant.EntityFrameworkCore)

[//]: # ()
[//]: # (Or just planning on using it in a C# project? Install the following package:)

[//]: # ()
[//]: # (    dotnet add package AppConstant)

## Usage
___

### Defining a constant

To define a constant, you need to create a class that inherits from `AppConstant<TConst, TValue>`.
The first type parameter is the type of the constant itself, and the second type parameter is the type of the value that the constant represents.

```csharp
public class MediaType : AppConstant<MediaType, string>
{
    public static MediaType Image => Set("image");
    public static MediaType Video => Set("video");
    public static MediaType Audio => Set("audio");
}
```

### Using a constant

To use a constant, just use the static property that is defined on the constant class.

```csharp
var image = MediaType.Image;
```

### Using a constant on an entity

To use a constant on an entity, you need to define a property of the constant type.

```csharp
public class Media
{
    public int Id { get; set; }
    public string Name { get; set; }
    public MediaType Type { get; set; }
}
```


### Using AppConstant with ASP.NET Core

To use AppConstant with ASP.NET Core, you need to register the AppConstant service after the AddControllers method.

```csharp
builder.Services.AddControllers().AddAppConstant();
```

If your constants are defined in a different assembly, you can specify that assembly as well.

```csharp
builder.Services.AddControllers().AddAppConstant(typeof(MediaType).Assembly);
```


### Using AppConstant with Entity Framework Core

To use AppConstant with Entity Framework Core, you need to register the AppConstant in the ConfigureConventions override method in the DbContext.

```csharp
protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
{
    configurationBuilder.AddAppConstantConverters();
}
```

### Motivation
___

Having worked on a few projects where constants were used, I've noticed that there are a few problems with them:

1. You can pass any value to a method that expects a constant, and the compiler will not complain.
2. There is no direct relation between the entity and the constant. It just lives on the entity as a primitive type.
3. There aren't any simple implementations of constant like behavior in C#.

### Inspiration
___
The package is heavily inspired by the [SmartEnum](https://github.com/ardalis/SmartEnum) package by [ardalis](https://github.com/ardalis). A lot of implementation details are used from that package.