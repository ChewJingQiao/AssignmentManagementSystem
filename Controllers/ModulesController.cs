using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class ModulesController : Controller
    {
        private readonly AssignmentManagementSystemContext _context;

        public ModulesController(AssignmentManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Module.ToListAsync());
        }
    }
}
