import { NgModule } from '@angular/core';
import {
  Routes,
  RouterModule
} from '@angular/router';

import { BudgetsComponent } from './budgets.component';
import { BudgetSheetComponent } from './budget-sheet.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Budgets'
    },
    children: [
      {
        path: '',
        data: { title: 'Sheets' },
        component: BudgetsComponent,
      },
      {
        path: ':year/:month',
        data: {
          title: '[month] [year]'
        },
        component: BudgetSheetComponent
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