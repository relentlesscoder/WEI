using System;

namespace WEI.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        object DataContext { get; }
    }
}
