import { Category } from './category.model';

export class Budget {
    public readonly id: number = 0;

    public totalBudgeted: number = 0;
    public totalSaved: number = 0;
    public incomeVsExpenditure: number = 0;

    public incomes: Income[] = new Array();
    public outgoings: Outgoing[] = new Array();
    public savings: Saving[] = new Array();

    public categories: Category[] = new Array();

    public transactions: Transaction[] = new Array();
}

export class Transaction {
    public readonly id: number;
    public date: Date;
    public amount: number;
    public notes: string;
    public budgetId: number;
    public readonly category: Category = new Category();

    constructor(budget: Budget = new Budget()) {
        this.budgetId = budget.id;
    }
}

export class Income {
    public readonly id: number = 0;
    public readonly name: string = '';
    public total: number = 0;
    public readonly category: Category = new Category();
}

export class Outgoing {
    public readonly id: number = 0;
    public readonly name: string = '';
    public readonly budgeted: number = 0;
    public actual: number = 0;
    public remaining: number = this.budgeted - this.actual;
    public readonly category: Category = new Category();
}

export class Saving {
    public readonly id: number = 0;
    public readonly name: string = '';
    public total: number = 0;
    public readonly category: Category = new Category();
}
