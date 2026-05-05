import { Routes } from '@angular/router';
import { DashboardPageComponent } from './dashboard/dashboard-page.component';
import { IncidentFormPageComponent } from './incidents/incident-form-page.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'dashboard' },
  { path: 'dashboard', component: DashboardPageComponent },
  { path: 'incidents/new', component: IncidentFormPageComponent }
];
