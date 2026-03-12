import { Component, inject, Inject } from '@angular/core';
import { Header } from './header/header';
import { Footer } from './footer/footer';
import { RouterOutlet } from "@angular/router";
import { Toast } from "../components/toast/toast";
import { ToastService } from '../core/services/toast/toast.service';

@Component({
  selector: 'app-layout',
  imports: [Header, Footer, RouterOutlet, Toast],
  templateUrl: './layout.html',
  styleUrl: './layout.scss',
})

export class Layout {
  toastService = inject(ToastService);

}
