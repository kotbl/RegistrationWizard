import {
  Component,
  inject,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective } from '@angular/forms';
import { MatSelect } from '@angular/material/select';
import { ReplaySubject, Subject, take, takeUntil } from 'rxjs';

export interface ISelectItem {
  id: number;
  name: string;
}

@Component({
  selector: 'app-select-field',
  templateUrl: './select-field.component.html',
  styleUrls: ['./select-field.component.css'],
})
export class SelectFieldComponent implements OnChanges {
  formGroupDirective = inject(FormGroupDirective);
  formGroup!: FormGroup;

  @Input('form-group-name') formGroupName!: string;
  @Input('form-control-name') formControlName!: string;
  @Input('placeholder-field') placeholder!: string;
  @Input() data: ISelectItem[] = [];
  @Input() errorMessage?: string;

  protected _onDestroy = new Subject<void>();

  public filteredData: ReplaySubject<ISelectItem[]> = new ReplaySubject<
    ISelectItem[]
  >(1);

  public filterCtrl: FormControl<string | null> = new FormControl<string>('');

  @ViewChild('selectControl', { static: true }) selectControl!: MatSelect;

  ngOnChanges(): void {
    this.formGroup = this.formGroupDirective.control;

    this.filteredData.next(this.data.slice());

    this.filterCtrl.valueChanges
      .pipe(takeUntil(this._onDestroy))
      .subscribe(() => {
        this.filters();
      });
  }

  ngAfterViewInit() {
    this.setInitialValue();
  }

  protected setInitialValue() {
    this.filteredData
      .pipe(take(1), takeUntil(this._onDestroy))
      .subscribe(() => {
        this.selectControl.compareWith = (a: ISelectItem, b: ISelectItem) =>
          a && b && a.id === b.id;
      });
  }

  protected filters() {
    if (!this.data) {
      return;
    }

    let search = this.filterCtrl.value;

    if (!search) {
      this.filteredData.next(this.data.slice());
      return;
    } else {
      search = search.toLowerCase();
    }

    this.filteredData.next(
      this.data.filter((x) => x.name.toLowerCase().indexOf(search) > -1)
    );
  }

  protected hasError(): boolean {
    const group = this.formGroup.get(this.formGroupName);
    const field = group?.get(this.formControlName);

    return (field?.touched && field?.hasError('required')) ?? false;
  }
}
