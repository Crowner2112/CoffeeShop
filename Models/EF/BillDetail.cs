namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BillDetail")]
    public partial class BillDetail
    {
        public int BillID { get; set; }

        public int ProductID { get; set; }

        public byte? Quantity { get; set; }

        public int? Warranty { get; set; }

        public decimal? Total { get; set; }

        public int BillDetailID { get; set; }

        public virtual Bill Bill { get; set; }

        public virtual Product Product { get; set; }
    }
}
