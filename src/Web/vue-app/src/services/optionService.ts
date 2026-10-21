import { IOptionService } from "@/injection/interfaces";
import { injectable } from "inversify";
import { ApiService } from "./apiService";
import { AxiosError, AxiosResponse } from "axios";
import { Option } from "@/types/entities";
import { Guid } from "../types";

@injectable()
export class OptionService extends ApiService implements IOptionService {
    public async getOptionsByIds(ids?: Guid[]): Promise<Option[]> {
        const response = await this
            ._httpClient
            .get<Option[]>(`${process.env.VUE_APP_API_BASE_URL}/options/${ids}`, {
                params: { ids }
            })
            .catch(function (error: AxiosError): AxiosResponse<Option> {
                return error.response as AxiosResponse<Option>;
            });
        return response.data as Option[]
    }

    public async getOptionById(id: Guid): Promise<Option> {
        const response = await this
            ._httpClient
            .get<Option>(`${process.env.VUE_APP_API_BASE_URL}/option/${id}`)
            .catch(function (error: AxiosError): AxiosResponse<Option> {
                return error.response as AxiosResponse<Option>;
            });
        return response.data as Option
    }

    public async getAllOptions(): Promise<Option[]> {
        const response = await this
           ._httpClient
           .get<AxiosResponse<Option>>(`${process.env.VUE_APP_API_BASE_URL}/options`)
           .catch(function (error: AxiosError): AxiosResponse<Option> {
               return error.response as AxiosResponse<Option>;
           });
        return response.data as Option[]
    }
}