using DT.Data;
using DT.Models;
using Microsoft.AspNetCore.Mvc;

namespace DT
{
    [Route("api/timetable")]
    [ApiController]
    public class TimeTableController : Controller
    {

        private readonly ApplicationDbContext _context;

        public TimeTableController(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
        //public IActionResult Display(int id)
        //{
        //    //var timeTable = _context.TimeTables.Include(t => t.Subjects).FirstOrDefault(t => t.Id == id);
        //    if (timeTable == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(timeTable);
        //}