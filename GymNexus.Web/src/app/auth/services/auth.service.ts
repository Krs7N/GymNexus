import { Injectable, Injector } from '@angular/core';
import { CrudService } from 'src/app/core/services/crud.service';
import { RegisterModel } from '../models/register-model';
import { LoginModel } from '../models/login-model';
import { Observable } from 'rxjs';
import { RegisterResponseModel } from '../models/register-response-model';
import { LoginResponseModel } from '../models/login-response-model';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends CrudService<RegisterModel | LoginModel>{

  constructor(injector: Injector) {
    super(injector);
  }

  register(registerModel: RegisterModel): Observable<RegisterResponseModel> {
    return this.httpClient.post<RegisterResponseModel>(`${environment.apiBaseUrl}/${this.getResourceUrl()}/register`, registerModel);
  }

  login(loginModel: LoginModel): Observable<LoginResponseModel> {
    return this.httpClient.post<LoginResponseModel>(`${environment.apiBaseUrl}/${this.getResourceUrl()}/login`, loginModel);
  }

  override getResourceUrl(): string {
      return 'api';
  }
}
