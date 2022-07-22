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
    public class TasksController : Controller
    {
        private readonly AssignmentManagementSystemContext _context;

        public TasksController(AssignmentManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Task.Include(d => d.Assignment).Include(d => d.Users).ToListAsync());
        }
    }
}
