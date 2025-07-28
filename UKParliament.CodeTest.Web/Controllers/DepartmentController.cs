using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly PersonManagerContext _context;
        public DepartmentController(PersonManagerContext context) => _context = context;

        [HttpGet]
        public IActionResult Get() => Ok(_context.Departments.ToList());
    }
}