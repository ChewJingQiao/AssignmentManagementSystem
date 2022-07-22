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

        public async Task<IActionResult> Index()
        {
          return View(await _context.Assignment.Include(d => d.Module).ToListAsync());
        }
    }
}
