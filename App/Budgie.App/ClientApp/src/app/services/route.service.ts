import { Injectable } from '@angular/core';

@Injectable()
export class RouteService {

  constructor() { }

  public getRoute() {
    return JSON.parse(`{
      "routes": [
          {
              "path": "2017"
          },
          {
              "path": "2018"
          }
      ]
  }`);
  }

}
