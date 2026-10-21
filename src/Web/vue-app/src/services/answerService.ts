import { IAnswerService } from "@/injection/interfaces";
import { injectable } from "inversify";
import { ApiService } from "./apiService";
import { AxiosError, AxiosResponse } from "axios";
import { Answer } from "@/types/entities";
import { Guid } from "../types";

@injectable()
export class AnswerService extends ApiService implements IAnswerService {
    public async getAnswersByStudentId(id?: Guid): Promise<Answer[]> {
        const response = await this
            ._httpClient
            .get<AxiosResponse<Answer>>(`${process.env.VUE_APP_API_BASE_URL}/answers/${id}`)
            .catch(function (error: AxiosError): AxiosResponse<Answer> {
                return error.response as AxiosResponse<Answer>;
            });
        return response.data as Answer[]
    }

    public async getAllAnswers(): Promise<Answer[]> {
        const response = await this
            ._httpClient
            .get<AxiosResponse<Answer>>(`${process.env.VUE_APP_API_BASE_URL}/answers`)
            .catch(function (error: AxiosError): AxiosResponse<Answer> {
                return error.response as AxiosResponse<Answer>;
            });
        return response.data as Answer[]
    }
}