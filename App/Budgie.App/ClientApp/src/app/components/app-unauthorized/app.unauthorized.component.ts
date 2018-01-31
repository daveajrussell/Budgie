import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
    selector: 'app-unauthorized',
    templateUrl: './app.unauthorized.component.html'
})
export class AppUnauthorizedComponent implements OnInit {

    constructor(private location: Location) {

    }

    ngOnInit() {
    }

    login() {
        //this.service.startSigninMainWindow();
    }

    goback() {
        this.location.back();
    }
}
