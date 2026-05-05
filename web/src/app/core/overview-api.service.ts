import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OverviewMetric } from './models';

@Injectable({ providedIn: 'root' })
export class OverviewApiService {
  private readonly http = inject(HttpClient);

  getMetrics$(): Observable<OverviewMetric[]> {
    return this.http.get<OverviewMetric[]>('/api/overview/metrics');
  }
}
