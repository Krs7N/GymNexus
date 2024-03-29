import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { MaterialModule } from 'src/app/shared/material.module';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    RouterModule,
    SharedModule,
    MaterialModule
  ]
})
export class AuthModule { }
