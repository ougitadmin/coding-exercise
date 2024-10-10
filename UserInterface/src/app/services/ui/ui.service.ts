import {Injectable} from "@angular/core";
import {BehaviorSubject, Observable} from "rxjs";
import {LocalstorageService} from "../localStorage/localstorage.service";

@Injectable({
  providedIn: 'root'
})
export class UiService {
  private menuState: BehaviorSubject<boolean>;
  private theme: BehaviorSubject<boolean>;

  constructor(private localStorageService: LocalstorageService) {
    const savedState = this.localStorageService.getItem('menuState') || 'open';
    const savedTheme = this.localStorageService.getItem('theme') || 'dark';
    this.menuState = new BehaviorSubject<boolean>(savedState === 'open');
    this.theme = new BehaviorSubject<boolean>(savedTheme === 'dark');
  }

  toggleMenu(): void {
    const newState = !this.menuState.value;
    this.menuState.next(newState);
    this.localStorageService.setItem('menuState', newState ? 'open' : 'closed');
  }

  toggleTheme(): void {
    const newTheme = !this.theme.value;
    this.theme.next(newTheme);
    this.localStorageService.setItem('theme', newTheme ? 'dark' : 'light');
  }

  getMenuState(): Observable<boolean> {
    return this.menuState.asObservable();
  }

  getTheme(): Observable<boolean> {
    return this.theme.asObservable();
  }
}
