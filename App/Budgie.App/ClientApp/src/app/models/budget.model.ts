import { Category } from './category.model';

export class Budget {
    public readonly id: number = 0;

    public totalBudgeted: number = 0;
    public totalSaved: number = 0;
    public incomeVsExpenditure: number = 0;

    public incomes: Income[] = new Array();
    public outgoings: Outgoing[] = new Array();

    public categories: Category[] = new Array();

    public transactions: Transaction[] = new Array();
}

export class Transaction {
    public readonly id: number = 0;
    public date: Date = new Date();
    public category: string = '';
    public readonly categoryId: number = 0;
    public amount: number = 0;
    public notes: string = '';
}

export class Income {
    public readonly id: number = 0;
    public readonly name: string = '';
    public readonly total: number = 0;
}

export class Outgoing {
    public readonly id: number = 0;
    public readonly name: string = '';
    public readonly budgeted: number = 0;
    public actual: number = 0;
    public readonly remaining: number = 0;
    public readonly category: Category = new Category();
}