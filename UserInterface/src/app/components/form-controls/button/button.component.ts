import { Component, Input, Output, EventEmitter } from '@angular/core';
import {NgClass} from "@angular/common";

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  standalone: true,
  imports: [
    NgClass
  ],
  styleUrls: ['./button.component.css']
})
export class ButtonComponent {
  @Input() state: 'error' | 'warning' | 'primary' | 'secondary' = 'primary';
  @Input() disabled: boolean = false;
  @Output() onClick = new EventEmitter<void>();

  handleClick(event: Event) {
    if (!this.disabled) {
      if (this.onClick.observed) {
        event.preventDefault();
      }
      this.onClick.emit();
    }
  }
}
