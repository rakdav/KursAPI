using Microsoft.EntityFrameworkCore;

namespace KursAPI.Models
{
    public static class SeedData
    {
        public static void SeedDatabase(Kursdb15Context context)
        {
            context.Database.Migrate();
            if (context.Persons.Count() == 0)
            {
                Person user = new Person { Email = "admin@mail.ru", Password = "1234" };
                user.Password = AuthOptions.GetHash(user.Password);
                context.Persons.Add(user);
                context.SaveChanges();
            }
        }
    }
}
