declare var cloudinary: any;

import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserModel } from 'src/app/auth/models/user-model';
import { ProfileService } from 'src/app/auth/services/profile.service';
import { SnackbarService } from '../services/snackbar.service';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-profile-dialog',
  templateUrl: './profile-dialog.component.html',
  styleUrls: ['./profile-dialog.component.scss']
})
export class ProfileDialogComponent implements OnInit {
  profileForm: FormGroup = new FormGroup({});

  constructor(
    private _fb: FormBuilder,
    private _profileService: ProfileService,
    private _snackbarService: SnackbarService,
    private _authService: AuthService,
    public dialogRef: MatDialogRef<ProfileDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserModel,
  ) { }

  ngOnInit(): void {
      this.profileForm = this._fb.group({
        firstName: [this.data.firstName, [Validators.minLength(3), Validators.maxLength(12)]],
        lastName: [this.data.lastName, [Validators.minLength(5), Validators.maxLength(15)]],
        email: [{value: this.data.email, disabled: true}, Validators.required],
        imageUrl: [this.data.imageUrl]
      });
  }

  openCloudinaryUploader(): void {
    cloudinary.openUploadWidget({
      cloudName: 'dekvgy42s',
      uploadPreset: 'gymnexus',
      sources: ['local', 'url', 'camera', 'image_search'],
      showAdvancedOptions: false,
      cropping: true,
      multiple: false,
      defaultSource: 'local',
      styles: {
        palette: {
          window: "#FFFFFF",
          windowBorder: "#90A0B3",
          tabIcon: "#0078FF",
          menuIcons: "#5A616A",
          textDark: "#000000",
          textLight: "#FFFFFF",
          link: "#0078FF",
          action: "#FF620C",
            inactiveTabIcon: "#0E2F5A",
            error: "#F44235",
            inProgress: "#0078FF",
            complete: "#20B832",
            sourceBg: "#E4EBF1"
          },
          fonts: {
            default: null,
            "'Fira Sans', sans-serif": {
              url: "https://fonts.googleapis.com/css?family=Fira+Sans",
              active: true
            }
          }
        }
      }, (error: Error, result: any) => {
        if (!error && result && result.event === "success") {
          this.profileForm.get('imageUrl')?.setValue(result.info.secure_url);
          this.profileForm.markAsDirty();
        }
      });
  }

  save(): void {
    if (this.profileForm.valid) {
      this._profileService.updateProfile(this.profileForm.getRawValue()).subscribe({
        next: (user) => {
          this._snackbarService.openSuccess('Profile updated successfully');
          this._authService.setUser(user);
          this.dialogRef.close();
        },
        error: (e) => {
          console.error(e);
        }
      });
    }
  }
}
