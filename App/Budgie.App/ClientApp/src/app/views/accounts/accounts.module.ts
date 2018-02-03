import { NgModule } from '@angular/core';

import { ModalModule } from 'ngx-bootstrap/modal';

import { AccountsComponent } from './accounts.component';
import { AccountsRoutingModule } from './accounts-routing.module';

@NgModule({
  imports: [
    AccountsRoutingModule,
    ModalModule.forRoot()
  ],
  declarations: [ AccountsComponent ]
})
export class AccountsModule { }
