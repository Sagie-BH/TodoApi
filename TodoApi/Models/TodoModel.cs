using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class TodoModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Text { get; set; }
        [Required]
        [Column("completed")]
        public bool isComplete { get; set; }
    }
}
