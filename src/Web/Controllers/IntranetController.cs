using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

// FRENCH
[Route("/connexion")]
[Route("/authentification-a-deux-facteurs")]
[Route("/mot-de-passe-oublie")]
[Route("/reinitialiser-mot-de-passe")]
[Route("/mon-compte")]
[Route("/administration/membres")]
[Route("/administration/membres/ajouter")]
[Route("/administration/reponses")]
[Route("/profile-d-un-etudiant")]
[Route("/livres")]
[Route("/livres/ajouter")]
[Route("/reponses")]
[Route("/etudiants")]
// ENGLISH
[Route("/login")]
[Route("/two-factor-authentication")]
[Route("/forgot-password")]
[Route("/reset-password")]
[Route("/my-account")]
[Route("/administration/members")]
[Route("/administration/members/add")]
[Route("/administration/answers")]
[Route("/student-profile")]
[Route("/books")]
[Route("/books/add")]
[Route("/answers")]
[Route("/api/csv-analyse")]
[Route("/students")]
public class IntranetController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}