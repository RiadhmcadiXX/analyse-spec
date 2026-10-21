import { Administrator, Book, Member, User, Student, Option } from "@/types/entities"
import { Guid } from "@/types";
export class Answer {
    id?: Guid |undefined;
    idOption?: Option | undefined;
    idStudent?: Student | undefined;
}