import { OrderProductDetailsModel } from "./order-product-details-model";

export class OrderDetailsModel {
    id: number;
    createdBy: string;
    createdOn: string;
    quantity: number;
    totalPrice: number;
    status: string;
    products: OrderProductDetailsModel[];

    constructor(id: number, createdBy: string, createdOn: string, quantity: number, totalPrice: number, status: string, products: OrderProductDetailsModel[]) {
        this.id = id;
        this.createdBy = createdBy;
        this.createdOn = createdOn;
        this.quantity = quantity;
        this.totalPrice = totalPrice;
        this.status = status;
        this.products = products;
    }
}