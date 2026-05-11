import { Routes } from '@angular/router';
import { IncidentFormPageComponent } from './incidents/incident-form-page.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'incidents/new' },
  { path: 'incidents/new', component: IncidentFormPageComponent }
];
