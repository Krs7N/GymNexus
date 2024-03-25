import { CategoryViewModel } from "../shared/models/category-view-model";
import { MarketplaceViewModel } from "../shared/models/marketplace-view-model";
import { StoreViewModel } from "../shared/models/store-view-model";

export class ProductViewModel {
    id: number;
    name: string;
    description: string;
    price: number;
    imageUrl: string;
    createdOn: string;
    store: StoreViewModel;
    category: CategoryViewModel;
    marketplace?: MarketplaceViewModel;
    likes: number = 0;
    isLikedByCurrentUser: boolean;

    constructor(
        id: number,
        name: string,
        description: string,
        price: number,
        imageUrl: string,
        createdOn: string,
        store: StoreViewModel,
        category: CategoryViewModel,
        marketplace: MarketplaceViewModel,
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
