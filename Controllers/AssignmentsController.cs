using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AssignmentManagementSystem.Models;
using AssignmentManagementSystem.Data;


namespace AssignmentManagementSystem.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly AssignmentManagementSystemContext _context;

        public AssignmentsController(AssignmentManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string msg = "")
        {
            ViewBag.msg = msg;
          return View(await _context.Assignment.Include(d => d.Module).ToListAsync());
        }

        public IActionResult AddAssignment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAssignment([Bind("AssignmentId, AssignmentName, handoutDate, submissionDate, Module,ModuleRefId")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _context.Assignment.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { msg = "Assignment Created Successfully!" });
            }
            return View(assignment);
        }
    }
}
