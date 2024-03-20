export class ProductModel {
    name: string;
    description: string;
    price: number;
    imageUrl: string;
    store: string;
    category: string;

    constructor(
        name: string,
        description: string,
        price: number,
        imageUrl: string,
        store: string,
        category: string
    ) {
        this.name = name;
        this.description = description;
        this.price = price;
        this.imageUrl = imageUrl;
        this.store = store;
        this.category = category;
    }
}
