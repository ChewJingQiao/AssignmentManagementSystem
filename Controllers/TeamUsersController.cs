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
    public class TeamUsersController : Controller
    {
        private readonly AssignmentManagementSystemContext _context;

        public TeamUsersController(AssignmentManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.TeamUser.Include(d => d.Team).Include(d => d.Users).ToListAsync());
        }
    }
}
