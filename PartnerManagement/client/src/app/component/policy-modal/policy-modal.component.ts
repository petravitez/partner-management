import { Component, Input } from '@angular/core';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PartnerService } from '../../service/partner.service';
import { CreatePolicy } from '../../models/policy-create.model';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-policy-modal',
  templateUrl: './policy-modal.component.html',
  imports: [ReactiveFormsModule, CommonModule, NgbModule]
})
export class PolicyModalComponent {
  partnerForm: FormGroup;
  partners: any[] = [];

  constructor(
    public activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private partnerService: PartnerService,
  ) {
    this.partnerForm = this.fb.group({
      partnerId: [null, Validators.required],
      policyNumber: ['', Validators.required],
      policyAmount: [0, [Validators.required, Validators.min(1)]]
    });

    this.loadPartners();
  }

  loadPartners(): void {
    this.partnerService.getPartners().subscribe(data => this.partners = data);
  }

  savePolicy(): void {
    if (this.partnerForm.invalid) return;

    const policy: CreatePolicy = this.partnerForm.value;
    this.partnerService.createPolicy(policy.partnerId, policy).subscribe(() => {
      this.activeModal.close('saved');
    });
  }
}
