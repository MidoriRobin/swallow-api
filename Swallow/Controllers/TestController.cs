using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Swallow.Models;

namespace Swallow.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [HttpGet]
        public ActionResult<IEnumerable<String>> GetTModels()
        {
            return Ok("Test Success");
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


