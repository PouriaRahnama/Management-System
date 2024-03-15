namespace Management_System.Models.Entities
{
    public abstract class BaseDateTime
    {
        [Column(TypeName = "datetime2(7)")]
        public virtual DateTime CreatedAt { get; set; }

        [Column(TypeName = "datetime2(7)")]
        public virtual DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "bit")]
        public virtual bool IsDeleted { get; set; } = false;

        [Column(TypeName = "datetime2(7)")]
        public virtual DateTime? DeletedAt { get; set; }
    }
}
