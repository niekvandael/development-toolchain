import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

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

    constructor(private _http: Http) {
        this._commonComponent = new CommonComponent();
        this._apiLocation = this._commonComponent.getAPILocation();
        this._loginUrl = this._apiLocation + '/Auth/Login';
    }

    public login(user: User): Observable<User[]> {
        return this._http.post(this._loginUrl, user /*TODO: Change object to plain values*/)
            .map((response: Response) => <User[]>response.json())
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}
