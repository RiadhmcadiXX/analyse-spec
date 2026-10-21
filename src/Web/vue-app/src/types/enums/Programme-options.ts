import { ProgrammeName } from "@/types/enums/ProgrammeName";
import { ProgrammeCode } from "@/types/enums/ProgrammeCode";
import { FormOption } from "@/types/formOption";
import {FormOptionProgramme} from "@/types/formOptionProgramme";

export const programmeOptions: FormOptionProgramme[] = Object.keys(ProgrammeName).map((key) => ({
    name: ProgrammeName[key as keyof typeof ProgrammeName], 
    code: ProgrammeName[key as keyof typeof ProgrammeName], 
}));
