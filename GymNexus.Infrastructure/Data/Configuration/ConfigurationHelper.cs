using GymNexus.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GymNexus.Infrastructure.Data.Configuration;

public static class ConfigurationHelper
{
    public static ApplicationUser RootApplicationUser = GetRootApplicationUser();

    public static ApplicationUser TestApplicationUser = GetTestApplicationUser();

    public static Post[] GetSeedPosts()
    {
        return new Post[]
        {
            new Post
            {
                Id = 1,
                Title = "Welcome to GymNexus",
                Content =
                    "Welcome to GymNexus! This is a social network for fitness enthusiasts. Share your progress, ask for advice, and connect with other people who share your passion for fitness.",
                CreatedBy = RootApplicationUser.Id,
                CreatedOn = DateTime.Now.AddDays(-5),
            },
            new Post
            {
                Id = 2,
                Title = "How to get started",
                Content =
                    "To get started, create an account and start sharing your fitness journey with the world. You can also connect with other users and see their progress.",
                CreatedBy = RootApplicationUser.Id,
                CreatedOn = DateTime.Now.AddMonths(-2)
            },
            new Post
            {
                Id = 3,
                Title = "How can I bench more?",
                Content =
                    "I am looking to start increasing my bench press and bench more, and put more pressure on my chest muscles. I am looking for advices, thanks in advance!",
                CreatedBy = RootApplicationUser.Id,
                CreatedOn = DateTime.Now.AddYears(-1)
            }
        };
    }

    public static Category[] GetSeedCategories()
    {
        return new Category[]
        {
            new Category
            {
                Id = 1,
                Name = "Protein Whey",
                Description = "Special whey protein made by Kevin Levrone's own brand",
            },
            new Category
            {
                Id = 2,
                Name = "Protein",
                Description = "This is the default brand of proteins that we can give you for now",
            },
            new Category
            {
                Id = 3,
                Name = "Creatine",
                Description = "Creatine is a substance that is found in small amounts in the body. It is also found in certain foods and can be taken as a dietary supplement. Creatine is involved in producing the energy that muscles need to work.",
            },
            new Category
            {
                Id = 4,
                Name = "Creatine Monohydrate",
                Description = "Creatine Monohydrate is of the more advanced types of creatine out there. It is one of the most researched supplements worldwide and can help the brain activity!",
            }
        };
    }

    public static Marketplace[] GetSeedMarketplaces()
    {
        return new Marketplace[]
        {
            new Marketplace()
            {
                Id = 1,
                Name = "Fitness1 Sofia",
                Description = "Fitness1 is a marketplace that offers a wide range of fitness products and supplements.",
                Address = "Boulevard \"Cherni vrah\" 25, Sofia",
                Latitude = 42.6777m,
                Longitude = 23.3221m
            },
            new Marketplace()
            {
                Id = 2,
                Name = "Pulse Gym Shop",
                Description = "Pulse Gym Shop offers various supplements and gym equipment. The store is part of the Pulse brand which has its own gym's all around the country",
                Address = "Mladost 4, 1715, Sofia",
                Latitude = 42.62518m,
                Longitude = 23.373451m
            },
            new Marketplace()
            {
                Id = 3,
                Name = "Sila BG",
                Description = "Sila BG is one of the leading brands in Bulgaria. A recent new-comer but with a high demand with various range of products.",
                Address = "Lyuben Karavelov 21, 9002, Varna",
                Latitude = 43.20887m,
                Longitude = 27.92242m
            },
            new Marketplace()
            {
                Id = 4,
                Name = "Fitness1 Burgas",
                Description = "Fitness1 is a marketplace that offers a wide range of fitness products and supplements.",
                Address = "Adam Mitskevich 5, 8001, Burgas",
                Latitude = 42.50064m,
                Longitude = 27.47921m
            }
        };
    }

    public static Product[] GetSeedProducts()
    {
        return new Product[]
        {
            new Product()
            {
                Id = 1,
                Name = "Kevin Levrone's Whey Protein",
                Description =
                    "Special whey protein made by Kevin Levrone's own brand. Comes in 2000 grams package, with a spoon that is 30g and recommended daily usage of 30g",
                CategoryId = 1,
                CreatedOn = DateTime.Now.AddDays(-5),
                ImageUrl = "https://www.kevinlevrone.com/wp-content/uploads/2021/06/levrone-whey-protein-2000g.jpg",
                StoreId = 1,
                Price = 50.00m,
            },
            new Product()
            {
                Id = 2,
                Name = "Protein",
                Description = "This is the default brand of proteins that we can give you for now. Comes in 1000 grams package, with a spoon that is 30g and recommended daily usage of 30g",
                CategoryId = 2,
                CreatedOn = DateTime.Now.AddMonths(-2),
                ImageUrl = "https://gymbeam.bg/media/catalog/product/cache/bf5a31e851f50f3ed6850cbbf183db11/j/u/just_whey_chocolate_milkshake_1_kg_gymbeam_1.png",
                StoreId = 1,
                Price = 26.00m,
            }
        };
    }

    public static Store[] GetSeedStores()
    {
        return new Store[]
        {
            new Store()
            {
                Id = 1,
                Name = "Root's local Gym Shop",
                Description = "This is the Root's store that is created to be useful for you and easier to start with. It is owned by the root user.",
                CreatedOn = DateTime.Now,
                MarketplaceId = 1,
                OwnerId = RootApplicationUser.Id
            }
        };
    }

    private static ApplicationUser GetRootApplicationUser()
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        var rootUser = new ApplicationUser
        {
            UserName = "root@abv.bg",
            NormalizedUserName = "ROOT@ABV.BG"
        };

        rootUser.PasswordHash = hasher.HashPassword(rootUser, "gymnexus");

        return rootUser;
    }

    private static ApplicationUser GetTestApplicationUser()
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        var testUser = new ApplicationUser
        {
            UserName = "test@abv.bg",
            NormalizedUserName = "TEST@ABV.BG"
        };

        testUser.PasswordHash = hasher.HashPassword(testUser, "gymnexus123");

        return testUser;
    }
}