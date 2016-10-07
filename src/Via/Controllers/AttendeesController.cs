using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Via.Data;
using Via.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Via.Controllers
{
    public class AttendeesController : Controller
    {
        private readonly ViaDbContext _context;
        private IHostingEnvironment _environment;

        public AttendeesController(ViaDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Attendees
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["LastNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "lastName_desc" : "";
            ViewData["FirstNameSortParm"] = sortOrder == "firstName" ? "firstName_desc" : "firstName";
            ViewData["DobSortParm"] = sortOrder == "date" ? "date_desc" : "date";
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;


            var attendees = from a in _context.Attendees
                            select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                attendees = attendees.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "lastName_desc":
                    attendees = attendees.OrderByDescending(s => s.LastName);
                    break;
                case "firstName":
                    attendees = attendees.OrderBy(s => s.FirstName);
                    break;
                case "firstName_desc":
                    attendees = attendees.OrderByDescending(s => s.FirstName);
                    break;
                case "date":
                    attendees = attendees.OrderBy(s => s.Dob);
                    break;
                case "date_desc":
                    attendees = attendees.OrderByDescending(s => s.Dob);
                    break;
                default:
                    attendees = attendees.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 50;
            return View(await PaginatedList<Attendee>.CreateAsync(attendees.AsNoTracking(), page ?? 1, pageSize));
        }


        [HttpPost]
        public async Task<IActionResult> AddPictures(ICollection<IFormFile> files, int id)
        {
            
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    if (file.FileName.ToLower().EndsWith(".jpg"))
                    {
                        string fileName = id + "_" + file.FileName;
                        string existingFilePath = uploads + "\\"+ fileName;
                        if (System.IO.File.Exists(existingFilePath)){
                            fileName = id + "_" + Guid.NewGuid() + "_" + fileName;
                        }
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            Picture p = new Picture
                            {
                                Url = fileName,
                                AttendeeId = id
                            };
                            _context.Add(p);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            return RedirectToAction("Edit", new { id = id });
        }

        // POST: Attendees/DeletePicture/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePicture(int id, int pictureId)
        {
            var picture = await _context.Pictures.SingleOrDefaultAsync(p => p.PictureId == pictureId);
            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = id });
        }

        // GET: Attendees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = await _context.Attendees
                .Include(p => p.Pictures)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (attendee == null)
            {
                return NotFound();
            }
            attendee.Notes = attendee.Notes ?? "-";
            return View(attendee);
        }

        // GET: Attendees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attendees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Address,BaptismChurch,BaptismDate,BaptismPastor,BirthCity,Dob,Email,FirstName,Gender,IsMember,LastName,MarriageDate,MarriedWithId,Notes,ParentFatherId,ParentMotherId,Phone,Profession")]
            Attendee attendee)
        {
            if (ModelState.IsValid)
            {
                attendee.CreatedAt = DateTime.Now;
                attendee.Log = "-";
                attendee.UpdatedAt = DateTime.Now;
                _context.Add(attendee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(attendee);
        }

        // GET: Attendees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = await _context
                    .Attendees
                    .Include(p=>p.Pictures).AsNoTracking()
                    .SingleOrDefaultAsync(m => m.Id == id);
            if (attendee == null)
            {
                return NotFound();
            }
            return View(attendee);
        }

        // POST: Attendees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            var attendeeToUpdate = await _context.Attendees.SingleOrDefaultAsync(a => a.Id == id);
            if (attendeeToUpdate == null)
            {
                return NotFound();
            }
            attendeeToUpdate.Log += "/n" + DateTime.Now + ": " + "bla bla bla";
            attendeeToUpdate.UpdatedAt = DateTime.Now;

            if (await TryUpdateModelAsync<Attendee>(
                attendeeToUpdate,
                "",
                a => a.LastName, a => a.FirstName, a => a.Gender, a => a.Email, a => a.Phone, a => a.Address, a => a.IsMember,
                a => a.Dob, a => a.BirthCity, a => a.BaptismDate, a => a.BaptismPastor, a => a.BaptismChurch,
                a => a.MarriageDate, a => a.MarriedWithId, a => a.ParentFatherId, a => a.ParentMotherId,
                a => a.Notes, a => a.Profession,
                a => a.Log,
                a => a.UpdatedAt))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(attendeeToUpdate);

        }

        // GET: Attendees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = await _context.Attendees.SingleOrDefaultAsync(m => m.Id == id);
            if (attendee == null)
            {
                return NotFound();
            }

            return View(attendee);
        }

        //// POST: Attendees/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    //var attendee = await _context.Attendees.SingleOrDefaultAsync(m => m.Id == id);
        //    //_context.Attendees.Remove(attendee);
        //    //await _context.SaveChangesAsync();
        //    //return RedirectToAction("Index");
        //}

        private bool AttendeeExists(int id)
        {
            return _context.Attendees.Any(e => e.Id == id);
        }
    }
}
