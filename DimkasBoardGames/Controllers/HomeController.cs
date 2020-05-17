// using System.Diagnostics;
// using DimkasBoardGames.Models;
// using Microsoft.AspNetCore.Mvc;
//
// namespace DimkasBoardGames.Controllers
// {
//     public class HomeController : Controller
//     {
//         public IActionResult Index()
//         {
//             return View();
//         }
//
//         public IActionResult About()
//         {
//             ViewData["Message"] = "Your application description page.";
//
//             return View();
//         }
//
//         public IActionResult Contact()
//         {
//             ViewData["Message"] = "Your contact page.";
//
//             return View();
//         }
//
//         public IActionResult Error()
//         {
//             return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//         }
//     }
// }