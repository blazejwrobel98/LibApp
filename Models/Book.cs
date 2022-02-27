using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255)]
        public string Name { get; set; }
        [Required(ErrorMessage = "AuthorName is required")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "Genre is required")]
        public Genre Genre { get; set; }
        [Required(ErrorMessage = "GenreId is required")]
        public byte GenreId { get; set; }
        [Required(ErrorMessage = "DateAdded is required")]
        public DateTime DateAdded { get; set; }
        [Required(ErrorMessage = "ReleaseDate is required")]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "NumberInStock in Stock")]
        [Range(1, 20, ErrorMessage = "NumberInStock must be between 1 and 20")]
        public int NumberInStock { get; set; }
        [Required(ErrorMessage = "NumberAvailable is required")]
        public int NumberAvailable { get; set; }
    }

}
