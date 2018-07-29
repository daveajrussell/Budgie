import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BudgetsComponent } from './budgets.component';
import { BudgetsRoutingModule } from './budgets-routing.module';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PipeModule } from '../../pipes/pipes.module';
import { InlineEditorModule } from '@qontu/ngx-inline-editor';

@NgModule({
  imports: [
    BudgetsRoutingModule,
    CommonModule,
    FormsModule,
    ModalModule.forRoot(),
    CollapseModule,
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    PipeModule,
    InlineEditorModule
  ],
  declarations: [
    BudgetsComponent
  ]
})
export class BudgetsModule {

  constructor() { }

}
