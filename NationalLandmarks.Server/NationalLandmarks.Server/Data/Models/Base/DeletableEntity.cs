namespace NationalLandmarks.Server.Data.Models.Base
{
    public abstract class DeletableEntity: BaseEntity, IDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string? DeletedBy { get; set; }
    }
}
