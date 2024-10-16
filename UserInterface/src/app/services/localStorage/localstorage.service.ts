import {Injectable} from '@angular/core';
import {AppComponent} from '../../app.component';


class LocalStorage implements Storage {
  [name: string]: any;
  // @ts-ignore
  length = 0;
  clear(): void {}
  getItem(key: string): string | null { // @ts-ignore
    return undefined;}
  key(index: number): string | null { // @ts-ignore
    return undefined;}
  removeItem(key: string): void {}
  setItem(key: string, value: string): void {}
}


@Injectable({
  providedIn: 'root'
})
export class LocalstorageService implements Storage {
  private storage: Storage;

  constructor() {
    try {
      this.storage = localStorage;
    } catch {
      this.storage = new LocalStorage();
    }
  }

  [name: string]: any;

  clear(): void {
    this.storage.clear();
  }

  getItem(key: string): string | null {
    return this.storage.getItem(key);
  }

  key(index: number): string | null {
    return this.storage.key(index);
  }

  removeItem(key: string): void {
    return this.storage.removeItem(key);
  }

  setItem(key: string, value: string): void {
    return this.storage.setItem(key, value);
  }

  readonly length = 0;
}
