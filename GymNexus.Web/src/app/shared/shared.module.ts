import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RelativeTimePipe } from './relative-time.pipe';
import { CloudinaryModule } from '@cloudinary/ng';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { MaterialModule } from './material.module';
import { ProfileDialogComponent } from './profile-dialog/profile-dialog.component';

@NgModule({
    declarations: [RelativeTimePipe, ConfirmDialogComponent, ProfileDialogComponent],
    imports: [CommonModule, FormsModule, ReactiveFormsModule, CloudinaryModule, MaterialModule],
    exports: [CommonModule, FormsModule, ReactiveFormsModule, RelativeTimePipe, CloudinaryModule, ConfirmDialogComponent],
})
export class SharedModule {}
