using PtViewer.Services;

namespace PtViewer.Models
{
    public class Initializer
    {
        private const string DemoUserName = "demo";
        public static void InitialDemoUser(ItemService itemService)
        {
            if (null == itemService.GetUserByUserName(DemoUserName))
            {
                var demoUser = new User { UserName = DemoUserName, Subscribes = new string[] { } };
                itemService.CreateUser(demoUser);
            }
        }
    }
}
