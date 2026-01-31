import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from "@angular/router";
import { MatIconButton } from "@angular/material/button";

@Component({
  selector: 'app-header-login',
  imports: [MatIconModule, RouterLink, MatIconButton],
  templateUrl: './header-login.html',
  styleUrl: './header-login.css',
})
export class HeaderLogin {

}
