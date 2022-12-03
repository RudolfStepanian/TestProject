using WebApplication4.Core.IRepositories;

namespace WebApplication4.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        Task CompleteAsync();
    }
}
