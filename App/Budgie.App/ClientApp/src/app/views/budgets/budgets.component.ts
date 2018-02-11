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

  budget: Budget = new Budget();
  transaction: Transaction = new Transaction();
  categories: Category[] = new Array();

  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,
    private budgetService: BudgetService,
    private categoryService: CategoryService,
    private modalService: BsModalService) {
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      if (!params.year && !params.month) {
        this.currentDate = moment();
        this.setDate();
        this.goToBudget(this.year, this.month);
      } else {
        this.currentDate = moment(`${params.year}-${moment().month(params.month).format("MM")}-01`);
        this.setDate();
        this.getReport();
      }
    });
  }

  openModal(template: TemplateRef<any>, content: Transaction) {
    this.bsModalRef = this.modalService.show(template);
    this.bsModalRef.content = JSON.parse(JSON.stringify(content));
  }

  protected parseDate(date: string): Date {
    return new Date(date);
  }

  closeModal() {
    this.bsModalRef.hide();
  }

  goBack = () => {
    this.currentDate = this.currentDate.subtract(1, 'month');
    this.setDate();
    this.goToBudget(this.year, this.month);
  }

  goForward = () => {
    this.currentDate = this.currentDate.add(1, 'month');
    this.setDate();
    this.goToBudget(this.year, this.month);
  }

  goToBudget = (year: string, month: string) => {
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
    this.reset();
    this.recalculateTransactions();
    this.recalculateOutgoings();
    this.recalculateTotals();
  }

  private reset() {
    this.budget.totalBudgeted = 0;
    this.budget.totalSaved = 0;
    this.budget.incomeVsExpenditure = 0;
    this.budget.incomes.forEach(x => x.total = 0);
    this.budget.outgoings.forEach(x => x.actual = 0);
    this.budget.savings.forEach(x => x.total = 0);
  }

  private recalculateTransactions() {
    this.budget.transactions.forEach((transaction) => {

      let income = this.budget.incomes.find((income) => {
        return income.category.id === transaction.category.id;
      });

      if (income) {
        income.total = Number(transaction.amount);
      }

      let outgoing = this.budget.outgoings.find((outgoing) => {
        return outgoing.category.id === transaction.category.id;
      });

      if (outgoing) {
        outgoing.actual += Number(transaction.amount);
        this.budget.incomeVsExpenditure -= Number(transaction.amount);
        outgoing.remaining = outgoing.budgeted - outgoing.actual;
      }

      let saving = this.budget.savings.find((saving) => {
        return saving.category.id === transaction.category.id;
      });

      if (saving) {
        saving.total = Number(transaction.amount);
      }
    });
  }

  private recalculateOutgoings() {
    this.budget.outgoings.forEach((outgoing) => {
      outgoing.remaining = outgoing.budgeted - outgoing.actual;
    });
  }

  private recalculateTotals() {
    this.budget.incomes.forEach((income) => {
      this.budget.incomeVsExpenditure += Number(income.total);
    });

    this.budget.outgoings.forEach((outgoing) => {
      this.budget.totalBudgeted += Number(outgoing.budgeted);
    });

    this.budget.savings.forEach((saving) => {
      this.budget.totalSaved += Number(saving.total);
    });
  }

  private getReport = () => {
    this.budgetService
      .getBudget(this.yearNumber, this.monthNumber + 1)
      .subscribe((budget) => {
        this.budget = budget;
        this.recalculate();
        this.transaction = new Transaction(budget);
      });

    this.categoryService
      .getCategories()
      .subscribe(categories => this.categories = categories);
  }

  protected saveEditable($event: any, outgoing: Outgoing) {
    this.budgetService.editOutgoing(outgoing)
      .subscribe(x => this.recalculate());
  }

  private setDate = () => {
    this.month = this.currentDate.format('MMMM');
    this.monthNumber = this.currentDate.month();
    this.year = this.currentDate.format('YYYY');
    this.yearNumber = this.currentDate.year();
  }
}