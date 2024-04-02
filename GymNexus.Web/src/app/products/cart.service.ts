import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { ProductCartModel } from './product-cart-model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private productsInCartSubject: BehaviorSubject<ProductCartModel[]> = new BehaviorSubject<ProductCartModel[]>([]);
  private productsInCart: ProductCartModel[] = [];

  private cartProductCountSubject: BehaviorSubject<number> = new BehaviorSubject<number>(0);

  constructor() {
    const savedCart = localStorage.getItem('cart');
    if (savedCart) {
      this.productsInCart = JSON.parse(savedCart);
      this.productsInCartSubject.next(this.productsInCart);
    }

    this.productsInCartSubject.subscribe(products => {
      this.productsInCart = products;
      this.cartProductCountSubject.next(this.productsInCart.length);
      localStorage.setItem('cart', JSON.stringify(this.productsInCart));
    });
  }

  addToCart(product: ProductCartModel): void {
    this.productsInCartSubject.next([...this.productsInCart, product]);
  }

  getProducts(): Observable<ProductCartModel[]> {
    return this.productsInCartSubject.asObservable();
  }

  isInCart(product: ProductCartModel): Observable<boolean> {
    return this.getProducts().pipe(
      map(products => products.some(p => p.id === product.id))
    );
  }

  removeFromCart(id: number): void {
    const index = this.productsInCart.findIndex(product => product.id === id);
    if (index > -1) {
        this.productsInCart.splice(index, 1);
        this.productsInCartSubject.next(this.productsInCart);
    }
  }

  clearCart(): void {
    this.productsInCart = [];
    this.productsInCartSubject.next(this.productsInCart);
    localStorage.removeItem('cart');
  }

  getCartProductsCount(): Observable<number> {
    return this.cartProductCountSubject.asObservable();
  }
}
