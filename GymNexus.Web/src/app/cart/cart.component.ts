import { Component, OnDestroy, OnInit } from '@angular/core';
import { CartService } from '../products/cart.service';
import { Observable, Subject, takeUntil } from 'rxjs';
import { ProductCartModel } from '../products/product-cart-model';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  cartProductCount: number = 0;
  selectedPaymentMethod: string = '';
  cartProducts: ProductCartModel[] = [];

  constructor(
    private _cartService: CartService
  ) { }

  ngOnInit(): void {
    this._cartService.getProducts().pipe(takeUntil(this._unsubscribeAll)).subscribe(products => {
      this.cartProducts = products;
    });

    this._cartService.getCartProductsCount().pipe(takeUntil(this._unsubscribeAll)).subscribe(count => {
      this.cartProductCount = count;
    });
  }

  get totalPrice(): number {
    return this.cartProducts.reduce((acc, product) => acc + product.price, 0);
  }

  checkout(): void {

  }

  removeFromCart(index: number): void {
    this._cartService.removeFromCart(index);
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
