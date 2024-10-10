import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-form-radio-group',
  templateUrl: './form-radio-group.component.html',
  styleUrls: ['../form-elements.css', './form-radio-group.component.css'],
  standalone: true,
})
export class FormRadioGroupComponent  {
  @Input() id: string = '';
  @Input() label: string = '';
  @Input() error: string = '';

  value: string = '';

}
