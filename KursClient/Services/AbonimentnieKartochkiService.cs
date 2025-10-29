
using KursClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursClient.Services
{
    public class AbonimentnieKartochkiService : BaseService<AbonimentnieKartochki>
    {

        public override bool Add(AbonimentnieKartochki obj)
        {
            return true;
        }

        public override bool Delete(AbonimentnieKartochki obj)
        {

            return true;
        }
        public override List<AbonimentnieKartochki> GetAll()
        {
            return new List<AbonimentnieKartochki>();
        }
        public override bool Update(AbonimentnieKartochki obj)
        {
            return true;
        }
        public override List<AbonimentnieKartochki> Search(string str)
        {
            return new List<AbonimentnieKartochki>();
        }
    }
}
