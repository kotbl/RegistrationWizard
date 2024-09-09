import { Component, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { PasswordValidators } from './validators/password-validators';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  private _formBuilder = inject(FormBuilder);
  userForm!: FormGroup;
  stepOne!: FormGroup;
  stepTwo!: FormGroup;

  constructor(private httpClient: HttpClient) {}

  ngOnInit() {
    this.stepOne = this._formBuilder.group({
      login: [null, [Validators.required, Validators.email]],
      password: [
        null,
        [
          Validators.required,
          PasswordValidators.patternValidator(new RegExp('(?=.*[0-9])'), {
            requiresDigit: true,
          }),
          PasswordValidators.patternValidator(new RegExp('(?=.*[a-zA-Z])'), {
            requiredCharacter: true,
          }),
        ],
      ],
      confirmPassword: [null, Validators.required],
      isAgree: [false, Validators.requiredTrue],
    });

    this.stepOne.setValidators(PasswordValidators.matchValidator);

    this.stepTwo = this._formBuilder.group({
      countryId: [null, [Validators.required]],
      provinceId: [null, [Validators.required]],
    });

    this.userForm = this._formBuilder.group({
      stepOne: this.stepOne,
      stepTwo: this.stepTwo,
    });
  }

  goNext(stepper: MatStepper) {
    if (this.stepOne.valid) stepper.next();
    else this.validateAllFormFields(this.stepOne);
  }

  saveForm(stepper: MatStepper) {
    if (this.stepOne.valid && this.stepTwo.valid) {
      this.httpClient
        .post('/User', {
          login: this.userForm.value.stepOne.login,
          password: this.userForm.value.stepOne.password,
          isAgree: this.userForm.value.stepOne.isAgree,
          countryId: this.userForm.value.stepTwo.countryId.id,
          provinceId: this.userForm.value.stepTwo.provinceId.id,
        })
        .subscribe(
          (x) => {
            console.log(x);
            stepper.next();
          },
          (err) => console.error(err)
        );

      return;
    }

    this.validateAllFormFields(this.stepTwo);
  }

  validateAllFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);

      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }
}
