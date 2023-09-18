
using Microsoft.AspNetCore.Mvc;
using Core_Practical_17.Models;
using Microsoft.AspNetCore.Authorization;
using Core_Practical_17.Repository;

namespace Core_Practical_17.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _dbaccess;

        public StudentsController(IStudentRepository dbaccess)
        {
            _dbaccess = dbaccess;
        }

       
        public async Task<IActionResult> Index()
        {
              return View(await _dbaccess.GetStudents());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _dbaccess.FindStudentById(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [Authorize(Roles= "Admin")]
        public IActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,DOB,Age")] Student student)
        {
            if (ModelState.IsValid)
            {
                await _dbaccess.AddStudent(student);  
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _dbaccess.FindStudentById(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Student student)
        {
            if (ModelState.IsValid)
            {
                int result = await _dbaccess.UpdateStudent(id, student);
                if(result != 0) 
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            return View(student);
        }

        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _dbaccess.FindStudentById(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int result = await _dbaccess.DeleteStudentById(id);
            if (result != 0)
            {
                return RedirectToAction(nameof(Index));
            }
            
            return View("Error");
            
        }

    }
}
