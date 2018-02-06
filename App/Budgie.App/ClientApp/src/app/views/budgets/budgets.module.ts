import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BudgetsComponent } from './budgets.component';
import { BudgetsRoutingModule } from './budgets-routing.module';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@NgModule({
  imports: [
    BudgetsRoutingModule,
    CommonModule,
    FormsModule,
    ModalModule.forRoot(),
    CollapseModule,
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
  ],
  declarations: [BudgetsComponent]
})
export class BudgetsModule {

  constructor() { }

}
