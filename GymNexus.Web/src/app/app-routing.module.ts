import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  // { path: '', pathMatch: 'full', redirectTo: 'posts' },
  { path: 'posts', loadChildren: () => import('./posts/posts.module').then(m => m.PostsModule) },
  // { path: 'login', loadChildren: () => import('./login/login.module').then(m => m.LoginModule) },
  // { path: 'register', loadChildren: () => import('./register/register.module').then(m => m.RegisterModule) },
  // { path: 'forgot-password', loadChildren: () => import('./forgot-password/forgot-password.module').then(m => m.ForgotPasswordModule) },
  // { path: 'reset-password', loadChildren: () => import('./reset-password/reset-password.module').then(m => m.ResetPasswordModule) },
  // { path: 'profile', loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule) },
  // { path: 'settings', loadChildren: () => import('./settings/settings.module').then(m => m.SettingsModule) },
  // { path: '404', loadChildren: () => import('./not-found/not-found.module').then(m => m.NotFoundModule) },
  // { path: '', redirectTo: '/home', pathMatch: 'full' },
  // { path: '**', redirectTo: '/404' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
