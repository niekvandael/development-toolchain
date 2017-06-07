import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NotifierModule, NotifierOptions } from 'angular-notifier';

import { AppComponent } from './app.component';
import { WelcomeComponent } from './home/welcome.component';
import { LoginComponent } from './auth/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { OrganizationListComponent } from './organizations/list/organization-list.component';
import { OrganizationInputComponent } from './organizations/input/organization-input.component';

import { WorkplaceListComponent } from './workplaces/list/workplace-list.component';

/* Feature Modules */
import { OrganizationModule } from './organizations/organization.module';
import { WorkplaceModule } from './workplaces/workplace.module';


/* Feature Service */
import { AuthService } from './auth/auth.service';
import { DashboardService } from './dashboard/dashboard.service';

/**
 * Custom angular notifier options
 */
const customNotifierOptions: NotifierOptions = {
    position: {
        horizontal: {
            position: 'left',
            distance: 12
        },
        vertical: {
            position: 'bottom',
            distance: 12,
            gap: 10
        }
    },
    theme: 'material',
    behaviour: {
        autoHide: 5000,
        onClick: false,
        onMouseover: 'pauseAutoHide',
        showDismissButton: true,
        stacking: 4
    },
    animations: {
        enabled: true,
        show: {
            preset: 'slide',
            speed: 300,
            easing: 'ease'
        },
        hide: {
            preset: 'fade',
            speed: 300,
            easing: 'ease',
            offset: 50
        },
        shift: {
            speed: 300,
            easing: 'ease'
        },
        overlap: 150
    }
};

@NgModule({
  imports: [
    BrowserModule,
      HttpModule,
      NotifierModule.forRoot(customNotifierOptions),
    RouterModule.forRoot([
      { path: 'welcome', component: WelcomeComponent },
      { path: 'login', component: LoginComponent },
      { path: 'organizations', component: OrganizationListComponent },
      { path: 'organization', component: OrganizationInputComponent, },
      { path: 'organization/:id', component: OrganizationInputComponent },
      { path: 'workplace', component: WorkplaceListComponent, },
      { path: 'dashboard', component: DashboardComponent, },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
    ]),
    OrganizationModule,
    WorkplaceModule,
    FormsModule
  ],
  declarations: [
    AppComponent,
    WelcomeComponent,
    LoginComponent,
    DashboardComponent  ],
  bootstrap: [AppComponent],
  providers: [
    AuthService,
    DashboardService
  ]
})
export class AppModule { }
