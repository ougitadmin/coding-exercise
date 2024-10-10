import {Component, Input, forwardRef, input, OnInit} from '@angular/core';
import {
  ControlValueAccessor,
  NG_VALUE_ACCESSOR
} from '@angular/forms';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-form-radio-input',
  templateUrl: './form-radio-input.component.html',
  styleUrls: ['../form-elements.css', 'form-radio-input.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FormRadioInputComponent),
      multi: true
    }
  ],
  standalone: true,
  imports: [NgClass]
})
export class FormRadioInputComponent implements ControlValueAccessor, OnInit {
  @Input() id: string = '';
  @Input() name: string = '';
  @Input() label: string = '';
  @Input() error: boolean = false;
  @Input() isFlagged: boolean = false;
  onChange: (value: string) => void = () => {};
  onTouched: () => void = () => {};
  value = '';
  @Input() optionValue = '';

  ngOnInit() {
    console.log(this.value);
  }

  onInputChange(event: Event): void {
    const target = event.target as HTMLInputElement;
    this.value = target.value;
    console.log(this.value);
    this.onChange(this.value);
    this.onTouched();
  }

  writeValue(value: string): void {
    this.value = value;
  }

  registerOnChange(fn: (value: string) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    // Handle the disabled state if needed
  }
}
