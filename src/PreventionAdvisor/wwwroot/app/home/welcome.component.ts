import { Component } from '@angular/core';

@Component({
    templateUrl: 'app/home/welcome.component.html'
})
export class WelcomeComponent {
    public pageTitle: string = 'Werkplek Rapporten';


    login(): void {
        alert('login');
    }

    register(): void {
        alert('Register');
    }
}
