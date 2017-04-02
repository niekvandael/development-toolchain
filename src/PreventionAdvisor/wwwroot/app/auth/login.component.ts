import { Component } from '@angular/core';
import { AuthService } from './auth.service'
@Component({
    templateUrl: 'app/auth/login.component.html'
})

export class LoginComponent {
    public pageTitle: string = 'Login';

    constructor(private _authService: AuthService) {

    }

    login() {
        this._authService.login({});
    }


}
