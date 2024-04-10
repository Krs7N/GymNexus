export class MarketplaceFormModel {
    name: string;
    description: string;
    address: string;

    constructor(name: string, description: string, address: string) {
        this.name = name;
        this.description = description;
        this.address = address;
    }
}