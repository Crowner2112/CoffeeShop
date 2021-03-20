namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Bill")]
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            BillDetails = new HashSet<BillDetail>();
            Shippers = new HashSet<Shipper>();
        }

        public int BillID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int EmployeeID { get; set; }

        public int ShopID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Shop Shop { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillDetail> BillDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shipper> Shippers { get; set; }
    }
}
