using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MoviesAPI.Authorization
{
    public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
    {
        // vai ser uma classe responsável por lidar com as autorizações e validar a idade 
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
        
        {
            var claims = context.User.Claims;
            foreach (var claim in claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }

            // context tem as informações dos usuários
            if (!context.User.Identity.IsAuthenticated)
            {
                return Task.CompletedTask; // Not authenticated
            }

            //recuperar info do token 
            var dateOfBirthClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);

            if (dateOfBirthClaim is null) 
                return Task.CompletedTask;

            var dateOfBirth = Convert.ToDateTime(dateOfBirthClaim.Value);
            var userAge = DateTime.Today.Year - dateOfBirth.Year;

            if (dateOfBirth > DateTime.Today.AddYears(-userAge))
                userAge--;

            if (userAge >= requirement.Idade) 
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
