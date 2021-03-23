namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ingredient
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        public int? Quantity { get; set; }

        [StringLength(20)]
        public string Unit { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
