import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateIncidentRequest } from './models';

@Injectable({ providedIn: 'root' })
export class IncidentsApiService {
  private readonly http = inject(HttpClient);

  createIncident$(request: CreateIncidentRequest): Observable<unknown> {
    return this.http.post('/api/incidents', request);
  }
}
