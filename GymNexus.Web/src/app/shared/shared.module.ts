import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RelativeTimePipe } from './relative-time.pipe';
import { CloudinaryModule } from '@cloudinary/ng';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { MaterialModule } from './material.module';
import { ProfileDialogComponent } from './profile-dialog/profile-dialog.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { RouterModule } from '@angular/router';

@NgModule({
    declarations: [RelativeTimePipe, ConfirmDialogComponent, ProfileDialogComponent, NotFoundComponent],
    imports: [CommonModule, FormsModule, ReactiveFormsModule, CloudinaryModule, MaterialModule, RouterModule],
    exports: [CommonModule, FormsModule, ReactiveFormsModule, RelativeTimePipe, CloudinaryModule, ConfirmDialogComponent, NotFoundComponent],
})
export class SharedModule {}
