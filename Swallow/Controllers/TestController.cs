using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swallow.Authorization;
//using Swallow.Models;

namespace Swallow.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [HttpGet("testget")]
        public ActionResult<IEnumerable<String>> GetTest()
        {
            return Ok("Authorize Test Success");
        }

        [Authorize("admin")]
        [HttpGet("testget-role")]
        public ActionResult<IEnumerable<String>> GetTestAdmin()
        {
            return Ok("Admin Test Success");
        }

        [AllowAnonymous]
        [HttpGet("testget-anon")]
        public ActionResult<IEnumerable<String>> GetTestAnon()
        {
            return Ok("Anon Test Success");
        }

        [HttpGet("{id}")]
        public ActionResult<String> GetTModelById(int id)
        {
            return null;
        }

        [HttpPost("")]
        public ActionResult<String> PostTModel(String model)
        {
            return null;
        }

        [HttpPut("{id}")]
        public IActionResult PutTModel(int id, String model)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<String> DeleteTModelById(int id)
        {
            return null;
        }
    }


