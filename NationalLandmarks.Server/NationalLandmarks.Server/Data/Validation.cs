namespace NationalLandmarks.Server.Data
{
    public class Validation
    {
        public class Landmark
        {
            public const int MaxDescriptionLength = 5000;
            public const int MaxNameLength = 150;
            public const int MaxAddressLength = 255;
        }

        public class User
        {
            public const int MaxFirstNameLength = 40;
            public const int MaxLastNameLength = 40;
        }

        public class Area
        {
            public const int MaxNameLength = 50;
        }

        public class Town
        {
            public const int MaxNameLength = 50;
        }
    }
}
