export interface CreatePartnerRequest {
    id: number;
    firstName: string;
    lastName: string;
    address?: string;
    partnerNumber: string;
    croatianPIN?: string;
    partnerTypeId: number;
    createdAtUtc: string; 
    createdByUser: string;
    isForeign: boolean;
    externalCode: string;
    genderId: number;
  }
  