using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Books;
using Domain.Entities.Identity;
using Domain.Entities.SPEC;
using Domain.Entities.Students;
using Microsoft.AspNetCore.Identity;
using Web.Dtos;
using GetMeMemberResponse = Web.Features.Members.Me.GetMe.GetMeResponse;
using GetMeAdminResponse = Web.Features.Admins.Me.GetMe.GetMeResponse;
using Web.Features.Members.Students;
using Web.Features.Members.Answers;
using Web.Features.Members.Books;
using Web.Features.Members.Options;
using Web.Features.Members.Questions;
using Web.Features.Members.Questions.UpdateQuestion;

namespace Web.Mapping.Profiles;

public class ResponseMappingProfile : Profile
{
    public ResponseMappingProfile()
    {
        CreateMap<IdentityResult, SucceededOrNotResponse>();

        CreateMap<IdentityError, Error>()
            .ForMember(error => error.ErrorType, opt => opt.MapFrom(identity => identity.Code))
            .ForMember(error => error.ErrorMessage, opt => opt.MapFrom(identity => identity.Description));

        CreateMap<Member, GetMeMemberResponse>()
            .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.User.RoleNames))
            .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.GetPhoneNumber()))
            .ForMember(x => x.PhoneExtension, opt => opt.MapFrom(x => x.GetPhoneExtension()));

        CreateMap<Administrator, GetMeAdminResponse>();

        CreateMap<Book, BookDto>()
            .ForMember(bookDto => bookDto.Created, opt => opt.MapFrom(book => book.Created.ToDateTimeUtc()))
            .ForMember(bookDto => bookDto.NameFr, opt => opt.MapFrom(book => book.NameFr))
            .ForMember(bookDto => bookDto.NameEn, opt => opt.MapFrom(book => book.NameEn))
            .ForMember(bookDto => bookDto.DescriptionFr, opt => opt.MapFrom(book => book.DescriptionFr))
            .ForMember(bookDto => bookDto.DescriptionEn, opt => opt.MapFrom(book => book.DescriptionEn));

        CreateMap<User, UserDto>()
            .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.RoleNames))
            .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.PhoneNumber))
            .ForMember(x => x.PhoneExtension, opt => opt.MapFrom(x => x.PhoneExtension))
            .ForMember(x => x.Programmes, opt => opt.MapFrom(x => x.Programmes.ToList()));


        CreateMap<Member, MemberDto>()
            .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.User.RoleNames))
            .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.PhoneNumber!.Number))
            .ForMember(x => x.PhoneExtension, opt => opt.MapFrom(x => x.PhoneNumber!.Extension))
            .ForMember(x => x.Programmes, opt => opt.MapFrom(x => x.User.Programmes.ToList()));

        CreateMap<Student, StudentDto>()
            .ForMember(dto => dto.Created, opt => opt.MapFrom(student => student.Created.ToDateTimeUtc()))
            .ForMember(dto => dto.Cegep, opt => opt.MapFrom(student => student.Cegep ?? string.Empty))
            .ForMember(dto => dto.FirstLogOn, opt => opt.MapFrom(student => student.FirstLogOn))
            .ForMember(dto => dto.SubmitedTime, opt => opt.MapFrom(student => student.SubmitedTime ?? default(DateTime?)))
            .ForMember(dto => dto.CP, opt => opt.MapFrom(student => student.CP ?? string.Empty))
            .ForMember(dto => dto.Birthday, opt => opt.MapFrom(student => student.Birthday ?? default(DateTime?)))
            .ForMember(dto => dto.Gender, opt => opt.MapFrom(student => student.Gender))
            .ForMember(dto => dto.AdmEmail, opt => opt.MapFrom(student => student.AdmEmail ?? string.Empty))
            .ForMember(dto => dto.CollEmail, opt => opt.MapFrom(student => student.CollEmail ?? string.Empty))
            .ForMember(dto => dto.Status, opt => opt.MapFrom(student => student.Status ?? string.Empty))
            .ForMember(dto => dto.OthStuQc, opt => opt.MapFrom(student => student.OthStuQc ?? string.Empty))
            .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(student => student.PostalCode ?? string.Empty))
            .ForMember(dto => dto.DesDate, opt => opt.MapFrom(student => student.DesDate ?? default(DateTime?)))
            .ForMember(dto => dto.MGS, opt => opt.MapFrom(student => student.MGS ?? default(byte?)))
            .ForMember(dto => dto.NumEstabSec, opt => opt.MapFrom(student => student.NumEstabSec ?? default(int?)))
            .ForMember(dto => dto.AdmTurn, opt => opt.MapFrom(student => student.AdmTurn ?? default(byte?)))
            .ForMember(dto => dto.Choice, opt => opt.MapFrom(student => student.Choice ?? string.Empty))
            .ForMember(dto => dto.Cohort, opt => opt.MapFrom(student => student.Cohort ?? string.Empty))
            .ForMember(dto => dto.Population, opt => opt.MapFrom(student => student.Population))
            .ForMember(dto => dto.CollSess01, opt => opt.MapFrom(student => student.CollSess01 ?? default(int?)))
            .ForMember(dto => dto.CollSess02, opt => opt.MapFrom(student => student.CollSess02 ?? default(int?)))
            .ForMember(dto => dto.CollSess03, opt => opt.MapFrom(student => student.CollSess03 ?? default(int?)))
            .ForMember(dto => dto.CollSess04, opt => opt.MapFrom(student => student.CollSess04 ?? default(int?)))
            .ForMember(dto => dto.ProgSess01, opt => opt.MapFrom(student => student.ProgSess01 ?? string.Empty))
            .ForMember(dto => dto.ProgSess02, opt => opt.MapFrom(student => student.ProgSess02 ?? string.Empty))
            .ForMember(dto => dto.ProgSess03, opt => opt.MapFrom(student => student.ProgSess03 ?? string.Empty))
            .ForMember(dto => dto.ProgSess04, opt => opt.MapFrom(student => student.ProgSess04 ?? string.Empty))
            .ForMember(dto => dto.NbFollow01, opt => opt.MapFrom(student => student.NbFollow01 ?? default(byte?)))
            .ForMember(dto => dto.NbFollow02, opt => opt.MapFrom(student => student.NbFollow02 ?? default(byte?)))
            .ForMember(dto => dto.NbFollow03, opt => opt.MapFrom(student => student.NbFollow03 ?? default(byte?)))
            .ForMember(dto => dto.NbFollow04, opt => opt.MapFrom(student => student.NbFollow04 ?? default(byte?)))
            .ForMember(dto => dto.NbSuccess01, opt => opt.MapFrom(student => student.NbSuccess01 ?? default(byte?)))
            .ForMember(dto => dto.NbSuccess02, opt => opt.MapFrom(student => student.NbSuccess02 ?? default(byte?)))
            .ForMember(dto => dto.NbSuccess03, opt => opt.MapFrom(student => student.NbSuccess03 ?? default(byte?)))
            .ForMember(dto => dto.NbSuccess04, opt => opt.MapFrom(student => student.NbSuccess04 ?? default(byte?)))
            .ForMember(dto => dto.ProgDec, opt => opt.MapFrom(student => student.ProgDec ?? string.Empty))
            .ForMember(dto => dto.YearDec, opt => opt.MapFrom(student => student.YearDec ?? default(DateTime?)))
            .ForMember(dto => dto.FirstAndLastName, opt => opt.MapFrom(student => student.FirstAndLastName ?? string.Empty))
            .ForMember(dto => dto.Identification, opt => opt.MapFrom(student => student.Identification ?? default(int?)))
            .ForMember(dto => dto.Genmels, opt => opt.MapFrom(student => student.Genmels ?? default(decimal?)))
            .ForMember(dto => dto.Sec5Fra, opt => opt.MapFrom(student => student.Sec5Fra ?? default(byte?)))
            .ForMember(dto => dto.FirstName, opt => opt.MapFrom(student => student.FirstName ?? string.Empty))
            .ForMember(dto => dto.LastName, opt => opt.MapFrom(student => student.LastName ?? string.Empty))
            .ForMember(dto => dto.TargetYear, opt => opt.MapFrom(student => student.TargetYear ?? string.Empty))
            .ForMember(dto => dto.TargetSemester, opt => opt.MapFrom(student => student.TargetSemester ?? string.Empty))
            .ForMember(dto => dto.FreqType, opt => opt.MapFrom(student => student.FreqType ?? default(char?)))
            .ForMember(dto => dto.PresType, opt => opt.MapFrom(student => student.PresType ?? default(char?)))
            .ForMember(dto => dto.CollSemTarget, opt => opt.MapFrom(student => student.CollSemTarget ?? string.Empty))
            .ForMember(dto => dto.ProgSemTarget, opt => opt.MapFrom(student => student.ProgSemTarget ?? string.Empty))
            .ForMember(dto => dto.Reinscription, opt => opt.MapFrom(student => student.Reinscription ?? default(bool?)))
            .ForMember(dto => dto.SuccessContract, opt => opt.MapFrom(student => student.SuccessContract ?? default(bool?)))
            .ForMember(dto => dto.TeachingServices, opt => opt.MapFrom(student => student.TeachingServices ?? default(byte?)))
            .ForMember(dto => dto.EufSuccess01, opt => opt.MapFrom(student => student.EufSuccess01 ?? default(byte?)))
            .ForMember(dto => dto.EufSuccess02, opt => opt.MapFrom(student => student.EufSuccess02 ?? default(byte?)))
            .ForMember(dto => dto.EufSuccess03, opt => opt.MapFrom(student => student.EufSuccess03 ?? default(byte?)))
            .ForMember(dto => dto.EufSuccess04, opt => opt.MapFrom(student => student.EufSuccess04 ?? default(byte?)))
            .ForMember(dto => dto.Sec5Eng, opt => opt.MapFrom(student => student.Sec5Eng ?? default(byte?)))
            .ForMember(dto => dto.ConsentHelp, opt => opt.MapFrom(student => student.ConsentHelp));


        //CreateMap<Student, StudentDto>()
        //    .ForMember(dto => dto.Id, opt => opt.MapFrom(student => student.Id))
        //    .ForMember(dto => dto.Cegep, opt => opt.MapFrom(student => student.Cegep))
        //    .ForMember(dto => dto.FirstLogOn, opt => opt.MapFrom(student => student.FirstLogOn))
        //    .ForMember(dto => dto.SubmitedTime, opt => opt.MapFrom(student => student.SubmitedTime))
        //    .ForMember(dto => dto.CP, opt => opt.MapFrom(student => student.CP))
        //    .ForMember(dto => dto.Birthday, opt => opt.MapFrom(student => student.Birthday))
        //    .ForMember(dto => dto.Gender, opt => opt.MapFrom(student => student.Gender))
        //    .ForMember(dto => dto.AdmEmail, opt => opt.MapFrom(student => student.AdmEmail))
        //    .ForMember(dto => dto.CollEmail, opt => opt.MapFrom(student => student.CollEmail))
        //    .ForMember(dto => dto.Status, opt => opt.MapFrom(student => student.Status))
        //    .ForMember(dto => dto.OthStuQc, opt => opt.MapFrom(student => student.OthStuQc))
        //    .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(student => student.PostalCode))
        //    .ForMember(dto => dto.DesDate, opt => opt.MapFrom(student => student.DesDate))
        //    .ForMember(dto => dto.MGS, opt => opt.MapFrom(student => student.MGS))
        //    .ForMember(dto => dto.NumEstabSec, opt => opt.MapFrom(student => student.NumEstabSec))
        //    .ForMember(dto => dto.AdmTurn, opt => opt.MapFrom(student => student.AdmTurn))
        //    .ForMember(dto => dto.Choice, opt => opt.MapFrom(student => student.Choice))
        //    .ForMember(dto => dto.Cohort, opt => opt.MapFrom(student => student.Cohort))
        //    .ForMember(dto => dto.Population, opt => opt.MapFrom(student => student.Population))
        //    .ForMember(dto => dto.CollSess01, opt => opt.MapFrom(student => student.CollSess01))
        //    .ForMember(dto => dto.CollSess02, opt => opt.MapFrom(student => student.CollSess02))
        //    .ForMember(dto => dto.CollSess03, opt => opt.MapFrom(student => student.CollSess03))
        //    .ForMember(dto => dto.CollSess04, opt => opt.MapFrom(student => student.CollSess04))
        //    .ForMember(dto => dto.ProgSess01, opt => opt.MapFrom(student => student.ProgSess01))
        //    .ForMember(dto => dto.ProgSess02, opt => opt.MapFrom(student => student.ProgSess02))
        //    .ForMember(dto => dto.ProgSess03, opt => opt.MapFrom(student => student.ProgSess03))
        //    .ForMember(dto => dto.ProgSess04, opt => opt.MapFrom(student => student.ProgSess04))
        //    .ForMember(dto => dto.ProgDec, opt => opt.MapFrom(student => student.ProgDec))
        //    .ForMember(dto => dto.YearDec, opt => opt.MapFrom(student => student.YearDec))
        //    .ForMember(dto => dto.FirstAndLastName, opt => opt.MapFrom(student => student.FirstAndLastName))
        //    .ForMember(dto => dto.Identification, opt => opt.MapFrom(student => student.Identification))
        //    .ForMember(dto => dto.Genmels, opt => opt.MapFrom(student => student.Genmels))
        //    .ForMember(dto => dto.Sec5Fra, opt => opt.MapFrom(student => student.Sec5Fra))
        //    .ForMember(dto => dto.FirstName, opt => opt.MapFrom(student => student.FirstName))
        //    .ForMember(dto => dto.LastName, opt => opt.MapFrom(student => student.LastName))
        //    .ForMember(dto => dto.TargetYear, opt => opt.MapFrom(student => student.TargetYear))
        //    .ForMember(dto => dto.TargetSemester, opt => opt.MapFrom(student => student.TargetSemester))
        //    .ForMember(dto => dto.FreqType, opt => opt.MapFrom(student => student.FreqType))
        //    .ForMember(dto => dto.PresType, opt => opt.MapFrom(student => student.PresType))
        //    .ForMember(dto => dto.CollSemTarget, opt => opt.MapFrom(student => student.CollSemTarget))
        //    .ForMember(dto => dto.ProgSemTarget, opt => opt.MapFrom(student => student.ProgSemTarget))
        //    .ForMember(dto => dto.Reinscription, opt => opt.MapFrom(student => student.Reinscription))
        //    .ForMember(dto => dto.SuccessContract, opt => opt.MapFrom(student => student.SuccessContract))
        //    .ForMember(dto => dto.TeachingServices, opt => opt.MapFrom(student => student.TeachingServices))
        //    .ForMember(dto => dto.EufSuccess01, opt => opt.MapFrom(student => student.EufSuccess01))
        //    .ForMember(dto => dto.EufSuccess02, opt => opt.MapFrom(student => student.EufSuccess02))
        //    .ForMember(dto => dto.EufSuccess03, opt => opt.MapFrom(student => student.EufSuccess03))
        //    .ForMember(dto => dto.EufSuccess04, opt => opt.MapFrom(student => student.EufSuccess04))
        //    .ForMember(dto => dto.Sec5Eng, opt => opt.MapFrom(student => student.Sec5Eng))
        //    .ForMember(dto => dto.ConsentHelp, opt => opt.MapFrom(student => student.ConsentHelp));

        CreateMap<Answer, AnswerDto>()
            .ForMember(answerDto => answerDto.Id, opt => opt.MapFrom(answer => answer.Id))
            .ForMember(answerDto => answerDto.IdOption, opt => opt.MapFrom(answer => answer.IdOption))
            .ForMember(answerDto => answerDto.IdStudent, opt => opt.MapFrom(answer => answer.IdStudent));

        CreateMap<Option, OptionDto>()
            .ForMember(optionDto => optionDto.Id, opt => opt.MapFrom(option => option.Id))
            .ForMember(optionDto => optionDto.IdQuestion, opt => opt.MapFrom(option => option.IdQuestion))
            .ForMember(optionDto => optionDto.Title, opt => opt.MapFrom(option => option.Title));

        CreateMap<Question, QuestionDto>()
            .ForMember(questionDto => questionDto.Id, opt => opt.MapFrom(question => question.Id))
            .ForMember(questionDto => questionDto.Title, opt => opt.MapFrom(question => question.Title))
            .ForMember(questionDto => questionDto.Label, opt => opt.MapFrom(question => question.DescriptionQuestion))
            .ForMember(questionDto => questionDto.Parent, opt => opt.MapFrom(question => question.Parent));
        
        
    }
}