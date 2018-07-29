import { Routes, RouterModule } from '@angular/router';

// Import Containers
import {
  DefaultLayoutComponent,
  UnauthorisedLayoutComponent
} from './containers';

import { AuthorizationGuard } from './authorization.guard';

export const routes: Routes = [
  { path: '', component: UnauthorisedLayoutComponent, pathMatch: 'full' },
  { path: 'home', component: UnauthorisedLayoutComponent, pathMatch: 'full' },
  {
    path: '',
    canActivate: [AuthorizationGuard],
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      // {
      //   path: 'dashboard',
      //   loadChildren: './views/dashboard/dashboard.module#DashboardModule'
      // },
      {
        path: 'budgets',
        loadChildren: './views/budgets/budgets.module#BudgetsModule'
      },
      {
        path: 'categories',
        loadChildren: './views/categories/categories.module#CategoriesModule'
      }
    ]
  }
];

export const routing = RouterModule.forRoot(routes);