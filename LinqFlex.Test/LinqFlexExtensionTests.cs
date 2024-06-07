using LinqFlex;
namespace LinqFlexExtension.Test
{
    public class LinqFlexExtensionTests
    {
        [Fact]
        public void OrderBy_ShouldSortAscending_ByKeySelector()
        {
            var data = new[]
            {
                new Person { Id = 1, Name = "Alice" },
                new Person { Id = 2, Name = "Bob" },
                new Person { Id = 3, Name = "Charlie" }
            }.AsQueryable();

            var sortedData = data.OrderBy(p => p.Name, true);

            Assert.Equal(new[] { "Alice", "Bob", "Charlie" }, sortedData.Select(p => p.Name));
        }

        [Fact]
        public void OrderBy_ShouldSortDescending_ByKeySelector()
        {
            var data = new[]
            {
                new Person { Id = 1, Name = "Alice" },
                new Person { Id = 2, Name = "Bob" },
                new Person { Id = 3, Name = "Charlie" }
            }.AsQueryable();

            var sortedData = data.OrderBy(p => p.Name, false);

            Assert.Equal(new[] { "Charlie", "Bob", "Alice" }, sortedData.Select(p => p.Name));
        }

        [Fact]
        public void OrderBy_ShouldSortByMultipleProperties_ByKeySelector()
        {
            var data = new[]
            {
                new Person { Id = 1, Name = "Alice", Age = 25 },
                new Person { Id = 2, Name = "Alice", Age = 30 },
                new Person { Id = 3, Name = "Bob", Age = 20 },
                new Person { Id = 4, Name = "Bob", Age = 35 }
            }.AsQueryable();

            var sortedData = data.OrderBy(x => x.Name, true).ThenBy(p => p.Age);

            Assert.Equal(new[] { 1, 2, 3, 4 }, sortedData.Select(p => p.Id));
        }

        [Fact]
        public void OrderBy_ShouldSortAscending_ByPropertyName()
        {
            var data = new[]
            {
                new Person { Id = 1, Name = "Alice" },
                new Person { Id = 2, Name = "Bob" },
                new Person { Id = 3, Name = "Charlie" }
            }.AsQueryable();

            var sortedData = data.OrderBy("Name", true);

            Assert.Equal(new[] { "Alice", "Bob", "Charlie" }, sortedData.Select(p => p.Name));
        }

        [Fact]
        public void OrderBy_ShouldSortDescending_ByPropertyName()
        {
            var data = new[]
            {
                new Person { Id = 1, Name = "Alice" },
                new Person { Id = 2, Name = "Bob" },
                new Person { Id = 3, Name = "Charlie" }
            }.AsQueryable();

            var sortedData = data.OrderBy("Name", false);

            Assert.Equal(new[] { "Charlie", "Bob", "Alice" }, sortedData.Select(p => p.Name));
        }

        [Fact]
        public void OrderBy_ShouldSortDescending_ByPropertyName_CaseInsensitive()
        {
            var data = new[]
            {
                new Person { Id = 1, Name = "Alice" },
                new Person { Id = 2, Name = "Bob" },
                new Person { Id = 3, Name = "Charlie" }
            }.AsQueryable();

            var sortedData = data.OrderBy("nAMe", false);

            Assert.Equal(new[] { "Charlie", "Bob", "Alice" }, sortedData.Select(p => p.Name));
        }

        [Fact]
        public void OrderBy_ShouldSortByMultipleProperties_ByPropertyName()
        {
            var data = new[]
            {
                new Person { Id = 1, Name = "Alice", Age = 25 },
                new Person { Id = 2, Name = "Alice", Age = 30 },
                new Person { Id = 3, Name = "Bob", Age = 20 },
                new Person { Id = 4, Name = "Bob", Age = 35 }
            }.AsQueryable();

            var sortedData = data.OrderBy("Name", true).ThenBy(p => p.Age);

            Assert.Equal(new[] { 1, 2, 3, 4 }, sortedData.Select(p => p.Id));
        }

        [Fact]
        public void OrderBy_ShouldThrowExceptionForInvalidProperty_ByPropertyName()
        {
            var data = new[]
            {
                new Person { Id = 1, Name = "Alice" },
                new Person { Id = 2, Name = "Bob" },
                new Person { Id = 3, Name = "Charlie" }
            }.AsQueryable();

            Assert.Throws<ArgumentException>(() => data.OrderBy("NonExistentProperty"));
        }

        [Fact]
        public void OrderBy_ShouldHandleEmptyCollection_ByKeySelector()
        {
            var data = new Person[] { }.AsQueryable();

            var sortedData = data.OrderBy(p => p.Name, true);

            Assert.Empty(sortedData);
        }

        [Fact]
        public void OrderBy_ShouldHandleEmptyCollection_ByPropertyName()
        {
            var data = new Person[] { }.AsQueryable();

            var sortedData = data.OrderBy("Name", true);

            Assert.Empty(sortedData);
        }

        [Fact]
        public void OrderBy_ShouldHandleNullProperties_ByKeySelector()
        {
            var data = new[]
            {
                new Person { Id = 1, Name = null },
                new Person { Id = 2, Name = "Bob" },
                new Person { Id = 3, Name = "Charlie" }
            }.AsQueryable();

            var sortedData = data.OrderBy(p => p.Name, true);

            Assert.Equal(new[] { 1, 2, 3 }, sortedData.Select(p => p.Id));
        }

        [Fact]
        public void OrderBy_ShouldHandleNullProperties_ByPropertyName()
        {
            var data = new[]
            {
                new Person { Id = 1, Name = null },
                new Person { Id = 2, Name = "Bob" },
                new Person { Id = 3, Name = "Charlie" }
            }.AsQueryable();

            var sortedData = data.OrderBy("Name", true);

            Assert.Equal(new[] { 1, 2, 3 }, sortedData.Select(p => p.Id));
        }

    }

    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}
