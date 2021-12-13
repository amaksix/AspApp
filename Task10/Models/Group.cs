using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task10.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int CourseId { get; set; }

        public Course Course { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
