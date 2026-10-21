import {IQuestionService, ISpecService} from "@/injection/interfaces";
import { injectable } from "inversify";
import { ApiService } from "./apiService";
import { AxiosError, AxiosResponse } from "axios";
import {Answer, Member, Question} from "@/types/entities";
import { Guid } from "../types";
import {SucceededOrNotResponse} from "@/types/responses";
import {ICreateSpecRequest} from "@/types/requests";

@injectable()
export class QuestionService extends ApiService implements IQuestionService {
    public async getQuestionsByIds(id?: Guid[]): Promise<Question[]> {
        const response = await this
            ._httpClient
            .get<AxiosResponse<Question>>(`${process.env.VUE_APP_API_BASE_URL}/questions/${id}`)
            .catch(function (error: AxiosError): AxiosResponse<Question> {
                return error.response as AxiosResponse<Question>;
            });
        return response.data as Question[]
    }

    public async getQuestionById(id: Guid): Promise<Question> {
        const response = await this
            ._httpClient
            .get<Question>(`${process.env.VUE_APP_API_BASE_URL}/question/${id}`)
            .catch(function (error: AxiosError): AxiosResponse<Question> {
                return error.response as AxiosResponse<Question>;
            });
        return response.data as Question
    }

    public async getAllQuestions(): Promise<Question[]> {
        const response = await this
           ._httpClient
           .get<AxiosResponse<Question>>(`${process.env.VUE_APP_API_BASE_URL}/questions`)
           .catch(function (error: AxiosError): AxiosResponse<Question> {
               return error.response as AxiosResponse<Question>;
           });
        return response.data as Question[]
    }


    public async updateQuestion(questions: Question[]): Promise<SucceededOrNotResponse> {
            const response = await this
                ._httpClient
                .put<any, AxiosResponse<SucceededOrNotResponse>>(
                    `${process.env.VUE_APP_API_BASE_URL}/questions/update`,
                    questions,
                    this.headersWithJsonContentType())
                .catch(function (error: AxiosError): AxiosResponse<SucceededOrNotResponse> {
                    return error.response as AxiosResponse<SucceededOrNotResponse>
                })
            const succeededOrNotResponse = response.data as SucceededOrNotResponse
            return new SucceededOrNotResponse(succeededOrNotResponse.succeeded, succeededOrNotResponse.errors)
    }

}