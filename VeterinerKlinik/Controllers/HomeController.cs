using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinik.Data;
using VeterinerKlinik.Models;

namespace VeterinerKlinik.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly VetClinicDbContext _context;


    public HomeController(ILogger<HomeController> logger, VetClinicDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        try
        {
            // Toplam hayvan sayýsý
            ViewBag.TotalPets = _context.Pets.Count();

            // Toplam sahip sayýsý
            ViewBag.TotalOwners = _context.Owners.Count();

            // Son muayeneler
            ViewBag.RecentInspections = _context.Inspections
                .Include(i => i.Pet)
                .OrderByDescending(i => i.ExamDate)
                .Take(5)
                .ToList();

            // Yaklaþan aþý hatýrlatmalarý
            ViewBag.UpcomingVaccinations = _context.Vaccinations
                .Include(v => v.Pet)
                .Where(v => v.NextVaccinationDate != null && v.NextVaccinationDate > DateOnly.FromDateTime(DateTime.Today))
                .OrderBy(v => v.NextVaccinationDate)
                .Take(5)
                .ToList();
        }
        catch (Exception ex)
        {   // Hata durumunda boþ deðerler kullan
            ViewBag.TotalPets = 0;
            ViewBag.TotalOwners = 0;

            // Hatayý logla
            Console.WriteLine($"Hata: {ex.Message}");
        }
        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
