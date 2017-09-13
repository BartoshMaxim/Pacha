using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pacha.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int ProductPrice { get; set; }

        public bool New { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Category Category { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}