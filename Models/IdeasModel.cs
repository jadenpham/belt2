using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace retake_two.Models
{
    public class Idea
    {
        [Key]
        [Column("idea_id")]
        public int IdeaId {get; set;}

        [Required]
        [Column("desciption")]
        [MinLength(5, ErrorMessage = "Please input at least 5 characters")]
        public string Description {get; set;}

        [Column("created_at")]
        public DateTime CreatedAt {get; set;} = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        [Column("user_id")]
        public int UserId{get; set;}

        public UserReg Creator {get; set;}

        public List<Like> Likes {get; set;}
    }
}