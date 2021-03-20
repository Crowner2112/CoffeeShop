namespace Models.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class News
    {
        public int NewsID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Discription { get; set; }
    }
}
