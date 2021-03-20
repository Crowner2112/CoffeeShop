namespace Models.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        public int OrderID { get; set; }
        [Key]
        public int OrderDetailID { get; set; }
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [StringLength(10)]
        public string EventCode { get; set; }
        public decimal? Price { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
