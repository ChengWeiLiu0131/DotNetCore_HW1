using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hw1.Models;
using Microsoft.AspNetCore.Mvc;
//using hw1.Models;

namespace hw1.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class CourseInstructorController : ControllerBase {
        private readonly ContosouniversityContext db;
        public CourseInstructorController (ContosouniversityContext db) {
            this.db = db;
        }

        // GET api/courseinstructor
        [HttpGet ("")]
        public ActionResult<IEnumerable<CourseInstructor>> GetCourseInstructors () {
            return db.CourseInstructor.ToList();
        }

        // GET api/courseinstructor/5
        [HttpGet ("{id}")]
        public ActionResult<CourseInstructor> GetCourseInstructorById (int id) {
            return db.CourseInstructor.Find(id);
        }

        // POST api/courseinstructor
        [HttpPost ("")]
        public void PostCourseInstructor (CourseInstructor value) {
            db.CourseInstructor.Add(value);
        }

        // PUT api/courseinstructor/5
        [HttpPut ("{id}")]
        public void PutCourseInstructor (int id, CourseInstructor value) {
            var courseInstructor = db.CourseInstructor.Find(id);
            if (courseInstructor != null) {
                db.CourseInstructor.Update(courseInstructor);
                db.SaveChanges();
            }
        }

        // DELETE api/courseinstructor/5
        [HttpDelete ("{id}")]
        public void DeleteCourseInstructorById (int id) {
            var courseInstructor = db.CourseInstructor.Find(id);
            if (courseInstructor != null) {
                db.CourseInstructor.Remove(courseInstructor);
                db.SaveChanges();
            }
        }
    }
}