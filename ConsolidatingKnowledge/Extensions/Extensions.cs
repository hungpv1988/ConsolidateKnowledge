using ConsolidatingKnowledge.CustomFormatter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.Extensions
{
    public static class Extensions
    {
        public static IMvcBuilder AddFileInfoModelOutputFormatter(this IMvcBuilder builder)
        {
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, FileInfoOutputFormatterSetup>());
            return builder;
        }
    }

    public class FileInfoOutputFormatterSetup : IConfigureOptions<MvcOptions>
    {
        void IConfigureOptions<MvcOptions>.Configure(MvcOptions options)
        {
            options.OutputFormatters.Add(new FileInfoOutputFormatter());
        }
    }
}
