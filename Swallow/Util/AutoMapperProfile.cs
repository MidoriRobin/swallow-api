using System;
using AutoMapper;
using Swallow.Models;
using Swallow.Models.Requests;

namespace Swallow.Util;
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
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
