namespace Models.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Event")]
    public partial class Event
    {
        public int EventID { get; set; }

        [StringLength(50)]
        public string EventName { get; set; }

        [StringLength(10)]
        public string EventCode { get; set; }

        [Column(TypeName = "ntext")]
        public string Discription { get; set; }
    }
}
