namespace NationalLandmarks.Server.Data.Models.Base
{
    public interface IDeletableEntity: IBaseEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }

        string? DeletedBy { get; set; }
    }
}
