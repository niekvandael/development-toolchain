import { AuthService } from './auth/auth.service';
import { Component } from '@angular/core';
import { User } from './auth/user';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'pm-app',
    templateUrl: 'app/app.component.html'
})

export class AppComponent {
    pageTitle: string = '';
    errorMessage: string;
    isAuthenticated: boolean;
    user: User;
    displayName: string;

    constructor(private _authService: AuthService, private _router: Router) {
        this._authService.getAuthenticatedUser()
                .subscribe(user => {
                    this.user = user;
                    this.isAuthenticated = true;
                    this.displayName = user.firstname;
                }, error => this.errorMessage = <any>error);
     };

    public logout() {
        this._authService.logout(this.loggedOut.bind(this));
    }

    loggedOut() {
        this.isAuthenticated = false;
        this.displayName = '';

        this._authService.isAuthenticated = false;
        this._authService.user = undefined;
        this._router.navigate(['login']);
    }

}
