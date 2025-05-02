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

  constructor(private partnerService: PartnerService, private router: Router) { }

  ngOnInit(): void {
    this.partnerService.getPartners().subscribe(data => {
      this.partners = data;
    });
  }

  isNewPartner(partnerId: number): boolean {
    return this.partnerService.newPartnerId === partnerId;
  }

  
  createPartner() {
    console.log('tu sam')
    this.router.navigate(['/partners/create']);
  }
}
