import { Component, Input } from '@angular/core';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PartnerService } from '../../service/partner.service';
import { CommonModule } from '@angular/common';
import { markFormGroupTouched } from '../../validators/update-form';
import { CreatePolicyRequest } from '../../models/policy-create.model';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
  standalone: true,
  selector: 'app-policy-modal',
  templateUrl: './policy-modal.component.html',
  imports: [ReactiveFormsModule, CommonModule, NgbModule, TranslocoModule]
})
export class PolicyModalComponent {
  policyForm: FormGroup;
  partners: any[] = [];

  constructor(
    public activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private partnerService: PartnerService,
  ) {
    this.policyForm = this.fb.group({
      partnerId: [null, Validators.required],
      policyNumber: ['', [
        Validators.required,
        Validators.minLength(10),
        Validators.maxLength(15)
      ]],
      policyAmount: [0, [Validators.required]]
    });
    
    this.loadPartners();
  }

  loadPartners(): void {
    this.partnerService.getPartnerDropdown().subscribe(data => this.partners = data);
  }

  savePolicy(): void {
    markFormGroupTouched(this.policyForm);
     
       if (this.policyForm.invalid) {
         return;
       }

    const policy: CreatePolicyRequest = this.policyForm.value;
    this.partnerService.createPolicy(policy.partnerId, policy).subscribe(() => {
      this.activeModal.close('saved');
    });
  }
}
