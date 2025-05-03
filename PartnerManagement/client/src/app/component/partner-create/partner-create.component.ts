import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PartnerService } from '../../service/partner.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-partner-create',
  templateUrl: './partner-create.component.html',
  styleUrls: ['./partner-create.component.css'],
  imports: [CommonModule, ReactiveFormsModule],
})
export class PartnerCreateComponent implements OnInit {
  partnerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private partnerService: PartnerService,
    private router: Router
  ) {
    this.partnerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      partnerNumber: ['', [Validators.required, Validators.pattern(/^[0-9]{20}$/)]],
      croatianPIN: ['', Validators.pattern(/^[0-9]{11}$/)],
      partnerTypeId: ['', Validators.required],
      createdByUser: ['', [Validators.required, Validators.email]],
      isForeign: [false, Validators.required],
      externalCode: ['', Validators.required],
      genderId: ['', Validators.required],
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.partnerForm.invalid) {
      return;
    }

    this.partnerService.createPartner(this.partnerForm.value).subscribe({
      next: (newPartner) => {
        console.log('tu sam')
        console.log(newPartner)
        this.router.navigate(['/partners'], {
          state: { newPartnerId: newPartner.id }
        });
      },
      error: (err) => {
        console.error('Error creating partner:', err);
      }
    });
  }
}
