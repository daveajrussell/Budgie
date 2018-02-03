import { Component } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
    templateUrl: './budget-sheet.component.html'
})
export class BudgetSheetComponent {
    constructor() {
    }

    minDate = new Date(2017, 5, 10);
    maxDate = new Date(2018, 9, 15);

    bsValue: Date = new Date();
    bsRangeValue: any = [new Date(2017, 7, 4), new Date(2017, 7, 20)];
    bsConfig: any = {
        containerClass: 'theme-blue',
        displayMonths: true,
        showWeekNumbers: false
    };
}