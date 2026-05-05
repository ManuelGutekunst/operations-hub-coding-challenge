import { Component, inject, OnInit } from '@angular/core';
import { OverviewStore } from './overview.store';

@Component({
  templateUrl: './dashboard-page.component.html',
  styleUrl: './dashboard-page.component.scss'
})
export class DashboardPageComponent implements OnInit {
  readonly store = inject(OverviewStore);

  ngOnInit(): void {
    void this.store.load();
  }
}
