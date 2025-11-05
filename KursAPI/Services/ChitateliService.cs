using KursAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KursAPI.Services
{
    public class ChitateliService : IService<Chitateli>
    {
        private readonly Kursdb15Context db;
        public ChitateliService(Kursdb15Context _db)=>this.db = _db;
        public async Task<IEnumerable<Chitateli>> GetAll()
        {
            return await db.Chitatelis.ToListAsync();
        }
        public async Task<Chitateli> GetById(int id)
        {
            return await db.Chitatelis.FindAsync(id);
        }
        public async Task Create(Chitateli entity)
        {
            db.Chitatelis.Add(entity);
            await db.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var chit = await db.Chitatelis.FindAsync(id);
            if (chit != null)
            {
                db.Chitatelis.Remove(chit);
                await db.SaveChangesAsync();
            }
        }
        public async Task Update(Chitateli entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.Chitatelis.Update(entity);
            await db.SaveChangesAsync();
        }
    }
}
