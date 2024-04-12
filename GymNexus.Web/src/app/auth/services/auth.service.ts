import { Injectable, Injector } from '@angular/core';
import { CrudService } from 'src/app/core/services/crud.service';
import { RegisterModel } from '../models/register-model';
import { LoginModel } from '../models/login-model';
import { BehaviorSubject, Observable } from 'rxjs';
import { RegisterResponseModel } from '../models/register-response-model';
import { LoginResponseModel } from '../models/login-response-model';
import { environment } from 'src/environments/environment.development';
import { UserModel } from '../models/user-model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends CrudService<RegisterModel | LoginModel>{

  user$: BehaviorSubject<UserModel | undefined> = new BehaviorSubject<UserModel | undefined>(undefined);

  constructor(injector: Injector, private _cookieService: CookieService) {
    super(injector);
  }

  register(registerModel: RegisterModel): Observable<RegisterResponseModel> {
    return this.httpClient.post<RegisterResponseModel>(`${environment.apiBaseUrl}/${this.getResourceUrl()}/register`, registerModel);
  }

  login(loginModel: LoginModel): Observable<LoginResponseModel> {
    return this.httpClient.post<LoginResponseModel>(`${environment.apiBaseUrl}/${this.getResourceUrl()}/login`, loginModel);
  }

  logout(): Observable<void> {
    this.user$.next(undefined);
    localStorage.clear();
    this._cookieService.delete('Authorization', '/');
    return this.httpClient.post<void>(`${environment.apiBaseUrl}/${this.getResourceUrl()}/logout`, null);
  }

  getUser(): UserModel | undefined {
    const firstName = localStorage.getItem('firstName');
    const lastName = localStorage.getItem('lastName');
    const email = localStorage.getItem('email');
    const imageUrl = localStorage.getItem('imageUrl');
    const roles = localStorage.getItem('roles');

    if (email && roles) {
      const user: UserModel = {
        firstName: firstName ? firstName : undefined,
        lastName: lastName ? lastName : undefined,
        email: email,
        imageUrl: imageUrl ? imageUrl : undefined,
        roles: roles.split(',')
      };

      return user;
    }

    return undefined;
  
  }

  setUser(user: UserModel): void {
    this.user$.next(user);

    localStorage.setItem('email', user.email);

    if (user.firstName) {
      localStorage.setItem('firstName', user.firstName);
    }

    if (user.lastName) {
      localStorage.setItem('lastName', user.lastName);
    }

    if (user.imageUrl) {
      localStorage.setItem('imageUrl', user.imageUrl);
    }

    localStorage.setItem('roles', user.roles.join(','));
  }

  user(): Observable<UserModel | undefined> {
    return this.user$.asObservable();
  }

  override getResourceUrl(): string {
      return 'api';
  }
}
