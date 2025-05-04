import { AsyncValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { HttpClient, HttpParams } from '@angular/common/http';
import { inject } from '@angular/core';
import { of } from 'rxjs';
import { debounceTime, switchMap, map, catchError, take } from 'rxjs/operators';
import { environment } from '../../enviroments/enviroment';

export function externalCodeUniqueValidator(): AsyncValidatorFn {
  const http = inject(HttpClient); 
  return (control: AbstractControl) => {
    if (!control.value) {
      return of(null);
    }

    return of(control.value).pipe(
      debounceTime(300),
      take(1),
      switchMap(value => {
        const params = new HttpParams().set('externalCode', value);

        return http.get<{ isUnique: boolean }>(`${environment.apiUrl}/partners/check-external-code`, { params }).pipe(
          map(res => (res.isUnique ? null : { externalCodeTaken: true })),
          catchError(() => of(null)) // Optional: silent fail on error
        );
      })
    );
  };
}
