namespace NationalLandmarks.Server.Data
{
    public class Validation
    {
        public class Landmark
        {
            public const int MaxDescriptionLength = 5000;
            public const int MaxNameLength = 150;
            public const int MaxAddressLength = 255;
            public const int MaxTownLength = 50;
        }
    }
}
