import { Component, OnInit, signal, computed, inject } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import { PartnerService } from '../../service/partner.service';
import { PartnerDetails } from '../../models/partner.details.model';
import { PolicyModalComponent } from '../policy-modal/policy-modal.component';
import { PartnerDetailsModalComponent } from '../partner-details-modal/partner-details-modal.component';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
  selector: 'app-partner-list',
  templateUrl: './partner-list.component.html',
  standalone: true,
  imports: [CommonModule, TranslocoModule]
})
export class PartnerListComponent implements OnInit {
  partners = signal<PartnerDetails[]>([]);
  totalCount = signal(0);
  currentPage = signal(1);
  pageSize = 10;
  newPartnerId = signal<number | null>(null);

  readonly totalPages = computed(() =>
    Math.ceil(this.totalCount() / this.pageSize)
  );

  private partnerService = inject(PartnerService);
  private router = inject(Router);
  private modalService = inject(NgbModal);

  ngOnInit(): void {
    const nav = this.router.getCurrentNavigation();
    const id = nav?.extras.state?.['newPartnerId'] ?? null;
    this.loadPartners(this.currentPage(), id);
  }

  loadPartners(page: number, highlightId: number | null = null): void {
    this.partnerService.getPartners({ page, pageSize: this.pageSize }).subscribe({
      next: (res) => {
        this.partners.set(res.items);
        this.totalCount.set(res.totalCount);

        if (highlightId !== null) {
          this.newPartnerId.set(highlightId);
          setTimeout(() => this.newPartnerId.set(null), 3000);
        }
      },
      error: (err) => console.error('Failed to load partners:', err)
    });
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages()) return;
    this.currentPage.set(page);
    this.loadPartners(page);
  }

  createPartner(): void {
    this.router.navigate(['/partners/create']);
  }

  openPolicyModal(): void {
    const modalRef = this.modalService.open(PolicyModalComponent, { size: 'lg' });
    modalRef.result.then(result => {
      if (result === 'saved') this.loadPartners(this.currentPage());
    }).catch(() => {});
  }

  openPartnerDetails(partner: PartnerDetails): void {
    const modalRef = this.modalService.open(PartnerDetailsModalComponent, { size: 'lg' });
    modalRef.componentInstance.partner = partner;
  }

  getPartnerTypeLabel(typeId: number): string {
    switch (typeId) {
      case 1: return 'partnerCreate.personal';
      case 2: return 'partnerCreate.legal';
      default: return 'common.unknown';
    }
  }
  
}