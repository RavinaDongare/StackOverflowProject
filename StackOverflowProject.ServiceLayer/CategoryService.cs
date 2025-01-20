using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;
using StackOverflowProject.ViewModels;
using StackOverflowProject.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace StackOverflowProject.ServiceLayer
{
    public interface ICategoryService
    {
        void InsertCategory(CategoryViewModel c);
        void UpdateCategory(CategoryViewModel c);
        void DeleteCategoryr(int Cid);
        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoryByCategoryId(int CID);

    }
    public  class CategoryService:ICategoryService
    {
        categoriesRepository categoriesRepository;
        public CategoryService()
        {
            categoriesRepository = new categoriesRepository();
        }
      public void InsertCategory(CategoryViewModel c)
        {
            var config = new MapperConfiguration(cgf => { cgf.CreateMap<CategoryViewModel, Category>(); cgf.IgnoreUnmmaped(); });
            IMapper mapper = config.CreateMapper();
            Category u = mapper.Map<CategoryViewModel, Category>(c);
            categoriesRepository.InsertCategory(u);
          
            
        }
       public void UpdateCategory(CategoryViewModel c)
        {
            var config = new MapperConfiguration(cgf => { cgf.CreateMap<CategoryViewModel, Category>(); cgf.IgnoreUnmmaped(); });
            IMapper mapper = config.CreateMapper();
            Category u = mapper.Map<CategoryViewModel, Category>(c);
            categoriesRepository.UpdateCategory(u);

        }
        public void DeleteCategoryr(int Cid)
        {
            categoriesRepository.DeleteCategoryr(Cid);

        }
        public List<CategoryViewModel> GetCategories()
        {
            List<Category> Category = categoriesRepository.GetCategories();
            var config = new MapperConfiguration(cgf => { cgf.CreateMap<Category,CategoryViewModel>(); cgf.IgnoreUnmmaped(); });
            IMapper mapper = config.CreateMapper();
            List < CategoryViewModel> u = mapper.Map< List < Category > ,List<CategoryViewModel>>(Category);

            return u;
        }
        public CategoryViewModel GetCategoryByCategoryId(int CID)
        {
            Category Category = categoriesRepository.GetCategoryByCategoryId(CID).FirstOrDefault();
            var config = new MapperConfiguration(cgf => { cgf.CreateMap<Category, CategoryViewModel>(); cgf.IgnoreUnmmaped(); });
            IMapper mapper = config.CreateMapper();
            CategoryViewModel u = null;
            if (Category != null)
            {
                 u = mapper.Map<Category, CategoryViewModel>(Category);
            }
           

            return u;

        }
    }
}
