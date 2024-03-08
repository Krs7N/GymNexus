import { AfterViewInit, Component, OnDestroy } from '@angular/core';
import * as L from 'leaflet';
import { MarketplaceService } from './marketplace.service';
import { Subject, takeUntil } from 'rxjs';
import { SnackbarService } from 'src/app/shared/snackbar.service';

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
export class MarketplaceMapComponent implements AfterViewInit, OnDestroy {

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(
    private _marketplaceService: MarketplaceService,
    private _snackbarService: SnackbarService) { }

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
          With additional information: <br>${marketplace.description}`);
        })
      },
      error: (e) => {
        this._snackbarService.openError(e.error?.errors?.message[0], 'Okay');
      }
    });
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
