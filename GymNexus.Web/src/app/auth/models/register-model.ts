export class RegisterModel {
    email: string;
    password: string;
    imageUrl?: string;

    constructor(email: string, password: string, imageUrl?: string) {
        this.email = email;
        this.password = password;
        this.imageUrl = imageUrl;
    }
}