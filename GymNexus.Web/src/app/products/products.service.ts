import { Injectable, Injector } from '@angular/core';
import { CrudService } from '../core/services/crud.service';
import { ProductModel } from './product-model';
import { Observable } from 'rxjs';
import { ProductViewModel } from './product-view-model';

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

  override getResourceUrl(): string {
    return 'products';
  }
}
