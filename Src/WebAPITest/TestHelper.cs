using Microsoft.EntityFrameworkCore;
using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPITest
{
    public class TestHelper
    {
        private readonly AppDbContext DbContext;
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase(databaseName: "DbInMemory");
            var dbContextOptions = builder.Options;

            DbContext = new AppDbContext(dbContextOptions);
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();
        }

        public AppDbContext Context
        {
            get { return DbContext; }
        }

        public void SeedData()
        {

            //DbContext.AddAsync();
        }

    }
}
