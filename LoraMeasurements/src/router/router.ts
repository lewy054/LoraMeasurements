import {createRouter, createWebHistory} from 'vue-router'
import App from "@/App.vue";
import HelloWorld from "@/components/HelloWorld.vue";

export default createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            component: HelloWorld,
        },
        // {
        //     path: '/map',
        //     component: Map,
        // },
        // {
        //     path: '/devices',
        //     component: Devices,
        // }
    ]
})