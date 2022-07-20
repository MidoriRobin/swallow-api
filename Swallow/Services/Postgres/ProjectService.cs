using System;
using AutoMapper;
using Swallow.Models;
using Swallow.Models.Requests;
using Swallow.Models.Responses;

namespace Swallow.Services.Postgres;

    public interface IProjectService
    {
        IEnumerable<ProjectResponse> GetAll();
        IEnumerable<ProjectResponse> GetAllByUser(int userId);
        ProjectResponse GetById(int id);
        ProjectResponse Create(CreateProjectReq model);
        void Update(int id, UpdateProjectReq model);
        void Delete(int id);
    }

    public class ProjectService : IProjectService
    {
        private SwallowContext _context;

        private readonly IMapper _mapper;

        public ProjectService(SwallowContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ProjectResponse> GetAll()
        {
            var responseList = _mapper.Map<IList<ProjectResponse>>(_context.Projects);
            
            return responseList;
        }

        public IEnumerable<ProjectResponse> GetAllByUser(int userId)
        {
            var projectList = _context.Projects.Where(x => x.CreatorId == userId);

            var responseList = _mapper.Map<IList<ProjectResponse>>(projectList);

            return responseList;
        }

        public ProjectResponse GetById(int id)
        {
            return _mapper.Map<ProjectResponse>(getProject(id));
            
        }

        public ProjectResponse Create(CreateProjectReq model)
        {
            
            var project = _mapper.Map<Project>(model);

            project.CreatedDate = DateTime.UtcNow;

            _context.Projects.Add(project);
            _context.SaveChanges();
            
            var projectResponse = _mapper.Map<ProjectResponse>(project);

            return projectResponse;
        }

        public void Update(int id, UpdateProjectReq model)
        {
            Project project;

            try
            {
                project = getProject(id);                
            }
            catch (KeyNotFoundException e)
            {
                throw new ApplicationException("Unable to update project");
            }

            _mapper.Map(model, project);
            _context.Projects.Update(project);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Project project;

            try
            {
                project = getProject(id);                
            }
            catch (KeyNotFoundException e)
            {
                throw new ApplicationException("Unable to delete project");
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();
        }

        private Project getProject(int id)
        {
            var project = _context.Projects.Find(id);

            if(project == null) throw new KeyNotFoundException("Project not found");

            return project;
        }
}
