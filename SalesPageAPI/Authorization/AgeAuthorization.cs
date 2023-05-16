using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SalesPageAPI.Authorization
{

    // Validate the age of user for he have access to the content
    public class AgeAuthorization : AuthorizationHandler<MinimumAgeRequiredPolicy>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequiredPolicy requirement)
        {
            var birthDay = context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);


            if (birthDay == null) 
            {
                return Task.CompletedTask;
            }

            DateTime dateOfBirth = Convert.ToDateTime(birthDay.Value);  

            int userAge = DateTime.Today.Year - dateOfBirth.Year; 


            if(dateOfBirth > DateTime.Now.AddYears(-userAge))
            {
                userAge--;
            }

            if (userAge >= requirement.Age)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
