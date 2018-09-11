using System;

namespace Lottery.Service.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commits all changes
        /// </summary>
        void Commit();
    }
}
