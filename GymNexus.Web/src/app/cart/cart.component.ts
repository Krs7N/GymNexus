import { Component, OnDestroy, OnInit } from '@angular/core';
import { CartService } from './cart.service';
import { Observable, Subject, takeUntil } from 'rxjs';
import { ProductCartModel } from '../products/product-cart-model';
import { SnackbarService } from '../shared/services/snackbar.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../shared/confirm-dialog/confirm-dialog.component';

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
    private _cartService: CartService,
    private _snackbarService: SnackbarService,
    public dialog: MatDialog
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

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Remove from Cart',
        message: 'Are you sure you want to remove the product from your cart?'
      }
    });

    dialogRef.afterClosed().pipe(takeUntil(this._unsubscribeAll)).subscribe(result => {
      if (result) {
        this._cartService.removeFromCart(index);
        this._snackbarService.openSuccess('Product removed from cart.');
      }
    });
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
