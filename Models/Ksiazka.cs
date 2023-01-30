using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace zpnet.Models
{
    [Table("ksiazki")]
    public class ksiazka
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? id { get; set; }
        public string? nazwa { get; set; }
        public string? dataStworzenia { get; set; }
        [Display(Name = "autor")]
        public int? autorid { get; set; }
        [ForeignKey("autorid")]
        public autor? autor { get; set; }


        [NotMapped]
        public bool? zaznaczone { get; set; }
        public ICollection<przepis>? przepisy { get; set; }
    }
}
