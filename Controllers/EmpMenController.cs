using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManWebAsp.Data;
using EmployeeManWebAsp.Models;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManWebAsp.Controllers
{
    public class EmpMenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpMenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmpMen
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmpMan.ToListAsync());
        }

        // GET: EmpMen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empMan = await _context.EmpMan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empMan == null)
            {
                return NotFound();
            }

            return View(empMan);
        }

        // GET: EmpMen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpMen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Joined")] EmpMan empMan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empMan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empMan);
        }

        // GET: EmpMen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empMan = await _context.EmpMan.FindAsync(id);
            if (empMan == null)
            {
                return NotFound();
            }
            return View(empMan);
        }

        // POST: EmpMen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Joined")] EmpMan empMan)
        {
            if (id != empMan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empMan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpManExists(empMan.Id))
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
            return View(empMan);
        }

        // GET: EmpMen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empMan = await _context.EmpMan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empMan == null)
            {
                return NotFound();
            }

            return View(empMan);
        }

        // POST: EmpMen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empMan = await _context.EmpMan.FindAsync(id);
            if (empMan != null)
            {
                _context.EmpMan.Remove(empMan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpManExists(int id)
        {
            return _context.EmpMan.Any(e => e.Id == id);
        }
    }
}
