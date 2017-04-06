import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { CommonComponent } from '../shared/common.component';

import { IOrganization } from './organization';

@Injectable()
export class OrganizationService {
    private _commonComponent: CommonComponent;
    private _apiLocation: string;

    private _organizationsUrl = '';
    private _organizationUrl = '';

    private _options: RequestOptions;

    constructor(private _http: Http) { 
        this._commonComponent = new CommonComponent();
        this._apiLocation = this._commonComponent.getAPILocation();
        this._organizationsUrl = this._apiLocation + 'api/Organization';
        this._organizationUrl = this._apiLocation + 'api/Organizations/';

        this._options = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            withCredentials: true
        });
    }

    getOrganizations(): Observable<IOrganization[]> {
        return this._http.get(this._organizationsUrl,  this._options)
            .map((response: Response) => <IOrganization[]> response.json())
            .catch(this.handleError);
    }

    getOrganization(id: number): Observable<IOrganization> {
         return this._http.get(this._organizationUrl + id,  this._options)
            .map((response: Response) => <IOrganization> response.json())
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        // in a real world app, we may send the server to some remote logging infrastructure
        // instead of just logging it to the console
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}
