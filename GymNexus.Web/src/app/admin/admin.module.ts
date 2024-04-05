import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { MaterialModule } from 'src/app/shared/material.module';
import { RouterModule } from '@angular/router';
import { adminRoutes } from './admin.routes';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { ErrorPageComponent } from '../shared/error-page/error-page.component';



@NgModule({
  declarations: [
    AdminDashboardComponent
  ],
  imports: [
    RouterModule.forChild(adminRoutes),
    SharedModule,
    ErrorPageComponent,
    MaterialModule
  ]
})
export class AdminModule { }
