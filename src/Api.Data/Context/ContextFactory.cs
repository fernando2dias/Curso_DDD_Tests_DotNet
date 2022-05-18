using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var connectionString = new SqliteConnectionStringBuilder("Data Source=Application.db;Cache=Shared")
            {
                Mode = SqliteOpenMode.ReadWriteCreate,
                // Password = password
            }.ToString();
            //   var connectionString = "dataBase.db";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlite(connectionString);
            return new MyContext(optionsBuilder.Options);

        }
    }
}