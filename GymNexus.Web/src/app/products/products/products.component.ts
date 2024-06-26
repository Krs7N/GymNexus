import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Subject, takeUntil } from 'rxjs';
import { UserModel } from 'src/app/auth/models/user-model';
import { AuthService } from 'src/app/auth/services/auth.service';
import { ProductViewModel } from '../product-view-model';
import { ProductsService } from '../products.service';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';
import { StoreViewModel } from 'src/app/shared/models/store-view-model';
import { ActivatedRoute } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { CartService } from '../../cart/cart.service';
import { ProductCartModel } from '../product-cart-model';
import { CategoryViewModel } from 'src/app/shared/models/category-view-model';
import { MatSelectChange } from '@angular/material/select';
import { parse } from 'date-fns';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
})
export class ProductsComponent implements OnInit, OnDestroy {
  private _unsubscribeAll: Subject<any> = new Subject<any>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  user: UserModel | undefined;
  userStores: StoreViewModel[] = [];
  allProducts: ProductViewModel[] = [];
  products: ProductViewModel[] = [];
  currentProducts: ProductViewModel[] = [];
  categories: CategoryViewModel[] = [];

  error: any;

  constructor(
    public cartService: CartService,
    private _sanitizer: DomSanitizer,
    private _snackbarService: SnackbarService,
    private _productsService: ProductsService,
    private _route: ActivatedRoute,
    private _authService: AuthService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.userStores = this._route.snapshot.data['userStores'];
    this.categories = this._route.snapshot.data['categories'];

    this.loadProducts();

    this.user = this._authService.getUser();
    
    if (this.user) {
      this.user.stores = this.userStores;
    }
  }
  
  deleteProduct(id: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Deletion',
        message: 'Are you sure you want to delete this product?'
      }
    });

    dialogRef.afterClosed().pipe(takeUntil(this._unsubscribeAll)).subscribe(result => {
      if (result) {
        this._productsService.delete(id)
          .pipe(takeUntil(this._unsubscribeAll))
          .subscribe({
            next: () => {
              this._snackbarService.openSuccess(`Successfully deleted product`);
              this.loadProducts();
            },
            error: (e) => {
              console.error(e);
            },
          });
      }
    });
  }

  applyFilter(event: MatSelectChange): void {
    const categoryFilter: number | string = event.value;

    if (categoryFilter === 'all') {
      this.loadProducts(true);
    } else {
      this.products = this.allProducts.filter((product) => product.category.id === categoryFilter);
      this.updatePaginator();
      this.setPage(this.paginator.pageIndex, this.paginator.pageSize);
    }
  }

  addToCart(product: ProductViewModel): void {
    const cartProduct = {
      id: product.id,
      imageUrl: product.imageUrl,
      name: product.name,
      price: product.price,
      quantity: 1,
    } as ProductCartModel;

    this.cartService.addToCart(cartProduct);
    this._snackbarService.openSuccess('Product added to cart.');
  }

  sortProducts(sortValue: string) {
    if (sortValue === 'likes') {
      this.products.sort((a, b) => b.likes - a.likes);
    } else if (sortValue === 'dateOld') {
      this.products.sort((a, b) => {
        const dateA = parse(a.createdOn, 'dd/MM/yyyy HH:mm', new Date());
        const dateB = parse(b.createdOn, 'dd/MM/yyyy HH:mm', new Date());

        return dateA.getTime() - dateB.getTime();
      });
    } else if (sortValue === 'dateNew') {
      this.products.sort((a, b) => {
        const dateA = parse(a.createdOn, 'dd/MM/yyyy HH:mm', new Date());
        const dateB = parse(b.createdOn, 'dd/MM/yyyy HH:mm', new Date());

        return dateB.getTime() - dateA.getTime();
      });
    } else {
      this.loadProducts();
    }
    this.setPage(this.paginator.pageIndex, this.paginator.pageSize);
  }

  removeFromCart(id: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Remove from Cart',
        message: 'Are you sure you want to remove the product from your cart?'
      }
    });

    dialogRef.afterClosed().pipe(takeUntil(this._unsubscribeAll)).subscribe(result => {
      if (result) {
        this.cartService.removeFromCart(id);
        this._snackbarService.openSuccess('Product removed from cart.');
      }
    });
  }

  toggleLike(product: ProductViewModel): void {
    this._productsService.toggleProductLike(product.id).subscribe({
      next: (hasUserLikedProduct) => {
        if (hasUserLikedProduct) {
          this._snackbarService.openSuccess('You liked this product');
        } else {
          this._snackbarService.openSuccess('You no longer like this product');
        }

        this.loadProducts(true);
      },
      error: (e) => {
        this.error = e;
      },
    });
  }

  isAdminOrSellerOfProduct(product: ProductViewModel): boolean | undefined {
    const isAdmin =
      this.user?.roles.includes('Owner') &&
      this.user?.roles.includes('Seller') &&
      this.user?.roles.includes('Writer');

    const isSellerOfProduct =
      this.user?.roles.includes('Seller') &&
      this.user.stores?.some((store) => store.id === product.store.id);

    return isAdmin || isSellerOfProduct;
  }

  getMarketplaceText(product: ProductViewModel): SafeHtml {
    const text = product.marketplace
      ? `Store is part of global marketplace partner: <b>${product.marketplace.name}</b>`
      : 'The store currently is not part of any of our global marketplace partners';

    return this._sanitizer.bypassSecurityTrustHtml(text);
  }

  changePage(event: PageEvent): void {
    this.setPage(event.pageIndex, event.pageSize);
  }

  private loadProducts(innerLoad: boolean = false): void {
    this._productsService
      .getAllProducts()
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe({
        next: (products) => {
          this.allProducts = products;
          this.products = [...this.allProducts];

          if (!innerLoad) {
            this.paginator.pageIndex = 0;
            this.setPage(0, 4);
          } else {
            this.setPage(this.paginator.pageIndex, this.paginator.pageSize);
          }
        },
        error: (e) => {
          this.error = e;
        },
      });
  }

  private updatePaginator() {
    this.paginator.length = this.products.length;
    this.paginator.pageIndex = 0;
  }

  private setPage(pageIndex: number, pageSize: number): void {
    const start = pageIndex * pageSize;
    this.currentProducts = this.products.slice(start, start + pageSize);
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
