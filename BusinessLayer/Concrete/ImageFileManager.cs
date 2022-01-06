using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ImageFileManager : IImageFileService
    {
        private readonly IImageFileDal _imageFileDal;

        public ImageFileManager(IImageFileDal imageFileDal)
        {
            _imageFileDal = imageFileDal;
        }

        public void Add(ImageFile t)
        {
            _imageFileDal.Create(t);
        }

        public void Addrange(List<ImageFile> t)
        {
            _imageFileDal.CreateAll(t);
        }

        public void Delete(ImageFile t)
        {
            _imageFileDal.Delete(t);
        }

        public List<ImageFile> GetAll()
        {
            return _imageFileDal.GetAll();
        }

        public List<ImageFile> GetAll(Expression<Func<ImageFile, bool>> Filter)
        {
            return _imageFileDal.GetAll(Filter);
        }

        public ImageFile GetByID(Expression<Func<ImageFile, bool>> Filter)
        {
             return _imageFileDal.GetById(Filter);
        }

        public void Update(ImageFile t, int id)
        {
            _imageFileDal.Update(t, id);
        }
    }
}
