import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderDetailsModel } from '../order-details-model';

@Component({
  selector: 'app-admin-manage-orders',
  templateUrl: './admin-manage-orders.component.html',
  styleUrls: ['./admin-manage-orders.component.scss']
})
export class AdminManageOrdersComponent implements OnInit, OnDestroy {

  orders: OrderDetailsModel[] = [];

  constructor(
    private _route: ActivatedRoute
  ) { }

  ngOnInit(): void {
      this.orders = this._route.snapshot.data["orders"];
  }

  getStatusColor(status: string): string {
    switch (status) {
      case 'pending':
        return 'orange';
      case 'confirmed':
        return 'blue';
      case 'completed':
        return 'green';
      default:
        return 'inherit';
    }
  }

  ngOnDestroy(): void {
      
  }

}
