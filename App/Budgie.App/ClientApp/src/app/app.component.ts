import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

import { Subscription } from 'rxjs/Subscription';
import { AuthService } from './services/auth.service';

@Component({
  // tslint:disable-next-line
  selector: 'body',
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