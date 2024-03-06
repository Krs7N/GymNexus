namespace GymNexus.Infrastructure.Constants;

public static class DataConstants
{
    // Common
    public const string DateTimeFormat = "dd/MM/yyyy HH:mm";

    // User
    public const int ProfilePictureMaxLength = 250;

    // Product
    public const int ProductNameMinLength = 5;
    public const int ProductNameMaxLength = 40;
    public const int ProductImageUrlMaxLength = 250;
    public const int ProductDescriptionMinLength = 10;
    public const int ProductDescriptionMaxLength = 500;
    public const string ProductPriceMinValue = "1.00";
    public const string ProductPriceMaxValue = "1000.00";

    // Store
    public const int StoreNameMinLength = 5;
    public const int StoreNameMaxLength = 40;
    public const int StoreDescriptionMinLength = 10;
    public const int StoreDescriptionMaxLength = 500;

    // Category
    public const int CategoryNameMinLength = 5;
    public const int CategoryNameMaxLength = 50;
    public const int CategoryDescriptionMinLength = 10;
    public const int CategoryDescriptionMaxLength = 500;

    // Marketplace
    public const int MarketplaceNameMinLength = 5;
    public const int MarketplaceNameMaxLength = 50;
    public const int MarketplaceDescriptionMinLength = 10;
    public const int MarketplaceDescriptionMaxLength = 500;
    public const int MarketplaceAddressMaxLength = 150;

    // Post
    public const int PostTitleMinLength = 5;
    public const int PostTitleMaxLength = 50;
    public const int PostContentMinLength = 10;
    public const int PostContentMaxLength = 500;
    public const int PostImageUrlMaxLength = 250;

    // Comment
    public const int CommentContentMinLength = 5;
    public const int CommentContentMaxLength = 250;

    // Order
    public const int OrderStatusMaxLength = 20;
    public const int OrderPaymentMethodMaxLength = 20;
}