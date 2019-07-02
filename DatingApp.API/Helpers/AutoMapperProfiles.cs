using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    // AutoMapper uses profiles to understand the source and destination of what it is mapping.
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // User being mapped to UserForDetailDto
            CreateMap<User, UserForDetailDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(usr => usr.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<User, UserForList>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(usr => usr.Photos.FirstOrDefault(p => p.IsMain).Url);
                })                
                .ForMember(dest => dest.Age, opt => {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                });            
            CreateMap<Photo, PhotosForDetailDto>();
        }
    }
}