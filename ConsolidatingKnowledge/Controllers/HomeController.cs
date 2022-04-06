using ConsolidatingKnowledge.ActionConstraints;
using ConsolidatingKnowledge.DI;
using ConsolidatingKnowledge.Filters;
using ConsolidatingKnowledge.ModelBinder;
using ConsolidatingKnowledge.Models;
using ConsolidatingKnowledge.Models.DTO;
using ConsolidatingKnowledge.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.Controllers
{
   // [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentService _studentService;
        private readonly AppConfiguration _appConfig;
        public HomeController(ILogger<HomeController> logger, IStudentService studentService, IOptions<AppConfiguration> appConfig)
        {
            _logger = logger;
            _studentService = studentService;
            _appConfig = appConfig.Value;
        }

        public IActionResult Index()
        {
            var id = HttpContext.Request.Query["id"];
            var student = _studentService.Get(string.IsNullOrWhiteSpace(id) ? Constants.NoStudentIdExist : int.Parse(id));
            var ditype = _appConfig.DIType;
            // this line is to test scoped DI
            //student = _studentService.Get(Constants.NoStudentIdExist);
            return View(student);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Home/Privacy/{name}")]
        public IActionResult Privacy(string name)
        {
            ViewData["name"] = name;
            return View();
        }

        //[HttpPost]
        //public IActionResult Privacy(string name)
        //{
        //    ViewBag["name"] = name;
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [IsMobile]
        public IActionResult Mobile() 
        {
            return Content("Welcome to mobile world that can be accessed by mobile device only, not desk browser");
        }

        [HttpGet]
        [Route("/Home/Students/{id}")]
        public IActionResult Get(int id)
        {
            var student = _studentService.Get(id);
            return Json(student);
        }

        //Bind data from route
        [HttpGet]
        [RouteValueFilter]
        [Route("/students/{id}/{apiversion}")]
        public IActionResult Student([FromHeader(Name ="x-token")] string token, string apiversion) 
        {
            return Json($"{token}{apiversion}");
        }

        [HttpPut]
        [Route("/students/{studentReference}/{version}")]
        public IActionResult Student([StudentBinder(typeof(StudentReferenceBinder))] StudentReference studentReference, string version, [Required][FromBody]  StudentDTO student)
        {
            return Json($"{studentReference.Id}");
        }

        public IActionResult ApiHome()
        {
            return View();
        }
    }
}
