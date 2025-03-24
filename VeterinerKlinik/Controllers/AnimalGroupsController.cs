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
    public class AnimalGroupsController : Controller
    {
        private readonly VetClinicDbContext _context;

        public AnimalGroupsController(VetClinicDbContext context)
        {
            _context = context;
        }

        // GET: AnimalGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnimalGroups.ToListAsync());
        }

        // GET: AnimalGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalGroup = await _context.AnimalGroups
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (animalGroup == null)
            {
                return NotFound();
            }

            return View(animalGroup);
        }

        // GET: AnimalGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnimalGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,GroupName")] AnimalGroup animalGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animalGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animalGroup);
        }

        // GET: AnimalGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalGroup = await _context.AnimalGroups.FindAsync(id);
            if (animalGroup == null)
            {
                return NotFound();
            }
            return View(animalGroup);
        }

        // POST: AnimalGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,GroupName")] AnimalGroup animalGroup)
        {
            if (id != animalGroup.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalGroupExists(animalGroup.GroupId))
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
            return View(animalGroup);
        }

        // GET: AnimalGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalGroup = await _context.AnimalGroups
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (animalGroup == null)
            {
                return NotFound();
            }

            return View(animalGroup);
        }

        // POST: AnimalGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animalGroup = await _context.AnimalGroups.FindAsync(id);
            if (animalGroup != null)
            {
                _context.AnimalGroups.Remove(animalGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalGroupExists(int id)
        {
            return _context.AnimalGroups.Any(e => e.GroupId == id);
        }
    }
}
