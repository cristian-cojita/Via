using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Via.Models
{
    public class Attendee
    {
        public int Id { get; set; }
        public int? ParentFatherId { get; set; }
        public int? ParentMotherId { get; set; }
        public int? MarriedWithId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Display(Name = "Adresa")]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data nasterii")]
        public DateTime Dob { get; set; }
        [Display(Name = "Localitatea nasterii")]
        public string BirthCity { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data botezului")]
        public DateTime? BaptismDate { get; set; }
        [Display(Name = "Biserica botezului")]
        public string BaptismChurch { get; set; }
        [Display(Name = "Botezat de")]
        public string BaptismPastor { get; set; }
        [Display(Name = "gen")]
        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data casatoriei")]
        public DateTime? MarriageDate { get; set; }
        [Display(Name = "Profesia")]
        public string Profession { get; set; }
        [Display(Name = "Note")]
        public string Notes { get; set; }
        public bool IsMember { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedAt { get; set; }
        public string Log { get; set; }

        public ICollection<Picture> Pictures { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }
    }
}
