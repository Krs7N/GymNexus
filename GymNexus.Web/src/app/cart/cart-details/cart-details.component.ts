import { Component, OnDestroy, OnInit } from '@angular/core';
import { CartService } from '../cart.service';
import { ProductCartModel } from 'src/app/products/product-cart-model';
import { Subject, takeUntil } from 'rxjs';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';
import { OrderModel } from '../order-model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart-details',
  templateUrl: './cart-details.component.html',
  styleUrls: ['./cart-details.component.scss']
})
export class CartDetailsComponent implements OnInit, OnDestroy {

  totalPrice: number = 0;

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  cartProducts: ProductCartModel[] = [];
  paymentMethod: string = '';

  constructor(
    private _cartService: CartService,
    private _router: Router,
    private _snackbarService: SnackbarService
  ) { }

  ngOnInit(): void {
    this._cartService.getProducts().pipe(takeUntil(this._unsubscribeAll)).subscribe(products => {
      if (products.length === 0) {
        this._router.navigate(['/products']);
        this._snackbarService.openWarning('Your cart is currently empty. Please add products first');
      }
      this.cartProducts = products;
    });

      this.totalPrice = this.cartProducts.reduce((acc, product) => acc + (product.price * product.quantity), 0);
  }

  recalculateTotalPrice(product: ProductCartModel) {
    localStorage.removeItem('cart');
    localStorage.setItem('cart', JSON.stringify(this.cartProducts));
    this.totalPrice = this.cartProducts.reduce((total, product) => total + (product.price * product.quantity), 0);
  }

  onCheckout(): void {
    debugger
    if (!this.paymentMethod) {
      this._snackbarService.openError('Please select a payment method');
      return;
    }

    const order = {
      products: this.cartProducts,
      paymentMethod: this.paymentMethod
    } as OrderModel;

    this._cartService.createOrder(order).pipe(takeUntil(this._unsubscribeAll)).subscribe({
      next: () => {
        this._cartService.clearCart();
        this._snackbarService.openSuccess('Your order has been placed. Thank you for your purchase!');
      },
      error: (e) => {
        this._snackbarService.openError('An error occurred while processing your order. Please try again later.');
      }
    });
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
