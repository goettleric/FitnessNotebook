using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;

namespace FitnessNotebook.Models
{
    public class Exercises
    {
        [Key]
        public int ExerciseID { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Name of Exercise")]
        [StringLength(100, MinimumLength = 2)]
        [Required]
        public string ExerciseName { get; set; }

        [Display(Name = "Repetitions Done")]
        [Range(1, 500)]
        [Required]
        public int Repetitions { get; set; }

        [Display(Name = "Sets Done")]
        [Range(1, 20)]
        [Required]
        public int Sets { get; set; }

        [Display(Name = "Type of Weight")]
        [StringLength(25, MinimumLength = 2)]
        [Required]
        public string WeightType { get; set; }

        [Display(Name = "Weight in pounds")]
        [Range(0, 1000)]
        [Required]
        public int Weight { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode= true)]
        [Display(Name = "Date exercise was done")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Duration in minutes")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid entry; Must be a number with a maximum of two decimal points.")]
        [Range(0, 999.99)]
        public decimal Duration { get; set; }

        [Display(Name = "Distance Done In Miles")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid entry; Must be a number in miles with a maximum of two decimal points.")]
        [Range(0, 999.99)]
        public decimal DistanceDone { get; set; }

        internal SqlDbType ToList()
        {
            throw new NotImplementedException();
        }

        internal static Expression<Func<object, object>> Equals(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}