import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginResponseModel } from '../models/login-response-model';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  hide: boolean = true;
  loginForm: FormGroup = new FormGroup({});

  constructor(
    private fb: FormBuilder,
    private _authService: AuthService,
    private _snackbarService: SnackbarService,
    private _cookieService: CookieService,
    private _router: Router,
    private _route: ActivatedRoute) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onLogin(): void {
    if (this.loginForm.valid) {
      this._authService.login(this.loginForm.value).subscribe({
        next: (response: LoginResponseModel) => {
          this._snackbarService.openSuccess('Login successful', 'Okay');

          const decodedToken = jwtDecode(response.token);
          const expirationDate = new Date(decodedToken.exp! * 1000);

          this._cookieService.set('Authorization', `Bearer ${response.token}`, expirationDate, '/', undefined, true, 'Strict');

          this._authService.setUser({ email: response.email, roles: response.roles, imageUrl: response.imageUrl, firstName: response.firstName, lastName: response.lastName });

          this._router.navigate(['/']);
        },
        error: (e) => {
          this._snackbarService.openError(e.error.errors?.message[0], 'Okay');
        }
      });
    } else {
      this._snackbarService.openError('The login form is invalid. Please try again.');
    }
  }

  loginWithFacebook() {
    window.location.href = 'api/login/facebook';
  }
}
