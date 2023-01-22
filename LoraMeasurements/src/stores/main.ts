import {defineStore} from 'pinia'
import {Device} from "@/model/device";
import {Measurement} from "@/model/measurement";
import { DateTime } from "luxon";

interface State {
    sideBar: boolean,
    devices: Array<Device>,
    measurements: Array<Measurement>,
    deviceLoading: boolean,
    measurementsLoading: boolean,
    devicesQuantity: number,
}

export const useMainStore = defineStore('main', {
    state: (): State => ({
        sideBar: false,
        devices: [],
        measurements: [],
        deviceLoading: false,
        measurementsLoading: false,
        devicesQuantity: 0,
    }),
    actions: {
        changeSideBarVisibility() {
            this.sideBar = !this.sideBar
        },
        async fetchDeviceData(page: number, rowsPerPage: number, sortBy: string, sortType: string) {
            this.deviceLoading = true;
            fetch(`ttn?PageNumber=${page}&RowsPerPage=${rowsPerPage}&SortBy=${sortBy}&SortType=${sortType}`)
                .then(r => r.json())
                .then(json => {
                    this.devices = json['item1'] as Array<Device>;
                    this.devicesQuantity = json['item2'] as number;
                    this.deviceLoading = false;
                });
        },
        unloadDeviceData() {
            this.devices = [];
        },
        async fetchAllDeviceData() {
            this.deviceLoading = true;
            fetch(`/ttn/1`)
                .then(r => r.json())
                .then(json => {
                    this.devices = json as Array<Device>;
                    this.deviceLoading = false;
                });
        },

        async fetchMeasurements(id: string, from: string, to: string) {
            this.measurementsLoading = true;
            fetch(`/measurement/${id}/${from}/${to}`)
                .then(r => r.json())
                .then(json => {
                    const measurementsArray = json as Array<Measurement>;
                    measurementsArray.forEach(e => {
                        e.measurementTime = DateTime.fromISO(e.measurementTime, { zone: 'utc' }).setZone('local', { keepLocalTime: false }).toFormat('ff')
                    })
                    this.measurements = measurementsArray;
                    this.measurementsLoading = false;
                });
        },
        unloadMeasurements() {
            this.measurements = [];
        },
    },
})