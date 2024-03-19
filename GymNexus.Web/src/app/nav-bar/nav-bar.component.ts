import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserModel } from '../auth/models/user-model';
import { AuthService } from '../auth/services/auth.service';
import { Subject, takeUntil } from 'rxjs';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../shared/confirm-dialog/confirm-dialog.component';
import { ProfileDialogComponent } from '../shared/profile-dialog/profile-dialog.component';
import { SnackbarService } from '../shared/snackbar.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit, OnDestroy {

  user?: UserModel;

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(private _authService: AuthService, private _snackbarService: SnackbarService,
    private _router: Router, public dialog: MatDialog) { }

  ngOnInit(): void {
    this._authService.user().pipe(takeUntil(this._unsubscribeAll)).subscribe(user => {
      this.user = user;
    });

    this.user = this._authService.getUser();
  }

  logout(): void {

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Logout',
        message: 'Are you sure you want to logout?'
      }
    });
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._authService.logout().subscribe({
          next: () => {
            this._snackbarService.openSuccess('You have been logged out');
          },
          error: (e) => {
            this._snackbarService.openError(e.error.errors.message[0], 'Okay');
          }
        });
        this._router.navigate(['/login']);
      }
    });
  }

  navigateToProfile(): void {
    this.dialog.open(ProfileDialogComponent, {
      width: '400px',
      data: this.user
    });
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
