<template>
  <EasyDataTable class="test"
      :headers="headers"
      :items="store.devices"
      alternating
      v-model:server-options="serverOptions"
      :server-items-length="store.devicesQuantity"
      :loading="store.deviceLoading"
      @click-row="showRow"
  />
</template>

<script setup lang="ts">
import {onUnmounted, onMounted, ref, watch} from 'vue'
import {useMainStore} from "@/stores/main";
import type {Header, ServerOptions } from "vue3-easy-data-table";
import router from "@/router/router";
import {Device} from "@/model/device";

const store = useMainStore();

onMounted(async () => {
  await store.fetchDeviceData(serverOptions.value.page, serverOptions.value.rowsPerPage,
      serverOptions.value.sortBy?.toString() ?? "", serverOptions.value.sortType?.toString() ?? "");
})

onUnmounted(() => {
  store.unloadDeviceData();
})

const serverOptions = ref<ServerOptions>({
  page: 1,
  rowsPerPage: 20,
  sortBy: 'name',
  sortType: 'desc',
});

const headers: Header[] = [
  {text: "Id", value: "id", sortable: true},
  {text: "Nazwa", value: "applicationId", sortable: true},
  {text: "Szerokość", value: "location.latitude", sortable: true},
  {text: "Długość", value: "location.longitude", sortable: true}
];
watch(serverOptions, () => {
  store.fetchDeviceData(serverOptions.value.page, serverOptions.value.rowsPerPage,
      serverOptions.value.sortBy?.toString() ?? "id", serverOptions.value.sortType?.toString() ?? "asc");
}, {deep: true});

const showRow = (item: Device) => {
  router.push({ path: `/devices/${item.id}`, params: { id: item.id } })
};

</script>

<style scoped>
.test >>> tbody tr:hover {
  cursor: pointer;
}
</style>