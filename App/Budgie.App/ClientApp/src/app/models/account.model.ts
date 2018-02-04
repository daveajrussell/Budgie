import { AccountType } from "app/models/account-type.enum";
import { AccountStatus } from "app/models/account-status.enum";

export class Account {
    public id: number;
    public name: string;
    public date: string;
    public type: AccountType;
    public balance: number;
    public status: AccountStatus;
}