using System.Web.Mvc;

namespace JieYiGuang.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //管理中心公共连接
            context.MapRoute(
                "Admin.Default",
                "Admin/{action}",
                new { controller = "Admin", action = "Login" }
            );
            context.MapRoute("Admin_MapRoute_ModuleMenuID", "Admin/Module/{controller}/{MvcTemplate}/{action}-{MvcMarkName}-{MvcClassID}.aspx", new { MvcClassID = @"[\d]{0,11}" });

            context.MapRoute("Admin_MapRoute_ModuleMVC", "Admin/Module/{controller}/{MvcTemplate}/{action}-{MvcMarkName}-{MvcClassID}.aspx", new { MvcClassID = @"[\d]{0,11}" });
        }
    }
}
