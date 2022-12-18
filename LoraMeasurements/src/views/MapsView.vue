<template>
  <div v-if="store.deviceLoading">
    Loading...
  </div>
  <div v-else>
    <ol-map :loadTilesWhileAnimating="true" :loadTilesWhileInteracting="true" style="height:90vh">
      <ol-view
          ref="view"
          :center="center"
          :rotation="rotation"
          :zoom="zoom"
          :projection="projection"
      />

      <ol-tile-layer>
        <ol-source-osm/>
      </ol-tile-layer>

      <ol-interaction-clusterselect @select="featureSelected" :pointRadius="20">
        <ol-style>
          <ol-style-stroke color="green" :width="5"></ol-style-stroke>
          <ol-style-fill color="rgba(255,255,255,0.5)"></ol-style-fill>
          <ol-style-icon src="mdi-home" :scale="0.05"></ol-style-icon>
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

          <ol-style-circle :radius="20">
            <ol-style-stroke color="black" :width="15" :lineDash="[]" lineCap="butt"></ol-style-stroke>
            <ol-style-fill color="black"></ol-style-fill>
          </ol-style-circle>

          <ol-style-text>
            <ol-style-fill color="white"></ol-style-fill>
          </ol-style-text>
        </ol-style>
      </ol-animated-clusterlayer>
    </ol-map>
  </div>
</template>

<script setup lang="ts">
import {ref, onMounted, onUnmounted, inject, watch} from 'vue';
import {useMainStore} from "@/stores/main";

const store = useMainStore();
const center = ref([19, 52.5])
const projection = ref('EPSG:4326')
const zoom = ref(8)
const rotation = ref(0);
const Feature = inject('ol-feature')
const Geom = inject('ol-geom')
const vectorsource = ref(null)

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
  style.getImage().getFill().setColor("rgba(" + color + ",1)");

  style.getImage().setRadius(radius)

  style.getText().setText(size.toString());

}

const featureSelected = (event: any) => {
  console.log(event)

}

onMounted(async () => {
  await store.fetchAllDeviceData();

})

watch(store.devices, () => {
  for (const device of store.devices) {
    let feature = new Feature({
      geometry: new Geom.Point([device.location.latitude, device.location.longitude]),
    });
    vectorsource.value!.source.addFeature(feature)

  }
}, {deep: true});

onUnmounted(() => {
  store.unloadDeviceData();
})
</script>

<style scoped>

</style>