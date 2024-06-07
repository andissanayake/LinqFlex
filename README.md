# LinqFlex

LinqFlex is a .NET library that enables dynamic ordering of IQueryable collections. It allows you to sort data dynamically at runtime based on property names or key selectors, providing greater flexibility in data manipulation.

## Installation

You can install the LinqFlex package via NuGet Package Manager:

```bash
dotnet add package LinqFlex
```

Or via the NuGet Package Manager Console:

```bash
Install-Package LinqFlex
```

## Usage
Key Selector Example
You can order your data using a key selector expression. 

```c#
using System.Linq;
using LinqFlex;

public class Example
{
    public void SortUsingKeySelector()
    {
        var data = new[]
        {
            new Person { Id = 1, Name = "Alice" },
            new Person { Id = 2, Name = "Bob" },
            new Person { Id = 3, Name = "Charlie" }
        }.AsQueryable();

        // Sort by Name in ascending order
        var sortedData = data.OrderBy(p => p.Name, true);

        // Sort by Name in descending order
        var sortedDataDesc = data.OrderBy(p => p.Name, false);
    }
}

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```

## Property Name Example
You can also order your data using a property name string. This is useful when the property to sort by is determined at runtime.
```c#
using System.Linq;
using LinqFlex;

public class Example
{
    public void SortUsingPropertyName()
    {
        var data = new[]
        {
            new Person { Id = 1, Name = "Alice" },
            new Person { Id = 2, Name = "Bob" },
            new Person { Id = 3, Name = "Charlie" }
        }.AsQueryable();

        // Sort by Name in ascending order
        var sortedData = data.OrderBy("Name", true);

        // Sort by Name in descending order
        var sortedDataDesc = data.OrderBy("Name", false);
    }
}

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```

## Chaining Multiple Sorts
You can chain multiple sorts using ThenBy to order your data by adding additional properties further.

```c#
using System.Linq;
using LinqFlex;

public class Example
{
    public void SortUsingMultipleProperties()
    {
        var data = new[]
        {
            new Person { Id = 1, Name = "Alice", Age = 25 },
            new Person { Id = 2, Name = "Alice", Age = 30 },
            new Person { Id = 3, Name = "Bob", Age = 20 },
            new Person { Id = 4, Name = "Bob", Age = 35 }
        }.AsQueryable();

        // Sort by Name in ascending order, then by Age in ascending order
        var sortedData = data.OrderBy(x => x.Name, true).ThenBy(p => p.Age);

        // Sort by Name in descending order, then by Age in ascending order
        var sortedDataDesc = data.OrderBy("Name", false).ThenBy(p => p.Age);
    }
}

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```
## License

This project is licensed under the [MIT License](LICENSE).
