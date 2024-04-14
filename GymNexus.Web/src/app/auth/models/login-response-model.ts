import { LoginResponsePartialModel } from "./login-response-partial-model";

export class LoginResponseModel extends LoginResponsePartialModel {
    token: string;

    constructor(firstName: string, lastName: string, email: string, token: string, roles: string[], isExternal: boolean, imageUrl?: string, ) {
        super(firstName, lastName, email, roles, isExternal, imageUrl)
        this.token = token;
    }
}