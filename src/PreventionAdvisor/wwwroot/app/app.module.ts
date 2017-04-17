import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { WelcomeComponent } from './home/welcome.component';
import { LoginComponent } from './auth/login.component';
import { OrganizationListComponent } from './organizations/list/organization-list.component';
import { OrganizationInputComponent } from './organizations/input/organization-input.component';

/* Feature Modules */
import { OrganizationModule } from './organizations/organization.module';

/* Feature Service */
import { AuthService } from './auth/auth.service';

@NgModule({
  imports: [
    BrowserModule,
    HttpModule,
    RouterModule.forRoot([
        { path: 'welcome', component: WelcomeComponent },
        { path: 'login', component: LoginComponent },
        { path: 'organizations', component: OrganizationListComponent },
        { path: 'organization', component: OrganizationInputComponent , },
        { path: 'organization/:id', component: OrganizationInputComponent },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
    ]),
      OrganizationModule,
      FormsModule
  ],
  declarations: [
      AppComponent,
      WelcomeComponent,
      LoginComponent
  ],
  bootstrap: [AppComponent],
  providers: [
      AuthService
  ]
})
export class AppModule { }
