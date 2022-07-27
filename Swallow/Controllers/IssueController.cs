using System;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Swallow.Authorization;
using Swallow.Models;
using Swallow.Models.Requests;
using Swallow.Models.Responses;
using Swallow.Services.Postgres;

namespace Swallow.Controllers;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IssueController : ControllerBase
    {
        // TODO: Test all routes
        private IIssueService _issueService;
        // private readonly ProjectService _projectService;

        public IssueController(IIssueService issueService)
        {
            _issueService = issueService;
        }


        // [Authorize("admin")]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IssueResponse> GetAll()
        {
            return Ok(_issueService.GetAll());
        }

        // [Authorize(Roles ="admin")]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            IssueResponse issue;

            try
            {
                issue = _issueService.GetById(id);
                
            }
            catch (System.Exception)
            {
                
                return NotFound();
            }

            return Ok(issue);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateIssueReq newIssueItem)
        {
            IssueResponse createdIssue;

            try
            {
                createdIssue = _issueService.Create(newIssueItem);
            }
            catch (System.Exception)
            {
                
                return BadRequest();
            }

            // var issues = await _issueService.GetIssuesAsync();

            var actionName = nameof(GetById);
            var routeValues = new { id = createdIssue.Id };

            return CreatedAtAction(actionName, routeValues, createdIssue);
        }

        // [Authorize(Roles ="admin")]
        [AllowAnonymous]
        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateIssueReq updatedIssue)
        {

            try
            {
                _issueService.Update(id, updatedIssue);
                
            }
            catch (System.Exception)
            {
                return BadRequest("Unable to update project");
            }

            return NoContent();
        }

        // [Authorize(Roles ="admin")]
        [HttpDelete("delete/{id:length(24)}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _issueService.Delete(id);
                
            }
            catch (System.Exception)
            {
                
                return NotFound();
            }

            return NoContent();
        }

        // // User role methods

        // [HttpGet("byAssigned/{id:length(24)}")]
        // public async Task<ActionResult<List<Issue>>> GetIssuesByAssigned(string id)
        // {
        //     var currentUser = HttpContext.User;

        //     var claimsId = currentUser.Claims.ToArray()[1].Value;

        //     if (id != claimsId)
        //     {
        //         return Unauthorized();
        //     }

        //     var usersIssues = await _issueService.GetIssuesByAssignedIdAsync(id);

        //     return usersIssues;
        // }

        // // Add a 
        // [HttpGet("byCreator/{id:length(24)}")]
        // public async Task<ActionResult<List<Issue>>> GetIssuesByCreator(string id)
        // {
        //     var currentUser = HttpContext.User;

        //     var claimsId = currentUser.Claims.ToArray()[1].Value;

        //     if (id != claimsId)
        //     {
        //         return Unauthorized();
        //     }

        //     var usersIssues = await _issueService.GetIssuesByCreatorAsync(id);

        //     return usersIssues;
        // }

        // // TODO: Fetch by status, fetch by date range(need a json body post req for that)

        // [HttpGet("byProject/{id:length(24)}")]
        // public async Task<ActionResult<List<Issue>>> GetIssuesByProject(string id, [FromBody]DateRangeDTO? dateRange)
        // {

        //     var currentUser = HttpContext.User;

        //     var claimsId = currentUser.Claims.ToArray()[1].Value;

        //     var project = await _projectService.GetProjectAsync(id);

        //     if (project is null)
        //     {
        //         return NotFound();
        //     }

        //     if (!project.MemberList.Contains(claimsId) || project.OwnerId != claimsId)
        //     {
        //         return Unauthorized();
        //     }
        //     // TODO: Figure out how to match object ids for checks

        //     List<Issue> projectIssues;

        //     if (dateRange.start.Year == 1 || dateRange.start.Year == 1970)
        //     {
        //         projectIssues = await _issueService.GetIssuesByProjectAsync(id);    
        //     } else {
        //         projectIssues = await _issueService.GetIssuesByProjectInDateRangeAsync(id, dateRange.start, dateRange.end);

        //     }


        //     return projectIssues;
        // }


        // [HttpGet("byProject/{id:length(24)}/byType/{type}")]
        // public async Task<ActionResult<List<Issue>>> GetIssuesByProjectAndType(string id, string type)
        // {
        //     var currentUser = HttpContext.User;

        //     var claimsId = currentUser.Claims.ToArray()[1].Value;

        //     var isUserInProject = await _projectService.IsUserInProject(id, claimsId);

        //     if (!isUserInProject)
        //     {
        //         return NotFound();
        //     }

        //     List<Issue> projectIssues = await _issueService.GetIssuesByTypeAndProjectIdAsync(type, id);
            
        //     return projectIssues;
        // }
    }

    

