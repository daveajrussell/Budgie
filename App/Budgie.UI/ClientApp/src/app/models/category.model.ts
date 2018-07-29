export class Category {
    public readonly id: number = 0;
    public name: string = '';
    public recurring: boolean = false;
    public recurringDate: Date;
    public recurringValue: number;
    public colour: string = '';
    public type: CategoryType = CategoryType.Variable;
}

export enum CategoryType {
    Income = 1,
    Dedicated = 2,
    Variable = 3,
    Savings = 4
}