import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from "@angular/common";
import { ModalModule } from 'ngx-bootstrap/modal';

import { AccountsComponent } from './accounts.component';
import { AccountsRoutingModule } from './accounts-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    AccountsRoutingModule,
    ModalModule.forRoot()
  ],
  declarations: [AccountsComponent]
})
export class AccountsModule { }
