using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ASPNETLab4.Control.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IConfiguration _configuration;

        public LibraryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("Library")]
        public IActionResult Greeting()
        {
            return Content("Welcome to library");
        }

        [HttpGet("Library/Books")]
        public IActionResult ListBooks()
        {
            // Зчитуємо список книг з файлу конфігурації
            List<string> books = _configuration.GetSection("Books").Get<List<string>>();
            if (books == null)
            {
                books = new List<string>();
            }

            // Формуємо список книг у текстовому форматі
            var plainText = "List of books:\n";
            foreach (var book in books)
            {
                plainText += book + "\n";
            }

            // Повертаємо текстовий вміст
            return Content(plainText, "text/plain");
        }
        [HttpGet("Library/Profile/{id?}")]
public IActionResult UserProfile(int? id)
{
    if (!id.HasValue || (id >= 0 && id <= 5))
    {
        var userId = id ?? 0;
        var userConfig = _configuration.GetSection("Profile").GetChildren().FirstOrDefault(u => u.Key == userId.ToString());
        
        if (userConfig != null)
        {
            string userName = userConfig["Name"];
            string userEmail = userConfig["Email"];
            return Content($"ID: {userId}, Name: {userName}, Email: {userEmail}");
        }
        else
        {
            return Content("User with this ID not found");
        }
    }
    else
    {
        return Content("Invalid user ID. Please enter an integer between 0 and 5.");
    }
}

    }
}