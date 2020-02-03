using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApiWithCore.Controllers
{
   
    //[ApiController]
    //[Route("api/[controller]")]
    public class TestController : Controller
    {
       

        [HttpGet]
        [Route("api/user")]
        public IActionResult GetValue()
        {
            return Ok(new { name="Ashu"});
        }
    }
}