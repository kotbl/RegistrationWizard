import { Component, OnInit, inject, OnDestroy } from '@angular/core';
import { FormGroupDirective } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ISelectItem } from '../select-field/select-field.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-registration-step2',
  templateUrl: './registration-step2.component.html',
  styleUrls: ['./registration-step2.component.css'],
})
export class RegistrationStep2Component implements OnInit, OnDestroy {
  formGroupDirective = inject(FormGroupDirective);
  protected countries: ISelectItem[] = [];
  protected provinces: ISelectItem[] = [];
  private countrySubscribe?: Subscription;

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {
    this.countries = [];
    this.provinces = [];

    this.countrySubscribe = this.formGroupDirective.control
      .get('stepTwo')
      ?.get('countryId')
      ?.valueChanges.subscribe(({ id }) => {
        this.httpClient.get<ISelectItem[]>(`/Province/${id}`).subscribe((x) => {
          this.provinces = x;
        });
      });

    this.httpClient.get<ISelectItem[]>('/Country').subscribe((x) => {
      this.countries = x;
    });
  }

  ngOnDestroy(): void {
    this.countrySubscribe?.unsubscribe();
  }
}
