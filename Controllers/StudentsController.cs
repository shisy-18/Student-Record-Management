using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SRegister.Models;

namespace SRegister.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context) => _context = context;

        // READ: List all students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // CREATE: Show Form
        public IActionResult Register() => View();

        // CREATE: Process Form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Student student)
        {
            if (ModelState.IsValid)
            {
                ApplyStudentLogic(student);
                _context.Add(student);
                await LogAction("Create", student.Username, $"Registered {student.FullName}");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // UPDATE: Show Form
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);
            return student == null ? NotFound() : View(student);
        }

        // UPDATE: Process Form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.StudentId) return NotFound();

            if (ModelState.IsValid)
            {
                ApplyStudentLogic(student);
                _context.Update(student);
                await LogAction("Update", student.Username, $"Updated profile for {student.FullName}");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // DELETE: Process
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await LogAction("Delete", "Admin", $"Removed student: {student.FullName}");
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Helper Logic
        private void ApplyStudentLogic(Student s)
        {
            s.UpperName = s.FullName.ToUpper();
            s.LowerEmail = s.Email.ToLower();
            s.UsernameLength = s.Username.Length;
        }

        private async Task LogAction(string action, string user, string details)
        {
            _context.AuditLogs.Add(new AuditLog { 
                Action = action, PerformedBy = user, Details = details 
            });
        }
    }
}