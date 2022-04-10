namespace NationalLandmarks.Server.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User: IdentityUser
    {
        public IEnumerable<Landmark> Landmarks { get; } = new HashSet<Landmark>();
    }
}
