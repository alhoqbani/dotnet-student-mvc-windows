using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentMVC.Models;
using Microsoft.AspNetCore.Http;

namespace StudentMVC.Controllers
{

    // This will be a prefix to all custom routes in this controller.
    [Route("/students")]
    public class StudentsController : Controller
    {

        private readonly StudentsDataContext _db;


        public StudentsController(StudentsDataContext context)
        {
            _db = context;
        }

        // Custom routing. will only mathch '/students'
        [Route("")]
        public IActionResult Index()
        {
            //return new ContentResult
            //{
            //    Content = "Hello Students Index page"
            //};



            var students = _db.Students.ToArray();

            /*
             * Will look for view by the name of the action in
             * /Views/Students/Index.cshtml or
             * /Views/Shared/Index.cshtml
             */
            return View(students);
        }


        // Custom routing. will only mathch '/students/{id}'
        [Route("{id:int}")]
        public IActionResult Show(long id)
        {

            //if(id == null) 
            //{
            //    return new ContentResult
            //    {
            //        Content = "Id is not integer"
            //    };
            //}

            //return new ContentResult
            //{
            //    Content = id.ToString()
            //};


            //// Passing data to the view (option 1) Dynamic obbject ViewBag
            //ViewBag.Name = "Hamoud Alhoqbani";
            //ViewBag.City = "Jeddah";
            //ViewBag.Age = 32;
            //ViewBag.Enrolled = false;
            //ViewBag.Gender = "Male";

            // Passing data to the view (option 2) String typed object.
            var student = _db.Students.FirstOrDefault(x => x.Id == id);

            return View(student);
        }

        // To Show the create new student form.
        [HttpGet, Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        // To Show the create new student form.
        [HttpPost, Route("create")]
        public IActionResult Create(CreateStudent model)
        {
            Student EmailExists = _db.Students.FirstOrDefault(x => x.Email.ToLower().Trim() == model.Email.ToLower().Trim());
            if (EmailExists != null)
            {
                ModelState.AddModelError("Email", "البريد الإلكتروني مسجل مسبقًا");
            }

            // When the form validations failed, we return to the form.
            if (!ModelState.IsValid)
            {
                return View();
            }

            var student = new Student
            {
                Name = model.Name,
                City = model.City,
                Gender = model.Gender,
                Email = model.Email,
                Age = model.Age,
                Enrolled = model.Enrolled,
                CreatedAt = DateTime.Now,
            };

            _db.Students.Add(student);
            _db.SaveChanges();

            return RedirectToAction("Show", "Students", new { id = student.Id });
        }

        [HttpGet, Route("{id:int}/edit")]
        public IActionResult Edit(long id)
        {
            var student = _db.Students.Where((x) => x.Id == id).FirstOrDefault();

            if (student == null)
            {
                throw new Exception("Model was not found.");
            }

            return View(student);
        }



        [HttpPost, Route("{id:int}/edit")]
        public IActionResult Edit(CreateStudent model, long id)
        {
            // If not students found with givin id, redirect to index page
            var Student = _db.Students.Where((x) => x.Id == id).FirstOrDefault();
            if (Student == null)
            {
                RedirectToAction("Index", "Students");
            }

            // Prevent duplicate email.
            Student EmailExists = _db.Students.FirstOrDefault(x => x.Email.ToLower().Trim() == model.Email.ToLower().Trim());
            if (EmailExists != null && EmailExists.Email != Student.Email)
            {
                ModelState.AddModelError("Email", "البريد الإلكتروني مسجل مسبقًا");
            }

            // When the form validations failed, we return to the form.
            if (!ModelState.IsValid)
            {
                return View(Student);
            }

            Student.Name = model.Name;
            Student.Email = model.Email;
            Student.Age = model.Age;
            Student.Gender = model.Gender;
            Student.City = model.City;
            Student.Enrolled = model.Enrolled;
            _db.SaveChanges();

            return RedirectToAction("Show", "Students", new { id = id });
        }


        [HttpDelete, Route("{id:int}")]
        public IActionResult Delete(long id)
        {
            var student = _db.Students.FirstOrDefault(x => x.Id == id);

            if (student == null)
            {
                return BadRequest();
            }

            _db.Students.Remove(student);
            _db.SaveChanges();

            return Json(new { success = true });
        }



        // A type to handle create student reqesut.
        // We add only the fillable proerties.
        public class CreateStudent
        {
            [Required]
            [StringLength(100,
                          MinimumLength = 5,
                          ErrorMessage = "Name must be between 5 and 100 characters"
                         )]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string City { get; set; }

            [Required]
            public string Gender { get; set; }

            [Required]
            public int Age { get; set; }

            [Required]
            public bool Enrolled { get; set; }
        }
    }

}