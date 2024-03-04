﻿namespace GymNexus.Infrastructure.Constants;

public static class DataConstants
{
    // User
    public const int ProfilePictureMaxLength = 250;

    // Product
    public const int ProductNameMaxLength = 40;
    public const int ProductImageUrlMaxLength = 250;
    public const int ProductDescriptionMaxLength = 500;
    public const string ProductPriceMinValue = "1.00";
    public const string ProductPriceMaxValue = "1000.00";

    // Store
    public const int StoreNameMaxLength = 40;
    public const int StoreDescriptionMaxLength = 500;

    // Category
    public const int CategoryNameMaxLength = 50;
    public const int CategoryDescriptionMaxLength = 500;

    // Marketplace
    public const int MarketplaceNameMaxLength = 50;
    public const int MarketplaceDescriptionMaxLength = 500;
    public const int MarketplaceAddressMaxLength = 150;

    // Post
    public const int PostTitleMaxLength = 50;
    public const int PostContentMaxLength = 500;
    public const int PostImageUrlMaxLength = 250;

    // Comment
    public const int CommentContentMaxLength = 250;
}