namespace Budget.Models
{
    public class Category
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        //relations
        public ICollection<Transaction>? Transactions { get; set; }
    }
}
