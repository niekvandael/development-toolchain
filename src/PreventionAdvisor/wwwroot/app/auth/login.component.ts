import { Component, Input, Output, EventEmitter  } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from './auth.service';
import { User } from './user';

@Component({
    templateUrl: 'app/auth/login.component.html'
})

export class LoginComponent {
    public pageTitle: string = 'Login';
    public user: User;

    constructor(private _authService: AuthService, private _router: Router) {
        this.user = new User();
    }

    public login() {
        this._authService.login(this.user).subscribe (
            (data) => this._router.navigate(['organizations'])
        );
    }

    public logout() {
        this._authService.logout().subscribe(
            (data) => console.log(data)
        );
    }
}
