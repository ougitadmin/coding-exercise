import {Component} from "@angular/core";
import {NgOptimizedImage} from "@angular/common";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [
    NgOptimizedImage
  ],
  standalone: true
})

export class HomeComponent {
  constructor() {
  }
}
