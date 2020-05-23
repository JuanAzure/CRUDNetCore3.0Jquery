using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JqueryAjaxAspMvcNetCore.Models
{
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }
        [MaxLength(12)]
        [Required(ErrorMessage ="This fiel is required")]
        [DisplayName("Account Number")]
        [Column(TypeName ="nvarchar(12)")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "This fiel is required")]
        [DisplayName("Beneficiary Name")]
        [Column(TypeName ="nvarchar(100)")]
        public string BeneficiaryName { get; set; }

        [Required(ErrorMessage = "This fiel is required")]
        [DisplayName("Bank Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string BankName { get; set; }

        [MaxLength(11)]
        [Required(ErrorMessage = "This fiel is required")]
        [DisplayName("SWIFT Code")]
        [Column(TypeName = "nvarchar(11)")]
        public string SWIFTCode { get; set; }

        [Required(ErrorMessage = "This fiel is required")]
        public int Amount { get; set; }
       // [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }



    }
}
