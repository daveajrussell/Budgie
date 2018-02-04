import { Component, OnInit, TemplateRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AccountsService } from '../../services/accounts.service';
import { CategoryService } from 'app/services/category.service';
import { Category } from 'app/models/category.model';
import { AccountType } from 'app/models/account-type.enum';
import { CategoryType } from 'app/models/category-type.enum';
import { AccountStatus } from 'app/models/account-status.enum';
import { Account } from 'app/models/account.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import * as moment from 'moment';

@Component({
  templateUrl: './accounts.component.html'
})
export class AccountsComponent implements OnInit {

  bsModalRef: BsModalRef;

  accountTypes = AccountType;
  accountStatuses = AccountStatus;
  catgeoryTypes = CategoryType;

  accounts: Account[] = new Array();
  account: Account = {
    id: 0,
    name: '',
    balance: 0,
    date: '',
    status: this.accountStatuses.Active,
    type: this.accountTypes.Current
  };
  categories: Category[] = new Array();
  category: Category = {
    id: 0,
    name: '',
    date: '',
    colour: '',
    type: this.catgeoryTypes.Budgeted
  };

  accountTypeKeys: any[];
  accountStatusKeys: any[];
  categoryTypeKeys: any[];

  constructor(
    private accountsService: AccountsService,
    private categoryService: CategoryService,
    private modalService: BsModalService) {
    this.accountTypeKeys = Object.keys(AccountType).filter(Number);
    this.accountStatusKeys = Object.keys(AccountStatus).filter(Number);
    this.categoryTypeKeys = Object.keys(CategoryType).filter(Number);
  }

  ngOnInit() {
    this.getAccounts();
    this.getCategories();
  }

  openModal(template: TemplateRef<any>, content: Category | Account) {
    this.bsModalRef = this.modalService.show(template);
    this.bsModalRef.content = JSON.parse(JSON.stringify(content));
  }

  closeModal() {
    this.bsModalRef.hide();
  }

  getAccounts = (): void => {
    this.accountsService.getAccounts()
      .subscribe(accounts => this.accounts = accounts);
  }

  addAccount = (account: Account) => {
    this.accountsService.addAccount(account)
      .subscribe((account) => {
        this.accounts.push(account);
        this.closeModal();
      });
  }

  editAccount = (account: Account) => {
    this.accountsService.editAccount(account)
      .subscribe(() => {
        let idx = this.accounts.map((x) => { return x.id; }).indexOf(account.id);
        if (idx > -1)
          this.accounts[idx] = account;
        this.closeModal();
      });
  }

  getCategories = (): void => {
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories);
  }

  addCategory = (category: Category) => {
    this.categoryService.addCategory(category)
      .subscribe((category) => {
        this.categories.push(category);
        this.closeModal();
      });
  }

  editCategory = (category: Category) => {
    this.categoryService.editCategory(category)
      .subscribe(() => {
        let idx = this.categories.map((x) => { return x.id; }).indexOf(category.id);
        if (idx > -1)
          this.categories[idx] = category;
        this.closeModal();
      });
  }
}
