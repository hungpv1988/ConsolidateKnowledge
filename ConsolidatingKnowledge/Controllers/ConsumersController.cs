using ConsolidatingKnowledge.ModelBinder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConsolidatingKnowledge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumersController : ControllerBase
    {
        // GET: api/<ConsumersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // POST api/<ConsumersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ConsumersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ConsumersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Consumes("application/json")]
        [Route("PostId")]
        public async Task<IActionResult> PostJson(FileInfoModel model)
        {
            var result = Ok(new { Consumes = "application/json", Values = model.Id });
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        [Route("PostId")]
        public async Task<IActionResult> PostXForm(FileInfoModel model)
        {
            var result = Ok(new { Consumes = "application/x-www-form-urlencoded", Values = model.Id + 1 });
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Route("PostId")]
        public async Task<IActionResult> PostMedia([ModelBinder(typeof(MultiPartModelBinder), Name = "json")] FileInfoModel model)
        {
            var result = Ok(new { Consumes = "multipart/form-data", Values = model.Id + Request.Form.Files.Count });
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        [HttpPost]
        [Consumes("application/xml")]
        [Route("PostId")]
        // this endpoint would use FileInfoXmlInputFormatter
        public async Task<IActionResult> PostMediaWithXmlFormat(FileInfoModelList model)
        {
            var result = Ok(new { Consumes = "application/xml", Values = model.Capacity });
            return await Task.FromResult(result).ConfigureAwait(false);
        }


        [HttpGet]
        [Route("PostId")]
        // be careful, FileInfoOutputFormatter would format the result
        public async Task<FileInfoModel> Get(int id) 
        {
            var model = new FileInfoModel()
            {
                Id = id + 1,
                Name = $"My name is ${id + 1}",
                Description = $"Description for the file {id + 1}"
            };

            return await Task.FromResult(model).ConfigureAwait(false);
        }


        [HttpGet]
        [Route("PostId02")]
        // be careful, FileInfoOutputFormatter would format the result
        public async Task<IActionResult> GetJsonResult(int id)
        {
            var model = new FileInfoModel()
            {
                Id = id + 1,
                Name = $"My name is ${id + 1}",
                Description = $"Description for the file {id + 1}"
            };

            //var contentResult = new ContentResult()
            //{
            //    Content = model.Description
            //};

            return await Task.FromResult(new OkObjectResult(model)).ConfigureAwait(false);
        }
        // serializer
        // outputformatter
        // inputformatter
        // body model binder
    }

    // todo: validation properties on model binding by reflectio like petson
    public class FileInfoModel 
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MinLength(20)]
        public string Description { get; set; }
    }
    public class FileInfoModelList: List<FileInfoModel>
    {

    }
}
