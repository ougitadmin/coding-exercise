import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.css'],
  standalone: true
})
export class LoadingComponent {
  @Input() colour: string = '#36c';

  constructor() {

  }
}
