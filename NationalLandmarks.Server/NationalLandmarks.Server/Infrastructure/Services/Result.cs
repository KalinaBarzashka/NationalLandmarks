namespace NationalLandmarks.Server.Infrastructure.Services
{
    public class Result
    {
        public bool Succeeded { get; private set; }

        public bool Failure => !this.Succeeded;

        public string Error { get; private set; }

        public static implicit operator Result(bool succeeded)
        {
            return new Result { Succeeded = succeeded };
        }

        public static implicit operator Result(string error)
        {
            return new Result 
            {
                Succeeded = false,
                Error = error 
            };
        }
    }
}
