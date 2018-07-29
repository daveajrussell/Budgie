import { Component, OnInit, TemplateRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

import { CategoryService } from '../../services';

import {
  Transaction,
  Category,
  CategoryType
} from 'app/models';

import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import * as moment from 'moment';

@Component({
  templateUrl: './categories.component.html'
})
export class CategoriesComponent implements OnInit {

  bsModalRef: BsModalRef;

  catgeoryTypes = CategoryType;

  categories: Category[] = new Array();
  category: Category = new Category();

  categoryTypeKeys: any[];

  currentDate = moment();

  minDate = new Date(this.currentDate.year(), this.currentDate.month(), moment().startOf('month').date());

  bsConfig: Partial<BsDatepickerConfig>;

  constructor(
    private categoryService: CategoryService,
    private modalService: BsModalService) {
    this.categoryTypeKeys = Object.keys(CategoryType).filter(Number);

    this.bsConfig = new BsDatepickerConfig();
    this.bsConfig.containerClass = 'theme-blue';
    this.bsConfig.showWeekNumbers = false;
    this.bsConfig.dateInputFormat = 'DD/MM/YYYY';
  }

  ngOnInit() {
    this.getCategories();
  }

  openModal(template: TemplateRef<any>, content: Category) {
    this.bsModalRef = this.modalService.show(template);
    this.bsModalRef.content = JSON.parse(JSON.stringify(content));
  }

  closeModal() {
    this.bsModalRef.hide();
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
