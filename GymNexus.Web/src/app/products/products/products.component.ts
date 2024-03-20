import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Subject, takeUntil } from 'rxjs';
import { UserModel } from 'src/app/auth/models/user-model';
import { AuthService } from 'src/app/auth/services/auth.service';
import { ProductViewModel } from '../product-view-model';
import { ProductsService } from '../products.service';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
})
export class ProductsComponent implements OnInit, OnDestroy {
  private _unsubscribeAll: Subject<any> = new Subject<any>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  user: UserModel | undefined;
  products: ProductViewModel[] = [];
  currentProducts: ProductViewModel[] = [];
  error: any;

  constructor(
    private _sanitizer: DomSanitizer,
    private _productsService: ProductsService,
    private _authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadProducts();

    this.user = this._authService.getUser();
  }

  // toggleLike(product: ProductViewModel): void {
  // this._postsService.togglePostLike(product.id).subscribe({
  //   next: (hasUserLikedPost) => {

  //     if (hasUserLikedPost) {
  //       this._snackbarService.openSuccess('You liked this post');
  //     } else {
  //       this._snackbarService.openSuccess('You no longer like this post');
  //     }

  //     this.loadProducts(true);
  //   },
  //   error: (e) => {
  //     this.error = e;
  //   },
  // });
  // }

  getMarketplaceText(product: ProductViewModel): SafeHtml {
    const text = product.marketplace
    ? `Store is part of global marketplace partner: <b>${product.marketplace}</b>`
    : "The store currently is not part of any of our global marketplace partners"

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
          this.products = products;

          if (!innerLoad) {
            this.paginator.pageIndex = 0;
            this.setPage(0, 2);
          } else {
            this.setPage(this.paginator.pageIndex, this.paginator.pageSize);
          }
        },
        error: (e) => {
          this.error = e;
        },
      });
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
