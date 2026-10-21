namespace Web.Features.Members.Students;

public class StudentDto
{
    public Guid Id { get; set; }
    public DateTime? Created { get; set; }
    public string Cegep { get; set; } = default!;
    public DateTime? FirstLogOn { get; set; }
    public DateTime? SubmitedTime { get; set; }
    public string CP { get; set; } = default!;
    public DateTime? Birthday { get; set; }
    public char Gender { get; set; }
    public string AdmEmail { get; set; } = default!;
    public string CollEmail { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string OthStuQc { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public DateTime? DesDate { get; set; }
    public byte? MGS { get; set; }
    public int? NumEstabSec { get; set; }
    public byte? AdmTurn { get; set; }
    public string Choice { get; set; } = default!;
    public string Cohort { get; set; } = default!;
    public char Population { get; set; }
    public int? CollSess01 { get; set; }
    public int? CollSess02 { get; set; }
    public int? CollSess03 { get; set; }
    public int? CollSess04 { get; set; }
    public string ProgSess01 { get; set; } = default!;
    public string ProgSess02 { get; set; } = default!;
    public string ProgSess03 { get; set; } = default!;
    public string ProgSess04 { get; set; } = default!;
    public byte? NbFollow01 { get; set; }
    public byte? NbFollow02 { get; set; }
    public byte? NbFollow03 { get; set; }
    public byte? NbFollow04 { get; set; }
    public byte? NbSuccess01 { get; set; }
    public byte? NbSuccess02 { get; set; }
    public byte? NbSuccess03 { get; set; }
    public byte? NbSuccess04 { get; set; }
    public string ProgDec { get; set; } = default!;
    public DateTime? YearDec { get; set; }
    public string FirstAndLastName { get; set; } = default!;
    public int? Identification { get; set; }
    public decimal? Genmels { get; set; }
    public byte? Sec5Fra { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string TargetYear { get; set; } = default!;
    public string TargetSemester { get; set; } = default!;
    public char? FreqType { get; set; }
    public char? PresType { get; set; }
    public string CollSemTarget { get; set; } = default!;
    public string ProgSemTarget { get; set; } = default!;
    public bool? Reinscription { get; set; }
    public bool? SuccessContract { get; set; }
    public byte? TeachingServices { get; set; }
    public byte? EufSuccess01 { get; set; }
    public byte? EufSuccess02 { get; set; }
    public byte? EufSuccess03 { get; set; }
    public byte? EufSuccess04 { get; set; }
    public byte? Sec5Eng { get; set; }
    public bool ConsSuccess { get; set; }
    public bool ConsentHelp { get; set; }
}
