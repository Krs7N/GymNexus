export class ProductModel {
    name: string;
    description: string;
    price: number;
    imageUrl: string;
    storeId: number;
    categoryId: number;
    marketplaceId: number;

    constructor(
        name: string,
        description: string,
        price: number,
        imageUrl: string,
        storeId: number,
        categoryId: number,
        marketplaceId: number
    ) {
        this.name = name;
        this.description = description;
        this.price = price;
        this.imageUrl = imageUrl;
        this.storeId = storeId;
        this.categoryId = categoryId;
        this.marketplaceId = marketplaceId;
    }
}
