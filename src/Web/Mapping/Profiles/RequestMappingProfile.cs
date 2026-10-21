using Application.Services.Members.Dtos;
using Application.Services.Users.Dtos;
using Application.Services.SPEC.Dtos;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Books;
using Domain.Entities.Identity;
using Domain.Entities.SPEC;
using Web.Dtos;
using Web.Features.Admins.Members.CreateMember;
using Web.Features.Admins.Members.UpdateMember;
using Web.Features.Admins.SPEC;
using Web.Features.Members.Books.CreateBook;
using Web.Features.Members.Books.EditBook;
using Web.Features.Members.Questions.UpdateQuestion;

namespace Web.Mapping.Profiles;

public class RequestMappingProfile : Profile
{
    public RequestMappingProfile()
    {
        
        CreateMap<Programme, ProgrammeDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code));
        CreateMap<ProgrammeDto, Programme>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code));
        
        CreateMap<UserCreationDto, User>()
            .ForMember(dest => dest.Programmes, opt => opt.MapFrom(src => src.Programmes));
        CreateMap<TranslatableStringDto, TranslatableString>().ReverseMap();

        CreateMap<CreateBookRequest, Book>();

        CreateMap<CreateSpecRequest, SpecRegistrationDto>();

        CreateMap<EditBookRequest, Book>();

        CreateMap<CreateMemberRequest, MemberRegistrationDto>();

        CreateMap<UpdateMemberRequest, User>()
            .ForMember(user => user.Id, opt => opt.Ignore())
            .ForMember(user => user.Email, opt => opt.MapFrom(request => request.Email.ToLower()))
            .ForMember(user => user.UserName, opt => opt.MapFrom(request => request.Email.ToLower()))
            .ForMember(user => user.NormalizedEmail, opt => opt.MapFrom(request => request.Email.ToUpper()))
            .ForMember(user => user.NormalizedUserName, opt => opt.MapFrom(request => request.Email.ToUpper()))
            .ForMember(user => user.Programmes, opt => opt.MapFrom(request => request.Programme));
        CreateMap<UpdateMemberRequest, Member>();

        CreateMap<UpdateQuestionRequest, Question>()
            .ForMember(questionRequest => questionRequest.Id, opt => opt.MapFrom(question => question.Id))
            .ForMember(questionRequest => questionRequest.Title, opt => opt.MapFrom(question => question.Title))
            .ForMember(questionRequest => questionRequest.DescriptionQuestion, opt => opt.MapFrom(question => question.Label));

    }
}