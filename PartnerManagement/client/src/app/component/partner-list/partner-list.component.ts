import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PartnerService } from '../../service/partner.service';
import { Router } from '@angular/router';

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
    private router: Router
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
}
