import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../services/auth.service';
import { jwtDecode } from 'jwt-decode';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';

export const authGuard: CanActivateFn = (route, state) => {
  const cookieService = inject(CookieService);
  const authService = inject(AuthService);
  const router = inject(Router);
  const snackbarService = inject(SnackbarService);
  const user = authService.getUser();
  const adminRoles = ['Owner', 'Seller', 'Writer'];

  if (state.url.includes('admin') && !adminRoles.every(role => user?.roles.includes(role))) {
    router.navigate(['/404']);
    return false;
  }

  if (cookieService.get('Authorization') && user) {
    let token = cookieService.get('Authorization');
    token = token.replace('Bearer ', '');
    let decodedToken = jwtDecode(token);

    if (decodedToken.exp && decodedToken.exp < Date.now() / 1000) {
      snackbarService.openWarning('Your session has expired. Please log in again.');
      authService.logout();
      router.navigate(['/login']);
      return false;
    }

    if (user.roles.includes('Owner')) {
      return true;
    }

    if (user.roles.includes('Writer')) {
      return true;
    }
  }

  authService.logout();
  router.navigate(['/login']);
  return false;
};
