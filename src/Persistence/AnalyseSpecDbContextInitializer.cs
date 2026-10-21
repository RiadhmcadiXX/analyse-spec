using Domain.Constants.User;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.SPEC;
using Domain.Entities.Students;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence;

public class AnalyseSpecDbContextInitializer
{
    private const string MemberEmail = "member@gmail.com";
    private const string AdminEmail = "admin@gmail.com";
    private const string Password = "Qwerty123!";

    private readonly ILogger<AnalyseSpecDbContextInitializer> _logger;
    private readonly AnalyseSpecDbContext _context;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public AnalyseSpecDbContextInitializer(ILogger<AnalyseSpecDbContextInitializer> logger,
        AnalyseSpecDbContext context,
        RoleManager<Role> roleManager,
        UserManager<User> userManager)
    {
        _logger = logger;
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await SeedRoles();
            await seedProgramme();
            await SeedAdmins();
            await SeedMembers();
            /*
            await SeedStudents();
            await SeedQuestions();
            await SeedOptions();
            await SeedAnswers();
            */
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
    //ToDo retirer le comment quand l'import marche
    /*
    private async Task SeedAnswers()
    {
        List<Answer> seedAnswers = new List<Answer>();

        // Fetching seeded options (assuming SeedOptions has already been called)
        var options = _context.Options.Include(o => o.IdQuestion).ToList();

        // Fetching students to associate with answers
        var students = _context.Students.ToList();

        // Exit early if the required options or students are not found
        if (!options.Any() || !students.Any())
        {
            return;
        }

        // Creating answers for the students and options
        foreach (var student in students)
        {
            var studentAnswers = _context.Answers.Include(a => a.IdOption).ThenInclude(o => o.IdQuestion)
                                                 .Where(a => a.IdStudent == student).ToList();

            foreach (var option in options)
            {
                // Check if the student already has an answer for the question
                var existingAnswer = studentAnswers.FirstOrDefault(a => a.IdOption.IdQuestion == option.IdQuestion);

                // If the answer already exists, skip adding it
                if (existingAnswer != null)
                {
                    continue;
                }

                seedAnswers.Add(new Answer
                {
                    IdOption = option,
                    IdStudent = student
                });
            }
        }

        // Adding the new answers to the database
        _context.Answers.AddRange(seedAnswers);
        await _context.SaveChangesAsync();
    }

    private async Task SeedOptions()
    {
        List<Option> seedOptions = new List<Option>();

        var questions = _context.Questions.ToList();

        if (!questions.Any())
        {
            return;
        }

        foreach (var question in questions)
        {
            // Check if the question already has an option
            var existingOption = _context.Options.FirstOrDefault(o => o.IdQuestion == question);
            if (existingOption != null)
            {
                continue;
            }

            // Add a single option for each question
            seedOptions.Add(new Option
            {
                Title = $"Lorem ipsum dolor sit amet",
                IdQuestion = question
            });
        }

        foreach (var option in seedOptions)
        {
            option.SanitizeForSaving();
        }

        _context.Options.AddRange(seedOptions);
        await _context.SaveChangesAsync();
    }


    private async Task SeedQuestions()
    {
        List<Question> seedQuestions = new List<Question>();

        seedQuestions.Add(new Question
        {
            Title = "Combien de doigts avez-vous?",
            Parent = null
        });

        seedQuestions.Add(new Question
        {
            Title = "Que pensez vous de la congolexicomatisation?",
            Parent = null
        });

        seedQuestions.Add(new Question
        {
            Title = "Êtes vous un farfadet ^rtemplis de malice?",
            Parent = null
        });

        seedQuestions.Add(new Question
        {
            Title = "Combien font 1 + 1 ?",
            Parent = null
        });

        seedQuestions.Add(new Question
        {
            Title = "Avez-vous déjà vu une licorne?",
            Parent = null
        });

        seedQuestions.Add(new Question
        {
            Title = "Quelle est votre couleur préférée?",
            Parent = null
        });

        seedQuestions.Add(new Question
        {
            Title = "Quel est votre animal préféré?",
            Parent = null
        });

        seedQuestions.Add(new Question
        {
            Title = "Quel est votre plat préféré?",
            Parent = null
        });

        seedQuestions.Add(new Question
        {
            Title = "Quel est votre sport préféré?",
            Parent = null
        });

        seedQuestions.Add(new Question
        {
            Title = "Quel est votre livre préféré?",
            Parent = null
        });

        seedQuestions.Add(new Question
        {
            Title = "Quel est votre film préféré?",
            Parent = null
        });

        seedQuestions.Add(new Question
        {
            Title = "Quel est votre passe-temps préféré?",
            Parent = null
        });


        seedQuestions.Add(new Question
        {
            Title = "Quelle est votre surnom?",
            Parent = null
        });

        seedQuestions.Add(new Question
        {
            Title = "Avez-vou vue le dernier film des minions?",
            Parent = null
        });

        var existingQuestion = _context.Questions.IgnoreQueryFilters().FirstOrDefault(x => x.Title == seedQuestions[0].Title);
        if (existingQuestion != null)
            return;
        _context.Questions.AddRange(seedQuestions);
        await _context.SaveChangesAsync();



        seedQuestions = new List<Question>();

        var question1 = _context.Questions.FirstOrDefault(q => q.Title == "Combien de doigts avez-vous?");

        seedQuestions.Add(new Question
        {
            Title = "Est-ce que je suis un enfant?",
            Parent = question1
        });

        existingQuestion = _context.Questions.IgnoreQueryFilters().FirstOrDefault(x => x.Title == seedQuestions[0].Title);
        if (existingQuestion != null)
            return;
        _context.Questions.AddRange(seedQuestions);
        await _context.SaveChangesAsync();
    }

    private async Task SeedStudents()
    {
        List<Student> seedStudents = new List<Student>();
        seedStudents.Add(new Student
        {
            Cegep = "Cegep Garneau",
            FirstLogOn = DateTime.Now,
            SubmitedTime = DateTime.Now,
            CP = "2202022",
            Birthday = new DateTime(2003, 5, 15),
            Gender = 'M',
            AdmEmail = "etudiant1@cegep.ca",
            CollEmail = "etudiant1@college.ca",
            Status = "Actif",
            OthStuQc = "Non",
            PostalCode = "H2X1Y4",
            DesDate = DateTime.Now.AddYears(2),
            MGS = 85,
            NumEstabSec = 123456,
            AdmTurn = 1,
            Choice = "Sciences informatiques",
            Cohort = "2023-2025",
            Population = 'A',
            CollSess01 = 20231,
            CollSess02 = 20232,
            CollSess03 = 20241,
            CollSess04 = 20242,
            ProgSess01 = "420.B0",
            ProgSess02 = "420.B0",
            ProgSess03 = "420.B0",
            ProgSess04 = "420.B0",
            NbFollow01 = 5,
            NbFollow02 = 6,
            NbFollow03 = 5,
            NbFollow04 = 4,
            NbSuccess01 = 5,
            NbSuccess02 = 5,
            NbSuccess03 = 4,
            NbSuccess04 = 4,
            ProgDec = "DEC Informatique",
            YearDec = new DateTime(2025, 6, 30),
            FirstAndLastName = "Jean Bon",
            Identification = 12345678,
            Genmels = 85.5m,
            Sec5Fra = 80,
            FirstName = "Jean",
            LastName = "Bon",
            TargetYear = "2024-2025",
            TargetSemester = "A2025",
            FreqType = 'F',
            PresType = 'P',
            CollSemTarget = "A2025",
            ProgSemTarget = "420.B0",
            Reinscription = true,
            SuccessContract = true,
            TeachingServices = 2,
            EufSuccess01 = 75,
            EufSuccess02 = 80,
            EufSuccess03 = 85,
            EufSuccess04 = 90,
            Sec5Eng = 78
        });
        seedStudents.Add(new Student
        {
            Cegep = "Cegep Garneau",
            FirstLogOn = DateTime.Now,
            SubmitedTime = DateTime.Now,
            CP = "1101011",
            Birthday = new DateTime(2003, 5, 15),
            Gender = 'M',
            AdmEmail = "etudiant2@cegep.ca",
            CollEmail = "etudiant2@college.ca",
            Status = "Actif",
            OthStuQc = "Non",
            PostalCode = "H2X1Y4",
            DesDate = DateTime.Now.AddYears(2),
            MGS = 85,
            NumEstabSec = 123456,
            AdmTurn = 1,
            Choice = "Sciences informatiques",
            Cohort = "2023-2025",
            Population = 'A',
            CollSess01 = 20231,
            CollSess02 = 20232,
            CollSess03 = 20241,
            CollSess04 = 20242,
            ProgSess01 = "420.B0",
            ProgSess02 = "420.B0",
            ProgSess03 = "420.B0",
            ProgSess04 = "420.B0",
            NbFollow01 = 5,
            NbFollow02 = 6,
            NbFollow03 = 5,
            NbFollow04 = 4,
            NbSuccess01 = 5,
            NbSuccess02 = 5,
            NbSuccess03 = 4,
            NbSuccess04 = 4,
            ProgDec = "DEC Informatique",
            YearDec = new DateTime(2025, 6, 30),
            FirstAndLastName = "Jean Clean",
            Identification = 12345678,
            Genmels = 85.5m,
            Sec5Fra = 80,
            FirstName = "Jean",
            LastName = "Clean",
            TargetYear = "2024-2025",
            TargetSemester = "A2025",
            FreqType = 'F',
            PresType = 'P',
            CollSemTarget = "A2025",
            ProgSemTarget = "420.B0",
            Reinscription = true,
            SuccessContract = true,
            TeachingServices = 2,
            EufSuccess01 = 75,
            EufSuccess02 = 80,
            EufSuccess03 = 85,
            EufSuccess04 = 90,
            Sec5Eng = 78
        });
        seedStudents.Add(new Student
        {
            Cegep = "Cegep de Sainte-Foy",
            FirstLogOn = DateTime.Now,
            SubmitedTime = DateTime.Now,
            CP = "3303033",
            Birthday = new DateTime(2002, 8, 20),
            Gender = 'F',
            AdmEmail = "etudiante1@cegep.ca",
            CollEmail = "etudiante1@college.ca",
            Status = "Actif",
            OthStuQc = "Non",
            PostalCode = "G1V0A6",
            DesDate = DateTime.Now.AddYears(2),
            MGS = 90,
            NumEstabSec = 654321,
            AdmTurn = 1,
            Choice = "Sciences de la nature",
            Cohort = "2022-2024",
            Population = 'B',
            CollSess01 = 20221,
            CollSess02 = 20222,
            CollSess03 = 20231,
            CollSess04 = 20232,
            ProgSess01 = "200.B0",
            ProgSess02 = "200.B0",
            ProgSess03 = "200.B0",
            ProgSess04 = "200.B0",
            NbFollow01 = 6,
            NbFollow02 = 6,
            NbFollow03 = 5,
            NbFollow04 = 5,
            NbSuccess01 = 6,
            NbSuccess02 = 6,
            NbSuccess03 = 5,
            NbSuccess04 = 5,
            ProgDec = "DEC Sciences de la nature",
            YearDec = new DateTime(2024, 6, 30),
            FirstAndLastName = "Marie Curie",
            Identification = 87654321,
            Genmels = 90.5m,
            Sec5Fra = 85,
            FirstName = "Marie",
            LastName = "Curie",
            TargetYear = "2024-2025",
            TargetSemester = "A2025",
            FreqType = 'F',
            PresType = 'P',
            CollSemTarget = "A2025",
            ProgSemTarget = "200.B0",
            Reinscription = true,
            SuccessContract = true,
            TeachingServices = 2,
            EufSuccess01 = 80,
            EufSuccess02 = 85,
            EufSuccess03 = 90,
            EufSuccess04 = 95,
            Sec5Eng = 82
        });
        seedStudents.Add(new Student
        {
            Cegep = "Cegep de L�vis",
            FirstLogOn = DateTime.Now,
            SubmitedTime = DateTime.Now,
            CP = "4404044",
            Birthday = new DateTime(2001, 11, 10),
            Gender = 'M',
            AdmEmail = "etudiant3@cegep.ca",
            CollEmail = "etudiant3@college.ca",
            Status = "Actif",
            OthStuQc = "Non",
            PostalCode = "G6V0A7",
            DesDate = DateTime.Now.AddYears(2),
            MGS = 88,
            NumEstabSec = 789012,
            AdmTurn = 1,
            Choice = "Techniques de l'informatique",
            Cohort = "2021-2023",
            Population = 'C',
            CollSess01 = 20211,
            CollSess02 = 20212,
            CollSess03 = 20221,
            CollSess04 = 20222,
            ProgSess01 = "420.A0",
            ProgSess02 = "420.A0",
            ProgSess03 = "420.A0",
            ProgSess04 = "420.A0",
            NbFollow01 = 5,
            NbFollow02 = 5,
            NbFollow03 = 5,
            NbFollow04 = 5,
            NbSuccess01 = 5,
            NbSuccess02 = 5,
            NbSuccess03 = 5,
            NbSuccess04 = 5,
            ProgDec = "DEC Techniques de l'informatique",
            YearDec = new DateTime(2023, 6, 30),
            FirstAndLastName = "Albert Einstein",
            Identification = 23456789,
            Genmels = 88.5m,
            Sec5Fra = 83,
            FirstName = "Albert",
            LastName = "Einstein",
            TargetYear = "2024-2025",
            TargetSemester = "A2025",
            FreqType = 'F',
            PresType = 'P',
            CollSemTarget = "A2025",
            ProgSemTarget = "420.A0",
            Reinscription = true,
            SuccessContract = true,
            TeachingServices = 2,
            EufSuccess01 = 78,
            EufSuccess02 = 82,
            EufSuccess03 = 86,
            EufSuccess04 = 90,
            Sec5Eng = 80,
        });
        seedStudents.Add(new Student
        {
            Cegep = "Cegep de Limoilou",
            FirstLogOn = DateTime.Now,
            SubmitedTime = DateTime.Now,
            CP = "5505055",
            Birthday = new DateTime(2004, 3, 25),
            Gender = 'F',
            AdmEmail = "etudiante2@cegep.ca",
            CollEmail = "etudiante2@college.ca",
            Status = "Actif",
            OthStuQc = "Non",
            PostalCode = "G1L0A8",
            DesDate = DateTime.Now.AddYears(2),
            MGS = 92,
            NumEstabSec = 345678,
            AdmTurn = 1,
            Choice = "Arts visuels",
            Cohort = "2024-2026",
            Population = 'D',
            CollSess01 = 20241,
            CollSess02 = 20242,
            CollSess03 = 20251,
            CollSess04 = 20252,
            ProgSess01 = "510.A0",
            ProgSess02 = "510.A0",
            ProgSess03 = "510.A0",
            ProgSess04 = "510.A0",
            NbFollow01 = 6,
            NbFollow02 = 6,
            NbFollow03 = 6,
            NbFollow04 = 6,
            NbSuccess01 = 6,
            NbSuccess02 = 6,
            NbSuccess03 = 6,
            NbSuccess04 = 6,
            ProgDec = "DEC Arts visuels",
            YearDec = new DateTime(2026, 6, 30),
            FirstAndLastName = "Frida Kahlo",
            Identification = 34567890,
            Genmels = 92.5m,
            Sec5Fra = 87,
            FirstName = "Frida",
            LastName = "Kahlo",
            TargetYear = "2024-2025",
            TargetSemester = "A2025",
            FreqType = 'F',
            PresType = 'P',
            CollSemTarget = "A2025",
            ProgSemTarget = "510.A0",
            Reinscription = true,
            SuccessContract = true,
            TeachingServices = 2,
            EufSuccess01 = 85,
            EufSuccess02 = 88,
            EufSuccess03 = 91,
            EufSuccess04 = 94,
            Sec5Eng = 84
        });

        var existingStudent = _context.Students.IgnoreQueryFilters().FirstOrDefault(x => x.CP == seedStudents[0].CP);
        if (existingStudent != null)
            return;
        _context.Students.AddRange(seedStudents);
        await _context.SaveChangesAsync();
    }
    */

    private async Task SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync(Roles.ADMINISTRATOR).Result)
            await _roleManager.CreateAsync(new Role { Name = Roles.ADMINISTRATOR, NormalizedName = Roles.ADMINISTRATOR.Normalize() });

        if (!_roleManager.RoleExistsAsync(Roles.MEMBER).Result)
            await _roleManager.CreateAsync(new Role { Name = Roles.MEMBER, NormalizedName = Roles.MEMBER.Normalize() });
    }

    private async Task seedProgramme()
{
    List<Programme> seedProgramme = new List<Programme>();

    seedProgramme.Add(new Programme { Name = ProgrammesNames.SciencesDeLaNature, Code = ProgrammeCode.SciencesDeLaNature });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.BaccalaureatInternational, Code = ProgrammeCode.BaccalaureatInternational });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.SciencesHumaines, Code = ProgrammeCode.SciencesHumaines });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.ArtsLettresCommunication, Code = ProgrammeCode.ArtsLettresCommunication });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.HygieneDentaire, Code = ProgrammeCode.HygieneDentaire });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.Physiotherapie, Code = ProgrammeCode.Physiotherapie });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.OrthesesVisuelles, Code = ProgrammeCode.OrthesesVisuelles });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.SoinsInfirmiers, Code = ProgrammeCode.SoinsInfirmiers });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.ComptabiliteEtGestion, Code = ProgrammeCode.ComptabiliteEtGestion });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.GestionDeCommerces, Code = ProgrammeCode.GestionDeCommerces });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.LogistiqueDuTransport, Code = ProgrammeCode.LogistiqueDuTransport });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.Bureautique, Code = ProgrammeCode.Bureautique });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.Informatique, Code = ProgrammeCode.Informatique });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.TechniquesPolicieres, Code = ProgrammeCode.TechniquesPolicieres });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.InterventionEnDelinquance, Code = ProgrammeCode.InterventionEnDelinquance });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.TechniquesJuridiques, Code = ProgrammeCode.TechniquesJuridiques });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.Documentation, Code = ProgrammeCode.Documentation });
    seedProgramme.Add(new Programme { Name = ProgrammesNames.DesignDinterieur, Code = ProgrammeCode.DesignDinterieur });

    foreach (var programme in seedProgramme)
    {
        var exists = await _context.Programmes.AnyAsync(p => p.Code == programme.Code);
        if (!exists)
        {
            _context.Programmes.Add(programme);
        }
    }

    await _context.SaveChangesAsync();
}


    private async Task SeedAdmins()
    {
        var user = await _userManager.FindByEmailAsync(AdminEmail);
        if (user != null)
            return;

        user = BuildUser(AdminEmail);
        var result = await _userManager.CreateAsync(user, Password);

        if (result.Succeeded)
            await _userManager.AddToRoleAsync(user, Roles.ADMINISTRATOR);
        else
            throw new Exception($"Could not seed/create {Roles.ADMINISTRATOR} user.");


        var admin = new Administrator("Super", "Admin");
        admin.SetUser(user);
        _context.Administrators.Add(admin);
        await _context.SaveChangesAsync();
    }

    private async Task SeedMembers()
    {
        var user = await _userManager.FindByEmailAsync(MemberEmail);
        if (user != null)
            return;

        user = BuildUser(MemberEmail);
        var result = await _userManager.CreateAsync(user, Password);

        if (result.Succeeded)
            await _userManager.AddToRoleAsync(user, Roles.MEMBER);
        else
            throw new Exception($"Could not seed/create {Roles.MEMBER} user.");

        var existingMember = _context.Members.IgnoreQueryFilters().FirstOrDefault(x => x.User.Id == user.Id);
        if (existingMember is { Active: true })
            return;

        if (existingMember == null)
        {
            var member = new Member("John", "Doe", 1, "123, my street", "Quebec", "A1A 1A1");
            member.SetUser(user);
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        }
        else if (!existingMember.Active)
        {
            existingMember.Activate();
            _context.Members.Update(existingMember);
            await _context.SaveChangesAsync();
        }
    }

    private User BuildUser(string email)
    {
        return new User
        {
            Email = email,
            UserName = email,
            NormalizedEmail = email.Normalize(),
            NormalizedUserName = email,
            PhoneNumber = "555-555-5555",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false
        };
    }
}