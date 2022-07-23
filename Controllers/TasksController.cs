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

        public async Task<IActionResult> Index(string msg = "")
        {
            ViewBag.msg = msg;
            return View(await _context.Task.Include(d => d.Assignment).Include(d => d.Users).ToListAsync());
        }

        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTask([Bind("TaskId, AssignmentId, UserId, SubmissionDate, SubmitStatus")] Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Task.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { msg = "Task Created Successfully!" });
            }
            return View(task);
        }
    }
}
