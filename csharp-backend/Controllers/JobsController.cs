using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using JobTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace csharp_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private JobTrackerContext _context;

        public JobsController(JobTrackerContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            if(_context.Jobs.ToList().Count == 0 )
            { 
                return NotFound();
            }
            return Ok(_context.Jobs
            // .Include(j => j.contact_id)
            .Include("Contact") //Table Name?
            .Include("Site")
                                //    .Include(j => j.site_id)
                                   .ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Job job = _context.Jobs.SingleOrDefault(j => j.job_id == id);
            if(_context == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Job job)
        {
            if(job == null)
            {
                return BadRequest();
            }
            _context.Jobs.Add(job);
            _context.SaveChanges();
            return Ok(_context.Jobs.Where(j => j.job_id == job.job_id).SingleOrDefault());
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Job job)
        {
            if(job == null || job.job_id != id)
            {
                return BadRequest();
            }
            _context.Jobs.Update(job);
            _context.SaveChanges();
            return Ok(job);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Job job = _context.Jobs.Find(id);
            if(job == null)
            {
                return NotFound();
            }
            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return Ok(job);
        }
    }
}