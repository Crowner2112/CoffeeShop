namespace Models.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Shipper")]
    public partial class Shipper
    {
        public int ShipperID { get; set; }

        [Required]
        [StringLength(30)]
        public string ShipperName { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public int? BillID { get; set; }

        public bool? Status { get; set; }

        public virtual Bill Bill { get; set; }
    }
}
