import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { Observable, of } from 'rxjs';

export class PasswordValidators {
  constructor() {}

  static matchValidator: ValidatorFn = (
    control: AbstractControl
  ): ValidationErrors | null => {
    if (!control.value.password && !control.value.confirmPassword) return null;

    if (control.value.password !== control.value.confirmPassword) {
      control.get('confirmPassword')!.setErrors({ passwordNoMatch: true });
    } else {
      control.get('confirmPassword')!.setErrors(null);
    }
    return null;
  };

  static patternValidator(regex: RegExp, error: ValidationErrors): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.value) {
        return null;
      }

      const valid = regex.test(control.value);

      if (valid) return null;

      return error;
    };
  }
}
