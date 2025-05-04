import { NgClass } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NgClass],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'partner-management';
  constructor(private translocoService: TranslocoService) {}

  activeLang(): string {
    return this.translocoService.getActiveLang();
  }
  
  setLang(lang: string): void {
    this.translocoService.setActiveLang(lang);
  }
}
