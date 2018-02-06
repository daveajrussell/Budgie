import { Category } from './category.model';

export class Budget {
    public readonly id: number = 0;

    public totalBudgeted: number = 0;
    public totalSaved: number = 0;
    public incomeVsExpenditure: number = 0;

    public incomes: Income[] = new Array();
    public dedicated: Outgoing[] = new Array();
    public budgeted: Outgoing[] = new Array();
    public savings: Outgoing[] = new Array();

    public categories: Category[] = new Array();

    public transaction: Transaction[] = new Array();
}

export class Transaction {
    public readonly id: number = 0;
    public date: Date = new Date();
    public category: string = '';
    public readonly categoryId: number = 0;
    public amount: number = 0;
    public notes: string = '';
    public type: TransactionType = TransactionType.Outgoing;
}

export enum TransactionType {
    Outgoing = 1,
    Income = 2
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