using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymManagmentSystemMVC.Data;
using GymManagmentSystemMVC.Models;

namespace GymManagmentSystemMVC.Controllers
{
    public class ClassViewModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassViewModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClassViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.classViewModels.ToListAsync());
        }

        // GET: ClassViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classViewModel = await _context.classViewModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classViewModel == null)
            {
                return NotFound();
            }

            return View(classViewModel);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,TrainerId,StartTime,EndTime,Capacity,Price")] ClassViewModel classViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classViewModel);
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,TrainerId,StartTime,EndTime,Capacity,Price")] ClassViewModel classViewModel)
        {
            if (id != classViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassViewModelExists(classViewModel.Id))
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
            return View(classViewModel);
        }

   
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classViewModel = await _context.classViewModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classViewModel == null)
            {
                return NotFound();
            }

            return View(classViewModel);
        }

        // POST: ClassViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classViewModel = await _context.classViewModels.FindAsync(id);
            if (classViewModel != null)
            {
                _context.classViewModels.Remove(classViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassViewModelExists(int id)
        {
            return _context.classViewModels.Any(e => e.Id == id);
        }
    }
}
