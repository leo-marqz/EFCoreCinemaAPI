using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace EFCoreCinemaAPI.Services
{
    public interface IDbContextEvents
    {
        void HandleTracked(object sender, EntityTrackedEventArgs args);
        void HandleStateChanged(object sender, EntityStateChangedEventArgs args);
        void HandleSavingChanges(object sender, SavingChangesEventArgs args);
        void HandleSavedChanges(object sender, SavedChangesEventArgs args);
        void HandleSaveChangesFailed(object sender, SaveChangesFailedEventArgs args);
    }

    public class DbContextEvents : IDbContextEvents
    {
        private readonly ILogger<DbContextEvents> logger;

        public DbContextEvents(ILogger<DbContextEvents> logger)
        {
            this.logger = logger;
        }

        public void HandleTracked(object sender, EntityTrackedEventArgs args)
        {
            var message = $"Entity tracked: {args.Entry.Entity.GetType().Name}, State: {args.Entry.State}";
            logger.LogInformation(message);
        }

        public void HandleStateChanged(object sender, EntityStateChangedEventArgs args)
        {
            var message = $"Entity state changed: {args.Entry.Entity.GetType().Name}, Old State: {args.OldState}, New State: {args.NewState}";
            logger.LogInformation(message);
        }

        public void HandleSavingChanges(object sender, SavingChangesEventArgs args)
        {
            var entities = ((ApplicationDbContext)sender).ChangeTracker.Entries();

            foreach (var entry in entities)
            {
                var message = $"Entity: {entry.Entity.GetType().Name}, State: {entry.State}";
                logger.LogInformation(message);
            }
        }

        public void HandleSavedChanges(object sender, SavedChangesEventArgs args)
        {
            var message = $"Changes saved: {args.EntitiesSavedCount} entities affected.";
            logger.LogInformation(message);
        }

        public void HandleSaveChangesFailed(object sender, SaveChangesFailedEventArgs args)
        {
            var message = $"Save changes failed: {args.Exception.Message}";
            logger.LogError(args.Exception, message);   
        }
    }
}
