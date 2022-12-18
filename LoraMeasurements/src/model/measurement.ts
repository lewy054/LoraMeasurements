export class Measurement {
    measurementTime: string;
    barometricPressure: number;
    relativeHumidity: number;
    temperature: number;
    analogIn: number;


    constructor(measurementTime: string, barometricPressure: number, relativeHumidity: number,
                temperature: number, analogIn: number) {
        this.measurementTime = measurementTime;
        this.barometricPressure = barometricPressure;
        this.relativeHumidity = relativeHumidity;
        this.temperature = temperature;
        this.analogIn = analogIn;

    }
}