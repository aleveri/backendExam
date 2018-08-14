using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace SB.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        DbContext Context { get; }
        Task Commit();
    }
}
