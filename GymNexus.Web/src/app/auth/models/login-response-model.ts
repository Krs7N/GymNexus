export class LoginResponseModel {
    email: string;
    imageUrl?: string;
    token: string;
    roles: string[];

    constructor(email: string, token: string, roles: string[], imageUrl?: string, ) {
        this.email = email;
        this.imageUrl = imageUrl;
        this.token = token;
        this.roles = roles;
    }
}