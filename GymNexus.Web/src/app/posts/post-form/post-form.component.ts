declare var cloudinary: any;

import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { PostsService } from '../posts.service';
import { SnackbarService } from 'src/app/shared/snackbar.service';

@Component({
  selector: 'app-post-form',
  templateUrl: './post-form.component.html',
  styleUrls: ['./post-form.component.scss']
})
export class PostFormComponent {
  @Output() postAdded: EventEmitter<void> = new EventEmitter<void>();

  postForm: FormGroup = new FormGroup({});

  constructor(
    private _fb: FormBuilder,
    private _postsService: PostsService,
    private _snackbarService: SnackbarService,
    public dialogRef: MatDialogRef<PostFormComponent>) {}

  ngOnInit(): void {
    this.postForm = this._fb.group({
      title: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(50)]],
      content: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(500)]],
      imageUrl: [null]
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
          this.postForm.get('imageUrl')?.setValue(result.info.secure_url);
        }
      });
  }

  onSubmit(): void {
    if (this.postForm.valid) {
      this._postsService.create(this.postForm.value).subscribe({
        next: () => {
          this._snackbarService.openSuccess("Your new post was successfully created", "Okay");
          this.postAdded.emit();
        },
        error: (e) => {
          this._snackbarService.openError(e.error?.errors?.message[0], "Okay");
        }
      });
      this.dialogRef.close();
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
