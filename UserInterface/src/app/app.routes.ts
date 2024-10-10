import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./journeys/home/home.component')
      .then(m => m.HomeComponent)
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./journeys/dashboard/dashboard.component')
      .then(m => m.DashboardComponent)
  },
  {
    path: 'test',
    loadComponent: () => import('./journeys/test/test.component')
      .then(m => m.TestComponent)
  }
];

