using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swallow.Models;
using Swallow.Services;

namespace Swallow.Controllers;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        // private readonly ProjectService _projectService;
        // public ProjectController(ProjectService projectService)
        // {
        //     _projectService = projectService;
        // }

        // [Authorize(Roles ="admin")]
        // [HttpGet]
        // public async Task<List<Project>> Get()
        // {
        //     return await _projectService.GetProjectsAsync();
        // }

        // [Authorize(Roles ="admin")]
        // [HttpGet("{id:length(24)}")]
        // public async Task<ActionResult<Project>> GetProjectsById(string id)
        // {
        //     var project = await _projectService.GetProjectAsync(id);

        //     if (project is null)
        //     {
        //         return NotFound();
        //     }


        //     return project;
        // }

        // [HttpPost]
        // public async Task<IActionResult> Post([FromBody]Project newProject)
        // {
        //     await _projectService.CreateAsync(newProject);

        //     return CreatedAtAction(nameof(Get), new { id = newProject.Id }, newProject);
        // }

        // [Authorize(Roles ="admin")]
        // [HttpPut("{id:length(24)}")]
        // public async Task<IActionResult> Put(string id, [FromBody]Project updatedProject)
        // {
        //     var project = await _projectService.GetProjectAsync(id);

        //     if (project is null)
        //     {
        //         return NotFound();
        //     }

        //     updatedProject.Id = project.Id;

        //     await _projectService.UpdateAsync(id, updatedProject);

        //     return NoContent();
        // }

        // [Authorize(Roles ="admin")]
        // [HttpDelete("{id:length(24)}")]
        // public async Task<IActionResult> Delete(string id)
        // {
        //     var project = await _projectService.GetProjectAsync(id);

        //     if (project is null)
        //     {
        //         return NotFound();
        //     }

        //     await _projectService.RemoveAsync(project.Id);

        //     return NoContent();
        // }
    

        // // User role methods ---

        // [HttpGet("{userId:length(24)}")]
        // public async Task<ActionResult<List<Project>>> GetProjectsByUserId(string userId)
        // {
        //     var currentUser = HttpContext.User;

        //     var claimsId = currentUser.Claims.ToArray()[1].Value;

        //     if (userId != claimsId)
        //     {
        //         return Unauthorized();
        //     }

        //     var projectList = await _projectService.GetProjectsByOwnerAsync(userId);

        //     return projectList;
        // }


        // [HttpGet("{memberId:length(24)}")]
        // public async Task<ActionResult<List<Project>>> GetProjectsByMemberId(string memberId)
        // {
        //     // Auth check function can be extrapolated
        //     var currentUser = HttpContext.User;

        //     var claimsId = currentUser.Claims.ToArray()[1].Value;

        //     if (memberId != claimsId)
        //     {
        //         return Unauthorized();
        //     }

        //     var projectList = await _projectService.GetProjectsByMemberIdAsync(memberId);

        //     return projectList;
        // }


        // [HttpGet("{groupId:length(24)}")]
        // public async Task<List<Project>> GetProjectsByGroupId(string groupId)
        // {
        //     //TODO: Check if logged in user is in the group - group must be created first

        //     var projectList = await _projectService.GetProjectsByGroupIdAsync(groupId);

        //     return projectList;
        // }

        
        // [HttpPut("update/{userId:length(24)}/{id:length(24)}")]
        // public async Task<ActionResult<List<Project>>> UpdateProjectByOwnerId(string userId, string id, [FromBody]Project updatedProject)
        // {
        //     // Can be extrapolated to a outer function
        //     var currentUser = HttpContext.User;

        //     var claimsId = currentUser.Claims.ToArray()[1].Value;

        //     if (userId != claimsId)
        //     {
        //         return Unauthorized();
        //     }

        //     var projectList = await _projectService.GetProjectByOwnerAsync(userId, updatedProject.Id);

        //     if (projectList is null)
        //     {
        //         return NotFound();
        //     }

        //     await _projectService.UpdateAsync(updatedProject.Id, updatedProject);

        //     return NoContent();
        // }

        // [HttpDelete("delete/{ownerId:length(24)}/{id:length(24)}")]
        // public async Task<IActionResult> DeleteProjectByOwnerId(string id, string userId)
        // {
        //     var currentUser = HttpContext.User;

        //     var claimsId = currentUser.Claims.ToArray()[1].Value;

        //     if (userId != claimsId)
        //     {
        //         return Unauthorized();
        //     }

        //     var project = await _projectService.GetProjectAsync(id);

        //     if (project is null)
        //     {
        //         return NotFound();
        //     }

        //     await _projectService.RemoveAsync(project.Id);

        //     return NoContent();
        // }

    }


