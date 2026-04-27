import { Routes } from '@angular/router';
import { NotFound } from './features/components/not-found/not-found';
import { Login } from './features/components/login/login';
import { CreateAccount } from './features/components/create-account/create-account';
import { authGuard } from './guard/auth.guard';
import { Dashboard } from './features/components/dashboard/dashboard';
import { UserAccount } from './features/components/user-account/user-account';

export const routes: Routes = [
  { 
    path: '', 
    redirectTo: '/dashboard', 
    pathMatch: 'full' 
  },
  { 
    path: 'login', 
    component: Login 
  },
  { 
    path: 'createaccount', 
    component: CreateAccount, 
  },
  { 
    path: 'dashboard', 
    component: Dashboard,
    canActivate: [authGuard]
  },
  { 
    path: 'useraccount', 
    component: UserAccount,
    canActivate: [authGuard]
  },
  { 
    path: '**', 
    component: NotFound
  }
];
