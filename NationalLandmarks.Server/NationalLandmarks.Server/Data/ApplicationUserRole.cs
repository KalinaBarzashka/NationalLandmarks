using Microsoft.AspNetCore.Identity;
using NationalLandmarks.Server.Data.Models;

namespace NationalLandmarks.Server.Data
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
