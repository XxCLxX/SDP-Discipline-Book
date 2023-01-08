using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asp_book.Areas.Identity.Data;
using asp_book.Models;

namespace asp_book.Controllers
{
    public class GroupSubjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupSubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GroupSubjects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GroupSubjects.Include(g => g.Group).Include(g => g.Subject);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GroupSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GroupSubjects == null)
            {
                return NotFound();
            }

            var groupSubject = await _context.GroupSubjects
                .Include(g => g.Group)
                .Include(g => g.Subject)
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (groupSubject == null)
            {
                return NotFound();
            }

            return View(groupSubject);
        }

        // GET: GroupSubjects/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupName");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
            return View();
        }

        // POST: GroupSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,GroupName,SubjectId,SubjectName")] GroupSubject groupSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupName", groupSubject.GroupId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", groupSubject.SubjectId);
            return View(groupSubject);
        }

        // GET: GroupSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GroupSubjects == null)
            {
                return NotFound();
            }

            var groupSubject = await _context.GroupSubjects.FindAsync(id);
            if (groupSubject == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupName", groupSubject.GroupId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", groupSubject.SubjectId);
            return View(groupSubject);
        }

        // POST: GroupSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,GroupName,SubjectId,SubjectName")] GroupSubject groupSubject)
        {
            if (id != groupSubject.SubjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupSubjectExists(groupSubject.SubjectId))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupName", groupSubject.GroupId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", groupSubject.SubjectId);
            return View(groupSubject);
        }

        // GET: GroupSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GroupSubjects == null)
            {
                return NotFound();
            }

            var groupSubject = await _context.GroupSubjects
                .Include(g => g.Group)
                .Include(g => g.Subject)
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (groupSubject == null)
            {
                return NotFound();
            }

            return View(groupSubject);
        }

        // POST: GroupSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GroupSubjects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GroupSubjects'  is null.");
            }
            var groupSubject = await _context.GroupSubjects.FindAsync(id);
            if (groupSubject != null)
            {
                _context.GroupSubjects.Remove(groupSubject);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupSubjectExists(int id)
        {
          return _context.GroupSubjects.Any(e => e.SubjectId == id);
        }
    }
}
