export class ProductCartModel {
    id: number;
    imageUrl: string;
    name: string;
    price: number;
    quantity: number = 1;

    constructor(id: number, imageUrl: string, name: string, price: number) {
        this.id = id;
        this.imageUrl = imageUrl;
        this.name = name;
        this.price = price;
    }
}
