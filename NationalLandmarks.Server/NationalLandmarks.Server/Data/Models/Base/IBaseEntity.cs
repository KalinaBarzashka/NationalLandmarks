namespace NationalLandmarks.Server.Data.Models.Base
{
    public interface IBaseEntity
    {
        DateTime CreatedOn { get; set; }

        string? CreatedByUsername { get; set; }

        DateTime? ModifiedOn { get; set; }

        string? ModifiedByUsername { get; set; }
    }
}
