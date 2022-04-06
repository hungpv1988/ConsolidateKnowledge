using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.CustomConfigurationProvider
{
    public class EntityFrameworkConfigurationProvider: ConfigurationProvider
    {
        public EntityFrameworkConfigurationProvider(Action<DbContextOptionsBuilder> optionsAction) 
        {
            OptionsAction = optionsAction;
        }

        public Action<DbContextOptionsBuilder> OptionsAction { get; }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<DBContext>();
            OptionsAction(builder);

            using (var context = new DBContext(builder.Options)) 
            {
                Data = context.ConfigurationEntries.Any() ? context.ConfigurationEntries.ToDictionary(c => c.Key, c => c.Value)
                                                          : new Dictionary<string, string>();
            }
        }
    }
}
