import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

import { AppComponent } from './app.component';
import { environment } from '../environments/environment';

// Import containers
import {
  DefaultLayoutComponent,
  UnauthorisedLayoutComponent
} from './containers';

const APP_CONTAINERS = [
  DefaultLayoutComponent,
  UnauthorisedLayoutComponent
]
// Import services
import {
  AuthService,
  BudgetService,
  CategoryService
} from './services';

const APP_SERVICES = [
  AuthService,
  BudgetService,
  CategoryService
]

// Import OIDC
import {
  AuthModule,
  OidcSecurityService,
  OidcConfigService
} from 'angular-auth-oidc-client';

import { AuthorizationGuard } from './authorization.guard';

import {
  AppAsideModule,
  AppBreadcrumbModule,
  AppHeaderModule,
  AppFooterModule,
  AppSidebarModule,
} from '@coreui/angular'

import { routing } from './app.routing';

// Import 3rd party components
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { InlineEditorModule } from '@qontu/ngx-inline-editor';

export function loadConfig(oidcConfigService: OidcConfigService) {
  return () => oidcConfigService.load_using_stsServer(environment.identityServerBaseUri);
}

@NgModule({
  imports: [
    HttpClientModule,
    AuthModule.forRoot(),
    BrowserModule,
    routing,
    AppAsideModule,
    AppBreadcrumbModule.forRoot(),
    AppFooterModule,
    AppHeaderModule,
    AppSidebarModule,
    PerfectScrollbarModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ChartsModule,
    InlineEditorModule
  ],
  declarations: [
    AppComponent,
    ...APP_CONTAINERS
  ],
  providers: [
    ...APP_SERVICES,
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
    AuthorizationGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
