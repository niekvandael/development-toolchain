import { Injectable } from '@angular/core';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { CommonService } from '../shared/commonService';

import { Workplace } from './workplace';

@Injectable()
export class WorkplaceService  extends CommonService {

    private _url = 'api/workplace';

    getWorkplaces(callback: any){
        this.doGet(this._url, callback);
    }

    getWorkplace(id: string, callback:any) {
        this.doGet(this._url + '/' + id, callback);
    }

    addWorkplace(workplace: Workplace, callback: any) {
        this.doPost(this._url, workplace, callback);
    }

    updateWorkplace(workplace: Workplace, callback: any) {
        this.doPut(this._url, workplace, callback);
    }
}
