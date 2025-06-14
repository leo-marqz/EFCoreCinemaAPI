using Microsoft.EntityFrameworkCore;

namespace EFCoreCinemaAPI.Models.Seeding
{
    public static class SeedingUsersMessages
    {
        //trabajando con reverse property
        public static void Seed(ModelBuilder builder)
        {
            var charlie = new User { Id = 1, Name = "Charlie" };
            var alice = new User { Id = 2, Name = "Alice" };

            var message1 = new Message
            {
                Id = 1,
                Content = "Hello Alice!",
                EmitterId = charlie.Id,
                ReceiverId = alice.Id,
            };

            var message2 = new Message
            {
                Id = 2,
                Content = "Hello Charlie!",
                EmitterId = alice.Id,
                ReceiverId = charlie.Id,
            };

            var message3 = new Message
            {
                Id = 3,
                Content = "How are you?",
                EmitterId = charlie.Id,
                ReceiverId = alice.Id,
            };

            var message4 = new Message
            {
                Id = 4,
                Content = "I'm fine, thanks!",
                EmitterId = alice.Id,
                ReceiverId = charlie.Id,
            };

            builder.Entity<User>().HasData(charlie, alice);
            builder.Entity<Message>().HasData(message1, message2, message3, message4);
        }
    }
}
