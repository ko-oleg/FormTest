using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FormTest.Models;
using Microsoft.EntityFrameworkCore;
using SocialApp.Services;

namespace FormTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Forms.ToListAsync());
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Form form)
        {
            db.Forms.Add(form);
            form.Id = db.Forms.Count()+1;
            await SendMessage(form);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Form form = await db.Forms.FirstOrDefaultAsync(p => p.Id == id);
                if (form != null)
                    return View(form);
            }
            return NotFound();
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if(id!=null)
            {
                Form form = await db.Forms.FirstOrDefaultAsync(p=>p.Id==id);
                if (form != null)
                    return View(form);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Form form)
        {
            db.Forms.Update(form);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> SendMessage(Form form)
        {
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(form.Email, "Создание формы", $"Ваша форма успешно создалась!<p>ФИО: {form.Fullname} <p>Телефон: {form.MobilePhone}<p>Email: {form.Email}<p>Название компании: {form.CompanyName}<p>Должность: {form.Post}");
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Form form = await db.Forms.FirstOrDefaultAsync(p => p.Id == id);
                if (form != null)
                    return View(form);
            }
            return NotFound();
        }
 
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Form form = await db.Forms.FirstOrDefaultAsync(p => p.Id == id);
                if (form != null)
                {
                    db.Forms.Remove(form);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}