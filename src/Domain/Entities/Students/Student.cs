using Domain.Common;

namespace Domain.Entities.Students;

public class Student: AuditableAndSoftDeletableEntity, ISanitizable
{
    //name of the college
    public string Cegep {get; set;} = default!;
    public DateTime FirstLogOn {get; set;} 
    public DateTime? SubmitedTime {get; set;} 
    //Permanent Code
    public string CP {get; set;} = default!;
    public DateTime? Birthday {get; set;} 
    public char Gender { get; set; }
    public string AdmEmail {get; set;} = default!;
    public string CollEmail {get; set;} = default!;
    public string Status {get; set;} = default!;
    public string OthStuQc {get; set;} = default!;
    public string PostalCode {get; set;} = default!;
    public DateTime? DesDate {get; set;} 
    //Secondaire General Average
    public byte? MGS  {get; set;} 
    public int? NumEstabSec {get; set;} 
    public byte? AdmTurn {get; set;}
    public string Choice {get; set;} = default!;
    public string Cohort {get; set;} = default!;
    public char Population {get; set;} 
    public int? CollSess01 {get; set;} 
    public int? CollSess02 {get; set;} 
    public int? CollSess03 {get; set;}
    public int? CollSess04 {get; set;} 
    public string ProgSess01 {get; set;} = default!;
    public string ProgSess02 {get; set;} = default!;
    public string ProgSess03 {get; set;} = default!;
    public string ProgSess04 {get; set;} = default!;
    //number of course
    public byte? NbFollow01 {get; set;} 
    public byte? NbFollow02 {get; set;} 
    public byte? NbFollow03 {get; set;} 
    public byte? NbFollow04 {get; set;} 
    public byte? NbSuccess01 {get; set;} 
    public byte? NbSuccess02 {get; set;} 
    public byte? NbSuccess03 {get; set;} 
    public byte? NbSuccess04 {get; set;} 
    public string ProgDec {get; set;} = default!;
    public DateTime? YearDec {get; set;} 
    public string FirstAndLastName {get; set;} = default!;
    public int? Identification { get; set;} 
    //general average of ministery of education
    public decimal? Genmels { get; set; } 
    public byte? Sec5Fra {get; set;} 
    public string FirstName {get; set;} = default!;
    public string LastName {get; set;} = default!;
    public string TargetYear {get; set;} 
    public string TargetSemester {get; set;} 
    public char? FreqType {get; set;} 
    public char? PresType {get; set;} 
    public string CollSemTarget {get; set;} = default!;
    public string ProgSemTarget {get; set;} = default!;
    public bool? Reinscription { get; set; }
    public bool? SuccessContract { get; set; } 
    public byte? TeachingServices { get; set; } 
    
    public byte? EufSuccess01 {get; set;} 
    public byte? EufSuccess02 {get; set;} 
    public byte? EufSuccess03 {get; set;}
    public byte? EufSuccess04 {get; set;} 
    
    public byte? Sec5Eng { get; set; } 
    
    public bool ConsentHelp {get; set;}
    
    public void SanitizeForSaving()
    {
        Cegep = Cegep.Trim();
        CP = CP.Trim();
        AdmEmail = AdmEmail.Trim();
        CollEmail = CollEmail.Trim();
        Status = Status.Trim();
        OthStuQc = OthStuQc.Trim();
        PostalCode = PostalCode.Trim();
        Choice = Choice.Trim();
        Cohort = Cohort.Trim();
        ProgSess01 = ProgSess01.Trim();
        ProgSess02 = ProgSess02.Trim();
        ProgSess03 = ProgSess03.Trim();
        ProgSess04 = ProgSess04.Trim();
        ProgDec = ProgDec.Trim();
        FirstAndLastName = FirstAndLastName.Trim();
        FirstName = FirstName.Trim();
        LastName = LastName.Trim();
        ProgSemTarget = ProgSemTarget.Trim();
    }
}