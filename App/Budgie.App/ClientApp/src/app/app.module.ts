import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetchdata/fetchdata.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';

import { AuthModule, OidcSecurityService, OidcConfigService } from 'angular-auth-oidc-client';
import { AuthService } from './services/auth.service';

import { environment } from '../environments/environment';

export function loadConfig(oidcConfigService: OidcConfigService) {
  console.log('APP_INITIALIZER STARTING');
  return () => oidcConfigService.load_using_stsServer(environment.identityServerBaseUri);
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    UnauthorizedComponent
  ],
  imports: [
    AuthModule.forRoot(),
    CommonModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'unauthorized', component: UnauthorizedComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetchdata', component: FetchDataComponent },
      { path: '**', redirectTo: 'home' }
    ])
  ],
  providers: [
    AuthModule,
    AuthService,
    OidcSecurityService,
    OidcConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: loadConfig,
      deps: [OidcConfigService],
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
