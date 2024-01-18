using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class AssessmentsController : Controller
    {
        private readonly SchoolContext _context;

        public AssessmentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Assessments
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Assessments.Include(a => a.Student);
            return View(await schoolContext.ToListAsync());
        }

        //public async Task<IActionResult> Index(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var assessments = await _context.Assessments
        //        .Where(a => a.StudentID == id)
        //        .ToListAsync();

        //    var student = await _context.Students.FindAsync(id);
        //    ViewBag.studentName = student.FirstMidName + " " + student.LastName;
        //    ViewBag.studentId = student.ID;

        //    return View(assessments);
        //}

        // GET: Assessments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessment = await _context.Assessments
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.AssessmentId == id);
            if (assessment == null)
            {
                return NotFound();
            }

            return View(assessment);
        }

        // GET: Assessments/Create
        public IActionResult Create()
        {
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "ID");
            return View();
        }

        // POST: Assessments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssessmentId,Date,AssessmentNumber,StudentID")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assessment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "ID", assessment.StudentID);
            return View(assessment);
        }

        // GET: Assessments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessment = await _context.Assessments.FindAsync(id);
            if (assessment == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "ID", assessment.StudentID);
            return View(assessment);
        }

        // POST: Assessments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssessmentId,Date,AssessmentNumber,StudentID")] Assessment assessment)
        {
            if (id != assessment.AssessmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assessment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssessmentExists(assessment.AssessmentId))
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
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "ID", assessment.StudentID);
            return View(assessment);
        }

        // GET: Assessments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessment = await _context.Assessments
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.AssessmentId == id);
            if (assessment == null)
            {
                return NotFound();
            }

            return View(assessment);
        }

        // POST: Assessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assessment = await _context.Assessments.FindAsync(id);
            _context.Assessments.Remove(assessment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssessmentExists(int id)
        {
            return _context.Assessments.Any(e => e.AssessmentId == id);
        }
    }
}
