import { Component } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  templateUrl: './budgets.component.html'
})
export class BudgetsComponent {
  constructor() {
  }

  isCollapsed: boolean = true;
  iconCollapse: string = "icon-arrow-down";

  minDate = new Date(2017, 5, 10);
  maxDate = new Date(2018, 9, 15);

  bsValue: Date = new Date();
  bsRangeValue: any = [new Date(2017, 7, 4), new Date(2017, 7, 20)];
  bsConfig: any = {
    containerClass: 'theme-blue',
    displayMonths: true,
    showWeekNumbers: false
  };

  collapsed(event: any): void {
    // console.log(event);
  }

  expanded(event: any): void {
    // console.log(event);
  }

  toggleCollapse(): void {
    this.isCollapsed = !this.isCollapsed;
    this.iconCollapse = this.isCollapsed ? "icon-arrow-down" : "icon-arrow-up";
  }
}