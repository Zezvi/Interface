using System.Data.Entity;

namespace PetShop.Models
{
    public partial class PetShopContext : DbContext
    {
        public PetShopContext()
            : base("zoo")
        {}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Action>().ToTable("Action");
            modelBuilder.Entity<BonusCard>().ToTable("Bonus_Card");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Check>().ToTable("Check");
            modelBuilder.Entity<Good>().ToTable("Good");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<User>().ToTable("User");

        }
        public virtual DbSet<Action> Action { get; set; }
        public virtual DbSet<BonusCard> BonusCard { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Check> Check { get; set; }
        public virtual DbSet<Good> Good { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<User> User { get; set; }

    }
}
