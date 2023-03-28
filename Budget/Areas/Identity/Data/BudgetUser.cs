using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.Models;
using Microsoft.AspNetCore.Identity;

namespace Budget.Areas.Identity.Data;

// Add profile data for application users by adding properties to the BudgetUser class
public class BudgetUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal? BudgetAmount { get; set; }

    //relations
    public virtual ICollection<Transaction>? Transactions { get; set; }
}

