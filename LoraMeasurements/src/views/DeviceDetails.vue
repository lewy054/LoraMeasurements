<template>
  <v-container v-if="store.measurementsLoading" class="h-100 w-100 d-flex justify-content-center">
    <v-progress-circular
        class="container h-auto d-flex ml-auto mr-auto"
        indeterminate
        :size="250"
        :width="5"
        color="primary"
    ></v-progress-circular>
  </v-container>
  <v-container v-else class="mt-5">
    <v-responsive
        max-width="500">
      <v-row>
        <v-text-field type="datetime-local" label="Od" v-model="from"></v-text-field>
        <v-text-field type="datetime-local" label="Do" v-model="to"></v-text-field>
      </v-row>
    </v-responsive>
    <v-row>
      <v-col class="chart">
        <canvas id="temperatureChart"></canvas>
      </v-col>
      <v-col class="chart">
        <canvas id="relativeHumidityChart"></canvas>
      </v-col>
    </v-row>
    <v-row>
      <v-col class="chart">
        <canvas id="barometricPressureChart"></canvas>
      </v-col>
      <v-col class="chart">
        <canvas id="analogInChart"></canvas>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import {useRoute} from "vue-router";
import {onMounted, onUnmounted, onUpdated, ref, watch} from 'vue';
import Chart from 'chart.js/auto';
import {useMainStore} from "@/stores/main";
import {DateTime} from "luxon";


const route = useRoute();
const id = route.params.id.toString();
const store = useMainStore();
const from = ref(DateTime.now().minus({days: 1}).toFormat("yyyy-LL-dd'T'T"));
const to = ref(DateTime.now().toFormat("yyyy-LL-dd'T'T"));

watch(from, async () => {
  await fetchData();
})

watch(to, async () => {
  await fetchData();
})

onMounted(async () => {
  await fetchData();
})

async function fetchData() {
  const fromTimestamp = DateTime.fromISO(from.value).toFormat('x');
  const toTimestamp = DateTime.fromISO(to.value).toFormat('x');
  await store.fetchMeasurements(id, fromTimestamp, toTimestamp);
}

onUpdated(() => {
  createTemperatureChart();
  createRelativeHumidityChart();
  createBarometricPressureChart();
  createAnalogInChart();
})

function createAnalogInChart() {
  const ctx = document.getElementById('analogInChart');
  if (ctx) {
    const labels = store.measurements.map(e => e.measurementTime);
    const data = store.measurements.map(e => e.analogIn);
    createChart(ctx, labels, data, "Napięcie", "#7d7a29")
  }
}

function createBarometricPressureChart() {
  const ctx = document.getElementById('barometricPressureChart');
  if (ctx) {
    const labels = store.measurements.map(e => e.measurementTime);
    const data = store.measurements.map(e => e.barometricPressure);
    createChart(ctx, labels, data, "Ciśnienie", "#0e6907")
  }
}

function createTemperatureChart() {
  const ctx = document.getElementById('temperatureChart');
  if (ctx) {
    const labels = store.measurements.map(e => e.measurementTime);
    const data = store.measurements.map(e => e.temperature);
    createChart(ctx, labels, data, "Temperatura", "#ff3333")
  }
}

function createRelativeHumidityChart() {
  const ctx = document.getElementById('relativeHumidityChart');
  if (ctx) {
    const labels = store.measurements.map(e => e.measurementTime);
    const data = store.measurements.map(e => e.relativeHumidity);
    createChart(ctx, labels, data, "Wilgotność względna", "#0033ff")
  }
}

function createChart(html: HTMLElement, labels: string[], data: number[], label: string, color: string): Chart {
  return new Chart(html, {
    type: 'line',
    data: {
      labels: labels,
      datasets: [{
        label: label,
        data: data,
        borderWidth: 1,
        borderColor: [
          color
        ],
      }],
    }
  });
}

onUnmounted(() => {
  store.unloadMeasurements();
})

</script>

<style scoped>
.chart {
  position: relative;
  height: 40vh;
}
</style>