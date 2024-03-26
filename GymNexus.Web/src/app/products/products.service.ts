import { Injectable, Injector } from '@angular/core';
import { CrudService } from '../core/services/crud.service';
import { ProductModel } from './product-model';
import { Observable } from 'rxjs';
import { ProductViewModel } from './product-view-model';
import { StoreViewModel } from '../shared/models/store-view-model';

@Injectable({
  providedIn: 'root'
})
export class ProductsService extends CrudService<ProductModel> {

  constructor(injector: Injector) { 
    super(injector);
  }

  getAllProducts(): Observable<ProductViewModel[]> {
    return this.httpClient.get<ProductViewModel[]>(`${this.APIUrl}`);
  }

  toggleProductLike(id: number) : Observable<boolean> {
    return this.httpClient.put<boolean>(`${this.APIUrl}/${id}/like`, null);
  }

  getProduct(id: number): Observable<ProductViewModel> {
    return this.httpClient.get<ProductViewModel>(`${this.APIUrl}/${id}`);
  }

  override getResourceUrl(): string {
    return 'products';
  }
}
