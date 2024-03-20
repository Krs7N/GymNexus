import { StoreViewModel } from "src/app/shared/models/store-view-model";

export class UserModel {
    email: string;
    imageUrl?: string;
    roles: string[];
    stores?: StoreViewModel[];

    constructor(email: string, roles: string[], imageUrl?: string) {
        this.email = email;
        this.imageUrl = imageUrl;
        this.roles = roles;
    }
}