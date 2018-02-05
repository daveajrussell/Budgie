export class Budget {

    public totalBudgeted: number;
    public totalSaved: number;
    public incomeVsExpenditure: number;

    public dedicated: Outgoing[];
    public budgeted: Outgoing[];
    public savings: Outgoing[];

    public transaction: Transaction[];
}

export class Transaction {
    public readonly id: number;
    public date: string;
    public category: string;
    public readonly categoryId: number;
    public amount: number;
    public notes: string;
    public type: TransactionType;
}

export enum TransactionType {
    Outgoing = 1,
    Income = 2
}

export class Income {
    public readonly id: number;
    public readonly name: string;
    public readonly balance: number;
}

export class Outgoing {
    public readonly id: number;
    public readonly name: string;
    public readonly budgeted: number;
    public actual: number;
    public readonly remaining: number;
}