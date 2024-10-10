import {Component, OnInit} from '@angular/core';
import {
  AbstractControl,
  FormBuilder, FormGroup,
  ReactiveFormsModule, ValidationErrors, ValidatorFn,
  Validators
} from "@angular/forms";
import {FormInputComponent} from "../../components/form-controls/form-input/form-input.component";
import {FormRadioGroupComponent} from "../../components/form-controls/form-radio-group/form-radio-group.component";
import {JsonPipe} from "@angular/common";
import {FormRadioInputComponent} from "../../components/form-controls/form-radio-input/form-radio-input.component";
import {FormSelectComponent} from "../../components/form-controls/form-select/form-select.component";
import {ButtonComponent} from "../../components/form-controls/button/button.component";
@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css'],
  imports: [
    FormInputComponent,
    ReactiveFormsModule,
    FormRadioGroupComponent,
    JsonPipe,
    FormRadioInputComponent,
    FormSelectComponent,
    ButtonComponent
  ],
  standalone: true
})


export class TestComponent implements OnInit {
  protected form: any;
  selectListOptions: { value: string; label: string; }[]  = [
    { value: 'option1', label: 'Option 1' },
    { value: 'option2', label: 'Option 2' },
    { value: 'option3', label: 'Option 3' },
    { value: 'option4', label: 'Option 4' },
  ];

  constructor(
    private formBuilder: FormBuilder
  ) {
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      options: ['option1', [Validators.required, optionsValidator(['option1', 'option2'])]],
      otherOptions: ['option3', [Validators.required, optionsValidator(['option1', 'option2'])]],
      selectList: ['option4', [Validators.required, optionsValidator(['option1', 'option2', 'option3'])]],
    });
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  markFormGroupTouched(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();
      control?.markAsDirty();
    });
  }

  onSubmit() {
    this.markFormGroupTouched(this.form);
    if (this.form.valid) {
      console.log(this.form.value);
    } else {
      console.log('Form is invalid');
    }
  }

  onReset() {
    this.form.reset();
  }
}

export function optionsValidator(options: Array<string>): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const validOptions = options;
    return validOptions.includes(control.value) ? null : { invalidOption: true };
  };
}
