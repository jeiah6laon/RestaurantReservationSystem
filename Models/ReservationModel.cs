using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservationSystem.Models
{
    public class ReservationModel
    {
        [Key]
        public int id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string name { get; set; }
        [Required]
        [DisplayName("Contact Number")]
        public string contactNo { get; set; }
        [Required]
        [DisplayName("Pax")]
        public int pax { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM-dd-yyyy}")]
        [DisplayName("Date of Reservation")]
        [DataType(DataType.Date)]
        public DateTime reservedDateTime { get; set; }

        [DisplayName("Time Slot")]
        public TimeEnum timeSlot { get; set; }

        
        public enum TimeEnum
        {
            [Display(Name ="9 AM - 10 AM")]
            NineToTen,
            [Display(Name = "10 AM - 11 AM")]
            TenToEleven,
            [Display(Name = "11 AM - 12 PM")]
            ElevenToTwelve,
            [Display(Name = "12 PM - 1 PM")]
            TwelveToOne,
            [Display(Name = "1 PM - 2 PM")]
            OneToTwo,
            [Display(Name = "2 PM - 3 PM")]
            TwoToThree,
            [Display(Name = "3 PM - 4 PM")]
            ThreeToFour,
            [Display(Name = "4 PM - 5 PM")]
            FourToFive,
            [Display(Name = "5 PM - 6 PM")]
            FiveToSix,
            [Display(Name = "6 PM - 7 PM")]
            SixToSeven,
            [Display(Name = "7 PM - 8 PM")]
            SevenToEight          
        }


        /*
                [Required]
                [DataType(DataType.Date)]
                [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM-dd-yyyy}")]
                [DisplayName("Date of Reservation")]
                public DateTime reservedDate { get; set; }

                [Required]
                [DataType(DataType.Time)]
                [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
                [DisplayName("from")]
                public DateTime startTime { get; set; }

                [Required]
                [DataType(DataType.Time)]
                [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
                [DisplayName("to")]
                public DateTime endTime { get; set; }
        */
    }
}
