import { IStudentService } from "@/injection/interfaces";
import { injectable } from "inversify";
import { ApiService } from "./apiService";
import { AxiosError, AxiosResponse } from "axios";
import { Student } from "@/types/entities";

@injectable()
export class StudentService extends ApiService implements IStudentService {
    public async getAllStudents(): Promise<Student[]> {
        const response = await this
            ._httpClient
            .get<AxiosResponse<Student>>(`${process.env.VUE_APP_API_BASE_URL}/students`)
            .catch(function (error: AxiosError): AxiosResponse<Student> {
                return error.response as AxiosResponse<Student>;
            });
        return response.data as Student[]
    }
}