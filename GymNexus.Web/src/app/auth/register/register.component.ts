declare var cloudinary: any;

import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerForm: FormGroup = new FormGroup({});

  constructor(
    private fb: FormBuilder,
    private _authService: AuthService,
    private _snackbarService: SnackbarService,
    private _router: Router) { }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
      imageUrl: [null]
    }, { validators: this.passwordMatcher });
  }

  passwordMatcher(control: AbstractControl): { [key: string]: boolean } | null {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');
    if (password && confirmPassword && password.value !== confirmPassword.value) {
      return { 'passwordMismatch': true };
    }
    return null;
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
        if (result && result.event === "success") {
          this.registerForm.get('imageUrl')?.setValue(result.info.secure_url);
        }
      });
  }

  onRegister(): void {
    if (this.registerForm.valid) {
      const registerModel = { email: this.registerForm.value.email, password: this.registerForm.value.password, imageUrl: this.registerForm.value.imageUrl };
      this._authService.register(registerModel).subscribe({
        next: () => {
          this._snackbarService.openSuccess('Registration successful', 'Okay');
        },
        error: (e) => {
          if (e.error.errors.incorrectPasswordFormat) {
            const errorMessage = e.error.errors.incorrectPasswordFormat.join();
            this._snackbarService.openError(errorMessage, undefined, 6000);
          }
        }
      });
    }
  }
}
