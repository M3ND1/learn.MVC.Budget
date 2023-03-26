using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Budget.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Sum { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Provide proper date format dd/MM/yyyy HH:mm")]
        public DateTime DateTime { get; set; }
        //realtions
        public int CategoryId { get; set; } //FK
        public Category? Category { get; set; }
    }
}
