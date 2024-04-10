export class LoginResponseModel {
    firstName?: string;
    lastName?: string;
    email: string;
    imageUrl?: string;
    token: string;
    roles: string[];

    constructor(firstName: string, lastName: string, email: string, token: string, roles: string[], imageUrl?: string, ) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.imageUrl = imageUrl;
        this.token = token;
        this.roles = roles;
    }
}