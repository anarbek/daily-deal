using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aGrouponClasses.Repositories;
using aGrouponClasses.Models;
using aGrouponClasses.Utils;
using B2B.BLL;

namespace aGrouponProjectMain.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ICategoryRepository _categoryRepository;
        public GroupController( IGroupRepository groupRepository,ICategoryRepository categoryRepository) {
            _groupRepository = groupRepository;
            _categoryRepository = categoryRepository;
        }

        public GroupController()
            : this( new GroupRepositoryEF(),new CategoryRepositoryEF()) {
        }
        //
        // GET: /Group/

        public ActionResult Index()
        {
            return View();
        }

       

        [ChildActionOnly]
        public ActionResult DealsByCityTabbedMenu()
        {
            List<tGroup> groupsMain = _groupRepository.All.ToList();
            ViewBag.currentIDCity = MembershipHelper.CurrentCity.IDCategory;
            return PartialView(groupsMain);
        }

    }
}
