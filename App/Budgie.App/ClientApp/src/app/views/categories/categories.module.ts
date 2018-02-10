import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from "@angular/common";
import { ModalModule } from 'ngx-bootstrap/modal';

import { CategoriesComponent } from './categories.component';
import { CategoriesRoutingModule } from './categories-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CategoriesRoutingModule,
    ModalModule.forRoot()
  ],
  declarations: [CategoriesComponent]
})
export class CategoriesModule { }
