import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { OverviewApiService } from '../core/overview-api.service';
import { OverviewMetric } from '../core/models';

export type OverviewStatus = 'idle' | 'loading' | 'success' | 'error';

@Injectable({ providedIn: 'root' })
export class OverviewStore {
  private readonly api = inject(OverviewApiService);

  readonly metrics = signal<OverviewMetric[]>([]);
  readonly status = signal<OverviewStatus>('idle');
  readonly errorMessage = signal<string | null>(null);

  async load(): Promise<void> {
    this.status.set('loading');
    this.errorMessage.set(null);

    try {
      const metrics = await firstValueFrom(this.api.getMetrics$());
      this.metrics.set(metrics);
      this.status.set('success');
    } catch {
      this.metrics.set([]);
      this.errorMessage.set('Could not load overview metrics.');
      this.status.set('error');
    }
  }
}
