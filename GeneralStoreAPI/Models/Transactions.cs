using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models
{
    public class Transactions
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey]
        public int CustomerId { get; set; }

        [ForeignKey]
        public string ProductSKU { get; set; }

        public int ItemCount { get; set; }
        public DateTime DateOfTransaction { get; set; }



    }
}