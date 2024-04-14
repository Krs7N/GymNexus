import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';
import { SnackbarService } from '../shared/services/snackbar.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(
    private _cookieService: CookieService,
    private _snackbarService: SnackbarService,
    private _authService: AuthService,
    private _router: Router
  ) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this._cookieService.get('Authorization');

    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `${token}`
        }
      });
    } else {
      this._snackbarService.openWarning('Your session has expired. Please log in again.');
      this._authService.logout();
      this._router.navigate(['/login']);
    }

    return next.handle(request);
  }
}
