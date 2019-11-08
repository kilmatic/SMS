using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SMS.Models;

namespace SMS.Controllers
{
    public class GradesController : ApiController
    {
        private SMSEntities db = new SMSEntities();

        // GET: api/Grades
        public IQueryable<Grade> GetGrades()
        {
            return db.Grades;
        }

        // GET: api/Grades/5
        [ResponseType(typeof(Grade))]
        public async Task<IHttpActionResult> GetGrade(string id)
        {
            Grade grade = await db.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            return Ok(grade);
        }

        // PUT: api/Grades/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGrade(string id, Grade grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grade.grade_id)
            {
                return BadRequest();
            }

            db.Entry(grade).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Grades
        [ResponseType(typeof(Grade))]
        public async Task<IHttpActionResult> PostGrade(Grade grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Grades.Add(grade);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GradeExists(grade.grade_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = grade.grade_id }, grade);
        }

        // DELETE: api/Grades/5
        [ResponseType(typeof(Grade))]
        public async Task<IHttpActionResult> DeleteGrade(string id)
        {
            Grade grade = await db.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            db.Grades.Remove(grade);
            await db.SaveChangesAsync();

            return Ok(grade);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GradeExists(string id)
        {
            return db.Grades.Count(e => e.grade_id == id) > 0;
        }
    }
}