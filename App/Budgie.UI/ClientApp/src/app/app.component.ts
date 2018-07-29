import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

import { Subscription } from 'rxjs/Subscription';
import { AuthService } from './services/auth.service';
import { AuthConfiguration, AuthorizationResult } from 'angular-auth-oidc-client';

@Component({
  selector: 'body',
  styleUrls: ['./app.component.css'],
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {

  isAuthorizedSubscription: Subscription;
  isAuthorized: boolean;

  constructor(private router: Router, public authService: AuthService) { }

  ngOnInit() {
    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
      window.scrollTo(0, 0)
    });

    this.isAuthorizedSubscription = this.authService.getIsAuthorized().subscribe(
      (isAuthorized: boolean) => {
        this.isAuthorized = isAuthorized;
      });

    this.authService.onAuthorizationResult().subscribe((authResult: AuthorizationResult) => {
      if (authResult === AuthorizationResult.authorized) {
        window.setTimeout(() => this.router.navigate(['budgets']), 1);
      }
    });
  }

  ngOnDestroy(): void {
    this.isAuthorizedSubscription.unsubscribe();
  }

  public login(): void {
    this.authService.login();
  }

  public refreshSession(): void {
    this.authService.refreshSession();
  }

  public logout(): void {
    this.authService.logout();
  }
}
