import { Injectable, Injector } from '@angular/core';
import { CrudService } from '../core/services/crud.service';
import { Observable } from 'rxjs';
import { OrderDetailsModel } from './order-details-model';

@Injectable({
  providedIn: 'root'
})
export class AdminService extends CrudService<any> {

  constructor(injector: Injector) {
    super(injector)
  }

  getAllOrders(): Observable<OrderDetailsModel[]> {
    return this.httpClient.get<OrderDetailsModel[]>(`${this.APIUrl}/orders/details`);
  }

  getAllOrdersCount(): Observable<number> {
    return this.httpClient.get<number>(`${this.APIUrl}/orders`);
  }

  getAllPendingOrdersCount(): Observable<number> {
    return this.httpClient.get<number>(`${this.APIUrl}/orders/pending`);
  }

  getAllConfirmedOrdersCount(): Observable<number> {
    return this.httpClient.get<number>(`${this.APIUrl}/orders/confirmed`);
  }

  getAllCompletedOrdersCount(): Observable<number> {
    return this.httpClient.get<number>(`${this.APIUrl}/orders/completed`);
  }

  override getResourceUrl(): string {
    return 'admin';
  }
}
