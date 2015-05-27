namespace Shitter.Data.UnitOfWork
{
    using Shitter.Models;
    using Shitter.Data.Repositories;

    public interface IShiterData
    {
        IRepository<User> Users { get; }

        IRepository<Shitt> Shitts { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Message> Messages { get; }

        IRepository<Notification> Notifications { get; }

        int SaveChanges();
    }
}
