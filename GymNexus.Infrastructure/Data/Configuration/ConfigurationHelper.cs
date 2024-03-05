using GymNexus.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GymNexus.Infrastructure.Data.Configuration;

public static class ConfigurationHelper
{
    public static ApplicationUser RootApplicationUser = GetRootApplicationUser();

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
                CreatedBy = RootApplicationUser.UserName,
                CreatedOn = DateTime.Now.AddDays(-5),
            },
            new Post
            {
                Id = 2,
                Title = "How to get started",
                Content =
                    "To get started, create an account and start sharing your fitness journey with the world. You can also connect with other users and see their progress.",
                CreatedBy = RootApplicationUser.UserName,
                CreatedOn = DateTime.Now.AddMonths(-2)
            },
            new Post
            {
                Id = 3,
                Title = "How can I bench more?",
                Content =
                    "I am looking to start increasing my bench press and bench more, and put more pressure on my chest muscles. I am looking for advices, thanks in advance!",
                CreatedBy = RootApplicationUser.UserName,
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
}