export interface PartnerDetails {
    id: number;
    fullname: string;
    partnerNumber: string;
    croatianPIN?: string;
    partnerTypeId: number;
    createdAtUtc: string; 
    createdByUser: string;
    isForeign: boolean;
    gender: string;
    isImportant: boolean;
  }
  