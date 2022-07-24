using System;
using AutoMapper;
using Swallow.Models;
using Swallow.Models.Requests;
using Swallow.Models.Responses;

namespace Swallow.Services.Postgres;

    public interface IIssueService
    {
        IEnumerable<IssueResponse> GetAll();
        IssueResponse GetById(int issueId);
        IEnumerable<IssueResponse> GetAllByUser(int userId);
        IEnumerable<IssueResponse> GetAllByProject(int projectId);

        void Delete(int issueId);
        void Update(int issueId, UpdateIssueReq updatedIssue);
        IssueResponse Create(CreateIssueReq newIssue);
    }

public class IssueService : IIssueService
{
    // TODO: Test Everything

    private SwallowContext _context;

    private readonly IMapper _mapper; 

    public IssueService(SwallowContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Delete(int issueId)
    {
        Issue issue;

        try
        {
            issue = getIssue(issueId);
        }
        catch (System.Exception)
        {
            
            throw new ApplicationException("Issue no found");
        }

        _context.Issues.Remove(issue);
        _context.SaveChanges();

    }

    public IEnumerable<IssueResponse> GetAll()
    {
        var responseList = _mapper.Map<IList<IssueResponse>>(_context.Issues);

        return responseList;
    }

    public IEnumerable<IssueResponse> GetAllByProject(int projectId)
    {
        var project = _context.Projects.Find(projectId);
        IEnumerable<IssueResponse> responseList;

        try
        {
            responseList = _mapper.Map<IList<IssueResponse>>(project.Issues);
            
        }
        catch (System.Exception)
        {
            
            throw new ApplicationException("Project not found");
        }

        return responseList;
    }

    public IEnumerable<IssueResponse> GetAllByUser(int userId)
    {
        var user = _context.Users.Find(userId);
        IEnumerable<IssueResponse> responseList;

        try
        {
            responseList = _mapper.Map<IList<IssueResponse>>(user.AssignedIssues);
            
        }
        catch (System.Exception)
        {
            
            throw new ApplicationException("Project not found");
        }

        return responseList;
    }

    public IssueResponse GetById(int issueId)
    {
        return _mapper.Map<IssueResponse>(getIssue(issueId));
    }

    private Issue getIssue(int id)
    {
        return _context.Issues.Find(id);
    }

    public void Update(int issueId, UpdateIssueReq updatedIssue)
    {
        Issue issue;

        try
        {
            issue = getIssue(issueId);
        }
        catch (System.Exception)
        {
            
            throw new ApplicationException("Unable to update project");
        }

        _mapper.Map(updatedIssue, issue);
        _context.Issues.Update(issue);
        _context.SaveChanges();
    }

    public IssueResponse Create(CreateIssueReq newIssue)
    {
        // Ensure only values permitted for editing are entered
        var issue = _mapper.Map<Issue>(newIssue);

        issue.CreatedDate = DateTime.UtcNow;

        try
        {
            _context.Issues.Add(issue);
            _context.SaveChanges();
        }
        catch (System.Exception)
        {
            
            throw new ApplicationException("Unable to update project");
        }

        var issueResponse =_mapper.Map<IssueResponse>(issue);

        return issueResponse;
    }

}
