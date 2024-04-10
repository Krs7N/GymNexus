import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { MaterialModule } from 'src/app/shared/material.module';
import { RouterModule } from '@angular/router';
import { adminRoutes } from './admin.routes';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { ErrorPageComponent } from '../shared/error-page/error-page.component';
import { AdminManageOrdersComponent } from './admin-manage-orders/admin-manage-orders.component';
import { ProductsTableComponent } from './admin-manage-orders/products-table/products-table.component';
import { AdminManagePostsComponent } from './admin-manage-posts/admin-manage-posts.component';
import { CreateMarketplaceFormComponent } from './create-marketplace-form/create-marketplace-form.component';



@NgModule({
  declarations: [
    AdminDashboardComponent,
    AdminManageOrdersComponent,
    ProductsTableComponent,
    AdminManagePostsComponent,
    CreateMarketplaceFormComponent
  ],
  imports: [
    RouterModule.forChild(adminRoutes),
    SharedModule,
    ErrorPageComponent,
    MaterialModule
  ]
})
export class AdminModule { }
