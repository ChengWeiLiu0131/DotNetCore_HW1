using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using hw1.Models;

namespace hw1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ContosouniversityContext db;
        public PersonController(ContosouniversityContext db)
        {
            this.db = db;
        }

        // GET api/person
        [HttpGet("")]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            return db.Person.ToList();
        }

        // GET api/person/5
        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            return db.Person.Find(id);
        }

        // POST api/person
        [HttpPost("")]
        public void PostPerson(Person value)
        {
            value.DateModified = DateTime.Now;
            db.Person.Add(value);
            db.SaveChanges();
        }

        // PUT api/person/5
        [HttpPut("{id}")]
        public void PutPerson(int id, Person value)
        {
            var person = db.Person.Find(id);
            if (person != null) {
                person.DateModified = DateTime.Now;
                db.Person.Update(person);
                db.SaveChanges();
            }
        }

        // DELETE api/person/5
        [HttpDelete("{id}")]
        public void DeletePersonById(int id)
        {
            var person = db.Person.Find(id);
            if (person != null) {
                db.Person.Remove(person);
                db.SaveChanges();
            }
        }
    }
}