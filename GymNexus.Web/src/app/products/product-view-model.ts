export class ProductViewModel {
    id: number;
    name: string;
    description: string;
    price: number;
    imageUrl: string;
    createdOn: string;
    store: string;
    category: string;
    marketplace?: string;
    likes: number = 0;
    isLikedByCurrentUser: boolean;

    constructor(
        id: number,
        name: string,
        description: string,
        price: number,
        imageUrl: string,
        createdOn: string,
        store: string,
        category: string,
        marketplace: string,
        likes: number,
        isLikedByCurrentUser: boolean) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.price = price;
        this.imageUrl = imageUrl;
        this.createdOn = createdOn;
        this.store = store;
        this.category = category;
        this.marketplace = marketplace;
        this.likes = likes;
        this.isLikedByCurrentUser = isLikedByCurrentUser;
    }
}
