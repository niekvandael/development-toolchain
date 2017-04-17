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

    constructor(public _authService: AuthService, private _router: Router) {
        this._authService.getAuthenticatedUser()
                .subscribe(user => {}, error => this.errorMessage = <any>error);
     };

    public logout() {
        this._authService.logout()
        .subscribe(r => {
            this._authService.setAuthenticated(undefined);
            this._router.navigate(['login']);
        });
    }

    loggedOut() {
        this._authService.setUser(undefined);
        this._router.navigate(['login']);
    }

}
