import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getBaseUrl() {
  return environment.appBaseUri;
}

export function apiUrlFactory() {
  return environment.apiBaseUri;
}

export function identityUrlFactory() {
  return environment.identityServerBaseUri;
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
  { provide: 'API_URL', useFactory: apiUrlFactory, deps: [] },
  { provide: 'IDENTITY_URL', useFactory: identityUrlFactory, deps: [] },
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule);
