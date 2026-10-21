using Application.Interfaces.Services.Members;
using Domain.Repositories;
using FastEndpoints;
using Infrastructure.Repositories.Books;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IMapper = AutoMapper.IMapper;

namespace Web.Features.Members.Students.GetAllStudents;

public class GetAllStudentsEndPoint : EndpointWithoutRequest<List<StudentDto>>
{
    private readonly IMapper _mapper;
    private readonly IStudentRepository _studentRepository;

    public GetAllStudentsEndPoint(IMapper mapper, IStudentRepository studentRepository)
    {
        _mapper = mapper;
        _studentRepository = studentRepository;
    }

    public override void Configure()
    {
        Get("students");
        Roles(Domain.Constants.User.Roles.MEMBER);
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var students = _studentRepository.GetAllStudents();
        await SendOkAsync(_mapper.Map<List<StudentDto>>(students), ct);
    }
}