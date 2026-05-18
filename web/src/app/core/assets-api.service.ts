import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AssetComponentOption, AssetSummary } from './models';

@Injectable({ providedIn: 'root' })
export class AssetsApiService {
  private readonly http = inject(HttpClient);

  getAssets$(): Observable<AssetSummary[]> {
    return this.http.get<AssetSummary[]>('/api/assets');
  }

  getAssetComponents$(assetCode: string): Observable<AssetComponentOption[]> {
    return this.http.get<AssetComponentOption[]>(`/api/assets/${encodeURIComponent(assetCode)}/components`);
  }
}
