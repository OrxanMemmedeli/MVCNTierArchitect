using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFImageFileRepository : GenericRepository<ImageFile>, IImageFileDal
    {

        private readonly MVCContext _context;
        public EFImageFileRepository()
        {
            _context = new MVCContext();
        }
        public void CreateAll(List<ImageFile> t)
        {
            foreach (var item in t)
            {
                var addedEntity = _context.Entry(item);
                addedEntity.State = EntityState.Added;
            }
            _context.SaveChanges();
        }
    }
}
