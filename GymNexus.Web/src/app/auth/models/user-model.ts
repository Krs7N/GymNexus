import { StoreViewModel } from "src/app/shared/models/store-view-model";

export class UserModel {
    firstName?: string;
    lastName?: string;
    email: string;
    imageUrl?: string;
    roles: string[];
    stores?: StoreViewModel[];

    constructor(email: string, roles: string[], imageUrl?: string, firstName?: string, lastName?: string) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.imageUrl = imageUrl;
        this.roles = roles;
    }
}