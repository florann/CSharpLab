import { Component, inject, viewChild } from '@angular/core';
import {MatDividerModule} from '@angular/material/divider';
import {MatMenu, MatMenuModule} from '@angular/material/menu';
import { MatIcon } from "@angular/material/icon";
import { Router } from '@angular/router';

@Component({
  selector: 'app-header-account',
  standalone: true,
  imports: [MatDividerModule, MatMenuModule, MatIcon],
  templateUrl: './header-account.html',
  styleUrl: './header-account.scss',
})
export class HeaderAccount {
  public menu = viewChild.required(MatMenu);

  router = inject(Router);
  
  navigateUserAccount(){
    console.log("Navigating to useraccount");
    this.router.navigate(['/useraccount']);
  }
}
