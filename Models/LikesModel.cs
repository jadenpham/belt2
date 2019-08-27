using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace retake_two.Models
{
    public class Like 
    {
        [Key]
        [Column("like_id")]
        public int LikeId {get; set;}

        [Column("user_id")]
        public int UserId{get; set;}

        [Column("idea_id")]
        public int IdeaId {get; set;}

        public UserReg Liker {get; set;}

        public Idea Ideas {get; set;}
    }
}