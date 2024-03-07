import { AfterViewInit, Component } from '@angular/core';
import * as L from 'leaflet';

@Component({
  selector: 'app-marketplace-map',
  templateUrl: './marketplace-map.component.html',
  styleUrls: ['./marketplace-map.component.scss']
})
export class MarketplaceMapComponent implements AfterViewInit {

  constructor() { }

  ngAfterViewInit(): void {
    const map = L.map('map').setView([42.7339, 25.4858], 8);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);
  }
}
