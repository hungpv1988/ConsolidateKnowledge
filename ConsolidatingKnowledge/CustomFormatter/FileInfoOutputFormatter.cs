using ConsolidatingKnowledge.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.CustomFormatter
{
    public class FileInfoOutputFormatter : OutputFormatter
    {
        public FileInfoOutputFormatter()
        {
           
        }
        protected override bool CanWriteType(Type type)
        {
            return type.IsAssignableFrom(typeof(FileInfoModel));
        }

        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return context.Object is FileInfoModel ? true : false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var buffer = new StringBuilder();
            if (context.Object is FileInfoModel)
            {
                FormatFileInfoModel(buffer, context.Object as FileInfoModel);
            }


            await context.HttpContext.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(buffer.ToString()));
        }

        public void FormatFileInfoModel(StringBuilder buffer, FileInfoModel model) 
        {
            buffer.AppendLine("Begin to introduce file info");
            buffer.AppendLine($"File Id {model.Id}");
            buffer.AppendLine($"File Name {model.Name}");
            buffer.AppendLine($"File Desc {model.Description}");
        }
    }
}
