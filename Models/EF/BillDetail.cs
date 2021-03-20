namespace Models.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BillDetail")]
    public partial class BillDetail
    {
        [Key]
        public int BillDetailID { get; set; }
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillID { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        public byte? Quantity { get; set; }

        public int? Warranty { get; set; }

        public decimal? Total { get; set; }

        public virtual Bill Bill { get; set; }

        public virtual Product Product { get; set; }
    }
}
