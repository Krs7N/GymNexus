import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RelativeTimePipe } from './relative-time.pipe';
import { CloudinaryModule } from '@cloudinary/ng';

@NgModule({
    declarations: [RelativeTimePipe],
    imports: [CommonModule, FormsModule, ReactiveFormsModule, CloudinaryModule],
    exports: [CommonModule, FormsModule, ReactiveFormsModule, RelativeTimePipe, CloudinaryModule],
})
export class SharedModule {}
