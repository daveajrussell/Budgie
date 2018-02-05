export class Account {
    public id: number;
    public name: string;
    public date: string;
    public type: AccountType;
    public balance: number;
    public status: AccountStatus;
}

export enum AccountType {
    Current = 1,
    Credit = 2,
    Savings = 3
}

export enum AccountStatus {
    Active = 1,
    Inactive = 2
}
