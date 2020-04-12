using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using corona.Data;
using corona.Models;
using Microsoft.AspNetCore.Authorization;

namespace corona.Controllers
{
    [Authorize]
    public class InfectionDataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InfectionDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InfectionData
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InfectionData.Include(i => i.Country);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InfectionData/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infectionData = await _context.InfectionData
                .Include(i => i.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (infectionData == null)
            {
                return NotFound();
            }

            return View(infectionData);
        }

        // GET: InfectionData/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        // POST: InfectionData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConfirmedCases,Deaths,Recovered,CountryId")] InfectionData infectionData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(infectionData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", infectionData.CountryId);
            return View(infectionData);
        }

        // GET: InfectionData/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infectionData = await _context.InfectionData.FindAsync(id);
            if (infectionData == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", infectionData.CountryId);
            return View(infectionData);
        }

        // POST: InfectionData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConfirmedCases,Deaths,Recovered,CountryId")] InfectionData infectionData)
        {
            if (id != infectionData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(infectionData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfectionDataExists(infectionData.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", infectionData.CountryId);
            return View(infectionData);
        }

        // GET: InfectionData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infectionData = await _context.InfectionData
                .Include(i => i.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (infectionData == null)
            {
                return NotFound();
            }

            return View(infectionData);
        }

        // POST: InfectionData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var infectionData = await _context.InfectionData.FindAsync(id);
            _context.InfectionData.Remove(infectionData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfectionDataExists(int id)
        {
            return _context.InfectionData.Any(e => e.Id == id);
        }
    }
}
