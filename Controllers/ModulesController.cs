using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AssignmentManagementSystem.Models;
using AssignmentManagementSystem.Data;
using AssignmentManagementSystem.Areas.Identity.Data;

namespace AssignmentManagementSystem.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ModulesController : Controller
    {
        private readonly AssignmentManagementSystemContext _context;

        public ModulesController(AssignmentManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string msg = "")
        {
            ViewBag.msg = msg;
            return View(await _context.Module.ToListAsync());
        }

        public IActionResult AddModule() //load the insert form
        {
            return View();
        }

        [HttpPost] //used to receive user input to database
        [ValidateAntiForgeryToken] ///avoid cross-site attack
        public async Task<IActionResult> AddModule([Bind("ModuleId", "ModuleName")] Module module)
        {
            //form is valid or not
            if (ModelState.IsValid)
            {
                _context.Module.Add(module);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { msg = "Module Created Successfully!" });
            }

            return View(module);
        }

        public async Task<IActionResult> AssignLecturer(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Module module = new Module();
            module = await _context.Module.FindAsync(id);
            if (module == null)
            {
                return NotFound();
            }
            
            IEnumerable<AssignmentManagementSystemUser> lecturers = await _context.User.ToListAsync();
            AssignLecturerModel model = new AssignLecturerModel();
            model.module = module;
            model.lecturers = lecturers;

            return View(model);
        }


    }
}
