import { Routes } from '@angular/router';
import { Landing } from './features/components/landing/landing';
import { NotFound } from './features/components/not-found/not-found';
import { Login } from './features/components/login/login';

export const routes: Routes = [
  { 
    path: '', 
    redirectTo: '/landing', 
    pathMatch: 'full' 
  },
  { 
    path: 'landing', 
    component: Landing 
  },
  { 
    path: 'login', 
    component: Login 
  },
  { 
    path: '**', 
    component: NotFound
  }
];
