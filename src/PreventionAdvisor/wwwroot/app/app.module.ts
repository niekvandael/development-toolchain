import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent }  from './app.component';
import { WelcomeComponent } from './home/welcome.component';
import { LoginComponent } from './auth/login.component';

/* Feature Modules */
import { ProductModule } from './products/product.module';

/* Feature Service */
import { AuthService } from './auth/auth.service'

@NgModule({
  imports: [
    BrowserModule,
    HttpModule,
    RouterModule.forRoot([
        { path: 'welcome', component: WelcomeComponent },
        { path: 'login', component: LoginComponent },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
    ]),
    ProductModule
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
