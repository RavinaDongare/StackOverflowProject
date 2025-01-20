using StackOverflowProject.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflowProject.Repositories
{

    public interface ICategoriesRepository
    {
        void InsertCategory(Category c);
        void UpdateCategory(Category c);
        void DeleteCategoryr(int Cid);
        List<Category> GetCategories();
        List<Category> GetCategoryByCategoryId(int CID);
    }
    public class categoriesRepository : ICategoriesRepository
    {
        StackOverflowDbContext dbContext;

        public categoriesRepository()
        {
            dbContext = new StackOverflowDbContext();
        }

      public  void InsertCategory(Category c)
        {
            dbContext.Categories.Add(c);
            dbContext.SaveChanges();
        }
       public void UpdateCategory(Category c)
        {
            Category category = dbContext.Categories.Where(temp => temp.CategoryID == c.CategoryID).FirstOrDefault();
            if (category != null)
            {
                category.CategoryName = c.CategoryName;
            }
        }
       public void DeleteCategoryr(int Cid)
        {
            Category category = dbContext.Categories.Where(temp => temp.CategoryID == Cid).FirstOrDefault();
            if (category != null)
            {
                dbContext.Categories.Remove(category);
                dbContext.SaveChanges();
            }
        }
       public List<Category> GetCategories()
        {
            List<Category> category = dbContext.Categories.ToList();
            return category;

        }
       public List<Category> GetCategoryByCategoryId(int Cid)
        {
            List<Category> category = dbContext.Categories.Where(temp => temp.CategoryID == Cid).ToList();
            return category;

        }



    }
}
