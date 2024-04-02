export class ProductCartModel {
    id: number;
    imageUrl: string;
    name: string;
    price: number;

    constructor(id: number, imageUrl: string, name: string, price: number) {
        this.id = id;
        this.imageUrl = imageUrl;
        this.name = name;
        this.price = price;
    }
}
