using System.Web;
using System.Web.Mvc;

namespace vinodsharma
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Utils.HandleErrorAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
