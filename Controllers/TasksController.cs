using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AssignmentManagementSystem.Models;
using AssignmentManagementSystem.Data;
using Task = AssignmentManagementSystem.Models.Task;

namespace AssignmentManagementSystem.Controllers
{
    public class TasksController : Controller
    {
        private readonly AssignmentManagementSystemContext _context;

        public TasksController(AssignmentManagementSystemContext context)
        {
            _context = context;
        }

        //GET Assignment ID and User ID
        /*public async Task<IActionResult> Index(string searchstring, string AssignmentType)
        {
            //retrieve the distinct flower type value from the flower table and attach to the html dropbox 
            IQueryable<string> sqlquery = from m in _context.Assignment
                                          orderby m.AssignmentName
                                          select m.AssignmentName;

            IEnumerable<SelectListItem> items = new SelectList(
                    await sqlquery.Distinct().ToListAsync());

            ViewBag.AssignmentType = items;

            var assignmentlist = from m in _context.Assignment
                                 select m;

            return View(await assignmentlist.ToListAsync());
        }*/

        //View Task
        public async Task<IActionResult> Index(string msg = "")
        {
            ViewBag.msg = msg;
            return View(await _context.Task.Include(d => d.Assignment).Include(d => d.Users).Include(d=>d.TeamAssignment).ToListAsync());
        }

        ////////////////Create Task
        public IActionResult CreateTask()
        {
            return View();
        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTask([Bind("TaskId, TeamAssignmentId, AssignmentId, UserId, SubmissionDate, SubmitStatus")] Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Task.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { msg = "Task Created Successfully!" });
            }
            return View(task);
        }

        /////////////////Edit Task
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        private bool TaskExists(string taskId)
        {
            return _context.Task.Any(e => e.TaskId == taskId);
        }

        // POST: Edit Task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TaskId, TeamAssignmentId, AssignmentId, UserId, SubmissionDate, SubmitStatus")] Task task)
        {
            if (id != task.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.TaskId))
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
            return View(task);
        }

        /////////////////Task Details
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        /////////////////GET: Delete Task
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        ///////////////POST: Delete Task
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var task = await _context.Task.FindAsync(id);
            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
