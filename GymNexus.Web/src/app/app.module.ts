import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { PostsModule } from './posts/posts.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CloudinaryModule } from '@cloudinary/ng';
import { AuthModule } from './auth/auth.module';
import { AuthInterceptor } from './auth/auth.interceptor';
import { MaterialModule } from './shared/material.module';
import { ErrorPageComponent } from "./shared/error-page/error-page.component";
import { CartComponent } from './cart/cart.component';
import { CartDetailsComponent } from './cart/cart-details/cart-details.component';

@NgModule({
    declarations: [
        AppComponent,
        NavBarComponent,
        CartComponent,
        CartDetailsComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
    ],
    bootstrap: [AppComponent],
    imports: [
        CloudinaryModule,
        BrowserModule,
        AppRoutingModule,
        MaterialModule,
        BrowserAnimationsModule,
        PostsModule,
        AuthModule,
        HttpClientModule,
        ErrorPageComponent
    ]
})
export class AppModule { }
