import { NgIf, NgForOf } from '@angular/common';
import { Component } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Router, ActivatedRoute, Params } from '@angular/router';
import * as moment from 'moment';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { BudgetService } from '../../services';
import { Budget, Transaction, TransactionType } from 'app/models';

@Component({
  templateUrl: './budgets.component.html'
})
export class BudgetsComponent implements OnInit {

  currentDate = moment();
  month: string = '';
  monthNumber: number = 0;
  year: string = '';
  yearNumber: number = 0;

  budget: Budget;

  transactionTypes = TransactionType;
  transactionTypeKeys: any[];

  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,
    private budgetService: BudgetService) {
    this.transactionTypeKeys = Object.keys(TransactionType).filter(Number);
    this.budget = new Budget();
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      if (!params.year && !params.month) {
        this.currentDate = moment();
        this.setDate();
        this.goToSheet(this.year, this.month);
        this.getReport();
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

  addTransaction = (transaction: Transaction) => {

  }

  editTransaction = (transaction: Transaction) => {

  }

  deleteTransaction = (transaction: Transaction) => {

  }

  private getReport = () => {
    this.budgetService
      .getBudget(this.yearNumber, this.monthNumber)
      .subscribe(budget => this.budget = budget);
  }

  private setDate = () => {
    this.month = this.currentDate.format('MMMM');
    this.monthNumber = this.currentDate.month();
    this.year = this.currentDate.format('YYYY');
    this.yearNumber = this.currentDate.year();
  }

  minDate = new Date(this.currentDate.year(), this.currentDate.month(), moment().startOf('month').date());
  maxDate = new Date(this.currentDate.year(), this.currentDate.month(), moment().endOf('month').date());

  bsValue: Date = new Date();
  bsConfig: any = {
    containerClass: 'theme-blue',
    displayMonths: true,
    showWeekNumbers: false,
    dateInputFormat: 'DD/MM/YYYY'
  };
}