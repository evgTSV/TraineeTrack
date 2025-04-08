using System.ComponentModel.DataAnnotations;

namespace TraineeTrack.API.Models;

public class PasswordAttribute() : RegularExpressionAttribute(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");