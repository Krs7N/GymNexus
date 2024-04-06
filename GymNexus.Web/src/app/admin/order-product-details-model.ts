export class OrderProductDetailsModel {
    name: string;
    imageUrl: string;
    category: string;
    quantity: number;
    price: number;

    constructor(name: string, imageUrl: string, category: string, quantity: number, price: number) {
        this.name = name;
        this.imageUrl = imageUrl;
        this.category = category;
        this.quantity = quantity;
        this.price = price;
    }
}