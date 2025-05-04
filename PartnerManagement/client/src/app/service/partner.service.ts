import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../enviroments/enviroment';
import { PartnerDetails } from '../models/partner.details.model';
import { CreatePartnerRequest } from '../models/partner-create.model';
import { CreatePolicyRequest } from '../models/policy-create.model';

@Injectable({
  providedIn: 'root'
})
export class PartnerService {

  private readonly baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getPartners(): Observable<PartnerDetails[]> {
    return this.http.get<PartnerDetails[]>(`${this.baseUrl}/partners`);
  }

  newPartnerId: number | null = null;

  createPartner(partner: CreatePartnerRequest): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/partners`, partner);
  }  

  createPolicy(partnerId: number, policy: CreatePolicyRequest): Observable<any> {
    return this.http.post(`${this.baseUrl}/partners/${partnerId}/policies`, policy);
  }
 
  

}
