using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.DatabaseModels
{
    [Table("movies")]
    public class Movie
    {
        [Column("Id")]
        public Guid Id { get; set; }               //GUID = identificator unic
        [Required]
        [Column("Title")]
        public string Title { get; set; }   //prop tab tab
        [Required]
        [Column("Summary")]
        public string Summary { get; set; }
        [Required]
        [Column("Genre")]
        public string Genre { get; set; }
        [Column("Review")]
        public string Review { get; set; }
        [Column("Year")]
        public int Year { get; set; }
        [Column("UserId")]
        public Guid UserId { get; set; }
        [Column("Path")]
        public string Path { get; set; }
    }
}