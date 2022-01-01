using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResumeManager.Data;
using ResumeManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ResumeDbContext context;
        private readonly IWebHostEnvironment webHost;

        public ResumeController(ResumeDbContext context, IWebHostEnvironment webHost)
        {
            this.context = context;
            this.webHost = webHost;
        }

        public IActionResult Index()
        {
            List<Applicant> applicants = context.Applicants.ToList();

            return View(applicants);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Applicant applicant = new Applicant();
            applicant.Experiences.Add(new Experience() { ExperienceId = 1 });

            ViewBag.Gender = GetGenderList();
            //applicant.Experiences.Add(new Experience() { ExperienceId = 2 });
            //applicant.Experiences.Add(new Experience() { ExperienceId = 3 });

            return View(applicant);
        }

        [HttpPost]
        public IActionResult Create(Applicant applicant)
        {
            //foreach (Experience experience in applicant.Experiences)
            //{
            //    if (string.IsNullOrEmpty(experience.CompanyName) || experience.CompanyName.Length == 0)
            //        applicant.Experiences.Remove(experience);
            //}

            string uniqueFileName = GetUploadedFileName(applicant);
            applicant.PhotoUrl = uniqueFileName;

            context.Add(applicant);
            context.SaveChanges();

            return RedirectToAction("index");
        }


        private string GetUploadedFileName(Applicant applicant)
        {
            string uniqueFileName = null;
            if (applicant.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + applicant.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    applicant.ProfilePhoto.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public IActionResult Delete(int id)
        {
            Applicant applicant = context.Applicants
                .Include(e => e.Experiences)
                .Where(a => a.Id == id)
                .FirstOrDefault();

            return View(applicant);
        }

        [HttpPost]
        public IActionResult Delete(Applicant applicant)
        {
            context.Attach(applicant);
            context.Entry(applicant).State = EntityState.Deleted;
            context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Details(int id)
        {
            Applicant applicant = context.Applicants
                .Include(e => e.Experiences)
                .Where(a => a.Id == id)
                .FirstOrDefault();

            return View(applicant);
        }

        public IActionResult Edit(int id)
        {
            Applicant applicant = context.Applicants
                .Include(e => e.Experiences)
                .Where(a => a.Id == id)
                .FirstOrDefault();

            ViewBag.Gender = GetGenderList();

            return View(applicant);
        }


        [HttpPost]
        public IActionResult Edit(Applicant applicant)
        {
            List<Experience> details = context.Experiences.Where(d => d.ApplicantId == applicant.Id).ToList();
            context.Experiences.RemoveRange(details);
            context.SaveChanges();

            applicant.Experiences.RemoveAll(n => n.YearsWorked == 0);

            if (applicant.ProfilePhoto != null)
            {
                string uniqueFileName = GetUploadedFileName(applicant);
                applicant.PhotoUrl = uniqueFileName;
            }

            context.Attach(applicant);
            context.Entry(applicant).State = EntityState.Modified;
            context.Experiences.AddRange(applicant.Experiences);

            context.SaveChanges();

            return RedirectToAction("index");
        }

        private List<SelectListItem> GetGenderList()
        {
            List<SelectListItem> lstGender = new List<SelectListItem>();

            lstGender.Insert(0, new SelectListItem() { Value = "", Text = "Select Gender" });

            lstGender.Add(new SelectListItem() { Value = "Male", Text = "Male" });
            lstGender.Add(new SelectListItem() { Value = "Female", Text = "Female" });

            return lstGender;
        }


    }
}
