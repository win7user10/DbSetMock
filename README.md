# DbSetMock

### When should I use it?

If you have troubles with testing some code which executes direct queries to your database like this:

```csharp
string query = @"select smth from smth_table";

var data = await _db.SomeDbSet
  .FromSqlRaw(query)
  .AsNoTracking()
  .FirstOrDefaultAsync();
//etc.
```

and you want to have way to test code, which uses these constructions

### How do I get started?

Create DB set which will returns some data for any query to this set like this

```csharp
var data = new List<SomeEntity>
{
  new SomeEntity()
};
YourDbContext.SomeDbSet = data.CreateMockedDbSet();
```

Or you can return the mock, which can be additionaly setuped.

```csharp
var data = new List<SomeEntity>
{
  new SomeEntity()
};
var someDbSetMock = data.CreateMockFromDbSet();

// Some mock additional setuping
// someDbSetMock.Setup()
YourDbContext.SomeDbSet = someDbSetMock.Object;
```