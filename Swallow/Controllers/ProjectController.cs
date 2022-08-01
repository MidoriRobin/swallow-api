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
        private IIssueService _issueService;
        public ProjectController(IProjectService projectService, IIssueService issueService) 
        {
            
            _projectService = projectService;
            _issueService = issueService;
             
        }
        

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

        [Authorize("admin")]
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

        [Authorize("admin")]
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


        [Authorize("admin")]
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


        [Authorize("admin")]
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

        
        [Authorize("admin")]
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

        // * User role methods

        [HttpGet("{id}/created")]
        // TODO: Change method to filter by creator, member, and both
        public IActionResult GetAllByCreator(int id)
        {
            try
            {
                _projectService.GetAllByUser(id);
            }
            catch (System.Exception)
            {
                
                return BadRequest("There was an issue processing the request");
            }

            return Ok();
        }

        [HttpGet("{id}/issues")]
        public IActionResult GetProjectIssues(int id)
        {
            IEnumerable<IssueResponse> issues;


            try
            {
                var project = _projectService.GetById(id);

                issues = _issueService.GetAllByProject(project.Id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No project found");
            }
            catch (Exception e)
            {
                
                return BadRequest();
            }


            return Ok(issues);
        }

        [HttpGet("user/{id}")]
        public IActionResult GetUserProject(int id)
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
            catch (System.Exception)
            {
                
                return BadRequest();
            }

            if (project.CreatorId != getUserContextId())
            {
                return Unauthorized();
            }

            return Ok(project);
        }

        [HttpGet("user/create")]
        public IActionResult UserCreateProject([FromForm] CreateProjectReq newProject)
        {
            ProjectResponse createdProject;

            var userId = getUserContextId();

            // Setting creator user to context user
            newProject.CreatorId = userId;

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


        // * Utils
        private int getUserContextId()
        {

            var currentUser = HttpContext.User;

            var claimsId = int.Parse(currentUser.Claims.ToArray()[1].Value);

            return claimsId;
        }

    }


