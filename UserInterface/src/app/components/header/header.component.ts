import {Component, OnInit} from "@angular/core";
import {UiService} from "../../services/ui/ui.service";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {NgClass, NgIf, NgOptimizedImage} from "@angular/common";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  imports: [
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    NgIf,
    NgClass
  ],
  standalone: true
})

export class HeaderComponent implements OnInit{
  inputValue: string = '';

  onInputChange(event: Event): void {
    const target = event.target as HTMLInputElement;
    this.inputValue = target.value;
  }

  menuState = false;
  theme = false;
  constructor(
    private uiService: UiService
  ) {
  }
  ngOnInit() {
    this.uiService.getMenuState().subscribe(state => {
      this.menuState = state;
    });
    this.uiService.getTheme().subscribe(theme => {
      this.theme = theme;
    });
  }

  toggleMenu() {
    this.uiService.toggleMenu();
  }

  toggleTheme() {
    this.uiService.toggleTheme();
  }
}
