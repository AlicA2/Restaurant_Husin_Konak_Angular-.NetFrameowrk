using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace HusinKonak.Data.Helpers.AutentifikacijaAutorizacija
{
    public class AutorizacijaSwaggerHeader : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "autentifikacija-token",
                In = ParameterLocation.Header,
                Description = "upisati token preuzet iz autentikacijacontrollera"
            });
        }

      
    }
}
