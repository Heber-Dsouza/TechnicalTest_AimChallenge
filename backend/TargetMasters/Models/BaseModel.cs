using System.ComponentModel.DataAnnotations;

namespace TargetMasters.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.Now;
            this.Deleted = false;
        }


        [Key]
        [StringLength(36)]
        public required string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt {  get; set; }
        public bool Deleted { get; set; }

    }
}
