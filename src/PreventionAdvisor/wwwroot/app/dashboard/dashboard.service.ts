import { Injectable } from '@angular/core';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { CommonService } from '../shared/commonService';

import { Dashboard } from './dashboard';

@Injectable()
export class DashboardService  extends CommonService {

    private _url = 'api/dashboard';

    getDashboards(callback: any){
        this.doGet(this._url, callback);
    }

    getDashboard(id: string, callback:any) {
        this.doGet(this._url + '/' + id, callback);
    }

    addDashboard(dashboard: Dashboard, callback: any) {
        this.doPost(this._url, dashboard, callback);
    }

    updateDashboard(dashboard: Dashboard, callback: any) {
        this.doPut(this._url, dashboard, callback);
    }
}
