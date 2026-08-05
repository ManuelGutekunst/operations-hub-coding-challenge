export interface AssetSummary {
  assetCode: string;
  name: string;
  category: string;
  status: string;
}

export interface AssetComponentOption {
  value: string;
  label: string;
}

export interface CreateIncidentRequest {
  assetCode: string;
  title: string;
  description: string;
  severity: string;
  startsAt: string;
  endsAt: string | null;
  plannedEndAt: string | null;
}
