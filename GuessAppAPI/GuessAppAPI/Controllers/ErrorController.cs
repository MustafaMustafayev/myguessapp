using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuessAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("Unauthorized")]
        public IActionResult Unauthorized()
        {
            return BadRequest("You are not logged in.");
        }

        [Route("NotFound")]
        public IActionResult NotFound()
        {   
            return BadRequest("This action was not found.");
        }

        [Route("Forbidden")]
        public IActionResult Forbidden()
        {
            return BadRequest("You do not have permission to this action.");
        }

        [Route("MethodNotAllowed")]
        public IActionResult MethodNotAllowed()
        {
            return BadRequest("Check your request type (POST/GET/PATCH/DELETE/PUT).");
        }
    }
}