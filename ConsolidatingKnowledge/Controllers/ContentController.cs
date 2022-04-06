using ConsolidatingKnowledge.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.Controllers
{

    [ApiController]
    public class ContentController : Controller
    {
        /// <summary>
        /// allow to post a media using multipart form
        /// </summary>
        /// <returns></returns>
       // [Consumes("multipart/form-data")]
        [Route("Content")]
        public async Task<IActionResult> PostMedia(  ContentModel mediaModel) 
        {
            // request body contains inputed text and binary data
            var formFiles = Request.Form.Files;
            // Request.Body can be used if request body only contains binary data
            //var binaryDataStream = formFiles.FirstOrDefault().OpenReadStream();
            foreach (var file in formFiles) 
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok(new { count = formFiles.Count  });
        }

        /// <summary>
        /// allow to post a media using multipart form
        /// </summary>
        /// <returns></returns>
        [Route("Content2")]
        public async Task<IActionResult> PostContent(ContentModel contentModel)
        {
            var result = Ok(new { name = contentModel.Name });
            return await Task.FromResult<IActionResult>(result).ConfigureAwait(false); 
        }

        
        [HttpGet]
        [Route("Content/Index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
