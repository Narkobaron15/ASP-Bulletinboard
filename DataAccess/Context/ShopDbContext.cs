using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = DataAccess.Entities.File;

namespace DataAccess.Context;

public class ShopDbContext : IdentityDbContext<User>
{
    public ShopDbContext(DbContextOptions options)
        : base(options) { }

    public ShopDbContext() { }

    // -------------- data collections
    public DbSet<Bulletin> Bulletins { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Favorite> Favorites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Favorite>()
                    .HasKey(obj => new { obj.UserId, obj.BulletinId });

        // Hardcode for tests
        modelBuilder.Entity<Country>().HasData(
            new Country { Id = 1, Name = "Ukraine" }
        );

        modelBuilder.Entity<City>().HasData(
            new City { Id = 1, Name = "Lviv", CountryId = 1 },
            new City { Id = 2, Name = "Kyiv", CountryId = 1 },
            new City { Id = 3, Name = "Kharkiv", CountryId = 1 }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category() { Id = 1, Name = "Electronics" },
            new Category() { Id = 2, Name = "Sport" },
            new Category() { Id = 3, Name = "Fashion" },
            new Category() { Id = 4, Name = "Home & Garden" },
            new Category() { Id = 5, Name = "Transport" },
            new Category() { Id = 6, Name = "Toys & Hobbies" },
            new Category() { Id = 7, Name = "Musical Instruments" },
            new Category() { Id = 8, Name = "Art" },
            new Category() { Id = 9, Name = "Animals" }
        );

        modelBuilder.Entity<Bulletin>().HasData(
            new Bulletin() { Id = 1, Title = "iPhone X", CategoryId = 1, Price = 650, CityId = 1, Contacts = "Call me +123872982839", BulletinType = BulletinType.Product },
            new Bulletin() { Id = 2, Title = "PowerBall", CategoryId = 2, Price = 650, CityId = 1, Contacts = "Call me +385585989889", BulletinType = BulletinType.Product },
            new Bulletin() { Id = 3, Title = "Nike T-Shirt", CategoryId = 3, Price = 650, CityId = 3, Contacts = "Text me @shirt_seller", BulletinType = BulletinType.Product },
            new Bulletin() { Id = 4, Title = "Samsung Galaxy S23", CategoryId = 1, Price = 650, CityId = 2, Contacts = "Message me @phone_seller for more info", BulletinType = BulletinType.Product },
            new Bulletin() { Id = 5, Title = "A doggo looks for new owner", CategoryId = 9, CityId = 2, Contacts = "Paw shelter at Independency st, 123, Kyiv", BulletinType = BulletinType.Voluntary }
        );

        modelBuilder.Entity<File>().HasData(
            new File() { Id = 1, Url = "https://tvoygadget.com.ua/wp-content/uploads/2019/01/IMG_1213-2.jpg", Name = "IMG_1213-2.jpg", BulletinId = 1 },
            new File() { Id = 2, Url = "https://content2.rozetka.com.ua/goods/images/big/10633023.jpg", Name = "10633023.jpg", BulletinId = 2 },
            new File() { Id = 3, Url = "https://photos6.spartoo.com/photos/196/19628054/19628054_500_A.jpg", Name = "19628054_500_A.jpg", BulletinId = 3 },
            new File() { Id = 4, Url = "http://brands-online.com.ua/images/products/view/59190403.jpg", Name = "59190403.jpg", BulletinId = 3 },
            new File() { Id = 5, Url = "https://fair.ua/image/cache/catalog/photo_prod/11458682/0_sportswear-club-men-s-t-shirt-1200x1200.jpg", Name = "0_sportswear-club-men-s-t-shirt-1200x1200.jpg", BulletinId = 3 },
            new File() { Id = 6, Url = "https://images.samsung.com/ua/smartphones/galaxy-s23-ultra/images/galaxy-s23-ultra-highlights-colors-green-back-s.jpg", Name = "galaxy-s23-ultra-highlights-colors-green-back-s.jpg", BulletinId = 4 },
            new File() { Id = 7, Url = "https://www.petfinder.com/sites/default/files/images/content/shelter-dog-cropped.jpg", Name = "shelter-dog-cropped.jpg", BulletinId = 5 }
            );
    }
}
