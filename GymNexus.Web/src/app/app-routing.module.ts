import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MarketplaceMapComponent } from './map/marketplace-map/marketplace-map.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { authGuard } from './auth/guards/auth.guard';
import { NotFoundComponent } from './shared/not-found/not-found.component';
import { CartDetailsComponent } from './cart/cart-details/cart-details.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'map' },
  { path: 'posts', loadChildren: () => import('./posts/posts.module').then(m => m.PostsModule) },
  { path: 'products', loadChildren: () => import('./products/products.module').then(m => m.ProductsModule) },
  { path: 'map', component: MarketplaceMapComponent, canActivate: [authGuard] },
  { path: 'cart', component: CartDetailsComponent, canActivate: [authGuard]},
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '404', component: NotFoundComponent },
  { path: '**', redirectTo: '404' }
  // { path: 'forgot-password', loadChildren: () => import('./forgot-password/forgot-password.module').then(m => m.ForgotPasswordModule) },
  // { path: 'reset-password', loadChildren: () => import('./reset-password/reset-password.module').then(m => m.ResetPasswordModule) },
  // { path: 'profile', loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule) },
  // { path: 'settings', loadChildren: () => import('./settings/settings.module').then(m => m.SettingsModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
