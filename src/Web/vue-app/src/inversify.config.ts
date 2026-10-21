import { Container } from "inversify";
import axios, { AxiosInstance } from 'axios';
import "reflect-metadata";

import { TYPES } from "@/injection/types";
import {
    IAdministratorService,
    IApiService,
    IAuthenticationService,
    IBookService,
    IMemberService,
    IUserService,
    IStudentService,
    IAnswerService,
    IOptionService,
    IQuestionService, 
    ICsvAnalyseService, 
    ISpecService
} from "@/injection/interfaces";
import {
    ApiService,
    AuthenticationService,
    BookService,
    MemberService,
    UserService,
    StudentService,
    AnswerService,
    OptionService,
    QuestionService,
    SpecService
} from "@/services";
import { AdministratorService } from "@/services/administratorService";
import {CsvAnalyseService} from "@/services/csvAnalyseService";

const dependencyInjection = new Container();
dependencyInjection.bind<AxiosInstance>(TYPES.AxiosInstance).toConstantValue(axios.create())
dependencyInjection.bind<IApiService>(TYPES.IApiService).to(ApiService).inSingletonScope()
dependencyInjection.bind<IAdministratorService>(TYPES.IAdministratorService).to(AdministratorService).inSingletonScope()
dependencyInjection.bind<IAuthenticationService>(TYPES.IAuthenticationService).to(AuthenticationService).inSingletonScope()
dependencyInjection.bind<IBookService>(TYPES.IBookService).to(BookService).inSingletonScope()
dependencyInjection.bind<IMemberService>(TYPES.IMemberService).to(MemberService).inSingletonScope()
dependencyInjection.bind<IUserService>(TYPES.IUserService).to(UserService).inSingletonScope()
dependencyInjection.bind<IStudentService>(TYPES.IStudentService).to(StudentService).inSingletonScope()
dependencyInjection.bind<IAnswerService>(TYPES.IAnswerService).to(AnswerService).inSingletonScope()
dependencyInjection.bind<IOptionService>(TYPES.IOptionService).to(OptionService).inSingletonScope()
dependencyInjection.bind<IQuestionService>(TYPES.IQuestionService).to(QuestionService).inSingletonScope()
dependencyInjection.bind<ICsvAnalyseService>(TYPES.ICsvAnalyseService).to(CsvAnalyseService).inSingletonScope()
dependencyInjection.bind<ISpecService>(TYPES.ISpecService).to(SpecService).inSingletonScope()


function useAdministratorService() {
    return dependencyInjection.get<IAdministratorService>(TYPES.IAdministratorService);
}

function useAuthenticationService() {
    return dependencyInjection.get<IAuthenticationService>(TYPES.IAuthenticationService);
}

function useMemberService() {
    return dependencyInjection.get<IMemberService>(TYPES.IMemberService);
}

function useBookService() {
    return dependencyInjection.get<IBookService>(TYPES.IBookService);
}

function useUserService() {
    return dependencyInjection.get<IUserService>(TYPES.IUserService);
}

function useStudentService() {
    return dependencyInjection.get<IStudentService>(TYPES.IStudentService);
}

function useAnswerService() {
    return dependencyInjection.get<IAnswerService>(TYPES.IAnswerService);
}

function useOptionService() {
    return dependencyInjection.get<IOptionService>(TYPES.IOptionService);
}

function useQuestionService() {
    return dependencyInjection.get<IQuestionService>(TYPES.IQuestionService);
}



function useAnalyseService() {
    return dependencyInjection.get<ICsvAnalyseService>(TYPES.ICsvAnalyseService);
}

function useSpecService() {
    return dependencyInjection.get<ISpecService>(TYPES.ISpecService);
}

export {
    dependencyInjection,
    useAdministratorService,
    useAuthenticationService,
    useBookService,
    useMemberService,
    useUserService,
    useStudentService,
    useAnswerService,
    useOptionService,
    useQuestionService,
    useAnalyseService,
    useSpecService
};