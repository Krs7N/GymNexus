import { Injectable, Injector } from '@angular/core';
import { CrudService } from '../core/services/crud.service';
import { StoreViewModel } from '../shared/models/store-view-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StoresService extends CrudService<StoreViewModel> {

  constructor(injector: Injector) {
    super(injector);
  }

  getStoresByMarketplace(marketplaceId: number | undefined): Observable<StoreViewModel[]> {
    return this.httpClient.get<StoreViewModel[]>(`${this.APIUrl}/${marketplaceId}`);
  }

  override getResourceUrl(): string {
      return 'stores';
  }
}
