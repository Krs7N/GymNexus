import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from '../shared/material.module';
import { ErrorPageComponent } from '../shared/error-page/error-page.component';
import { RouterModule } from '@angular/router';
import { productsRoutes } from './products.routes';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(productsRoutes),
    SharedModule,
    MaterialModule,
    ErrorPageComponent
  ]
})
export class ProductsModule { }
