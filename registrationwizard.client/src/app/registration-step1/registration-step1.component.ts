import { Component, inject, OnInit, signal } from '@angular/core';
import { FormGroup, FormGroupDirective } from '@angular/forms';

@Component({
  selector: 'app-registration-step1',
  templateUrl: './registration-step1.component.html',
  styleUrls: ['./registration-step1.component.css'],
})
export class RegistrationStep1Component implements OnInit {
  private formGroupStepName: string = 'stepOne';

  formGroup!: FormGroup;
  formGroupDirective = inject(FormGroupDirective);
  hidePassword = signal(true);
  hideConfirmPassword = signal(true);

  ngOnInit(): void {
    this.formGroup = this.formGroupDirective.control;
  }

  clickPasswordEvent(event: MouseEvent) {
    this.hidePassword.set(!this.hidePassword());
    event.stopPropagation();
  }

  clickConfirmPasswordEvent(event: MouseEvent) {
    this.hideConfirmPassword.set(!this.hideConfirmPassword());
    event.stopPropagation();
  }

  hasLoginError() {
    const group = this.formGroup.get(this.formGroupStepName);
    const field = group?.get('login');

    return (
      field?.touched &&
      (field?.hasError('email') || field?.hasError('required'))
    );
  }

  hasPasswordError() {
    const group = this.formGroup.get(this.formGroupStepName);
    const field = group?.get('password');

    return (
      field?.touched &&
      (field?.hasError('required') ||
        field?.hasError('requiresDigit') ||
        field?.hasError('requiredCharacter'))
    );
  }

  hasConfirmPasswordError() {
    const group = this.formGroup.get(this.formGroupStepName);
    const field = group?.get('confirmPassword');

    return (
      field?.touched &&
      (field?.hasError('required') || field?.hasError('passwordNoMatch'))
    );
  }

  hasIsAgreeError() {
    const group = this.formGroup.get(this.formGroupStepName);
    const field = group?.get('isAgree');

    return field?.touched && field?.hasError('required');
  }
}
