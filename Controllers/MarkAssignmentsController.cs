using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AssignmentManagementSystem.Models;
using AssignmentManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using AssignmentManagementSystem.Areas.Identity.Data;


namespace AssignmentManagementSystem.Controllers
{
    public class MarkAssignmentsController : Controller
    {
        private readonly AssignmentManagementSystemContext _context;
        UserManager<AssignmentManagementSystemUser> UserManager;
        SignInManager<AssignmentManagementSystemUser> SignInManager;

        public MarkAssignmentsController(AssignmentManagementSystemContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string msg = "")
        {
            ViewBag.msg = msg;
            List<TeamAssignment> teamassignmentlist = await _context.TeamAssignment.ToListAsync();
            List<LecturerModule> lecturermodulelist = await _context.LecturerModule.ToListAsync();
            List<Assignment> assignmentlist = await _context.Assignment.ToListAsync();
            string username = User.Identity.Name;
            var userlist = this._context.User.ToList();
            foreach(AssignmentManagementSystemUser ee in userlist)
            {
                if(ee.UserName == username)
                {
                    string useridnow = ee.Id;

                    List<string> moduleofuser = new List<string>();
                    List<string> assignmentofuser = new List<string>();
                    List<TeamAssignment> finallist = new List<TeamAssignment>();
                    foreach (LecturerModule lml in lecturermodulelist)
                    {
                        if (lml.UserId == useridnow)
                        {
                            moduleofuser.Add(lml.ModuleId);
                        }
                    }
                    foreach (Assignment a in assignmentlist)
                    {
                        foreach (string b in moduleofuser)
                        {
                            if (a.ModuleRefId == b)
                            {
                                assignmentofuser.Add(a.AssignmentId);
                            }
                        }
                    }
                    foreach (TeamAssignment tal in teamassignmentlist)
                    {
                        foreach (string c in assignmentofuser)
                        {
                            if (tal.AssignmentId == c)
                            {
                                finallist.Add(tal);
                            }
                        }
                    }
                    return View(finallist);
                }

            }

            return null;
            //return View(await _context.TeamAssignment.ToListAsync());
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