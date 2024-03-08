export class MarketplaceModel {
    name: string;
    description: string;
    address: string;
    latitude: number;
    longitude: number;

    constructor(name: string, description: string, address: string, latitude: number, longitude: number) {
        this.name = name;
        this.description = description;
        this.address = address;
        this.latitude = latitude;
        this.longitude = longitude;
    }
}