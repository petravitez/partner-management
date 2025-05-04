import { FormGroup } from '@angular/forms';

export function markFormGroupTouched(formGroup: FormGroup): void {
  Object.values(formGroup.controls).forEach(control => {
    control.markAsTouched();
    control.updateValueAndValidity();

    if ((control as FormGroup).controls) {
      markFormGroupTouched(control as FormGroup); 
    }
  });
}
