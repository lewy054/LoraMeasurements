import {defineStore} from 'pinia'

interface State {
    sideBar: boolean,
}

export const useMainStore = defineStore('main', {
    state: (): State => ({
        sideBar: false,
    }),
    actions: {
        changeSideBarVisibility() {
            this.sideBar = !this.sideBar
        }
    },
})