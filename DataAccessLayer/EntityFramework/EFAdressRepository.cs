using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFAdressRepository : GenericRepository<Adress>, IAdressDal
    {
        MVCContext context = new MVCContext();
        public Adress GetLast()
        {
            var adress = context.Adresses.OrderByDescending(x => x.ID).First();
            return adress;
        }
    }
}
