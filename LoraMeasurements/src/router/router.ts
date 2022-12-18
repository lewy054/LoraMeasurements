import {createRouter, createWebHistory} from 'vue-router'
import HelloWorld from "@/components/HelloWorld.vue";
import DevicesList from "@/views/DevicesList.vue";
import DeviceDetails from "@/views/DeviceDetails.vue";
import MapsView from "@/views/MapsView.vue";

export default createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            component: HelloWorld,
        },
        {
            path: '/devices',
            component: DevicesList,
        },
        {
            path: '/map',
            component: MapsView,
        },
        {
            path: '/devices/:id',
            component: DeviceDetails,
        },
    ]
})