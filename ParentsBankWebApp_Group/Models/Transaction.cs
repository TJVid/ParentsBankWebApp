using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParentsBankWebApp_Group.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        #region Constructors
        public Transaction()
        {
            AccountId = 0;
            Account = null;
            transactionDate = DateTime.Now;
            amount = 0;
            note = string.Empty;
        }

        public Transaction(int _accountID, Account _account, DateTime _transactionDate, int _amount, string _note)
        {
            AccountId = _accountID;
            Account = _account;
            transactionDate = _transactionDate;
            amount = _amount;
            note = _note;
        }

        public Transaction(int _accountID, Account _account, int _amount, string _note)
        {
            AccountId = _accountID;
            Account = _account;
            transactionDate = DateTime.Now;
            amount = _amount;
            note = _note;
        }
        #endregion

        #region Properties​
        [Key]
        public int Id { get; set; }
        [Display(Name ="Account")]
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        //Navigation Property
        public virtual Account Account { get; set; }
        [Display(Name = "Transaction Date")]
        [vldDateNotFuture(ErrorMessage = "Transaction date cannot be future")]
        public DateTime transactionDate { get; set; }
        [Range(0.1, 9999999, ErrorMessage ="Value must be greater than or equal to 0.1")]           //Amount shouldn't be negative. 10 million is assumed to be the limit
        [Display(Name = "Amount")]
        public float amount { get; set; }
        [Required]
        [Display(Name = "Note")]
        public string note { get; set; }
        #endregion

    }
}