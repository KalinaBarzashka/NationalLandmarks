namespace NationalLandmarks.Server.Data.Models
{
    public class Follow
    {
        public int Id { get; set; }

        public string UserId { get; set; } //which we are going to follow

        public User User { get; set; }

        public string FollowerId { get; set; }

        public User Follower { get; set; }
    }
}
