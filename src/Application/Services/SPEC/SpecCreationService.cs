using Application.Interfaces.Services.SPEC;
using Application.Services.SPEC.Dtos;
using Domain.Entities.DataLogs;
using Domain.Entities.SPEC;
using Domain.Entities.Students;
using Domain.Repositories;

namespace Application.Services.SPEC;

public class SpecCreationService : ISpecCreationService
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IOptionRepository _optionRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IRejectRepository _rejectRepository;
    private readonly IStudentRepository _studentRepository;
    private string[]? _reference;

    private List<Student>? Students { get; set; }
    private List<Question>? Questions { get; set; }
    private List<Answer>? Answers { get; set; }
    private List<Option>? Options { get; set; }
    private List<int>? ConsentIndexes { get; set; }

    private List<Reject>? Rejects { get; set; }

    public SpecCreationService(IAnswerRepository answerRepository, IOptionRepository optionRepository,
        IQuestionRepository questionRepository, IRejectRepository rejectRepository,
        IStudentRepository studentRepository)
    {
        _answerRepository = answerRepository;
        _optionRepository = optionRepository;
        _questionRepository = questionRepository;
        _rejectRepository = rejectRepository;
        _studentRepository = studentRepository;
        
    }
    

    public async Task CreateSpec(SpecRegistrationDto fileData)
    {
        Students = new List<Student>();
        Questions = new List<Question>();
        Answers = new List<Answer>();
        Options = new List<Option>();
        Rejects = new List<Reject>();
        ConsentIndexes = new List<int>();
        var fileContent = fileData.FileData.Replace("\r\n", "\n");
        string[] columns = fileContent.Split("\n");
        _reference = columns[0].Split(";");
        CreateSpecCanva(_reference.Skip(99).ToArray());
        foreach (var column in columns)
        {
            var consentPortrait = true;
            var consentHelp = true;
            string[] data = column.Split(";");
            string[] studentsData = data.Take(99).ToArray();
            if (studentsData[0] == "Garneau" && !string.IsNullOrEmpty(data[99]))
                try
                {
                    foreach (var index in ConsentIndexes)
                        if (index < data.Length)
                        {
                            if (data[index].Contains("Non") && _reference[index].Contains("PORTRAIT"))
                                consentPortrait = false;
                            if (data[index].Contains("Non") && _reference[index].Contains("REUSSITE"))
                                consentHelp = false;
                        }

                    if (consentPortrait)
                    {
                        var student = AddStudent(studentsData, consentHelp);
                        AddStudentAnswer(student, data.Skip(99).ToArray());
                    }
                }
                catch (Exception exception)
                {
                    string[] exceptionData = [exception.Message, exception.StackTrace ?? ""];
                    Rejects.Add(new Reject { Data = data, Exception = exceptionData });
                }
        }

        Console.Write(
            $"number of student : {Students.Count} \n number of question : {Questions.Count} \n number of option {Options.Count} \n number of answer : {Answers.Count} \n number of reject : {Rejects.Count}");
        /*modele d'exception pour afficher a l'utilisateur d'une erreur c'est produite
         * if(Rejects.Count != 0)
         *      throw new ApplicationException("une errreur c'est produite dans le traitement des donnee");
         */
        await _studentRepository.CreateBulkStudents(Students);

        await _questionRepository.CreateBulkQuestions(Questions);

        await _optionRepository.CreateBulkOptions(Options);

        await _answerRepository.CreateBulkAnswers(Answers);

        await _rejectRepository.CreateBulkRejects(Rejects);
    }

    private void AddStudentAnswer(Student student, string[] answer)
    {
        for (var i = 0; i < answer.Length; i++)
            if (!string.IsNullOrEmpty(answer[i]))
            {
                var question = Questions.FirstOrDefault(q => q.Title == _reference[i + 99]);
                if (question != null)
                {
                    var option = Options.FirstOrDefault(o => o.Title == answer[i] && o.IdQuestion == question);
                    if (option == null)
                    {
                        option = new Option { IdQuestion = question, Title = answer[i] };
                        Options.Add(option);
                    }

                    Answers.Add(new Answer { IdOption = option, IdStudent = student });
                }
            }
    }

    //Method relative to the canvas creation
    private void CreateSpecCanva(string[] data)
    {
        var i = 99;
        foreach (var str in data)
        {
            if (str.Contains('Q'))
            {
                var parentString = str.Substring(0, str.Length - 1);
                var pQuestion = FindQuestion(parentString);
                if (pQuestion != null)
                {
                    Questions.Add(new Question { Parent = pQuestion, Title = str });
                }
                else if (str[str.Length - 1] >= 'A')
                {
                    AddQuestion(null, str, parentString);
                }
                else if (str[str.Length - 1] <= '9' && str[str.Length - 2] >= 'A')
                {
                    pQuestion = FindQuestion(str.Substring(0, str.Length - 2));
                    AddQuestion(pQuestion, str, parentString);
                }
                else
                {
                    Questions.Add(new Question { Title = str });
                }
            }

            if (str.Contains("CONS"))
                ConsentIndexes.Add(i);
            i++;
        }
    }

    private Question? FindQuestion(string str)
    {
        return Questions.FirstOrDefault(q => q.Title == str);
    }

    private void AddQuestion(Question? pQuestion, string title, string parent)
    {
        var question = new Question { Parent = pQuestion, Title = parent };
        Questions.Add(question);
        Questions.Add(new Question { Parent = question, Title = title });
    }

    //Method relative to the student creation
    private Student AddStudent(string[] data, bool consentHelp)
    {
        var student = new Student();
        student.Cegep = data[0];
        student.FirstLogOn = DateTime.Parse(data[1]);
        student.SubmitedTime = string.IsNullOrEmpty(data[2]) ? null : DateTime.Parse(data[2]);
        student.CP = data[3];
        student.Birthday = DateTime.Parse(data[4]);
        student.Gender = data[5][0];
        student.AdmEmail = data[6];
        student.CollEmail = data[7];
        student.Status = data[8];
        student.OthStuQc = data[9];
        student.PostalCode = data[10];
        student.DesDate = string.IsNullOrEmpty(data[11]) ? null : DateTime.Parse(data[11]);
        student.MGS = string.IsNullOrEmpty(data[12]) ? null : byte.Parse(data[12]);
        student.NumEstabSec = string.IsNullOrEmpty(data[13]) ? null : int.Parse(data[13]);
        student.AdmTurn = string.IsNullOrEmpty(data[14]) ? null : byte.Parse(data[14]);
        student.Choice = data[15];
        student.Cohort = data[16];
        student.Population = data[17][0];
        student.CollSess01 = string.IsNullOrEmpty(data[18]) ? null : int.Parse(data[18]);
        student.CollSess02 = string.IsNullOrEmpty(data[19]) ? null : int.Parse(data[19]);
        student.CollSess03 = string.IsNullOrEmpty(data[20]) ? null : int.Parse(data[20]);
        student.CollSess04 = string.IsNullOrEmpty(data[21]) ? null : int.Parse(data[21]);
        student.ProgSess01 = data[30];
        student.ProgSess02 = data[31];
        student.ProgSess03 = data[32];
        student.ProgSess04 = data[33];
        student.NbFollow01 = string.IsNullOrEmpty(data[42]) ? null : byte.Parse(data[42]);
        student.NbFollow02 = string.IsNullOrEmpty(data[43]) ? null : byte.Parse(data[43]);
        student.NbFollow03 = string.IsNullOrEmpty(data[44]) ? null : byte.Parse(data[44]);
        student.NbFollow04 = string.IsNullOrEmpty(data[45]) ? null : byte.Parse(data[45]);
        student.NbSuccess01 = string.IsNullOrEmpty(data[54]) ? null : byte.Parse(data[54]);
        student.NbSuccess02 = string.IsNullOrEmpty(data[55]) ? null : byte.Parse(data[55]);
        student.NbSuccess03 = string.IsNullOrEmpty(data[56]) ? null : byte.Parse(data[56]);
        student.NbSuccess04 = string.IsNullOrEmpty(data[57]) ? null : byte.Parse(data[57]);
        student.ProgDec = data[66];
        student.YearDec = string.IsNullOrEmpty(data[67]) ? null : DateTime.Parse(data[67]);
        student.FirstAndLastName = data[68];
        student.Identification = string.IsNullOrEmpty(data[69]) ? null : int.Parse(data[69]);
        student.Genmels = string.IsNullOrEmpty(data[70]) ? null : int.Parse(data[70]);
        student.Sec5Fra = string.IsNullOrEmpty(data[71]) ? null : byte.Parse(data[71]);
        student.FirstName = data[72];
        student.LastName = data[73];
        student.TargetYear = data[74];
        student.TargetSemester = data[75];
        student.FreqType = string.IsNullOrEmpty(data[76]) ? null : data[76][0];
        student.PresType = string.IsNullOrEmpty(data[78]) ? null : data[78][0];
        student.CollSemTarget = data[79];
        student.ProgSemTarget = data[80];
        student.Reinscription = data[81] == "Oui" ? true : data[81] == "Non" ? false : null;
        student.SuccessContract = data[83] == "Oui" ? true : data[83] == "Non" ? false : null;
        student.TeachingServices = string.IsNullOrEmpty(data[84]) ? null : byte.Parse(data[84]);
        student.EufSuccess01 = string.IsNullOrEmpty(data[85]) ? null : byte.Parse(data[85]);
        student.EufSuccess02 = string.IsNullOrEmpty(data[86]) ? null : byte.Parse(data[86]);
        student.EufSuccess03 = string.IsNullOrEmpty(data[87]) ? null : byte.Parse(data[87]);
        student.EufSuccess04 = string.IsNullOrEmpty(data[88]) ? null : byte.Parse(data[88]);
        student.Sec5Eng = string.IsNullOrEmpty(data[98]) ? null : byte.Parse(data[98]);
        student.ConsentHelp = consentHelp;
        Students.Add(student);
        return student;
    }
}