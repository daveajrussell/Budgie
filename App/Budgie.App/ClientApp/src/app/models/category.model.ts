import { CategoryType } from "app/models/category-type.enum";

export class Category {
    public id: number;
    public name: string;
    public date: string;
    public colour: string;
    public type: CategoryType;
}