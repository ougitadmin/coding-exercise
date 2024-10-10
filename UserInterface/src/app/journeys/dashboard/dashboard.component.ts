import {Component} from "@angular/core";
import {NgOptimizedImage} from "@angular/common";

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css'],
    imports: [
        NgOptimizedImage
    ],
    standalone: true
})

export class DashboardComponent {
  constructor() {
  }
}
