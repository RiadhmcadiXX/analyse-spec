namespace Domain.Entities.StudentCours;

public class StudentCours
{
    public string AnneeSession { get; set; }
    public string CodeCours { get; set; }
    public string TitreCours { get; set; }
    public string ServiceEnseignement { get; set; }
    public double Note { get; set; }
    public double? Moyenne { get; set; }
    public string Remarque { get; set; }
    public double NbUnites { get; set; }
    public string Categorie { get; set; }
    public string Programme { get; set; }
}
