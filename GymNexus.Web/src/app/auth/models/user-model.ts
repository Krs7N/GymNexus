export class UserModel {
    email: string;
    imageUrl?: string;
    roles: string[];

    constructor(email: string, roles: string[], imageUrl?: string, ) {
        this.email = email;
        this.imageUrl = imageUrl;
        this.roles = roles;
    }
}