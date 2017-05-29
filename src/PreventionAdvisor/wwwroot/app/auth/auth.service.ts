import { Injectable } from '@angular/core';
import { RequestOptions, Headers } from '@angular/http';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { CommonService } from '../shared/commonService';

import { User } from './user';


@Injectable()
export class AuthService extends CommonService {
    private _authenticated: boolean;
    private _user: User;

    public login(user: User, callback: any) {
        let options = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/x-www-form-urlencoded'
            }),
            withCredentials: true
        });

        this.doPost('api/Login', `Username=${user.username}&Password=${user.password}`, callback, options, this.errorHandler.bind(this));
    }

    public logout(callback: any) {
       this.doPost('api/Logout', null, callback);
    }

    public getAuthenticatedUser(callback: any) {
       this.doGet('api/whoami', callback);
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

    public errorHandler(): void {
        this._notifier.notify('warning', 'Gebruikersnaam of wachtwoord niet correct!');
    }
}
