import { Injectable } from '@angular/core';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { CommonService } from '../shared/commonService';

import { ChecklistItem } from './checklistItem';

@Injectable()
export class ChecklistItemService  extends CommonService {

    private _url = 'api/checklistItem';

    getChecklistItems(callback: any){
        this.doGet(this._url, callback);
    }

    getChecklistItem(id: string, callback:any) {
        this.doGet(this._url + '/' + id, callback);
    }

    addChecklistItem(checklistItem: ChecklistItem, callback: any) {
        this.doPost(this._url, checklistItem, callback);
    }

    updateChecklistItem(checklistItem: ChecklistItem, callback: any) {
        this.doPut(this._url, checklistItem, callback);
    }

}
