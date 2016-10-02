using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ParentsBankWebApp_Group.Models
{
    [Table("Accounts")]
    [Authorize]
    public class Account
    {

        #region Properties​
        [Key]
        public int Id { get; set; }
        [EmailAddress]
        [Display(Name = "Owner email ID")]
        public string ownerMailID { get; set; }
        [EmailAddress]
        [Display(Name = "Recipient  email ID")]
        public string recipientMailID { get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }
        //[Editable(false)]             
        //Open date should not be editable  
        [Display(Name = "Open Date")]
        public DateTime openDate { get; set; }
        [RegularExpression(@"^(?=.*[1-9])\d{0,2}(?:\.\d{0,2})?$", ErrorMessage = "Please enter a number between 0.01 and 99.99")]  //To allow numbers only from 0.01 to 99.99 reference: http://stackoverflow.com/questions/3727052/0-01-to-99-99-in-a-regular-expression
        [Display(Name = "Interest Rate")]
        public float interestRate { get; set; }
        [Display(Name = "Principal")]
        public float principal { get; set; }
        public virtual List<Transaction> transactions { get; set; }
        public virtual List<WishListItem> wishListItems { get; set; }
        #endregion

        #region Constructors
        public Account() { openDate = DateTime.Today; }


        public Account(string _ownerMailID, string _recipientMailID, string _name, DateTime _openDate, float _interestRate, float _principal)
        {
            ownerMailID = _ownerMailID;
            recipientMailID = _recipientMailID;
            name = _name;
            openDate = _openDate;
            interestRate = _interestRate;
            principal = _principal;
        }
        #endregion

        #region Methods
        public int getDaysSinceOpen()
        {
            return (openDate - DateTime.Now).Days;
        }

        public float calcInterest()
        {
            float interest = 0;
            //rough calculation of interest. Need better understanding of business logic
            interest = principal * interest * getDaysSinceOpen() / 365;
            return interest;
        }
        #endregion

    }
}