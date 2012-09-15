using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aGrouponClasses.Utils {
    public class CustomMessages {
        public static string ErrorMessage(string Message) {
            return string.Format(" <div onclick=\"jQuery(this).fadeOut();\" class=\"notibar msgerror\">  <a class=\"close\"></a> <p>{0}</p> </div>", Message);
        }

        public static string SuccessMessage(string Message) {
            return string.Format(" <div onclick=\"jQuery(this).fadeOut();\" class=\"notibar msgsuccess\">  <a class=\"close\"></a> <p>{0}</p> </div>", Message);
        }

        public static string DivBegin(int i)
        {
            if (i % 3 == 0)
                return "<div class=\"blog-small-home\">";
            return "";
        }

        public static string DivEnd(int i) {
            if (i % 3 == 0)
                return "</div>";
            return "";
        }
    }
}
