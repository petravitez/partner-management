// partner-details-modal.component.ts
import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PartnerDetails } from '../../models/partner.details.model';

@Component({
  standalone: true,
  selector: 'app-partner-details-modal',
  imports: [CommonModule, NgbModule],
  templateUrl: './partner-details-modal.component.html'
})
export class PartnerDetailsModalComponent {
  @Input() partner!: PartnerDetails;

  constructor(public activeModal: NgbActiveModal) {}
}
