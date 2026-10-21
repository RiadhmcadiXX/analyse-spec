import {ApiService} from "@/services/apiService";
import {ISpecService} from "@/injection/interfaces";
import {SucceededOrNotResponse} from "@/types/responses";
import {AxiosError, AxiosResponse} from "axios";
import {injectable} from "inversify";
import {ICreateSpecRequest} from "@/types/requests";

@injectable()
export class SpecService extends ApiService implements ISpecService {
    public async createSpec(request: ICreateSpecRequest): Promise<SucceededOrNotResponse> {
        const response = await this
            ._httpClient
            .post<ICreateSpecRequest, AxiosResponse<any>>(
                `${process.env.VUE_APP_API_BASE_URL}/spec`,
                request,
                { headers: { "Content-Type": "multipart/form-data" }}
            )
            .catch(function (error: AxiosError): AxiosResponse<SucceededOrNotResponse> {
                return error.response as AxiosResponse<SucceededOrNotResponse>
            })
        const succeededOrNotResponse = response.data as SucceededOrNotResponse
        return new SucceededOrNotResponse(succeededOrNotResponse.succeeded, succeededOrNotResponse.errors)
    }
}