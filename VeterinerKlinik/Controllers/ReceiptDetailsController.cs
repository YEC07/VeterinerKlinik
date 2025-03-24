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
    public class ReceiptDetailsController : Controller
    {
        private readonly VetClinicDbContext _context;

        public ReceiptDetailsController(VetClinicDbContext context)
        {
            _context = context;
        }

        // GET: ReceiptDetails
        public async Task<IActionResult> Index()
        {
            var vetClinicDbContext = _context.ReceiptDetails.Include(r => r.Inspection).Include(r => r.Receipt);
            return View(await vetClinicDbContext.ToListAsync());
        }

        // GET: ReceiptDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptDetail = await _context.ReceiptDetails
                .Include(r => r.Inspection)
                .Include(r => r.Receipt)
                .FirstOrDefaultAsync(m => m.DetailId == id);
            if (receiptDetail == null)
            {
                return NotFound();
            }

            return View(receiptDetail);
        }

        // GET: ReceiptDetails/Create
        public IActionResult Create()
        {
            ViewData["InspectionId"] = new SelectList(_context.Inspections, "InspectionId", "InspectionId");
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "ReceiptId", "ReceiptId");
            return View();
        }

        // POST: ReceiptDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetailId,ReceiptId,InspectionId,Amount")] ReceiptDetail receiptDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receiptDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InspectionId"] = new SelectList(_context.Inspections, "InspectionId", "InspectionId", receiptDetail.InspectionId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "ReceiptId", "ReceiptId", receiptDetail.ReceiptId);
            return View(receiptDetail);
        }

        // GET: ReceiptDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptDetail = await _context.ReceiptDetails.FindAsync(id);
            if (receiptDetail == null)
            {
                return NotFound();
            }
            ViewData["InspectionId"] = new SelectList(_context.Inspections, "InspectionId", "InspectionId", receiptDetail.InspectionId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "ReceiptId", "ReceiptId", receiptDetail.ReceiptId);
            return View(receiptDetail);
        }

        // POST: ReceiptDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetailId,ReceiptId,InspectionId,Amount")] ReceiptDetail receiptDetail)
        {
            if (id != receiptDetail.DetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receiptDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptDetailExists(receiptDetail.DetailId))
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
            ViewData["InspectionId"] = new SelectList(_context.Inspections, "InspectionId", "InspectionId", receiptDetail.InspectionId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "ReceiptId", "ReceiptId", receiptDetail.ReceiptId);
            return View(receiptDetail);
        }

        // GET: ReceiptDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptDetail = await _context.ReceiptDetails
                .Include(r => r.Inspection)
                .Include(r => r.Receipt)
                .FirstOrDefaultAsync(m => m.DetailId == id);
            if (receiptDetail == null)
            {
                return NotFound();
            }

            return View(receiptDetail);
        }

        // POST: ReceiptDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receiptDetail = await _context.ReceiptDetails.FindAsync(id);
            if (receiptDetail != null)
            {
                _context.ReceiptDetails.Remove(receiptDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptDetailExists(int id)
        {
            return _context.ReceiptDetails.Any(e => e.DetailId == id);
        }
    }
}
