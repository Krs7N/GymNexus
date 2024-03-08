import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserModel } from '../auth/models/user-model';
import { AuthService } from '../auth/services/auth.service';
import { Subject, takeUntil } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit, OnDestroy {

  user?: UserModel;

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(private _authService: AuthService, private _router: Router) { }

  ngOnInit(): void {
    this._authService.user().pipe(takeUntil(this._unsubscribeAll)).subscribe(user => {
      this.user = user;
    });

    this.user = this._authService.getUser();
  }

  logout(): void {
    this._authService.logout();
    this._router.navigate(['/']);
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
