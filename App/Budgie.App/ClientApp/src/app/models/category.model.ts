export class Category {
    public readonly id: number;
    public name: string;
    public date: string;
    public colour: string;
    public type: CategoryType;
}

export enum CategoryType {
    Income = 1,
    Dedicated = 2,
    Budgeted = 3,
    Savings = 4
}