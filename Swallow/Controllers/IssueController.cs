using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Swallow.Models;
using Swallow.Models.DTOs;
using Swallow.Services;

namespace Swallow.Controllers;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IssueController : ControllerBase
    {
        // private readonly IssueService _issueService;
        // private readonly ProjectService _projectService;

        // public IssueController(IssueService issueService, ProjectService projectService)
        // {
        //     _issueService = issueService;
        //     _projectService = projectService;
        // }


        // [Authorize(Roles ="admin")]
        // [HttpGet]
        // public async Task<List<Issue>> Get()
        // {
        //     return await _issueService.GetIssuesAsync();
        // }

        // [Authorize(Roles ="admin")]
        // [HttpGet("{id:length(24)}")]
        // public async Task<ActionResult<Issue>> GetIssueById(string id)
        // {
        //     var issue = await _issueService.GetIssueAsync(id);

        //     if (issue is null)
        //     {
        //         return NotFound();
        //     }

        //     return issue;
        // }

        // [HttpPost]
        // public async Task<IActionResult> Post([FromBody]IssuesDTO newIssueItem)
        // {

        //     Console.WriteLine(newIssueItem);
        //     Issue newIssue = newIssueItem.ConvertToIssue();

        //     // var issues = await _issueService.GetIssuesAsync();
        //     await _issueService.CreateAsync(newIssue);

        //     return CreatedAtAction(nameof(Get), new { id = newIssue.Id }, newIssueItem);
        // }

        // [Authorize(Roles ="admin")]
        // [HttpPut("update/{id:length(24)}")]
        // public async Task<IActionResult> Put(string id, Issue updatedIssue)
        // {

        //     var issue = await _issueService.GetIssueAsync(id);

        //     if (issue is null)
        //     {
        //         return NotFound();
        //     }

        //     await _issueService.UpdateAsync(id, updatedIssue);

        //     return NoContent();
        // }

        // [Authorize(Roles ="admin")]
        // [HttpDelete("delete/{id:length(24)}")]
        // public async Task<IActionResult> Delete(string id)
        // {
        //     var issue = await _issueService.GetIssueAsync(id);

        //     if (issue is null)
        //     {
        //         return NotFound();
        //     }

        //     await _issueService.RemoveAsync(id);

        //     return NoContent();
        // }

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

    

