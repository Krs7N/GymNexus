import { Injectable, Injector } from '@angular/core';
import { CrudService } from 'src/app/core/services/crud.service';
import { MarketplaceModel } from './marketplace-model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class MarketplaceService extends CrudService<MarketplaceModel> {

  constructor(injector: Injector) {
    super(injector);
  }

  getAllMarketplaces(): Observable<MarketplaceModel[]> {
    return this.httpClient.get<MarketplaceModel[]>(`${environment.apiBaseUrl}/${this.getResourceUrl()}`);
  }

  override getResourceUrl(): string {
    return 'api/marketplaces';
  }
}
