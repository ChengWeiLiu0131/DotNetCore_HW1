using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hw1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace hw1.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase {
        private readonly ContosouniversityContext db;
        public DepartmentController (ContosouniversityContext db) {
            this.db = db;
        }

        // GET api/department
        [HttpGet ("")]
        public ActionResult<IEnumerable<Department>> GetDepartments () {
            return db.Department.ToList();
        }

        // GET api/department/5
        [HttpGet ("{id}")]
        public ActionResult<Department> GetDepartmentById (int id) {
            return db.Department.Find(id);
        }

        // POST api/department
        [HttpPost ("")]
        public void PostDepartment (Department value) {
            value.DateModified = DateTime.Now;
            SqlParameter name = new SqlParameter("@Name", value.Name);
            SqlParameter budget = new SqlParameter("@Budget", value.Budget);
            SqlParameter startDate = new SqlParameter("@StartDate", value.StartDate);
            SqlParameter instructorID = new SqlParameter("@InstructorID", value.InstructorId);
            value.DepartmentId = db.Department.FromSqlRaw("execute Department_Insert @Name,@Budget,@StartDate,@InstructorID",
                name, budget, startDate, instructorID).Select(x => x.DepartmentId).ToList().First();
        }

        // PUT api/department/5
        [HttpPut ("{id}")]
        public void PutDepartment (int id, Department value) {
            var department = db.Department.Find (id);
            if (department != null) {
                SqlParameter departmentID = new SqlParameter("@DepartmentID", department.DepartmentId);
                SqlParameter name = new SqlParameter("@Name", department.Name);
                SqlParameter budget = new SqlParameter("@Budget", department.Budget);
                SqlParameter startDate = new SqlParameter("@StartDate", department.StartDate);
                SqlParameter instructorID = new SqlParameter("@InstructorID", department.InstructorId);
                SqlParameter rowVersion = new SqlParameter("@RowVersion_Original", department.RowVersion);
                db.Database.ExecuteSqlRaw("execute Department_Update @DepartmentID,@Name,@Budget,@StartDate,@InstructorID,@RowVersion_Original",
                    departmentID, name, budget, startDate, instructorID, rowVersion);
            }
            // var department = db.Department.Find (id);
            // if (department != null) {
            //     department.DateModified = DateTime.Now;
            //     db.Department.Update(department);
            //     db.SaveChanges();
            // }   
        }

        // DELETE api/department/5
        [HttpDelete ("{id}")]
        public void DeleteDepartmentById (int id) {
            var department = db.Department.Find (id);
            if (department != null) {
                SqlParameter departmentID = new SqlParameter("@DepartmentID", department.DepartmentId);
                SqlParameter rowVersion = new SqlParameter("@RowVersion_Original", department.RowVersion);
                db.Database.ExecuteSqlRaw("execute Department_Delete @DepartmentID,@RowVersion_Original", departmentID, rowVersion);
            }
            //var department = db.Department.Find (id);
            // if (department != null) {
            //     db.Department.Remove(department);
            //     db.SaveChanges();
            // }
        }

        [HttpGet("GetDepartmentCourseCount")]
        public ActionResult<IEnumerable<VwDepartmentCourseCount>> GetDepartmentCourseCount()
        {
            return db.VwDepartmentCourseCount.FromSqlRaw("SELECT * FROM [dbo].[vwDepartmentCourseCount]").ToList();
        }
    }
}