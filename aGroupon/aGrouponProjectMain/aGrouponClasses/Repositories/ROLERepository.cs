using System;
using System.Linq;
using System.Linq.Expressions;
using aGrouponClasses.Models;

namespace B2B.Models
{ 
    public class ROLERepositoryEF : IROLERepository
    {
        aGrouponModelsDataContext context = new aGrouponModelsDataContext();

        public IQueryable<tRole> All
        {
			get { return context.tRoles; }
        }

        public tRole Find(int id)
        {
            return context.tRoles.Where(t => t.IDRole.Equals(id)).FirstOrDefault();
        }

        public void InsertOrUpdate(tRole role)
        {
            if (role.IDRole == default(int)) {
                // New entity
                context.tRoles.InsertOnSubmit(role);
            } else {
                // Existing entity
                tRole userToUpdate = Find(role.IDRole);
                if (userToUpdate != null && userToUpdate.IDRole > 0) {
                    userToUpdate.Name = role.Name;
                }
            }
        }

        public void Delete(int id)
        {
            tRole role = Find(id);
            if (role != null && role.IDRole > 0)
                context.tRoles.DeleteOnSubmit(role);
            context.SubmitChanges();
        }

        public void Save()
        {
            context.SubmitChanges();
        }
		
		//public List<ROLE> GetListSorted(string sort_by, string sort_dir)
        //{
        //    var mObject = from t in context.ROLEs
        //                    select t;

        //    if (sort_by != string.Empty)
        //    {
        //        var lObjects = mObject.SortBy(sort_by + sort_dir);
        //        var sortedObjects = from t in lObjects
        //                            select t;
        //        return sortedObjects.ToList<ROLE>();
        //    }
        //    else
        //    {
        //        var lObjects = mObject;
        //        var sortedObjects = from t in lObjects
        //                            select t;
        //        return sortedObjects.ToList<ROLE>();
        //    }
        //}
    }

	public interface IROLERepository
    {
		IQueryable<tRole> All { get; }
        tRole Find(int id);
        void InsertOrUpdate(tRole role);
        void Delete(int id);
        void Save();

		//List<ROLE> GetListSorted(string sort_col,string sort_dir);
    }
}