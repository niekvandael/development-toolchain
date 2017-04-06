import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';

import { CommonComponent } from '../shared/common.component';
import { User } from './user';

@Injectable()
export class AuthService {

    private _commonComponent: CommonComponent;
    private _apiLocation: string;

    private _loginUrl: string;
    private _logoutUrl: string;
    private _reportUrl: string; // TODO DELETE

    private _options: RequestOptions;


    constructor(private _http: Http) {
        this._commonComponent = new CommonComponent();
        this._apiLocation = this._commonComponent.getAPILocation();
        this._loginUrl = this._apiLocation + 'api/Login';
        this._logoutUrl = this._apiLocation + 'api/Logout';

        this._reportUrl = this._apiLocation + 'api/Report'; // TODO DELETE

        this._options = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            withCredentials: true
        });
    }

    public login(user: User): Observable<User[]> {
        var options = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/x-www-form-urlencoded'
            }),
            withCredentials: true
        });

        return this._http.post(this._loginUrl, `Username=${user.username}&Password=${user.password}`, options )
            .map((response: Response) => <User[]>response.json())
            .catch(this.handleError);
    }

    public logout(): Observable<User[]> {
        return this._http.post(this._logoutUrl, null, this._options)
            .map((response: Response) => <User[]>response.json())
            .catch(this.handleError);
    }

    public downloadReport(): Observable<User[]> {                       // TODO DELETE

        return this._http.post(this._reportUrl,null, this._options)
            .map((response: Response) => <User[]>response.json())
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}
