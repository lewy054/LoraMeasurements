export class Device {
    id: string;
    applicationId: string;
    location: Location;


    constructor(id: string, applicationId: string, location: Location) {
        this.id = id;
        this.applicationId = applicationId;
        this.location = location;

    }
}

class Location {
    latitude: string;
    longitude: string;

    constructor(latitude: string, longitude: string) {
        this.latitude = latitude;
        this.longitude = longitude;
    }
}