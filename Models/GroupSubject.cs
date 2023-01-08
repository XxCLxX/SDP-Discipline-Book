using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_book.Models
{
    public class GroupSubject
    {
        [Key]
        [Column(Order = 1)]
        public int GroupId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int SubjectId { get; set; }

        public virtual Group? Group { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
