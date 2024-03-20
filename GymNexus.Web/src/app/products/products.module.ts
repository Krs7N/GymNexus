import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from '../shared/material.module';
import { ErrorPageComponent } from '../shared/error-page/error-page.component';
import { RouterModule } from '@angular/router';
import { productsRoutes } from './products.routes';
import { ProductsComponent } from './products/products.component';



@NgModule({
  declarations: [
    ProductsComponent
  ],
  imports: [
    RouterModule.forChild(productsRoutes),
    SharedModule,
    MaterialModule,
    ErrorPageComponent
  ]
})
export class ProductsModule { }
