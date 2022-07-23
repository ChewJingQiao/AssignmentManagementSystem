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
    public class MarkAssignmentsController : Controller
    {
        private readonly AssignmentManagementSystemContext _context;

        public MarkAssignmentsController(AssignmentManagementSystemContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string msg = "")
        {
            ViewBag.msg = msg;
            return View(await _context.TeamAssignment.ToListAsync());
        }

        public async Task<IActionResult> Mark(int?
           id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taid = await _context.TeamAssignment.FindAsync(id);
            if (taid == null)
            {
                return NotFound();
            }
            return View(taid);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int TeamAssignmentId, [Bind("TeamAssignmentId,TeamId,AssignmentId,s3Location,submitStatus,mark")] TeamAssignment teamAssignment)
        {
            if (TeamAssignmentId != teamAssignment.TeamAssignmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamAssignmentExists(teamAssignment.TeamAssignmentId))
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
            return View(teamAssignment);
        }

        private bool TeamAssignmentExists(int teamAssignmentId)
        {
            return _context.TeamAssignment.Any(e => e.TeamAssignmentId == teamAssignmentId);
        }
    }
}