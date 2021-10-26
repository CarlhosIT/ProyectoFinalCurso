using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Helper
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombrePropiedad = bindingContext.ModelName;
            var proveerdorValores = bindingContext.ValueProvider.GetValue(nombrePropiedad);

            if (proveerdorValores == ValueProviderResult.None) 
            {
                return Task.CompletedTask;
            }

            try 
            {
                var valorDeserializado = JsonConvert.DeserializeObject<T>(proveerdorValores.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(valorDeserializado);
            }
            catch 
            {
                bindingContext.ModelState.TryAddModelError(nombrePropiedad,"Fallo al deserializar la lista de enteros ");
            }

            return Task.CompletedTask;
        }
    }
}
