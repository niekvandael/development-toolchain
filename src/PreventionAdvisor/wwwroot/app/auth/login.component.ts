import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';

import { Router } from '@angular/router';

import { AuthService } from './auth.service';
import { User } from './user';

@Component({
    templateUrl: 'app/auth/login.component.html'
})

export class LoginComponent {
    public pageTitle: string = 'Login';
    public user: User;

    constructor(private _authService: AuthService, private _router: Router, private _location: Location) {
        this.user = new User();
    }

    public login() {
        this._authService.login(this.user, this.loginCallback.bind(this));
    }

    private loginCallback(data: User) {
        this._authService.setUser(data);

        this._router.navigate(['dashboard']);

        
        // TODO: Check if previous is a download link
        /*
        if (window.history.length > 1) {
            this._location.back();
        } else {
            this._router.navigate(['dashboard']);
        }
        */
    }
}
