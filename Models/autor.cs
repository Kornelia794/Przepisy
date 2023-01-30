using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zpnet.Models
{

    [Table("autorzy")]
    public class autor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Data urodzenia")]
        public DateTime? Data_u { get; set; }



        public ICollection<przepis>? przepisy { get; set; }
        public ICollection<ksiazka>? ksiazki { get; set; }
           public string imieNazwisko
        {
            get
            {
                return imie + " " + nazwisko;
            }
        }
    }
}
