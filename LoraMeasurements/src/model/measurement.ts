export class Measurement {
    measurementTime: string;
    barometricPressure: number;
    relativeHumidity: number;
    temperature: number;
    analogIn: number;
    rssi: number;
    channelRssi: number;
    channelId: number;
    snr: number;


    constructor(measurementTime: string, barometricPressure: number, relativeHumidity: number,
                temperature: number, analogIn: number, rssi: number, channelRssi: number, 
                channelId: number, snr: number) {
        this.measurementTime = measurementTime;
        this.barometricPressure = barometricPressure;
        this.relativeHumidity = relativeHumidity;
        this.temperature = temperature;
        this.analogIn = analogIn;
        this.channelId = channelId;
        this.channelRssi = channelRssi;
        this.snr = snr;
        this.rssi = rssi;
    }
}