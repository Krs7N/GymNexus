import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import * as L from 'leaflet';
import { MarketplaceService } from './marketplace.service';
import { Subject, takeUntil } from 'rxjs';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/auth/services/auth.service';

const iconRetinaUrl = 'assets/marker-icon-2x.png';
const iconUrl = 'assets/marker-icon.png';
const shadowUrl = 'assets/marker-shadow.png';
const iconDefault = L.icon({
  iconRetinaUrl,
  iconUrl,
  shadowUrl,
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  tooltipAnchor: [16, -28],
  shadowSize: [41, 41]
});
L.Marker.prototype.options.icon = iconDefault;

@Component({
  selector: 'app-marketplace-map',
  templateUrl: './marketplace-map.component.html',
  styleUrls: ['./marketplace-map.component.scss']
})
export class MarketplaceMapComponent implements OnInit, AfterViewInit, OnDestroy {

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(
    private _router: Router,
    private _route: ActivatedRoute,
    private _authService: AuthService,
    private _marketplaceService: MarketplaceService,
    private _snackbarService: SnackbarService) { }

  ngOnInit(): void {
    if (this._route.snapshot.fragment) {
      this._authService.getExternalLoginData(this._route.snapshot.fragment).pipe(
        takeUntil(this._unsubscribeAll)
      ).subscribe({
        next: (res) => {
          this._authService.setUser(res);
          this._router.navigate(['/']);
          this._snackbarService.openSuccess('Login successful', 'Okay');
        }
      });
    }
  }

  ngAfterViewInit(): void {
    const map = L.map('map').setView([42.7339, 25.4858], 8);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    this._marketplaceService.getAllMarketplaces().pipe(takeUntil(this._unsubscribeAll)).subscribe({
      next: (marketplaces) => {
        marketplaces.forEach(marketplace => {
          const marker = L.marker([marketplace.latitude, marketplace.longitude]).addTo(map);
          marker.bindPopup(`This is global marketplace partner of GymNexus:<br><br>
          Name: <b>${marketplace.name}</b><br>
          Address: <b>${marketplace.address}</b><br><br>
          Additional information: <br>${marketplace.description}`);
        })
      },
      error: (e) => {
        if (e.error && e.error.errors) {
          this._snackbarService.openError(e.error?.errors?.message[0], 'Okay');
        } else {
          this._snackbarService.openError('An error occurred while loading data. Please try again later');
        }
      }
    });
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
