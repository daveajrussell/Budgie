import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from "@angular/common";
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { CategoriesComponent } from './categories.component';
import { CategoriesRoutingModule } from './categories-routing.module';
import { DateFormatPipe } from '../../pipes';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CategoriesRoutingModule,
    BsDatepickerModule.forRoot(),
    ModalModule.forRoot()
  ],
  declarations: [
    CategoriesComponent,
    DateFormatPipe
  ],
  providers: [
    DateFormatPipe
  ]
})
export class CategoriesModule { }
