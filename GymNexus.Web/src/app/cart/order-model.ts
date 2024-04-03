import { ProductCartModel } from "../products/product-cart-model";

export class OrderModel {
    products: ProductCartModel[];
    paymentMethod: string;

    constructor(products: ProductCartModel[], paymentMethod: string) {
        this.products = products;
        this.paymentMethod = paymentMethod;
    }
}