import { Injectable, Component, OnInit, OnDestroy, Inject, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import { Subscription } from 'rxjs/Subscription';

import { AuthWellKnownEndpoints, OidcSecurityService, OidcConfigService, OpenIDImplicitFlowConfiguration, AuthorizationResult } from 'angular-auth-oidc-client';

@Injectable()
export class AuthService implements OnInit, OnDestroy {
  isAuthorizedSubscription: Subscription;
  isAuthorized: boolean;

  constructor(public oidcSecurityService: OidcSecurityService,
    public oidcConfigService: OidcConfigService,
    private http: HttpClient,
    @Inject('BASE_URL') originUrl: string,
    @Inject('IDENTITY_URL') identityUrl: string
  ) {
    const openIdImplicitFlowConfiguration = new OpenIDImplicitFlowConfiguration();
    openIdImplicitFlowConfiguration.stsServer = identityUrl;
    openIdImplicitFlowConfiguration.redirect_url = originUrl + '/callback';
    openIdImplicitFlowConfiguration.client_id = 'budgie.spa.app';
    openIdImplicitFlowConfiguration.response_type = 'id_token token';
    openIdImplicitFlowConfiguration.scope = 'openid profile budgie.api';
    openIdImplicitFlowConfiguration.post_logout_redirect_uri = originUrl;
    openIdImplicitFlowConfiguration.forbidden_route = originUrl;
    openIdImplicitFlowConfiguration.unauthorized_route = originUrl;
    openIdImplicitFlowConfiguration.auto_userinfo = true;
    openIdImplicitFlowConfiguration.log_console_warning_active = true;
    openIdImplicitFlowConfiguration.log_console_debug_active = false;
    openIdImplicitFlowConfiguration.max_id_token_iat_offset_allowed_in_seconds = 10;

    const authWellKnownEndpoints = new AuthWellKnownEndpoints();
    authWellKnownEndpoints.setWellKnownEndpoints(this.oidcConfigService.wellKnownEndpoints);

    this.oidcSecurityService.setupModule(openIdImplicitFlowConfiguration, authWellKnownEndpoints);

    if (this.oidcSecurityService.moduleSetup) {
      this.doCallbackLogicIfRequired();
    } else {
      this.oidcSecurityService.onModuleSetup.subscribe(() => {
        this.doCallbackLogicIfRequired();
      });
    }
  }

  ngOnInit() {
    this.isAuthorizedSubscription = this.oidcSecurityService.getIsAuthorized().subscribe(
      (isAuthorized: boolean) => {
        this.isAuthorized = isAuthorized;
      });
  }

  ngOnDestroy(): void {
    this.isAuthorizedSubscription.unsubscribe();
    this.oidcSecurityService.onModuleSetup.unsubscribe();
  }

  getIsAuthorized(): Observable<boolean> {
    return this.oidcSecurityService.getIsAuthorized();
  }

  onAuthorizationResult(): EventEmitter<AuthorizationResult> {
    return this.oidcSecurityService.onAuthorizationResult;
  }

  login() {
    console.log('start login');
    this.oidcSecurityService.authorize();
  }

  refreshSession() {
    console.log('start refreshSession');
    this.oidcSecurityService.authorize();
  }

  logout() {
    console.log('start logoff');
    this.oidcSecurityService.logoff();
  }

  private doCallbackLogicIfRequired() {
    if (typeof location !== "undefined" && window.location.hash) {
      this.oidcSecurityService.authorizedCallback();
    }
  }
}
