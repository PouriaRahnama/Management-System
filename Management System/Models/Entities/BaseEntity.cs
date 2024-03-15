namespace Management_System.Models.Entities
{
    public abstract class BaseEntity : BaseDateTime
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public virtual Guid Id { get; set; }


    }
}