export class LoginResponsePartialModel {
    firstName?: string;
    lastName?: string;
    email: string;
    imageUrl?: string;
    roles: string[];
    isExternal: boolean = false;
  
    constructor(firstName: string, lastName: string, email: string, roles: string[], isExternal: boolean, imageUrl?: string) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.imageUrl = imageUrl;
        this.isExternal = isExternal;
        this.roles = roles;
    }
}