import { Component, Input, Output, EventEmitter  } from '@angular/core';
import { AuthService } from './auth.service';
import { User } from './user';

@Component({
    templateUrl: 'app/auth/login.component.html'
})

export class LoginComponent {
    public pageTitle: string = 'Login';
    public user: User;

    constructor(private _authService: AuthService) {
        this.user = new User();
    }

    public login() {
        this._authService.login(this.user).subscribe(
            (data) => console.log(data)
        );
    }

    public logout() {
        this._authService.logout().subscribe(
            (data) => console.log(data)
        );
    }

    public download() {
        this._authService.downloadReport().subscribe(
            (data) => console.log(data)
        );
    }
}
