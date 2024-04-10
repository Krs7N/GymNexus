import { Injectable, Injector } from '@angular/core';
import { CrudService } from 'src/app/core/services/crud.service';
import { UserModel } from '../models/user-model';
import { Observable } from 'rxjs';
import { StoreViewModel } from 'src/app/shared/models/store-view-model';

@Injectable({
  providedIn: 'root'
})
export class ProfileService extends CrudService<UserModel> {

  constructor(injector: Injector) {
    super(injector);
  }

  updateProfile(model: { firstName: string, lastName: string, email: string, imageUrl: string }): Observable<UserModel> {
    debugger
    return this.httpClient.put<UserModel>(`${this.APIUrl}/update`, model);
  }

  getUserStores(): Observable<StoreViewModel[]> {
    return this.httpClient.get<StoreViewModel[]>(`${this.APIUrl}/stores`);
  }

  override getResourceUrl(): string {
    return 'profile';
  }
}
