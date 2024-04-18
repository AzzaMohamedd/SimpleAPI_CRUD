using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITIAPIDay2.Models;
using ITIAPIDay2.DTO;

namespace ITIAPIDay2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ITIContext _context;

        public DepartmentsController(ITIContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            var depts = await _context.Departments.ToListAsync();

            if (depts == null) return NotFound();
            else
            {
                List<DepartmentDTO> deptsDTO = new List<DepartmentDTO>();
                foreach (var dept in depts)
                {
                    DepartmentDTO depDTO = new DepartmentDTO()
                    {
                        ID = dept.Dept_Id,
                        Name = dept.Dept_Name,
                        Description = dept.Dept_Desc,
                        Location = dept.Dept_Location,
                        Dept_Manager = dept.Dept_Manager,
                        NumOfStudents = dept.Students.Count()
                    };
                    deptsDTO.Add(depDTO);
                }
                return Ok(deptsDTO);
            }
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetDepartmentByID(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }
            else
            {
                DepartmentDTO depDTO = new DepartmentDTO()
                {
                    ID = department.Dept_Id,
                    Name = department.Dept_Name,
                    Description = department.Dept_Desc,
                    Location = department.Dept_Location,
                    Dept_Manager = department.Dept_Manager,
                    NumOfStudents = department.Students.Count()
                };
                return Ok(depDTO);
            }
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditDepartment(int id, Department department)
        {
            if (id != department.Dept_Id) return BadRequest();
            if (department == null) return NotFound();

            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> AddDepartment(Department department)
        {
            if (department == null) return BadRequest();
            else
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetDepartment", new { id = department.Dept_Id }, department);
            }
        }
        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return Ok(department);
        }
    }
}
