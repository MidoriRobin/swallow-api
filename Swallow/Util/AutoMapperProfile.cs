using System;
using AutoMapper;
using Swallow.Models;
using Swallow.Models.Requests;
using Swallow.Models.Responses;

namespace Swallow.Util;
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Issue, IssueResponse>();
            CreateMap<CreateIssueReq, Issue>();

            CreateMap<UpdateIssueReq, Issue>().ForAllMembers(x => x.Condition (
                (src, dest, prop) =>
                {

                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;
                    if (prop.GetType() == typeof(int) && prop.Equals(0)) return false;
                    if (prop.GetType() == typeof(DateTime)) 
                    {
                        var asDate = (DateTime) prop;

                        if (asDate.Year == 0001)
                        {
                            return false;                            
                        }

                    }

                    return true;
                }
            ));

            CreateMap<Project, ProjectResponse>();
            CreateMap<User, AuthenticateResponse>();


            // CreateRequest -> User
            CreateMap<CreateRequest, User>();

            // UpdateRequest -> User
            CreateMap<UpdateRequest, User>()
                .ForAllMembers(x => x.Condition (
                    (src, dest, prop) =>
                    {
                        // ignore both null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));
            
            CreateMap<CreateProjectReq, Project>();

            CreateMap<UpdateProjectReq, Project>()
                .ForAllMembers(x => x.Condition (
                    (src, dest, prop) => 
                    {
                        if (prop is null) return false;
                        if(prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));
        }
        
    }
