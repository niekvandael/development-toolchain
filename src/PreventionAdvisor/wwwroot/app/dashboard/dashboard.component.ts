import { Component, OnInit }  from '@angular/core';

import { Dashboard } from './dashboard';
import { DashboardService } from './dashboard.service';

@Component({
    templateUrl: 'app/dashboard/dashboard.component.html',
    styleUrls: ['app/dashboard/dashboard.component.css']
})
export class DashboardComponent implements OnInit {
    listFilter: string;
    errorMessage: string;

    dashboard: Dashboard = new Dashboard();

    constructor(private _dashboardService: DashboardService) {

    }

    ngOnInit(): void {
        this._dashboardService.getDashboards(this.setDashboard.bind(this));
    }

    private setDashboard(dashboard: Dashboard ){
        this.dashboard = dashboard;
    }

}
