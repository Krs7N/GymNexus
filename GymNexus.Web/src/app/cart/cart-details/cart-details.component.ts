import { Component, OnDestroy, OnInit } from '@angular/core';
import { CartService } from '../cart.service';
import { ProductCartModel } from 'src/app/products/product-cart-model';
import { Subject, takeUntil } from 'rxjs';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';

@Component({
  selector: 'app-cart-details',
  templateUrl: './cart-details.component.html',
  styleUrls: ['./cart-details.component.scss']
})
export class CartDetailsComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  cartProducts: ProductCartModel[] = [];
  paymentMethod: string = '';

  constructor(
    private _cartService: CartService,
    private _snackbarService: SnackbarService
  ) { }

  ngOnInit(): void {
      this._cartService.getProducts().pipe(takeUntil(this._unsubscribeAll)).subscribe(products => {
        this.cartProducts = products;
      });
  }

  get totalPrice(): number {
    return this.cartProducts.reduce((acc, product) => acc + product.price, 0);
  }

  onCheckout(): void {
    if (!this.paymentMethod) {
      this._snackbarService.openError('Please select a payment method');
      return;
    }

    this._cartService.clearCart();
    this._snackbarService.openSuccess('Your order has been placed. Thank you for your purchase!');
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
