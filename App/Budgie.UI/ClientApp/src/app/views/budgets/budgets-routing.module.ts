import { NgModule } from '@angular/core';
import {
  Routes,
  RouterModule
} from '@angular/router';

import { BudgetsComponent } from './budgets.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        data: {
          title: 'Budgets'
        },
        component: BudgetsComponent,
      },
      {
        path: ':year/:month',
        data: {
          title: 'Budgets'
        },
        component: BudgetsComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BudgetsRoutingModule {
  constructor() {
  }
}
