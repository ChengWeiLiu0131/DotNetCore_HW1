using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hw1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using hw1.Models;

namespace hw1.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase {
        private readonly ContosouniversityContext db;
        public CourseController (ContosouniversityContext db) {
            this.db = db;
        }

        // GET api/course
        [HttpGet ("")]
        public ActionResult<IEnumerable<Course>> GetCourses () {
            return db.Course.ToList();
        }

        // GET api/course/5
        [HttpGet ("{id}")]
        public ActionResult<Course> GetCourseById (int id) {
            return db.Course.Find(id);
        }

        // POST api/course
        [HttpPost ("")]
        public void PostCourse (Course value) {
            value.DateModified = DateTime.Now;
            db.Course.Add(value);
            db.SaveChanges();
        }

        // PUT api/course/5
        [HttpPut ("{id}")]
        public void PutCourse (int id, Course value) {
            var course = db.Course.Find(id);
            if (course != null) {
                course.DateModified = DateTime.Now;
                db.Course.Update(course);
                db.SaveChanges();
            }
        }

        // DELETE api/course/5
        [HttpDelete ("{id}")]
        public void DeleteCourseById (int id) {
            var course = db.Course.Find(id);
            if (course != null) {
                // course.DateModified = DateTime.Now;
                db.Course.Remove(course);
                db.SaveChanges();
            }
        }

        [HttpGet("GetCourseStudentCount")]
        public ActionResult<IEnumerable<VwCourseStudentCount>> GetCourseStudentCount()
        {
            return db.VwCourseStudentCount.ToList();
        }

        [HttpGet("GetCourseStudents")]
        public ActionResult<IEnumerable<VwCourseStudents>> GetCourseStudents()
        {
            return db.VwCourseStudents.ToList();
        }
    }
}