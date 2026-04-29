import { Component, inject, Inject } from '@angular/core';
import { Header } from './header/header';
import { Footer } from './footer/footer';
import { RouterOutlet } from "@angular/router";
import { ToastService } from '../core/services/toast/toast.service';
import { SearchBarCore } from "../components/search-bar/search-bar-core";

@Component({
  selector: 'app-layout',
  imports: [Header, Footer, RouterOutlet, SearchBarCore],
  templateUrl: './layout.html',
  styleUrl: './layout.scss',
})

export class Layout {
  toastService = inject(ToastService);

}
