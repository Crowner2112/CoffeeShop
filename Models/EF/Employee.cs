namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Bills = new HashSet<Bill>();
        }

        public int EmployeeID { get; set; }

        [Required]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        public DateTime? DOB { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public byte? Role { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public byte? Status { get; set; }

        public int ShopID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }
    }
}
