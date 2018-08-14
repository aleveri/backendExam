using Microsoft.EntityFrameworkCore;
using SB.Interfaces;
using System.Threading.Tasks;

namespace SB.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }

        public UnitOfWork(DbContext conext) => Context = conext;

        public Task Commit() => Context.SaveChangesAsync();

        public void Dispose() => Context.Dispose();
    }
}
