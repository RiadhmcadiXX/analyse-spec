import { injectable } from "inversify"
import { AxiosError, AxiosResponse } from "axios"
import { ApiService } from "./apiService"
import { IAnalyseCsvRequest } from "@/types/requests/analyseCsvRequest"
import { SucceededOrNotResponse } from "@/types/responses"
import { ICsvAnalyseService } from "@/injection/interfaces"

@injectable()
export class CsvAnalyseService extends ApiService implements ICsvAnalyseService {
    
    public async analyseCsv(request: IAnalyseCsvRequest): Promise<SucceededOrNotResponse> {
        const formData = new FormData()
        formData.append("file", request.file)

        console.log("🟡 Envoi du fichier CSV :", request.file.name)
        console.log("➡️ POST vers :", `${process.env.VUE_APP_API_BASE_URL}/csv-analyse`)

        const response = await this._httpClient
            .post<FormData, AxiosResponse<any>>(
                `${process.env.VUE_APP_API_BASE_URL}/csv-analyse`,
                formData,
                { headers: { "Content-Type": "multipart/form-data" } }
            )
            .catch((error: AxiosError): AxiosResponse<SucceededOrNotResponse> => {
                console.error("❌ Erreur Axios :", error.message)
                console.log("📦 Response dans le catch :", error.response)
                return error.response as AxiosResponse<SucceededOrNotResponse>
            })

        console.log("✅ Réponse reçue du backend :", response.data)

        const succeededOrNotResponse = response.data as SucceededOrNotResponse
        return new SucceededOrNotResponse(
            succeededOrNotResponse.succeeded,
            succeededOrNotResponse.errors,
            succeededOrNotResponse.data
        )
    }
}