import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { AdminService } from '../admin.service';

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
    // const dialogRef = this.dialog.open(ConfirmDialogComponent, {
    //   width: '400px',
    //   data: {
    //     title: 'Delete Post',
    //     message: 'Are you sure you want to delete this post?'
    //   }
    // });

    // dialogRef.afterClosed().subscribe(result => {
    //   if (result) {
    //     this._postsService.delete(id).pipe(takeUntil(this._unsubscribeAll)).subscribe({
    //       next: () => {
    //         this._snackbarService.openSuccess('Post deleted successfully');
    //         this._router.navigate(['/posts']);
    //       },
    //       error: (e) => {
    //         this.error = e;
    //       },
    //     });
    //   }
    // });
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
