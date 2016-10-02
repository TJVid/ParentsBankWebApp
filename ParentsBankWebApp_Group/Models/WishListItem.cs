using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParentsBankWebApp_Group.Models
{
    [Table("WishListItems")]
    public class WishListItem
    {
        #region Constructors
        public WishListItem() { }

        public WishListItem(int _accountID, Account _account, DateTime _dateAdded, float _cost, string _description, string _link, bool _purchased)
        {
            AccountID = _accountID;
            Account = _account;
            cost = _cost;
            description = _description;
            link = _link;
            purchased = _purchased;
        }

        #endregion

        #region Properties​
        [Key]
        public int Id { get; set; }

        [ForeignKey("Account")]
        public int AccountID { get; set; }
        // Navigation property
        public virtual Account Account { get; set; }

        [Display(Name = "Date Added")]
        public DateTime dateAdded { get; set; }
        [Required]
        [Display(Name = "Cost")]
        public float cost { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Url]
        public string link { get; set; }
        public bool purchased { get; set; }
        #endregion

        //Unimplemented Business rules
        //A recipient cannot delete a wish list item
    }
}