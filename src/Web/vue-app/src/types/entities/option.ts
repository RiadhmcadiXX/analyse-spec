import { Administrator, Book, Member, User, Student, Question } from "@/types/entities"
import { Guid } from "../guid";
export class Option {
    id?: Guid;
    idQuestion?: Question;
    title?: string;
}