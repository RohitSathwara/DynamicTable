using System.Diagnostics;
using DT.Data;
using DT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DT.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new TimeTableViewModel { Subjects = new List<SubjectViewModel>() });
        }

        [HttpPost]
        public IActionResult Generate(TimeTableViewModel model)
        {
            if (ModelState.IsValid)
            {
                int totalHours = model.WorkingDays * model.SubjectsPerDay;
                int enteredHours = model.Subjects.Sum(s => s.Hours);

                if (totalHours != enteredHours)
                {
                    ModelState.AddModelError("", "Total subject hours must equal total hours of the week.");
                    return View("Index", model);
                }

                var timeTable = new TimeTable
                {
                    WorkingDays = model.WorkingDays,
                    SubjectsPerDay = model.SubjectsPerDay,
                    Subjects = model.Subjects.Select(s => new Subject { Name = s.Name, TotalHours = s.Hours }).ToList()
                };

                _context.TimeTables.Add(timeTable);
                _context.SaveChanges();

                return RedirectToAction("Display", new { id = timeTable.Id });
            }
            return View("Index", model);
        }

        [HttpGet]
        [Route("display-table")]
        public IActionResult Display(int id)
        {
            var timeTable = _context.TimeTables
                .Include(t => t.Subjects)
                .FirstOrDefault(t => t.Id == id);

            if (timeTable == null)
            {
                return NotFound();
            }

            var viewModel = new TimeTableViewModel
            {
                WorkingDays = timeTable.WorkingDays,
                SubjectsPerDay = timeTable.SubjectsPerDay,
                Subjects = timeTable.Subjects.Select(s => new SubjectViewModel { Name = s.Name, Hours = s.TotalHours }).ToList()
            };

            // Distribute subjects dynamically
            viewModel.GeneratedTimeTable = GenerateTimeTableGrid(viewModel);

            return View(viewModel);
        }

        private List<List<string>> GenerateTimeTableGrid(TimeTableViewModel model)
        {
            List<string> subjectPool = new List<string>();

            foreach (var subject in model.Subjects)
            {
                subjectPool.AddRange(Enumerable.Repeat(subject.Name, subject.Hours));
            }

            Random rand = new Random();
            subjectPool = subjectPool.OrderBy(_ => rand.Next()).ToList();

            var timetable = new List<List<string>>();

            for (int i = 0; i < model.SubjectsPerDay; i++)
            {
                timetable.Add(new List<string>(new string[model.WorkingDays]));
            }

            int index = 0;
            for (int col = 0; col < model.WorkingDays; col++)
            {
                for (int row = 0; row < model.SubjectsPerDay; row++)
                {
                    timetable[row][col] = (index < subjectPool.Count) ? subjectPool[index++] : "-";
                }
            }

            return timetable;
        }


    }
}
