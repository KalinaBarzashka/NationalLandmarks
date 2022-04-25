namespace NationalLandmarks.Server.Data.Models.Base
{
    public abstract class BaseEntity: IBaseEntity
    {
        public DateTime CreatedOn { get; set; }

        public string? CreatedByUsername { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedByUsername { get; set; }
    }
}
