<template>
  <v-container v-if="store.measurementsLoading">
    Loading...
  </v-container>
  <v-container v-else style="height: 90vh">
    <v-row>
      <v-col class="chart">
        <canvas id="temperatureChart"></canvas>
      </v-col>
      <v-col class="chart">
        <canvas id="relativeHumidityChart"></canvas>
      </v-col>
    </v-row>
    <v-row >
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
import {onMounted, onUnmounted, onUpdated} from 'vue';
import Chart from 'chart.js/auto';
import {useMainStore} from "@/stores/main";

const route = useRoute();
const id = route.params.id.toString();
const store = useMainStore();

onMounted(async () => {
  await store.fetchMeasurements(id);
  console.log(store.measurements)

})

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

function createChart(html: HTMLElement, labels: string[], data: number[], label: string, color:string): Chart {
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
.chart{
  position: relative;
  height: 45vh;
}
</style>