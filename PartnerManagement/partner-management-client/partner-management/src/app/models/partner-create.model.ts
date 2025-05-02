export interface CreatePartner {
    id: number;
    firstName: string;
    lastName: string;
    address?: string;
    partnerNumber: string;
    croatianPIN?: string;
    partnerTypeId: number;
    createdAtUtc: string; // ISO string (DateTime from .NET)
    createdByUser: string;
    isForeign: boolean;
    externalCode: string;
    genderId: number;
  }
  