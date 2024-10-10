import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {LoadingComponent} from "./components/loading/loading.component";
import {HeaderComponent} from "./components/header/header.component";
import {NavigationComponent} from "./components/navigation/navigation.component";
import {ToastComponent} from "./components/toast/toast.component";
import {ToastService} from "./services/toast/toast.service";
import {UiService} from "./services/ui/ui.service";
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, LoadingComponent, HeaderComponent, NavigationComponent, ToastComponent, NgIf, NgForOf],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Polaris';
  objectValues = Object.values;
  toastList = {};
  menuState = false;
  loaded = false;
  theme= false;

  constructor(
    private toastService: ToastService,
    private uiService: UiService
  ) {
  }

  ngOnInit() {
    this.uiService.getMenuState().subscribe(state => {
      this.menuState = state;
      this.loaded = true;
    });
    this.uiService.getTheme().subscribe(theme => {
      this.theme = theme;
    });
    this.toastList = this.toastService.toastList;
  }
}
