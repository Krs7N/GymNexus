import { Injectable, Injector } from '@angular/core';
import { CrudService } from 'src/app/core/services/crud.service';
import { NomenclatureModel } from '../models/nomenclature-model';
import { CategoryViewModel } from '../models/category-view-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NomenclatureService extends CrudService<NomenclatureModel> {

  constructor(injector: Injector) { 
    super(injector);
  }

  getCategories(): Observable<CategoryViewModel[]> {
    return this.httpClient.get<CategoryViewModel[]>(`${this.APIPrefix}products/categories`);
  }
}
