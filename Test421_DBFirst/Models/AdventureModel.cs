namespace Test421_DBFirst.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AdventureModel : DbContext
    {
        public AdventureModel()
            : base("name=AdventureModel")
        {
        }

        public virtual DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .Property(e => e.PersonType)
                .IsFixedLength();
        }
    }
}
