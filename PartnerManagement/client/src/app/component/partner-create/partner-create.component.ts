import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PartnerService } from '../../service/partner.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { oibValidator } from '../../validators/oib-validator';
import { markFormGroupTouched } from '../../validators/update-form';
import { externalCodeUniqueValidator } from '../../validators/external-code.validator';

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
      firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(255)]],
      lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(255)]],
      address: [''], 
      partnerNumber: ['', [Validators.required, Validators.pattern(/^\d{20}$/)]],
      croatianPIN: ['', [Validators.pattern(/^\d{11}$/), oibValidator()]],
      partnerTypeId: ['', Validators.required],
      createdAtUtc: [new Date().toISOString()], 
      createdByUser: ['', [Validators.required, Validators.email, Validators.maxLength(255)]],
      isForeign: [false, Validators.required],
      externalCode: [
        '',
        [
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(20),
          Validators.pattern(/^[a-zA-Z0-9]*$/)
        ],
        [externalCodeUniqueValidator()] 
      ],
      genderId: ['', Validators.required], 
    });
    
  }

  ngOnInit(): void {}

  onSubmit(): void {
    markFormGroupTouched(this.partnerForm);
  
    if (this.partnerForm.invalid) {
      return;
    }
  
    this.partnerService.createPartner(this.partnerForm.value).subscribe({
      next: (newPartner) => {
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
