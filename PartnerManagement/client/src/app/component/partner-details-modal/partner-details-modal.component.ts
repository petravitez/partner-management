// partner-details-modal.component.ts
import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PartnerDetails } from '../../models/partner.details.model';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
  standalone: true,
  selector: 'app-partner-details-modal',
  imports: [CommonModule, NgbModule, TranslocoModule],
  templateUrl: './partner-details-modal.component.html'
})
export class PartnerDetailsModalComponent {
  @Input() partner!: PartnerDetails;

  constructor(public activeModal: NgbActiveModal) {}

  getPartnerTypeLabel(typeId: number): string {
    switch (typeId) {
      case 1: return 'partnerCreate.personal';
      case 2: return 'partnerCreate.legal';
      default: return 'common.unknown';
    }
  }
}
