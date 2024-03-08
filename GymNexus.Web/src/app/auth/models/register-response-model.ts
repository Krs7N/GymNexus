export class RegisterResponseModel {
    email: string;
    imageUrl?: string;

    constructor(email: string, imageUrl?: string, ) {
        this.email = email;
        this.imageUrl = imageUrl;
    }
}