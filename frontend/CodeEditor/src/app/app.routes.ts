import { Routes } from '@angular/router';
import { Landing } from './features/components/landing/landing';
import { NotFound } from './features/components/not-found/not-found';
import { Login } from './features/components/login/login';
import { CodeEditor } from './features/components/code-editor/code-editor';
import { CreateAccount } from './features/components/create-account/create-account';

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
    component: CreateAccount 
  },
  { 
    path: 'codeeditor', 
    component: CodeEditor 
  },
  { 
    path: '**', 
    component: NotFound
  }
];
