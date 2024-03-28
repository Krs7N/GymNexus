declare var cloudinary: any;

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryViewModel } from 'src/app/shared/models/category-view-model';
import { MarketplaceViewModel } from 'src/app/shared/models/marketplace-view-model';
import { StoreViewModel } from 'src/app/shared/models/store-view-model';
import { ProductsService } from '../products.service';
import { StoresService } from 'src/app/stores/stores.service';
import { MatSelectChange } from '@angular/material/select';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';
import { ProductModel } from '../product-model';
import { Actions } from 'src/app/enums/actions';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  id: number = 0;
  productForm: FormGroup;
  stores: StoreViewModel[] = [];
  categories: CategoryViewModel[] = [];
  marketplaces: MarketplaceViewModel[] = [];
  action: string = '';

  constructor(
    private fb: FormBuilder,
    private _route: ActivatedRoute,
    private _router: Router,
    private _snackbarService: SnackbarService,
    private _storesService: StoresService,
    private _productsService: ProductsService
  ) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(40)]],
      description: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(500)]],
      price: ['', [Validators.required, Validators.min(1.00), Validators.max(1000.00)]],
      imageUrl: [null, [Validators.required]],
      storeId: ['', [Validators.required]],
      categoryId: ['', [Validators.required]],
      marketplaceId: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.id = this._route.snapshot.params['id'];
    this.categories = this._route.snapshot.data['categories'];
    this.marketplaces = this._route.snapshot.data['marketplaces'];
    this.action = this._route.snapshot.data['action'];

    if (this.id) {
      this._productsService.getProduct(this.id).subscribe(product => {
        this._storesService.getStoresByMarketplace(product.marketplace?.id).subscribe(stores => {
          if (stores.length === 0) {
            this._snackbarService.openWarning('No stores are associated with this marketplace. Please choose a different marketplace area.');
          }
          this.stores = stores;
        });
        this.productForm.patchValue({
          name: product.name,
          description: product.description,
          price: product.price,
          imageUrl: product.imageUrl,
          storeId: product.store.id,
          categoryId: product.category.id,
          marketplaceId: product.marketplace?.id
        });
      });
    }
  }

  openCloudinaryUploader(): void {
    cloudinary.openUploadWidget({
      cloudName: 'dekvgy42s',
      uploadPreset: 'gymnexus',
      sources: ['local', 'url', 'camera', 'image_search'],
      showAdvancedOptions: false,
      cropping: true,
      multiple: false,
      defaultSource: 'local',
      styles: {
        palette: {
          window: "#FFFFFF",
          windowBorder: "#90A0B3",
          tabIcon: "#0078FF",
          menuIcons: "#5A616A",
          textDark: "#000000",
          textLight: "#FFFFFF",
          link: "#0078FF",
          action: "#FF620C",
            inactiveTabIcon: "#0E2F5A",
            error: "#F44235",
            inProgress: "#0078FF",
            complete: "#20B832",
            sourceBg: "#E4EBF1"
          },
          fonts: {
            default: null,
            "'Fira Sans', sans-serif": {
              url: "https://fonts.googleapis.com/css?family=Fira+Sans",
              active: true
            }
          }
        }
      }, (error: Error, result: any) => {
        if (!error && result && result.event === "success") {
          this.productForm.get('imageUrl')?.setValue(result.info.secure_url);
        }
      });
  }

  onMarketplaceChanged(event: MatSelectChange) {
    this._storesService.getStoresByMarketplace(event.value).subscribe(stores => {
      if (stores.length === 0) {
        this.productForm.get('store')?.reset();
        this.productForm.updateValueAndValidity();
        this._snackbarService.openWarning('No stores are associated with this marketplace. Please choose a different marketplace area.');
      }
      this.stores = stores;
    });
  }

  saveProduct(): void {
    if (this.productForm.valid) {
      if (this.action === Actions.CREATE) {
        this._productsService.create(this.productForm.value as ProductModel).subscribe({
          next: () => {
            this._snackbarService.openSuccess('Product created successfully.');
            this._router.navigate(['/products']);
          },
          error: () => {
            this._snackbarService.openError('An error occurred while creating the product.');
          }
        });

        return;
      }

      this._productsService.update(this.id, this.productForm.value as ProductModel).subscribe({
        next: () => {
          this._snackbarService.openSuccess('Product updated successfully.');
          this._router.navigate(['/products']);
        },
        error: () => {
          this._snackbarService.openError('An error occurred while updating the product.');
        }
      });
    }
  }
}
