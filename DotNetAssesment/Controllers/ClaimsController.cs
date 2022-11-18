using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotNetAssesment.Data;
using DotNetAssesment.Models;

namespace DotNetAssesment.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly DotNetAssesmentContext _context;

        public ClaimsController(DotNetAssesmentContext context)
        {
            _context = context;
        }

        //This one for the full list
        public async Task<IActionResult> Index()
        {
            var dotNetAssesmentContext = _context.Claim.Include(c => c.Vehicle);
            return View(await dotNetAssesmentContext.ToListAsync());
        }

        //The details of a single one 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Claim == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim
                .Include(c => c.Vehicle)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        //This returns the view to the create form
        public IActionResult Create()
        {
            ViewData["VehicleID"] = new SelectList(_context.Vehicle, "ID", "ID");
            return View();
        }

        //Used when submiting the create form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Description,Status,Date,VehicleID")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleID"] = new SelectList(_context.Vehicle, "ID", "ID", claim.VehicleID);
            return View(claim);
        }

        //Returns the edit form view
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Claim == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            ViewData["VehicleID"] = new SelectList(_context.Vehicle, "ID", "ID", claim.VehicleID);
            return View(claim);
        }

        //Called on submiting edit form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Description,Status,Date,VehicleID")] Claim claim)
        {
            if (id != claim.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimExists(claim.ID))
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
            ViewData["VehicleID"] = new SelectList(_context.Vehicle, "ID", "ID", claim.VehicleID);
            return View(claim);
        }

        //Returns the delete view
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Claim == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim
                .Include(c => c.Vehicle)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        //Called when submiting the delete

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Claim == null)
            {
                return Problem("Entity set 'DotNetAssesmentContext.Claim'  is null.");
            }
            var claim = await _context.Claim.FindAsync(id);
            if (claim != null)
            {
                _context.Claim.Remove(claim);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimExists(int id)
        {
          return _context.Claim.Any(e => e.ID == id);
        }
    }
}
