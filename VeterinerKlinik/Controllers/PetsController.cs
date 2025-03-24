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
    public class PetsController : Controller
    {
        private readonly VetClinicDbContext _context;

        public PetsController(VetClinicDbContext context)
        {
            _context = context;
        }

        // GET: Pets
        public async Task<IActionResult> Index()
        {
            var vetClinicDbContext = _context.Pets.Include(p => p.Group).Include(p => p.Owner);
            return View(await vetClinicDbContext.ToListAsync());
        }

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.Group)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.AnimalGroups, "GroupId", "GroupId");
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId");
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetId,OwnerId,GroupId,Name,Species,Breed,BirthDate,Gender,Weight,MicrochipNumber,RegistrationDate,Age")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.AnimalGroups, "GroupId", "GroupId", pet.GroupId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", pet.OwnerId);
            return View(pet);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.AnimalGroups, "GroupId", "GroupId", pet.GroupId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", pet.OwnerId);
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetId,OwnerId,GroupId,Name,Species,Breed,BirthDate,Gender,Weight,MicrochipNumber,RegistrationDate,Age")] Pet pet)
        {
            if (id != pet.PetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.PetId))
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
            ViewData["GroupId"] = new SelectList(_context.AnimalGroups, "GroupId", "GroupId", pet.GroupId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", pet.OwnerId);
            return View(pet);
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.Group)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.PetId == id);
        }
    }
}
