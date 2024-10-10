import {Component, ElementRef, HostBinding, HostListener, OnInit, Renderer2, ViewChild} from "@angular/core";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {UiService} from "../../services/ui/ui.service";
import {NgOptimizedImage} from "@angular/common";

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css'],
  imports: [
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage
  ],
  standalone: true
})

export class NavigationComponent implements OnInit{
  @ViewChild('navigationDiv', { static: true }) navigationDiv: ElementRef | undefined;
  menuState = false;

  constructor(
    private renderer: Renderer2,
    private uiService: UiService
  ) {
  }
  @HostListener('mouseenter') onMouseEnter() {
    this.renderer.addClass(this.navigationDiv?.nativeElement, 'open');
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.renderer.removeClass(this.navigationDiv?.nativeElement, 'open');
  }

  ngOnInit() {
    this.uiService.getMenuState().subscribe(state => {
      this.menuState = state;
    });
  }
}
