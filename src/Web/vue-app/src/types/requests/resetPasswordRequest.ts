import {Guid} from "@/types";

export interface IResetPasswordRequest {
  userId?: string,
  token: string,
  password: string
  passwordConfirmation: string
}