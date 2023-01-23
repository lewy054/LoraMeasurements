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
      <v-row >
        <v-text-field type="datetime-local" label="Od" v-model="from"></v-text-field>
        <v-text-field type="datetime-local" label="Do" v-model="to"></v-text-field>
      </v-row>
    </v-responsive>

    <v-card>
      <v-tabs grow
              v-model="tab"
              fixed-tabs
              bg-color="indigo-darken-2"
      >
        <v-tab value="one">Dane o pogodzie</v-tab>
        <v-tab value="two">Dane o urządzeniu</v-tab>
      </v-tabs>
      <v-card-text>
        <v-window v-model="tab">
          <v-window-item value="one" :eager="true">
            <v-row class="justify-center">
              <v-col class="tempChart" :cols="12">
                <canvas id="temperatureChart"></canvas>
              </v-col>
            </v-row>
            <v-row>
              <v-col class="chart">
                <canvas id="relativeHumidityChart"></canvas>
              </v-col>
              <v-col class="chart">
                <canvas id="barometricPressureChart"></canvas>
              </v-col>
            </v-row>
          </v-window-item>

          <v-window-item value="two" :eager="true">
            <v-row>
              <v-col class="chart">
                <canvas id="analogInChart"></canvas>
              </v-col>
              <v-col class="chart">
                <canvas id="channelRssiChart"></canvas>
              </v-col>
            </v-row>
            <v-row>
              <v-col class="chart">
                <canvas id="rssiChart"></canvas>
              </v-col>
              <v-col class="chart">
                <canvas id="snrChart"></canvas>
              </v-col>
            </v-row>
            <v-row class="justify-center">
              <v-col class="pieChart">
                <canvas id="channelIdChart"></canvas>
              </v-col>
            </v-row>
          </v-window-item>

        </v-window>
      </v-card-text>
    </v-card>
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
const tab = ref(null);

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
  createRssiChart();
  createSnrChart();
  createChannelRsiChart();
  createChannelIdChart();
})

function createChannelRsiChart() {
  const ctx = document.getElementById('channelRssiChart');
  if (ctx) {
    const labels = store.measurements.map(e => e.measurementTime);
    const data = store.measurements.map(e => e.channelRssi);
    createChart(ctx, labels, data, "Rssi kanałów", "#0e6907")
  }
}

function createChannelIdChart() {
  const ctx = document.getElementById('channelIdChart');
  if (ctx) {
    let groupedData: Array<{ key: number; value: number; }> = [];
    store.measurements.forEach(e => {
      if (groupedData.some(d => d.key === e.channelId)) {
        let item = Object.entries(groupedData).find(a => a[1].key === e.channelId)
        if (item) {
          item[1].value += 1
        }
      } else {
        groupedData.push({
          key: e.channelId,
          value: e.channelId,
        });
      }
    })
    return new Chart(ctx, {
      type: 'doughnut',
      data: {
        labels: groupedData.map(e => e.key),
        datasets: [{
          label: "Ilość wykorzystania kanału",
          data: groupedData.map(e => e.value),
        }],
      },
    });
  }
}

function createSnrChart() {
  const ctx = document.getElementById('snrChart');
  if (ctx) {
    const labels = store.measurements.map(e => e.measurementTime);
    const data = store.measurements.map(e => e.snr);
    createChart(ctx, labels, data, "SNR", "#cc10c9")
  }
}

function createRssiChart() {
  const ctx = document.getElementById('rssiChart');
  if (ctx) {
    const labels = store.measurements.map(e => e.measurementTime);
    const data = store.measurements.map(e => e.rssi);
    createChart(ctx, labels, data, "Rssi", "#774905")
  }
}

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

.pieChart {
  position: relative;
  display: flex;
  justify-content: center;
  height: 50vh;
}

.tempChart {
  height: 60vh;
  display: flex;
  justify-content: center;
}

.chart {
  position: relative;
  height: 50vh;
}
</style>