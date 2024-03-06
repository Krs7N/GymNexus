import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-post-form',
  templateUrl: './post-form.component.html',
  styleUrls: ['./post-form.component.scss']
})
export class PostFormComponent {
  postForm: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.postForm = this.fb.group({
      title: [''],
      content: ['']
      // Add other form controls as needed
    });
  }

  onSubmit(): void {
    // Handle form submission, e.g., call a service to save the new post
    console.log(this.postForm.value);
  }

  onCancel(): void {
    // Optional: Handle the cancellation logic
  }
}
