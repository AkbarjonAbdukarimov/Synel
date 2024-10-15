using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Synel.Data;
using Synel.Models;
using Synel.Services;
using Synel.Types;

namespace Synel.Controllers
{
    public class EmployeesController : Controller
    {

        private readonly IEmployeeService employeeService;
        public EmployeesController(IEmployeeService service)
        {
            employeeService = service;
        }
        [HttpGet]
        // GET: Employees
        public async Task<IActionResult> Index(int? rowsAffected = null, string? searchString = null, SortOrder? sortOrder = null, SortFields? sortField = null)
        {
            // Retrieve all employees
            List<Employees> emps = await employeeService.GetEmployeesAsync( searchString, sortOrder, sortField);


            // Pass the filtered and sorted employees to the view
            if (rowsAffected != null)
            {

                ViewBag.RowsAffected = rowsAffected;
            }
            //if (emps == null)
            //{
            //    emps = new List<Employees>();  // Handle null by setting an empty list
            //}
            return View(emps);
        }

        // GET: Employees/Details/5
        //public async Task<IActionResult> Details(int id)
        //{
        //  var employee=await employeeService.GetEmployeeById(id);
        //    return View(employees);
        //}

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file)
        {
            int rows = await employeeService.InsertFileDataAsync(file);
            return RedirectToAction("Index", new { rowsAffected = rows });

        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await employeeService.GetEmployeeByIdAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PayrolNumber,Fornames,Surname,DoB,Telephone,Mobile,Address,Address2,Postcode,EmailHome,StartDate")] Employees updatedEmployee)
        {

            if (id != updatedEmployee.Id)
            {
                return BadRequest();
            }

            // Update the employee
            await employeeService.UpdateEmployeeAsync(updatedEmployee);



            return RedirectToAction("Index");
        }


    }
}
