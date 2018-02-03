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
    data: { title: 'Budgets' },
    component: BudgetsComponent,
    children: [
      // {
      //   path: ':year/:month',
      //   data: {
      //     title: '[month] [year]'
      //   },
      //   component: BudgetSheetComponent
      // }
      {
        path: 'test',
        component: BudgetSheetComponent,
        data: {
          title: 'Test'
        }
      }
    ]
  },
  // {
  //   path: ':year/:month',
  //   data: {
  //     title: '[month] [year]'
  //   },
  //   component: BudgetSheetComponent
  // }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BudgetsRoutingModule {
  constructor() {
  }
}