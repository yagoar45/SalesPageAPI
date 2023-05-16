
using Microsoft.AspNetCore.Authorization;

namespace SalesPageAPI.Authorization
{
    // Class responsible for the access policy 
    public class MinimumAgeRequiredPolicy : IAuthorizationRequirement
    {

        public MinimumAgeRequiredPolicy(int age)
        {
            Age = age;
        }
        public int Age { get; set; }
    }
}
