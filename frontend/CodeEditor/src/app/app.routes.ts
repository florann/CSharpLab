import { Routes } from '@angular/router';
import { Landing } from './features/components/landing/landing';
import { NotFound } from './features/components/not-found/not-found';
import { Login } from './features/components/login/login';
import { CodeEditor } from './features/components/code-editor/code-editor';
import { CreateAccount } from './features/components/create-account/create-account';
import { authGuard } from './guard/auth.guard';
import { Dashboard } from './features/components/dashboard/dashboard';
import { UserAccount } from './features/components/user-account/user-account';

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
    path: 'createaccount', 
    component: CreateAccount, 
  },
  { 
    path: 'dashboard', 
    component: Dashboard,
    canActivate: [authGuard]
  },
  { 
    path: 'codeeditor', 
    component: CodeEditor,
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
