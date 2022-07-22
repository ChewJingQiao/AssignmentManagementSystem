﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AssignmentManagementSystem.Models;
using AssignmentManagementSystem.Data;

namespace AssignmentManagementSystem.Controllers
{
    public class TeamAssignmentsController : Controller
    {
        private readonly AssignmentManagementSystemContext _context;

        public TeamAssignmentsController(AssignmentManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.TeamAssignment.ToListAsync());
        }
    }
}
