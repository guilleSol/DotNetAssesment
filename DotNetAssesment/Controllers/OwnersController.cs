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
    public class OwnersController : Controller
    {
        private readonly DotNetAssesmentContext _context;

        public OwnersController(DotNetAssesmentContext context)
        {
            _context = context;
        }

        //This one for the full list
        public async Task<IActionResult> Index()
        {
              return View(await _context.Owner.ToListAsync());
        }

        //The details of a single one 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Owner == null)
            {
                return NotFound();
            }

            var owner = await _context.Owner
                .FirstOrDefaultAsync(m => m.ID == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        //This returns the view to the create form
        public IActionResult Create()
        {
            return View();
        }

        //Used when submiting the create form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,DriverLicense,Vehicles")] Owner owner)
        {
            Console.WriteLine(owner.Vehicles.Count());
            if (ModelState.IsValid)
            {
                Console.WriteLine("here");
                _context.Add(owner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }

        //Returns the edit form view
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Owner == null)
            {
                return NotFound();
            }

            var owner = await _context.Owner.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            return View(owner);
        }

        //Called on submiting edit form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,DriverLicense")] Owner owner)
        {
            if (id != owner.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(owner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerExists(owner.ID))
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
            return View(owner);
        }

        //Returns the delete view
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Owner == null)
            {
                return NotFound();
            }

            var owner = await _context.Owner
                .FirstOrDefaultAsync(m => m.ID == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        //Called when submiting the delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Owner == null)
            {
                return Problem("Entity set 'DotNetAssesmentContext.Owner'  is null.");
            }
            var owner = await _context.Owner.FindAsync(id);
            if (owner != null)
            {
                _context.Owner.Remove(owner);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerExists(int id)
        {
          return _context.Owner.Any(e => e.ID == id);
        }
    }
}
