using IB_Group.Models;
using IB_Group_Demo.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IB_Group.Controllers
{
    
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProjectController(IProjectRepository projectRepository, IWebHostEnvironment hostEnvironment)
        {
            _projectRepository = projectRepository;
            webHostEnvironment = hostEnvironment;
        }

        [Route("Projects")]
        public async Task<IActionResult> Index()
        {
            List<ProjectType> projectTypes = await _projectRepository.GetAllAsync();
            //foreach (var item in projectTypes)
            //{
            //    item.ImagePath = UploadedFile(item.ImagePath);
            //}
            return View(projectTypes);
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _projectRepository.GetByIdAsync(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectType project)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _projectRepository.CreateAsync(project);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. ");
            }
            return View(project);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            return View(await _projectRepository.GetByIdAsync(Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectType project)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dbProject = await _projectRepository.GetByIdAsync(id);
                    if (await TryUpdateModelAsync<ProjectType>(dbProject))
                    {
                        await _projectRepository.UpdateAsync(dbProject);
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. ");
            }
            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var dbProject = await _projectRepository.GetByIdAsync(id);
                if (dbProject != null)
                {
                    await _projectRepository.DeleteAsync(dbProject);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to delete. ");
            }

            return RedirectToAction(nameof(Index));
        }

        
        [Route("Project/OnPostMyUploader")]
        public IActionResult OnPostMyUploader(IFormFile MyUploader)
        {
            if (MyUploader != null)
            {
                string newFileName = Guid.NewGuid().ToString() + "_" + MyUploader.FileName;
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "mediaUpload");
                string filePath = Path.Combine(uploadsFolder, newFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    MyUploader.CopyTo(fileStream);
                }
                return new ObjectResult(new { status = "success", FilePath = newFileName, FileName = MyUploader.FileName });
            }
            return new ObjectResult(new { status = "fail" });

        }
    }
}
