using Microsoft.EntityFrameworkCore;
using Resturant_Menu.Models;
namespace Resturant_Menu.Data
{
    /*
     DbContext is the primary class for interacting with a database using Entity Framework Core.
     DbContext acts as a bridge between your .NET Core application and the database.
     */
    public class MenuContext : DbContext
    {
        public MenuContext(DbContextOptions<MenuContext>options) : base(options)
        {
            /*
              DbContextOptions which contains configuration information needed by Entity Framework Core to connect to the database 
             */
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             * OnModelCreating  allows you to define relationships, keys, and other constraints for your entities in the database.
             */

            /*
             composite primary key for the DishIngredient entity using both DishId and IngredientId.
                This means that the combination of these two fields must be unique in the DishIngredient table.
             */
            modelBuilder.Entity<DishIngredient>().HasKey(di => new
            {
                di.DishId,
                di.IngredientId
            });

            //one to many relation between DishIngredient ==> Dish
            modelBuilder.Entity<DishIngredient>().HasOne(d => d.Dish).WithMany(di => di.DishIngredients).HasForeignKey(d => d.DishId);

            //one to many relation between DishIngredient ==> Ingredient
            modelBuilder.Entity<DishIngredient>().HasOne(i => i.Ingredient).WithMany(di => di.DishIngredients).HasForeignKey(i => i.IngredientId);

            modelBuilder.Entity<Dish>().HasData(
                new Dish { Id = 1, Name = "Margarita",Price=7.50,ImageURL= "https://ohsweetbasil.com/wp-content/uploads/how-to-make-authentic-margherita-pizza-at-home-recipe-4.jpg" }
                );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id=1,Name="Tomato"},
                new Ingredient { Id=2,Name="Mozzerlla"}
                );

            modelBuilder.Entity<DishIngredient>().HasData(
                new DishIngredient { DishId = 1,IngredientId=1},
                new DishIngredient { DishId = 1, IngredientId = 2}
                );
            base.OnModelCreating(modelBuilder); // This ensures that any additional configuration in the base class's OnModelCreating method is also applied.
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<DishIngredient> DishIngredients { get; set; }

    }
}
