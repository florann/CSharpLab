import { Routes } from '@angular/router';
import { Landing } from './components/landing/landing';
import { NotFound } from './components/not-found/not-found';

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
  // Not found page 
  { 
    path: '**', 
    component: NotFound
  }
];
