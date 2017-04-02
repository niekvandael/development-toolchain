import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';

import { IUser } from './user';

@Injectable()
export class AuthService {
    private _loginUrl = 'api/Login';

    constructor(private _http: Http) { }

    login(user: IUser): Observable<IUser[]> {
        return this._http.post(this._loginUrl, user)
            .map((response: Response) => <IUser[]>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}
