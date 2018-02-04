import { Component } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Router, ActivatedRoute, Params } from '@angular/router';
import * as moment from 'moment';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';

@Component({
  templateUrl: './budgets.component.html'
})
export class BudgetsComponent implements OnInit {

  currentDate = moment();
  month = '';
  year = '';

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      if (!params.year && !params.month) {
        this.currentDate = moment();
        this.setDate();
        this.goToSheet(this.year, this.month);
      } else {
        this.currentDate = moment(`${params.year}-${moment().month(params.month).format("MM")}-01`);
        this.setDate();
      }
    });
  }

  goBack = () => {
    this.currentDate = this.currentDate.subtract(1, 'month');
    this.setDate();
    this.goToSheet(this.year, this.month);
  }

  goForward = () => {
    this.currentDate = this.currentDate.add(1, 'month');
    this.setDate();
    this.goToSheet(this.year, this.month);
  }

  goToSheet = (year: string, month: string) => {
    this.router.navigate(['/budgets', year, month.toLowerCase()]);
  }

  private setDate = () => {
    this.month = this.currentDate.format('MMMM');
    this.year = this.currentDate.format('YYYY');
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