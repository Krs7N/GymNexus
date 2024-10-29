import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';


@Injectable({
  providedIn: 'root'
})

export class SnackbarService {

  constructor(private _snackbar: MatSnackBar) { }

  openSuccess(message: string, action?: string) {
    this.openSnackBar(message, 'success', action);
  }

  openWarning(message: string, action?: string) {
    this.openSnackBar(message, 'warning', action);
  }

  openError(message: string, action?: string, duration?: number) {
    this.openSnackBar(message, 'error', action, duration);
  }

  private openSnackBar(message: string, className: 'error' | 'success' | 'warning', action?: string, duration: number = 3000) {
    this._snackbar.open(message, action, {
      duration: duration,
      panelClass: [className]
    });
  }
}
