using Microsoft.AspNetCore.Identity;
using TodoApi.Enums;

namespace TodoApi.Model
{
  public class ApplicationUser : IdentityUser
  {
    public UserRole Role { get; set; }
  }
}
