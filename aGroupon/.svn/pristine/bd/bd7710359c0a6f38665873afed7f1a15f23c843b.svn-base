using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aGrouponClasses.Models;

namespace aGrouponClasses.Repositories {
    public class CategoryRepositoryEF:ICategoryRepository {
        aGrouponModelsDataContext context = new aGrouponModelsDataContext();

        public IQueryable<tCategory> All {
            get { return context.tCategories; }
        }

        public tCategory Find(int id) {
            return context.tCategories.Where(t => t.IDCategory.Equals(id)).FirstOrDefault();
        }

        public void InsertOrUpdate(tCategory user) {
            if (user.InitialLetter.Length > 1)
                user.InitialLetter = user.InitialLetter.Substring(0, 1);

            if (user.IDCategory == default(int)) {
                // New entity
                context.tCategories.InsertOnSubmit(user);
            } else {
                // Existing entity
                //context.USERs.Attach(user);
                //context.Entry(user).State = EntityState.Modified;
                tCategory userToUpdate = Find(user.IDCategory);
                if (userToUpdate != null && userToUpdate.IDCategory > 0) {
                    userToUpdate.IDCategoryType = user.IDCategoryType;
                    userToUpdate.IDGroup = user.IDGroup;
                    userToUpdate.InitialLetter = user.InitialLetter;
                    userToUpdate.Slug = user.Slug;
                    userToUpdate.Sort = user.Sort;
                    userToUpdate.Name = user.Name;
                    userToUpdate.DisplayFlag = user.DisplayFlag;
                }
            }
            context.SubmitChanges();
        }

        public void Delete(int id) {
            tCategory user = Find(id);
            if (user != null && user.IDCategory > 0)
                context.tCategories.DeleteOnSubmit(user);
            context.SubmitChanges();
        }

        public void Save() {
            context.SubmitChanges();
        }

        public List<tCategory> GetListByIDGroup(int idGroup)
        {
            return context.tCategories.Where(t => t.IDGroup.Equals(idGroup)).ToList();
        }

        public List<tCategory> GetListByIDCategoryType(int iDCategoryType) {
            return context.tCategories.Where(t => t.IDCategoryType.Equals(iDCategoryType)).ToList();
        }
    }

    public interface ICategoryRepository {
        IQueryable<tCategory> All { get; }
        tCategory Find(int id);
        void InsertOrUpdate(tCategory user);
        void Delete(int id);
        void Save();

        List<tCategory> GetListByIDGroup(int idGroup);

        List<tCategory> GetListByIDCategoryType(int iDCategoryType);


        //List<USER> GetListSorted(string sort_col,string sort_dir);

    }
}
