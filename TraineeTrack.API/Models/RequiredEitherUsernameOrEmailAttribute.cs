using System.ComponentModel.DataAnnotations;
using TraineeTrack.API.Models.Requests;

namespace TraineeTrack.API.Models;

public class RequiredEitherUsernameOrEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance is SignInRequest request
            && string.IsNullOrWhiteSpace(request.Username)
            && string.IsNullOrWhiteSpace(request.Email))
            return new ValidationResult("Username or email is required");
        
        return ValidationResult.Success;
    }
}