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
    public class VehiclesController : Controller
    {
        private readonly DotNetAssesmentContext _context;

        public VehiclesController(DotNetAssesmentContext context)
        {
            _context = context;
        }

        //This one for the full list
        public async Task<IActionResult> Index()
        {
            var dotNetAssesmentContext = _context.Vehicle.Include(v => v.Owner);
            return View(await dotNetAssesmentContext.ToListAsync());
        }

        //The details of a single one 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        //This returns the view to the create form
        public IActionResult Create()
        {
            ViewData["OwnerID"] = new SelectList(_context.Owner, "ID", "ID");
            return View();
        }

        //Used when submiting the create form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Brand,Vin,Color,Year,OwnerID")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerID"] = new SelectList(_context.Owner, "ID", "ID", vehicle.OwnerID);
            return View(vehicle);
        }

        //Returns the edit form view
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["OwnerID"] = new SelectList(_context.Owner, "ID", "ID", vehicle.OwnerID);
            return View(vehicle);
        }

        //Called on submiting edit form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Brand,Vin,Color,Year,OwnerID")] Vehicle vehicle)
        {
            if (id != vehicle.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.ID))
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
            ViewData["OwnerID"] = new SelectList(_context.Owner, "ID", "ID", vehicle.OwnerID);
            return View(vehicle);
        }

        //Returns the delete view
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        //Called when submiting the delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'DotNetAssesmentContext.Vehicle'  is null.");
            }
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
          return _context.Vehicle.Any(e => e.ID == id);
        }
    }
}
