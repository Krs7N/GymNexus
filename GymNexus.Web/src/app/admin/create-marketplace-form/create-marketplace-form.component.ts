import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-create-marketplace-form',
  templateUrl: './create-marketplace-form.component.html',
  styleUrls: ['./create-marketplace-form.component.scss']
})
export class CreateMarketplaceFormComponent implements OnInit {

  marketplaceForm: FormGroup = new FormGroup({});

  constructor(public dialogRef: MatDialogRef<CreateMarketplaceFormComponent>) { }

  ngOnInit(): void {
      this.marketplaceForm = new FormGroup({
        name: new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(50)]),
        description: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(500)]),
        address: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(150)]),
      });
  }

  onSubmit(): void {
    if (this.marketplaceForm.valid) {
      this.dialogRef.close(this.marketplaceForm.value);
    }
  }
}
