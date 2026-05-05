import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { AssetsApiService } from '../core/assets-api.service';
import { IncidentsApiService } from '../core/incidents-api.service';
import { AssetSummary, CreateIncidentRequest } from '../core/models';

type IncidentFormValue = {
  assetCode: FormControl<string>;
  title: FormControl<string>;
  severity: FormControl<string>;
  description: FormControl<string>;
  startsAt: FormControl<string>;
  endsAt: FormControl<string>;
  plannedEndAt: FormControl<string>;
};

@Component({
  imports: [ReactiveFormsModule],
  templateUrl: './incident-form-page.component.html',
  styleUrl: './incident-form-page.component.scss'
})
export class IncidentFormPageComponent implements OnInit {
  private readonly assetsApi = inject(AssetsApiService);
  private readonly incidentsApi = inject(IncidentsApiService);

  readonly assets = signal<AssetSummary[]>([]);
  readonly loadingAssets = signal(true);
  readonly submitError = signal<string | null>(null);
  readonly submitSuccess = signal<string | null>(null);
  readonly isSubmitting = signal(false);

  readonly form = new FormGroup<IncidentFormValue>(
    {
      assetCode: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
      title: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
      severity: new FormControl('Medium', { nonNullable: true, validators: [Validators.required] }),
      description: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
      startsAt: new FormControl(this.getDefaultStart(), { nonNullable: true, validators: [Validators.required] }),
      endsAt: new FormControl('', { nonNullable: true }),
      plannedEndAt: new FormControl('', { nonNullable: true })
    },
    { validators: this.validateDateRange }
  );

  async ngOnInit(): Promise<void> {
    try {
      const assets = await firstValueFrom(this.assetsApi.getAssets$());
      this.assets.set(assets);

      if (assets.length > 0) {
        this.form.controls.assetCode.setValue(assets[0].assetCode);
      }
    } finally {
      this.loadingAssets.set(false);
    }
  }

  async submit(): Promise<void> {
    this.submitError.set(null);
    this.submitSuccess.set(null);

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);

    try {
      await firstValueFrom(this.incidentsApi.createIncident$(this.buildRequest()));
      this.submitSuccess.set('Incident created successfully.');
    } catch (error) {
      const message = error instanceof HttpErrorResponse
        ? error.error?.message ?? 'Could not create the incident.'
        : 'Could not create the incident.';

      this.submitError.set(message);
    } finally {
      this.isSubmitting.set(false);
    }
  }

  private buildRequest(): CreateIncidentRequest {
    const raw = this.form.getRawValue();

    return {
      assetCode: raw.assetCode,
      title: raw.title,
      description: raw.description,
      severity: raw.severity,
      startsAt: new Date(raw.startsAt).toISOString(),
      endsAt: raw.endsAt ? new Date(raw.endsAt).toISOString() : null,
      plannedEndAt: raw.plannedEndAt ? new Date(raw.plannedEndAt).toISOString() : null
    };
  }

  private getDefaultStart(): string {
    const now = new Date();
    now.setMinutes(now.getMinutes() - now.getTimezoneOffset());
    return now.toISOString().slice(0, 16);
  }

  private validateDateRange(control: AbstractControl): ValidationErrors | null {
    const startsAt = control.get('startsAt')?.value;
    const endsAt = control.get('endsAt')?.value;

    if (!startsAt || !endsAt) {
      return null;
    }

    return new Date(endsAt).getTime() < new Date(startsAt).getTime()
      ? { endsBeforeStart: true }
      : null;
  }
}
