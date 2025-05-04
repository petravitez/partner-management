import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function oibValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const oib = control.value;

    if (!oib) return null; 
    if (!/^\d{11}$/.test(oib)) {
      return { oibInvalid: 'OIB must be exactly 11 digits.' };
    }

    let remainder = 10;
    for (let i = 0; i < 10; i++) {
      let digit = parseInt(oib[i], 10);
      remainder = (remainder + digit) % 10;
      if (remainder === 0) remainder = 10;
      remainder = (remainder * 2) % 11;
    }

    let checkDigit = 11 - remainder;
    if (checkDigit === 10) checkDigit = 0;

    if (checkDigit !== parseInt(oib[10], 10)) {
      return { oibInvalid: 'OIB checksum failed.' };
    }

    return null;
  };
}
