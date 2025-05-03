import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PartnerService } from '../../service/partner.service';
import { Router } from '@angular/router';
import { PolicyModalComponent } from '../policy-modal/policy-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PartnerDetails } from '../../models/partner.details.model';
import { PartnerDetailsModalComponent } from '../partner-details-modal/partner-details-modal.component';

@Component({
  standalone: true,
  selector: 'app-partner-list',
  templateUrl: './partner-list.component.html',
  styleUrls: ['./partner-list.component.css'],
  imports: [CommonModule],
})
export class PartnerListComponent implements OnInit {
  partners: any[] = [];
  newPartnerId: number | null = null;

  constructor(
    private partnerService: PartnerService,
    private router: Router,
    private modalService: NgbModal
  ) {
    const nav = this.router.getCurrentNavigation();
    this.newPartnerId = nav?.extras.state?.['newPartnerId'] ?? null;
    console.log(this.newPartnerId)
  }

  ngOnInit(): void {
    this.partnerService.getPartners().subscribe(data => {
      this.partners = data;
    });

  }
 
  createPartner() {
    this.router.navigate(['/partners/create']);
  }

  isNewPartner(id: number): boolean {
    return id === this.newPartnerId;
  }

  openPolicyModal(): void {
    const modalRef = this.modalService.open(PolicyModalComponent, { size: 'lg' });
  
    modalRef.result.then(result => {
      if (result === 'saved') {
        this.loadPartners(); 
      }
    }).catch(() => {});
  }

  loadPartners(): void {
    this.partnerService.getPartners().subscribe({
      next: (data) => {
        this.partners = data;
      },
      error: (err) => {
        console.error('Failed to load partners:', err);
      }
    });
  }

  openPartnerDetails(partner: PartnerDetails): void {
    const modalRef = this.modalService.open(PartnerDetailsModalComponent, { size: 'lg' });
    modalRef.componentInstance.partner = partner;
  }
  


}
