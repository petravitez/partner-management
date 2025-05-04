import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../enviroments/enviroment';
import { PartnerDetails } from '../models/partner.details.model';
import { CreatePartnerRequest } from '../models/partner-create.model';
import { CreatePolicyRequest } from '../models/policy-create.model';
import { PartnerDropdownDto } from '../models/partner-dropdown.model';

@Injectable({
  providedIn: 'root'
})
export class PartnerService {

  private readonly baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

getPartners(params: { page: number; pageSize: number }): Observable<{ items: PartnerDetails[]; totalCount: number }> {
  return this.http.get<{ items: PartnerDetails[]; totalCount: number }>(`${this.baseUrl}/partners`, {
    params: {
      page: params.page,
      pageSize: params.pageSize
    }
  });
}

  newPartnerId: number | null = null;

  createPartner(partner: CreatePartnerRequest): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/partners`, partner);
  }  

  createPolicy(partnerId: number, policy: CreatePolicyRequest): Observable<any> {
    return this.http.post(`${this.baseUrl}/partners/${partnerId}/policies`, policy);
  }
 
  getPartnerDropdown(): Observable<PartnerDropdownDto[]> {
    return this.http.get<PartnerDropdownDto[]>(`${this.baseUrl}/partners/dropdown`);
  }

}
