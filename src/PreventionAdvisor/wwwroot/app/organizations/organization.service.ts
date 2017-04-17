import { Injectable } from '@angular/core';
import { RequestOptions, Headers } from '@angular/http';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { CommonService } from '../shared/commonService';

import { IOrganization } from './organization';

@Injectable()
export class OrganizationService  extends CommonService {

    private _url = 'api/organization';

    getOrganizations(callback: any){
        this.doGet(this._url, callback);
    }

    getOrganization(id: string, callback:any) {
        this.doGet(this._url + '/' + id, callback);
    }

    addOrganization(org: IOrganization, callback: any) {
        this.doPost(this._url, org, callback);
    }

    updateOrganization(org: IOrganization, callback: any) {
        this.doPut(this._url, org, callback);
    }

}
