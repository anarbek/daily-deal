using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aGrouponClasses.Models;
using aGrouponClasses.Repositories;
using aGrouponClasses.Utils;

namespace aGrouponProjectMain.Controllers
{
    public class HelpController : Controller
    {
        private IContentRepository _contentRepository;
        private ICategoryRepository _categoryRepository;
        public HelpController(IContentRepository contentRepository,ICategoryRepository categoryRepository) {
            _contentRepository = contentRepository;
            _categoryRepository = categoryRepository;
        }

        public HelpController()
            : this(new ContentRepositoryEF(),new CategoryRepositoryEF()) {
        }

        public ActionResult Tour()
        {
            List<tContent> contents = _contentRepository.GetListByIDCategory((int) Enums.enmCategories.Tour);
            int ContentID = 0;
            if (contents != null && contents.Count > 0)
                ContentID = contents[0].IDContent;
            return RedirectToAction("ContentDetails", "Content", new { id = ContentID });
        }

    }
}
