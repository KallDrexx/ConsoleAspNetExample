using Microsoft.AspNet.Mvc;

namespace ConsoleAspNetExample.Controllers
{
    public class TestController : Controller
    {
        private readonly IValueHolder _valueHolder;

        public TestController(IValueHolder valueHolder)
        {
            _valueHolder = valueHolder;
        }

        public IActionResult Me()
        {
            _valueHolder.AddOne();

            return Json(new { value = _valueHolder.Get(), success = true});
        }
    }
}
