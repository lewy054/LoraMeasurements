<template>
  <div v-if="store.deviceLoading">
    <v-progress-circular
        class="container h-auto d-flex ml-auto mr-auto"
        indeterminate
        :size="250"
        :width="5"
        color="primary"
    ></v-progress-circular>
  </div>
  <div v-else>
    <ol-map :loadTilesWhileAnimating="true" :loadTilesWhileInteracting="true" style="height:90vh">
      <ol-view
          ref="view"
          :center="center"
          :rotation="rotation"
          :zoom="zoom"
          :projection="projection"/>
      
      <ol-tile-layer>
        <ol-source-osm/>
      </ol-tile-layer>
      <ol-interaction-clusterselect @select="featureSelected" :pointRadius="20">
        <ol-style>
          <ol-style-icon :src="markerIcon" :scale="0.05"></ol-style-icon>
        </ol-style>
      </ol-interaction-clusterselect>

      <ol-animated-clusterlayer :animationDuration="500" :distance="40">
        <ol-source-vector ref="vectorsource">
          <ol-feature v-for="device in store.devices" :key="device.id">
            <ol-geom-point
                :coordinates="[Number(device.location.longitude), Number(device.location.latitude)]"
            ></ol-geom-point>
          </ol-feature>
        </ol-source-vector>

        <ol-style :overrideStyleFunction="overrideStyleFunction">
          <ol-style-stroke color="red" :width="2"></ol-style-stroke>
          <ol-style-fill color="rgba(255,255,255,0.1)"></ol-style-fill>

          <ol-style-circle>
            <ol-style-fill color="#3399CC"></ol-style-fill>
            <ol-style-stroke color="#fff" :width="1"></ol-style-stroke>
          </ol-style-circle>

          <ol-style-text>
            <ol-style-fill color="#fff"></ol-style-fill>
          </ol-style-text>
        </ol-style>
      </ol-animated-clusterlayer>

      <ol-fullscreen-control/>
      <ol-scaleline-control/>
      <ol-zoom-control/>
      <ol-zoomslider-control/>
    </ol-map>
  </div>
</template>

<script setup lang="ts">
import {ref, onMounted, onUnmounted, inject, watch} from 'vue';
import {useMainStore} from "@/stores/main";
import markerIcon from '@/assets/marker.png'

const store = useMainStore();
const center = ref([19, 52.5])
const projection = ref('EPSG:4326')
const zoom = ref(8)
const rotation = ref(0);
const Feature = inject('ol-feature')
const Geom = inject('ol-geom')
const vectorsource = ref(null);

const overrideStyleFunction = (feature: any, style: any) => {

  let clusteredFeatures = feature.get('features');
  let size = clusteredFeatures.length;

  let color = size > 20 ? "192,0,0" : size > 8 ? "255,128,0" : "0,128,0";
  let radius = Math.max(8, Math.min(size, 20));
  let dash = 2 * Math.PI * radius / 6;
  let calculatedDash = [0, dash, dash, dash, dash, dash, dash];

  style.getImage().getStroke().setLineDash(dash);
  style.getImage().getStroke().setColor("rgba(" + color + ",0.5)");
  style.getImage().getStroke().setLineDash(calculatedDash);

  style.getImage().setRadius(radius)
  style.getText().setText(size.toString());

}

const featureSelected = (event: any) => {
  console.log(event)
  if (event.selected.length == 1) {
    console.log(event.selected[0].values_);
  } else {
    console.log('empty');
  }
}

onMounted(async () => {
  await store.fetchAllDeviceData();

})

watch(store.devices, () => {
  for (const device of store.devices) {
    let feature = new Feature({
      geometry: new Geom.Point([device.location.latitude, device.location.longitude]),
    });
    feature.set('id', device.id)
    if (vectorsource.value) {
      vectorsource.value.source.addFeature(feature)
    }

  }
}, {deep: true});

onUnmounted(() => {
  store.unloadDeviceData();
})
</script>

<style scoped>

</style>