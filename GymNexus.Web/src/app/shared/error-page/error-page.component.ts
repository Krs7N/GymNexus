import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared.module';
import { MaterialModule } from '../material.module';

@Component({
  selector: 'app-error-page',
  standalone: true,
  templateUrl: './error-page.component.html',
  styleUrls: ['./error-page.component.scss'],
  imports: [
    SharedModule,
    MaterialModule,
    RouterModule
  ]
})
export class ErrorPageComponent {
  @Input() error: any;
}
