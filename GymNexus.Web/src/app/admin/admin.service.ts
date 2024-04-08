import { Injectable, Injector } from '@angular/core';
import { CrudService } from '../core/services/crud.service';
import { Observable } from 'rxjs';
import { OrderDetailsModel } from './order-details-model';
import { HttpHeaders } from '@angular/common/http';
import { PostOverviewModel } from '../posts/post-overview-model';

@Injectable({
  providedIn: 'root'
})
export class AdminService extends CrudService<any> {

  constructor(injector: Injector) {
    super(injector)
  }

  getMostLikedPost(): Observable<PostOverviewModel> {
    return this.httpClient.get<PostOverviewModel>(`${this.APIUrl}/posts/mostLiked`);
  }

  getMostCommentedPost(): Observable<PostOverviewModel> {
    return this.httpClient.get<PostOverviewModel>(`${this.APIUrl}/posts/mostCommented`);
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

  changeOrderStatus(id: number, status: string): Observable<string> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    const body = JSON.stringify(status);
    return this.httpClient.put<string>(`${this.APIUrl}/orders/${id}/changeStatus`, body, { headers });
  }

  override getResourceUrl(): string {
    return 'admin';
  }
}
