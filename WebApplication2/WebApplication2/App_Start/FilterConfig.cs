using System.Web;
using System.Web.Mvc;

namespace WebApplication2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new Filters.VerifyAccess());
            //filters.Add(new Filters.VerifySession()); //añado al start las verificaciones de sesiones llamando a la clase verify session
        }
    }
}
