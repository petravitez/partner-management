export interface PartnerDetails {
    fullname: string;
    partnerNumber: string;
    croatianPIN?: string;
    partnerTypeId: number;
    createdAtUtc: string; 
    createdByUser: string;
    isForeign: boolean;
    gender: string;
    policyNumber?: string;
    policyAmount?: number;
  }
  