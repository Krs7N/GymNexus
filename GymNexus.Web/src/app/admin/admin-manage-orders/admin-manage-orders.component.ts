import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderDetailsModel } from '../order-details-model';
import { AdminService } from '../admin.service';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-admin-manage-orders',
  templateUrl: './admin-manage-orders.component.html',
  styleUrls: ['./admin-manage-orders.component.scss']
})
export class AdminManageOrdersComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  orders: OrderDetailsModel[] = [];

  constructor(
    private _route: ActivatedRoute,
    private _snackbarService: SnackbarService,
    private _adminService: AdminService
  ) { }

  ngOnInit(): void {
      this.orders = this._route.snapshot.data["orders"];
  }

  changeOrderStatus(id: number, status: string) {
    this._adminService.changeOrderStatus(id, status).pipe(takeUntil(this._unsubscribeAll)).subscribe({
      next: (newStatus: string) => {
        this.refresh();
        this._snackbarService.openSuccess(`The status of the order was successfully changed from ${status} to ${newStatus}.`);
      },
      error: () => {
        this._snackbarService.openError("An error occured while changing the status. Please try again later!");
      }
    });
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

  refresh() {
    this._adminService.getAllOrders().pipe(takeUntil(this._unsubscribeAll)).subscribe(orders => {
      this.orders = orders;
    });
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
