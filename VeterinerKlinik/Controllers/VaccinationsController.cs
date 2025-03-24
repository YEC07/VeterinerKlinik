using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinik.Data;
using VeterinerKlinik.Models;

namespace VeterinerKlinik.Controllers
{
    public class VaccinationsController : Controller
    {
        private readonly VetClinicDbContext _context;

        public VaccinationsController(VetClinicDbContext context)
        {
            _context = context;
        }

        // GET: Vaccinations
        public async Task<IActionResult> Index()
        {
            var vetClinicDbContext = _context.Vaccinations.Include(v => v.Pet).Include(v => v.Staff).Include(v => v.Vaccine);
            return View(await vetClinicDbContext.ToListAsync());
        }

        // GET: Vaccinations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations
                .Include(v => v.Pet)
                .Include(v => v.Staff)
                .Include(v => v.Vaccine)
                .FirstOrDefaultAsync(m => m.VaccinationId == id);
            if (vaccination == null)
            {
                return NotFound();
            }

            return View(vaccination);
        }

        // GET: Vaccinations/Create
        public IActionResult Create()
        {
            ViewData["PetId"] = new SelectList(_context.Pets, "PetId", "PetId");
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId");
            ViewData["VaccineId"] = new SelectList(_context.Inventory, "ItemId", "ItemId");
            return View();
        }

        // POST: Vaccinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VaccinationId,PetId,VaccineId,StaffId,VaccinationDate,NextVaccinationDate,Notes")] Vaccination vaccination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetId"] = new SelectList(_context.Pets, "PetId", "PetId", vaccination.PetId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", vaccination.StaffId);
            ViewData["VaccineId"] = new SelectList(_context.Inventory, "ItemId", "ItemId", vaccination.VaccineId);
            return View(vaccination);
        }

        // GET: Vaccinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations.FindAsync(id);
            if (vaccination == null)
            {
                return NotFound();
            }
            ViewData["PetId"] = new SelectList(_context.Pets, "PetId", "PetId", vaccination.PetId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", vaccination.StaffId);
            ViewData["VaccineId"] = new SelectList(_context.Inventory, "ItemId", "ItemId", vaccination.VaccineId);
            return View(vaccination);
        }

        // POST: Vaccinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VaccinationId,PetId,VaccineId,StaffId,VaccinationDate,NextVaccinationDate,Notes")] Vaccination vaccination)
        {
            if (id != vaccination.VaccinationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccinationExists(vaccination.VaccinationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetId"] = new SelectList(_context.Pets, "PetId", "PetId", vaccination.PetId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", vaccination.StaffId);
            ViewData["VaccineId"] = new SelectList(_context.Inventory, "ItemId", "ItemId", vaccination.VaccineId);
            return View(vaccination);
        }

        // GET: Vaccinations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations
                .Include(v => v.Pet)
                .Include(v => v.Staff)
                .Include(v => v.Vaccine)
                .FirstOrDefaultAsync(m => m.VaccinationId == id);
            if (vaccination == null)
            {
                return NotFound();
            }

            return View(vaccination);
        }

        // POST: Vaccinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaccination = await _context.Vaccinations.FindAsync(id);
            if (vaccination != null)
            {
                _context.Vaccinations.Remove(vaccination);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationExists(int id)
        {
            return _context.Vaccinations.Any(e => e.VaccinationId == id);
        }
    }
}
