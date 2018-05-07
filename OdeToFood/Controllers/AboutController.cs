using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        [Route("")]
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
