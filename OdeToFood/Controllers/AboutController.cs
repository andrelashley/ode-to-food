using Microsoft.AspNetCore.Authorization;

namespace OdeToFood.Controllers
{
    [Authorize]
    public class AboutController
    {
        public string Phone()
        {
            return "1-250-813-0854";
        }

        public string Address()
        {
            return "Canada";
        }
    }
}
