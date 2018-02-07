import { NgIf, NgForOf } from '@angular/common';
import { Component, TemplateRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import * as moment from 'moment';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { BudgetService, CategoryService } from '../../services';
import { Budget, Transaction, Category, CategoryType, Outgoing } from 'app/models';

@Component({
  templateUrl: './budgets.component.html'
})
export class BudgetsComponent implements OnInit {

  bsModalRef: BsModalRef;

  currentDate = moment();
  month: string = '';
  monthNumber: number = 0;
  year: string = '';
  yearNumber: number = 0;

  minDate = new Date(this.currentDate.year(), this.currentDate.month(), moment().startOf('month').date());
  maxDate = new Date(this.currentDate.year(), this.currentDate.month(), moment().endOf('month').date());

  bsConfig: any = {
    containerClass: 'theme-blue',
    displayMonths: true,
    showWeekNumbers: false,
    dateInputFormat: 'DD/MM/YYYY'
  };

  budget: Budget;
  transaction: Transaction;
  categories: Category[];

  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,
    private budgetService: BudgetService,
    private categoryService: CategoryService,
    private modalService: BsModalService) {
    this.budget = new Budget();
    this.transaction = new Transaction();
    this.categories = new Array();
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

  openModal(template: TemplateRef<any>, content: Transaction) {
    console.log(content);
    this.bsModalRef = this.modalService.show(template);
    this.bsModalRef.content = JSON.parse(JSON.stringify(content));
  }

  closeModal() {
    this.bsModalRef.hide();
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
    this.budgetService.addTransaction(transaction)
      .subscribe((transaction) => {
        this.budget.transactions.push(transaction);
        this.recalculate();
        this.closeModal();
      });
  }

  editTransaction = (transaction: Transaction) => {
    this.budgetService.editTransaction(transaction)
      .subscribe(() => {
        let idx = this.budget.transactions.map((x) => { return x.id; }).indexOf(transaction.id);
        if (idx > -1)
          this.budget.transactions[idx] = transaction;
        this.recalculate();
        this.closeModal();
      });
  }

  deleteTransaction = (transaction: Transaction) => {
    this.budgetService.deleteTransaction(transaction)
      .subscribe((transaction) => {
        let idx = this.budget.transactions.map((x) => { return x.id; }).indexOf(transaction.id);
        if (idx > -1)
          this.budget.transactions.splice(idx, 1);
        this.recalculate();
        this.closeModal();
      });
  }

  private recalculate() {
    this.budget.outgoings.forEach(x => x.actual = 0);
    this.budget.transactions.forEach((x) => {
      let outgoing = this.budget.outgoings.find(y => y.category.id == x.categoryId);
      outgoing.actual += x.amount;
    });
  }

  private getReport = () => {
    this.budgetService
      .getBudget(this.yearNumber, this.monthNumber)
      .subscribe(budget => this.budget = budget);

    this.categoryService
      .getCategories()
      .subscribe(categories => this.categories = categories);
  }

  private setDate = () => {
    this.month = this.currentDate.format('MMMM');
    this.monthNumber = this.currentDate.month();
    this.year = this.currentDate.format('YYYY');
    this.yearNumber = this.currentDate.year();
  }
}