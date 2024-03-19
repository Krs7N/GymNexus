import { Injectable, Injector } from '@angular/core';
import { CrudService } from 'src/app/core/services/crud.service';
import { UserModel } from '../models/user-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfileService extends CrudService<UserModel> {

  constructor(injector: Injector) {
    super(injector);
  }

  updateProfilePicture(model: { email: string, imageUrl: string }): Observable<UserModel> {
    return this.httpClient.put<UserModel>(`${this.APIUrl}/update`, model);
  }

  override getResourceUrl(): string {
    return 'profile';
  }
}
