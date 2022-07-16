using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swallow.Models;
using Swallow.Models.Requests;
using Swallow.Services;
using Swallow.Services.Postgres;

namespace Swallow.Controllers;

    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        // private readonly ProjectService _projectService;

        private IProjectService _projectService;
        public ProjectController(IProjectService projectService) => _projectService = projectService;
        

        [HttpGet]
        public IActionResult Get()
        {

            var projects = _projectService.GetAll();

            if(projects is null)
            {
                return NotFound();
            }

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            Project project;

            try
            {
                project = _projectService.GetById(id);
                
            }
            catch (KeyNotFoundException e)
            {
                
                return NotFound();
            }

            return Ok(project);
        }


        [HttpPost]
        public IActionResult Post([FromForm] CreateProjectReq newProject)
        {

            try
            {
                _projectService.Create(newProject);
            }
            catch (System.Exception)
            {
                
                return BadRequest("There was an issue making the project");
            }

            return CreatedAtAction(nameof(Get), new { message = "Project Created"}, newProject);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateProjectReq updateProject)
        {
            try
            {
                _projectService.Update(id, updateProject);
            }
            catch (System.Exception)
            {
                
                return BadRequest("Could not update project");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _projectService.Delete(id);
            }
            catch (System.Exception)
            {
                
                return BadRequest("Project could not be found");
            }

            return NoContent();
        }

    }


