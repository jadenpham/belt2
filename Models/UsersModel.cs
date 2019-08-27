using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace retake_two.Models
{
    public class UserReg
    {
        [Key]
        [Column("id")]
        public int UserId {get; set;}

        [Required]
        [Column("name")]
        [Display(Name="Name")]
        [MinLength(2,ErrorMessage="Name must be at least 2 characters.")]
        public string Name {get; set;}

        [Required]
        [Column("alias")]
        [MinLength(3,ErrorMessage="Alias must be at least 3 characters.")]
        // [RegularExpression("@^[a-zA-Z0-9]+$", ErrorMessage="Must be only letters and numbers")]
        public string Alias {get; set;}


        [Required]
        [Column("email")]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [DataType(DataType.Password)]
        [Column("pw")]
        [MinLength(8,ErrorMessage="Password must be at least 8 characters.")]
        public string Password {get; set;}

        [Column("created_at")]
        public DateTime CreatedAt{get; set;} = DateTime.Now;
        
        [Column("updated_at")]
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get; set;}

        public List<Idea> Ideas {get; set;}

        public List<Like> Likes {get; set;}

    }

     public class UserLog
    {
        [Required]
        public string Email {get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;}
    }

}
 