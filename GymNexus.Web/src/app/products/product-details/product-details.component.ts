declare var cloudinary: any;

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CategoryViewModel } from 'src/app/shared/models/category-view-model';
import { MarketplaceViewModel } from 'src/app/shared/models/marketplace-view-model';
import { StoreViewModel } from 'src/app/shared/models/store-view-model';
import { ProductsService } from '../products.service';

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

  constructor(private fb: FormBuilder, private _route: ActivatedRoute, private _productsService: ProductsService) {
    this.productForm = this.fb.group({
      name: [''],
      description: [''],
      price: [''],
      imageUrl: [null],
      store: [null],
      category: [null],
      marketplace: [null]
    });
  }

  ngOnInit(): void {
    this.id = this._route.snapshot.params['id'];

    
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

  saveProduct(): void {
    if (this.productForm.valid) {
      console.log(this.productForm.value);
    }
  }
}
