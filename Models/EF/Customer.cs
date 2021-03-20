namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerID { get; set; }

        [StringLength(50)]
        public string CustomerName { get; set; }

        public DateTime? DOB { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public DateTime? LastOnline { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
