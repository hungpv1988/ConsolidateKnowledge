using ConsolidatingKnowledge.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsolidatingKnowledge.CustomFormatter
{
    public class FileInfoXmlInputFormatter : InputFormatter
    {
        public FileInfoXmlInputFormatter() 
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/xml"));
        }

        protected override bool CanReadType(Type type)
        {
            return type.IsAssignableFrom(typeof(FileInfoModelList));
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            using (var streamReader = new StreamReader(context.HttpContext.Request.Body))
            {
                var xmlDoc = await XDocument.LoadAsync(streamReader, LoadOptions.None, CancellationToken.None);
                var fileInfoNodes = xmlDoc.Root.Descendants().Where(desc => desc.Name == "FileInfo");
               
                var fileInfoModelList = new FileInfoModelList();
                foreach (var item in fileInfoNodes) 
                {
                    var fileInfoModel = ExtractFileInfo(item);
                    fileInfoModelList.Add(fileInfoModel);
                }

                return InputFormatterResult.Success(fileInfoModelList);
            }
        }

        public FileInfoModel ExtractFileInfo(XElement element) 
        {
            var fileInfoModel = new FileInfoModel();
            var descendants = element.Descendants();
            fileInfoModel.Name = descendants.Where(desc => string.Equals(desc.Name.LocalName, "name", StringComparison.OrdinalIgnoreCase)).First().Value;
            fileInfoModel.Description = descendants.Where(desc => string.Equals(desc.Name.LocalName, "description", StringComparison.OrdinalIgnoreCase)).First().Value;
             
            return fileInfoModel;
        }
    }
}
