using System.ComponentModel.DataAnnotations;

namespace TargetMasters.Models
{
    public class GuestName : BaseModel
    {
        [Required]
        [StringLength(20)]
        public string Word { get; set; }
        public WordType WordType { get; set; }
    }

    public enum WordType
    {
        Noun = 0,
        Adjective,
    }

}
