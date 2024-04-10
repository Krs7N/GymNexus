import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { AdminService } from '../admin.service';
import { MatDialog } from '@angular/material/dialog';
import { CreateMarketplaceFormComponent } from '../create-marketplace-form/create-marketplace-form.component';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss']
})
export class AdminDashboardComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  allOrders: number = 0;
  pendingOrders: number = 0;
  confirmedOrders: number = 0;
  completedOrders: number = 0;
  error: any;

  constructor(
    public dialog: MatDialog,
    private _snackbarService: SnackbarService,
    private _router: Router,
    private _adminService: AdminService
  ) { }

  ngOnInit(): void {
    forkJoin([
      this._adminService.getAllOrdersCount(),
      this._adminService.getAllPendingOrdersCount(),
      this._adminService.getAllConfirmedOrdersCount(),
      this._adminService.getAllCompletedOrdersCount(),
    ]).pipe(takeUntil(this._unsubscribeAll)).subscribe({
      next: ([allOrders, pendingOrders, confirmedOrders, completedOrders]) => {
        this.allOrders = allOrders;
        this.pendingOrders = pendingOrders;
        this.confirmedOrders = confirmedOrders;
        this.completedOrders = completedOrders;
      },
      error: (e) => {
        this.error = e;
      }
    });
  }

  addMarketplace(): void {
    const dialogRef = this.dialog.open(CreateMarketplaceFormComponent, {
      width: '500px'
    });

    dialogRef.afterClosed().subscribe(result => {
      debugger
      if (result) {
        this._adminService.addMarketplace(result).pipe(takeUntil(this._unsubscribeAll)).subscribe({
          next: () => {
            this._snackbarService.openSuccess('Successfully added new global marketplace partner to GymNexus!');
            this._router.navigate(['/map']);
          },
          error: (e) => {
            this.error = e;
          },
        });
      }
    });
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
