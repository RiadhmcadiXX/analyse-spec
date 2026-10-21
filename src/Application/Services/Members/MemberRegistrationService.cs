using Application.Interfaces.Services.Members;
using Application.Interfaces.Services.Users;
using Application.Interfaces.Services.Notifications;

using Application.Services.Members.Dtos;
using Application.Services.Users.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services.Members;

public class MemberRegistrationService : IMemberRegistrationService
{
    private readonly IMapper _mapper;
    private readonly IMemberRepository _memberRepository;
    private readonly IUserCreationService _userCreationService;
    private readonly IUserRepository _userRepository;
    private readonly INotificationService _notificationService;

    public MemberRegistrationService(
        IMapper mapper,
        IMemberRepository memberRepository,
        IUserCreationService userCreationService,
        IUserRepository userRepository,
        INotificationService notificationService)
    {
        _mapper = mapper;
        _memberRepository = memberRepository;
        _userCreationService = userCreationService;
        _userRepository = userRepository;
        _notificationService = notificationService;
    }

    public async Task<Member> RegisterMember(MemberRegistrationDto memberRegistrationDto)
    {
        var userCreationDto = _mapper.Map<UserCreationDto>(memberRegistrationDto);
        var user = await _userCreationService.CreateUserOrUpdateDeletedUserWithSameEmail(userCreationDto);
        string password = "TemporaryPassword!" + Guid.NewGuid().ToString("N");
        
        await _userRepository.CreateUserPassword(user, password);
        Console.WriteLine("Mdp for the user:  " + password);
        Console.WriteLine("Email sender:  " + user.Email);
        await _notificationService.SendInitialPassword(user, password);

        var member = _mapper.Map<Member>(memberRegistrationDto);
        member.SetUser(user);

        await _memberRepository.Create(member);

        return member;
    }
}