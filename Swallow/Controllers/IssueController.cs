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
        private IProjectService _projectService;

        public IssueController(IIssueService issueService, IProjectService projectService)
        {
            _issueService = issueService;
            _projectService = projectService;
        }


        [Authorize("admin")]
        [HttpGet]
        public ActionResult<IssueResponse> GetAll()
        {
            return Ok(_issueService.GetAll());
        }

        [Authorize("admin")]
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

        [Authorize("admin")]
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

        [Authorize("admin")]
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

        [Authorize("admin")]
        [HttpDelete("{id}")]
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

        //* User role methods

        [HttpGet("assigned/{id}")]
        public IActionResult GetIssuesByAssigned(int id)
        {
            var claimsId = getUserContextId();

            if (id != claimsId)
            {
                return Unauthorized();
            }

            var usersIssues = _issueService.GetAllByAssigned(id);

            return Ok(usersIssues);
        }

        [HttpPost("userCreate")]
        public IActionResult CreateByUser([FromBody]CreateIssueReq newIssueItem)
        {
            IssueResponse createdIssue;

            var userId = getUserContextId();

            try
            {
                var isInProject = _projectService.GetById(newIssueItem.ProjectId).CreatorId == userId;

                if (!isInProject)
                {
                    return Unauthorized();
                }

                createdIssue = _issueService.Create(newIssueItem);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
            catch (System.Exception)
            {
                
                return BadRequest();
            }

            var actionName = nameof(GetById);
            var routeValues = new { id = createdIssue.Id };

            return CreatedAtAction(actionName, routeValues, createdIssue);
        }


        [HttpPut("byUser/{id}")]
        public IActionResult UpdateByUser(int id, [FromBody]UpdateIssueReq updatedIssue)
        {

            if (!CheckCreator(id))
            {
                return Unauthorized();
            }

            return Update(id, updatedIssue);
        }

        [HttpDelete("byUser/{id}")]
        public IActionResult DeleteByUser(int id)
        {

            if (!CheckCreator(id))
            {
                return Unauthorized();
            }

            return Delete(id);
        }


        private int getUserContextId(){

            var currentUser = HttpContext.User;

            var claimsId = int.Parse(currentUser.Claims.ToArray()[1].Value);

            return claimsId;
        }

        private bool CheckCreator(int issueId)
        {
            bool isValid = false;

            var userId = getUserContextId();
            var isCreator = _issueService.GetById(issueId).CreatorId == userId;

            if (isCreator)
            {
                isValid = true;
            }

            return isValid;
        }
    
    }

    

