import {defineStore} from 'pinia'
import {Device} from "@/model/device";

interface State {
    sideBar: boolean,
    devices: Array<Device>,
    deviceLoading: boolean,
    devicesQuantity: number,
}

export const useMainStore = defineStore('main', {
    state: (): State => ({
        sideBar: false,
        devices: [],
        deviceLoading: false,
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
                    return;
                });
        },
        unloadDeviceData() {
            this.devices = [];
        },
        async fetchAllDeviceData() {
            this.deviceLoading = true;
            fetch(`ttn/1`)
                .then(r => r.json())
                .then(json => {
                    this.devices = json as Array<Device>;
                    this.deviceLoading = false;
                    return;
                });
        },
    },
})

class Test {
    devices: Array<Device>;
    quantity: number;

    constructor(devices: Array<Device>, quantity: number) {
        this.devices = devices;
        this.quantity = quantity;
    }
}