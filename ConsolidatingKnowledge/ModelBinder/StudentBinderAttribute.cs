using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.ModelBinder
{
    public class StudentBinderAttribute : ModelBinderAttribute
    {
        public StudentBinderAttribute(Type type) 
        {
            BinderType = type;
        }
    }

    public class StudentReferenceBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value) || !int.TryParse(value, out var id))
            {
                throw new ArgumentException("wrong id format");
            }

            if (id < 0 || id > int.MaxValue)
            {
                throw new ArgumentException("id range is not valid");
            }

            StudentReference sr = new StudentReference()
            {
                Id = id
            };

            bindingContext.Result = ModelBindingResult.Success(sr);
            return Task.CompletedTask;
        }
    }

    public class StudentReference 
    {
        public int Id { get; set; }
        public Guid GuId { get; set; }
    }
}
