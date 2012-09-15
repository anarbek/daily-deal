using System.Linq;
using aGrouponClasses.Models;

namespace aGrouponClasses.Repositories {
    public class GroupRepositoryEF : IGroupRepository {
        aGrouponModelsDataContext context = new aGrouponModelsDataContext();
        IQueryable<tGroup> IGroupRepository.All {
            get { return context.tGroups; }
        }

        public tGroup Find(int id) {
            return context.tGroups.Where(t => t.IDGroup.Equals(id)).FirstOrDefault();
        }

        public void InsertOrUpdate(tGroup user) {
            if (user.IDGroup == default(int)) {
                // New entity
                context.tGroups.InsertOnSubmit(user);
            } else {
                // Existing entity
                tGroup userToUpdate = Find(user.IDGroup);
                if (userToUpdate != null && userToUpdate.IDGroup > 0) {
                    userToUpdate.Name = user.Name;
                    userToUpdate.IDGroup = user.IDGroup;
                }
            }
            context.SubmitChanges();
        }

        void IGroupRepository.Delete(int id) {
            tGroup user = Find(id);
            if (user != null && user.IDGroup > 0)
                context.tGroups.DeleteOnSubmit(user);
            context.SubmitChanges();
        }

        void IGroupRepository.Save() {
            context.SubmitChanges();
        }
    }

    public interface IGroupRepository {
        IQueryable<tGroup> All { get; }
        tGroup Find(int id);
        void InsertOrUpdate(tGroup user);
        void Delete(int id);
        void Save();
    }
}
