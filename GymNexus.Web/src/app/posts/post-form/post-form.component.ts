declare var cloudinary: any;

import { Component, EventEmitter, Inject, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { PostsService } from '../posts.service';
import { SnackbarService } from 'src/app/shared/snackbar.service';
import { PostViewModel } from '../post-view-model';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-post-form',
  templateUrl: './post-form.component.html',
  styleUrls: ['./post-form.component.scss']
})
export class PostFormComponent implements OnInit, OnDestroy {
  @Output() postAdded: EventEmitter<void> = new EventEmitter<void>();

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  postForm: FormGroup = new FormGroup({});

  constructor(
    private _fb: FormBuilder,
    private _postsService: PostsService,
    private _snackbarService: SnackbarService,
    public dialogRef: MatDialogRef<PostFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {}

  ngOnInit(): void {
    this.postForm = this._fb.group({
      title: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(50)]],
      content: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(500)]],
      imageUrl: [null]
    });

    if (this.data.post) {
      this.postForm.patchValue({
        title: this.data.post.title,
        content: this.data.post.content,
        imageUrl: this.data.post.imageUrl
      });
    }
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

      if (this.data.post) {
        this._postsService.update(this.data.post.id, this.postForm.value).subscribe({
          next: () => {
            this._snackbarService.openSuccess("Your post was successfully updated", "Okay");
            this.postAdded.emit();
          },
          error: (e) => {
            this._snackbarService.openError(e.error?.errors?.message[0], "Okay");
          }
        });
      } else {
        this._postsService.create(this.postForm.value).subscribe({
          next: () => {
            this._snackbarService.openSuccess("Your new post was successfully created", "Okay");
            this.postAdded.emit();
          },
          error: (e) => {
            this._snackbarService.openError(e.error?.errors?.message[0], "Okay");
          }
        });
      }
      
      this.dialogRef.close();
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
