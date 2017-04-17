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
    private _authenticatedUserUrl: string;

    private _options: RequestOptions;

    private _authenticated: boolean;
    private _user: User;

    constructor(private _http: Http) {
        this._commonComponent = new CommonComponent();
        this._apiLocation = this._commonComponent.getAPILocation();
        this._loginUrl = this._apiLocation + 'api/Login';
        this._logoutUrl = this._apiLocation + 'api/Logout';
        this._authenticatedUserUrl = this._apiLocation + 'api/whoami';

        this._options = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            withCredentials: true
        });
    }

    public login(user: User): Observable<User> {
        let options = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/x-www-form-urlencoded'
            }),
            withCredentials: true
        });

        return this._http.post(this._loginUrl, `Username=${user.username}&Password=${user.password}`, options )
            .map((response: Response) => <User[]>response.json())
            .catch(this.handleError);
    }

    public logout(callback: any)  {
        this._http.post(this._logoutUrl, null, this._options)
            .map(callback())
            .catch(this.handleError);
    }

    private handleError(err: Response) {
        return Observable.throw(err);
    }

    public getAuthenticatedUser(): Observable<User> {
       return this._http.get(this._authenticatedUserUrl, this._options)
            .map((response: Response) => {
                this.setUser(<User>response.json());
                return <User>response.json();
            })
            .catch(this.handleError);
    }

    public isAuthenticated(): boolean{
        return this._authenticated;
    }

    public setAuthenticated(authenticated: boolean){
        this._authenticated = authenticated;
    }

    public setUser(user: User){
        this._user = user;
        if(this._user != undefined)
        {
            this._authenticated = true;
        }
        else
        {
            this._authenticated = false;
        }
    }

    public getUser(): User {
        return this._user;
    }
}
