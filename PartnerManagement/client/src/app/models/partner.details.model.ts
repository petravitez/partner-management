export interface PartnerDetails {
    id: number;
    fullname: string;
    address: string;
    partnerNumber: string;
    croatianPIN?: string;
    partnerTypeId: number;
    createdAtUtc: string; 
    createdByUser: string;
    isForeign: boolean;
    gender: string;
    externalCode: string;
    isImportant: boolean;
  }
  