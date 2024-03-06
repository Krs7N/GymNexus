import { Component } from '@angular/core';
import { Cloudinary } from '@cloudinary/url-gen';

export const cldinary = new Cloudinary({cloud: { cloudName: 'dekvgy42s'}});

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'GymNexus';
}
