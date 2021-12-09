using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swallow.Models;
using Swallow.Services;

namespace Swallow.Controllers;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;
        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<List<Project>> Get()
        {
            return await _projectService.GetProjectsAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Project>> GetProjectsById(int id)
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Project newProject)
        {
            return null;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, Project updatedProject)
        {
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            return null;
        }
    }


