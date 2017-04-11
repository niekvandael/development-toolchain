import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Router } from '@angular/router';

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

    constructor(private _http: Http, private _router: Router) { 
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
            .catch((error) => {
                return this.handleError(error);
             });
    }

    getOrganization(id: number): Observable<IOrganization> {
         return this._http.get(this._organizationUrl + id,  this._options)
            .map((response: Response) => <IOrganization> response.json())
            .catch(this.handleError);
    }

    private handleError(err: Response) {
        if (err.status === 401) {
            return this.unauthorised();
        } else if (err.status === 403) {
            return this.forbidden();
        } else {
            return Observable.throw(err);
        }
    }

    private unauthorised(): Observable<any> {
        this._router.navigate(['login']);
        return null;
    }

    private forbidden(): Observable<any> {
        this._router.navigate(['/']);
        return null;
    }
}
