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
    public class InspectionsController : Controller
    {
        private readonly VetClinicDbContext _context;

        public InspectionsController(VetClinicDbContext context)
        {
            _context = context;
        }

        // GET: Inspections
        public async Task<IActionResult> Index()
        {
            var vetClinicDbContext = _context.Inspections.Include(i => i.Pet).Include(i => i.Staff);
            return View(await vetClinicDbContext.ToListAsync());
        }

        // GET: Inspections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections
                .Include(i => i.Pet)
                .Include(i => i.Staff)
                .FirstOrDefaultAsync(m => m.InspectionId == id);
            if (inspection == null)
            {
                return NotFound();
            }

            return View(inspection);
        }

        // GET: Inspections/Create
        public IActionResult Create()
        {
            ViewData["PetId"] = new SelectList(_context.Pets, "PetId", "PetId");
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId");
            return View();
        }

        // POST: Inspections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InspectionId,PetId,StaffId,ExamDate,Symptoms,Diagnosis,Treatment,Notes,Cost")] Inspection inspection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inspection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetId"] = new SelectList(_context.Pets, "PetId", "PetId", inspection.PetId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", inspection.StaffId);
            return View(inspection);
        }

        // GET: Inspections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections.FindAsync(id);
            if (inspection == null)
            {
                return NotFound();
            }
            ViewData["PetId"] = new SelectList(_context.Pets, "PetId", "PetId", inspection.PetId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", inspection.StaffId);
            return View(inspection);
        }

        // POST: Inspections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InspectionId,PetId,StaffId,ExamDate,Symptoms,Diagnosis,Treatment,Notes,Cost")] Inspection inspection)
        {
            if (id != inspection.InspectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inspection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InspectionExists(inspection.InspectionId))
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
            ViewData["PetId"] = new SelectList(_context.Pets, "PetId", "PetId", inspection.PetId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", inspection.StaffId);
            return View(inspection);
        }

        // GET: Inspections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections
                .Include(i => i.Pet)
                .Include(i => i.Staff)
                .FirstOrDefaultAsync(m => m.InspectionId == id);
            if (inspection == null)
            {
                return NotFound();
            }

            return View(inspection);
        }

        // POST: Inspections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inspection = await _context.Inspections.FindAsync(id);
            if (inspection != null)
            {
                _context.Inspections.Remove(inspection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InspectionExists(int id)
        {
            return _context.Inspections.Any(e => e.InspectionId == id);
        }
    }
}
