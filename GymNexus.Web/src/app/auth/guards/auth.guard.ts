import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../services/auth.service';
import { jwtDecode } from 'jwt-decode';
import { SnackbarService } from 'src/app/shared/snackbar.service';

export const authGuard: CanActivateFn = (route, state) => {
  const cookieService = inject(CookieService);
  const authService = inject(AuthService);
  const router = inject(Router);
  const snackbarService = inject(SnackbarService);
  const user = authService.getUser();

  if (cookieService.get('Authorization') && user) {
    let token = cookieService.get('Authorization');
    token = token.replace('Bearer ', '');
    let decodedToken = jwtDecode(token);

    if (decodedToken.exp && decodedToken.exp < Date.now() / 1000) {
      authService.logout();
      router.navigate(['/']);
      return false;
    }

    if (user.roles.includes('Writer')) {
      return true;
    }

    snackbarService.openError('You do not have permission to access this page');
    return false;
  }

  authService.logout();
  router.navigate(['/']);
  return false;
};
