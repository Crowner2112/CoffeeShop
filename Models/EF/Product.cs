namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BillDetails = new HashSet<BillDetail>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        public int CategoryID { get; set; }

        public decimal? Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        public int? Warranty { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Status { get; set; }

        public int? ViewCount { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Discription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillDetail> BillDetails { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
