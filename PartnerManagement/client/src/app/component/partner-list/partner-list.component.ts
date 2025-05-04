import { Component, signal, computed, effect } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PartnerDetails } from '../../models/partner.details.model';
import { PartnerService } from '../../service/partner.service';
import { PolicyModalComponent } from '../policy-modal/policy-modal.component';
import { PartnerDetailsModalComponent } from '../partner-details-modal/partner-details-modal.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-partner-list',
  templateUrl: './partner-list.component.html',
  standalone: true,
  imports: [CommonModule]
})
export class PartnerListComponent {
  partners = signal<PartnerDetails[]>([]);
  newPartnerId = signal<number | null>(null);

  constructor(
    private partnerService: PartnerService,
    private router: Router,
    private modalService: NgbModal
  ) {
    const nav = this.router.getCurrentNavigation();
    const id = nav?.extras.state?.['newPartnerId'] ?? null;

    if (id !== null) {
      this.newPartnerId.set(id);
      setTimeout(() => this.newPartnerId.set(null), 3000);
    }

    this.loadPartners();

    effect(() => {
      const current = this.partners();
    });
  }

  createPartner() {
    this.router.navigate(['/partners/create']);
  }

  isNewPartner = (id: number): boolean => {
    return this.newPartnerId() === id;
  };

  loadPartners(): void {
    this.partnerService.getPartners().subscribe({
      next: (data) => {
        this.partners.set(data);
      },
      error: (err) => {
        console.error('Failed to load partners:', err);
      }
    });
  }

  openPolicyModal(): void {
    const modalRef = this.modalService.open(PolicyModalComponent, { size: 'lg' });

    modalRef.result.then(result => {
      if (result === 'saved') {
        this.loadPartners();
      }
    }).catch(() => {});
  }

  openPartnerDetails(partner: PartnerDetails): void {
    const modalRef = this.modalService.open(PartnerDetailsModalComponent, { size: 'lg' });
    modalRef.componentInstance.partner = partner;
  }
}
