using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace zpnet.Models
{
    [Table("przepisy")]
    public class przepis
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string  nazwa { get; set; }
        public string typ { get; set; }
        [Display(Name = "treść przepisu")]
        public string? przepisText { get; set; }
        [Display(Name = "autor")]
        public int? autorid { get; set; }
        [ForeignKey("autorid")]
        public autor? autor { get; set; }


        public ICollection<ksiazka>? ksiazki { get; set; }
    }

}
