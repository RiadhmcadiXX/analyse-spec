import { Guid } from "../guid";
export class Question {
    id?: Guid;
    title?: string;
    label?: string;
    parent?: Question;
}