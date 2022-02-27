using System;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Customer>>();

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.MembershipTypes.Any())
                    SeedMembershipTypes(context);

                if (!context.Roles.Any())
                    SeedRoles(context);

                if (!context.Customers.Any())
                     SeedCustomers(userManager);

                if (!context.Genre.Any())
                    SeedGenres(context);

                if (!context.Books.Any())
                    SeedBooks(context);

                context.SaveChanges();
            }
        }

        private static void SeedBooks(ApplicationDbContext context)
        {
            context.Books.AddRange(
                // Generate 10 books with random data for testing purposes 
                new Book
                {
                    GenreId = 1,
                    Name = "The Hunger Games",
                    AuthorName = "Suzanne Collins",
                    ReleaseDate = DateTime.Parse("15/01/2012"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 2
                },
                new Book
                {
                    GenreId = 2,
                    Name = "The Martian",
                    AuthorName = "Andy Weir",
                    ReleaseDate = DateTime.Parse("10/02/2012"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 5
                },
                new Book
                {
                    GenreId = 3,
                    Name = "The Hobbit",
                    AuthorName = "J.R.R. Tolkien",
                    ReleaseDate = DateTime.Parse("10/04/2012"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 8
                },
                new Book
                {
                    GenreId = 4,
                    Name = "The Fault in our Stars",
                    AuthorName = "John Green",
                    ReleaseDate = DateTime.Parse("21/01/2014"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 3
                },
                new Book
                {
                    GenreId = 5,
                    Name = "The Book Thief",
                    AuthorName = "Markus Zusak",
                    ReleaseDate = DateTime.Parse("15/01/2002"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 12
                },
                new Book
                {
                    GenreId = 6,
                    Name = "The Chronicles of Narnia",
                    AuthorName = "C.S. Lewis",
                    ReleaseDate = DateTime.Parse("12/01/2011"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 16
                },
                new Book
                {
                    GenreId = 7,
                    Name = "The Lion, the Witch and the Wardrobe",
                    AuthorName = "C.S. Lewis",
                    ReleaseDate = DateTime.Parse("10/01/2014"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 12
                },
                new Book
                {
                    GenreId = 8,
                    Name = "The Chronicles of Narnia: Prince Caspian",
                    AuthorName = "C.S. Lewis",
                    ReleaseDate = DateTime.Parse("10/05/2012"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 5
                }

            );
        }

        private static void SeedGenres(ApplicationDbContext context)
        {
            context.Genre.AddRange(
                new Genre
                {
                    Id = 1,
                    Name = "Romance"
                },
                new Genre
                {
                    Id = 2,
                    Name = "Fantasy"
                },
                new Genre
                {
                    Id = 3,
                    Name = "Science Fiction"
                },
                new Genre
                {
                    Id = 4,
                    Name = "Mystery"
                },
                new Genre
                {
                    Id = 5,
                    Name = "Historical Fiction"
                },
                new Genre
                {
                    Id = 6,
                    Name = "Horror"
                },
                new Genre
                {
                    Id = 7,
                    Name = "Thriller"
                },
                new Genre
                {
                    Id = 8,
                    Name = "Adventure"
                }
            );
        }

        private static void SeedCustomers(UserManager<Customer> userManager)
        {
            var hasher = new PasswordHasher<Customer>();

            var customer1 = new Customer
            {
                Name = "Kate Brown",
                Email = "kate.brown@gmail.com",
                NormalizedEmail = "kate.brown@gmail.com",
                UserName = "kate.brown@gmail.com",
                NormalizedUserName = "kate.brown@gmail.com",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test")
            };

  
            userManager.CreateAsync(customer1).Wait();
            userManager.AddToRoleAsync(customer1, "user").Wait();

            var customer2 = new Customer
            {
                Name = "John Smith",
                Email = "john.smith@gmail.com",
                NormalizedEmail = "john.smith@gmail.com",
                UserName = "john.smith@gmail.com",
                NormalizedUserName = "john.smith@gmail.com",
                MembershipTypeId = 2,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test")
            };

            userManager.CreateAsync(customer2).Wait();
            userManager.AddToRoleAsync(customer2, "user").Wait();

            var customer3 = new Customer
            {
                Name = "Jane Doe",
                Email = "jane.doe@gmail.com",
                NormalizedEmail = "jane.doe@gmail.com",
                UserName = "jane.doe@gmail.com",
                NormalizedUserName = "jane.doe@gmail.com",
                MembershipTypeId = 3,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test")
            };

            userManager.CreateAsync(customer3).Wait();
            userManager.AddToRoleAsync(customer3, "user").Wait();

        }

        private static void SeedRoles(ApplicationDbContext context)
        {
            context.Roles.AddRange(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "user"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "StoreManager",
                    NormalizedName = "storemanager"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Owner",
                    NormalizedName = "owner"
                }
            );

            context.SaveChanges();
        }

        private static void SeedMembershipTypes(ApplicationDbContext context)
        {
            context.MembershipTypes.AddRange(
                new MembershipType
                {
                    Id = 1,
                    Name = "Pay as You Go",
                    SignUpFee = 0,
                    DurationInMonths = 0,
                    DiscountRate = 0
                },
                new MembershipType
                {
                    Id = 2,
                    Name = "Monthly",
                    SignUpFee = 30,
                    DurationInMonths = 1,
                    DiscountRate = 10
                },
                new MembershipType
                {
                    Id = 3,
                    Name = "Quaterly",
                    SignUpFee = 90,
                    DurationInMonths = 3,
                    DiscountRate = 15
                },
                new MembershipType
                {
                    Id = 4,
                    Name = "Yearly",
                    SignUpFee = 300,
                    DurationInMonths = 12,
                    DiscountRate = 20
                }
            );

            context.SaveChanges();
        }
    }
}