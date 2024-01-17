using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Controllers
{
    public class AssessmentsController : Controller
    {
        private readonly SchoolContext _context;

        public AssessmentsController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int StudentID)
        {
       

            var assessments = await _context.Assessments
                .Where(a => a.StudentID == StudentID)
                .Select(a => a.AssessmentNumber)
                .ToListAsync();

            var student = await _context.Students.FindAsync(StudentID);
            ViewBag.studentName = student.FirstMidName + " " + student.LastName; 

            return View(assessments);
        }

        public IActionResult Create()
        {
            return View();
        }


    }
}
