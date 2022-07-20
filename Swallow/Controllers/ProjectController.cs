using System;
using Microsoft.AspNetCore.Mvc;
using Swallow.Authorization;
using Swallow.Models;
using Swallow.Models.Requests;
using Swallow.Models.Responses;
using Swallow.Services.Postgres;

namespace Swallow.Controllers;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        // private readonly ProjectService _projectService;

        private IProjectService _projectService;
        public ProjectController(IProjectService projectService) => _projectService = projectService;
        

        [Authorize("admin")]
        [HttpGet]
        public IActionResult GetAll()
        {

            var projects = _projectService.GetAll();

            if(projects is null)
            {
                return NotFound();
            }

            return Ok(projects);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetAllByUser(int userid)
        {

            var projects = _projectService.GetAllByUser(userid);

            if(projects is null)
            {
                return NotFound();
            }

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            ProjectResponse project;

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

            ProjectResponse createdProject;

            try
            {
                createdProject = _projectService.Create(newProject);
            }
            catch (System.Exception)
            {
                
                return BadRequest("There was an issue making the project");
            }

            var actionName = nameof(GetById);
            var routeValues = new { id = createdProject.Id };

            return CreatedAtAction(actionName, routeValues, createdProject);
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


