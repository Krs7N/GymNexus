import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from '../shared/material.module';
import { ErrorPageComponent } from '../shared/error-page/error-page.component';
import { RouterModule } from '@angular/router';
import { productsRoutes } from './products.routes';
import { ProductsComponent } from './products/products.component';
import { ProductDetailsComponent } from './product-details/product-details.component';



@NgModule({
  declarations: [
    ProductsComponent,
    ProductDetailsComponent
  ],
  imports: [
    RouterModule.forChild(productsRoutes),
    SharedModule,
    MaterialModule,
    ErrorPageComponent
  ]
})
export class ProductsModule { }
