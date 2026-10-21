<template>
	<div>
		<canvas ref="chartRef" class="chart-component" width="400" height="400"></canvas>
	</div>
</template>

<script lang="ts" setup>
import Chart, { ChartConfiguration, ChartConfigurationCustomTypesPerDataset } from 'chart.js/auto';
import { ref, onMounted, toRefs, watch } from 'vue';

// eslint-disable-next-line
const props = defineProps<{
	type: ChartConfiguration["type"];
	options: ChartConfiguration["options"] | ChartConfigurationCustomTypesPerDataset["options"];
	data: ChartConfiguration["data"] | ChartConfigurationCustomTypesPerDataset["data"];
}>();

const chartRef = ref<HTMLCanvasElement | null>(null);

let chart: Chart | null = null; // TODO: Make this ref

onMounted(() => {
	if (chartRef.value) {
		const ctx = chartRef.value.getContext('2d');
		if (ctx) {
			const { type, data, options } = toRefs(props);
			chart = new Chart(ctx, {
				type: type.value,
				data: data.value,
				options: options.value
			});
			watch([type, data, options], () => {
				if (chart) {
					chart.data = data.value;
					chart.options = options.value ?? {};
					chart.update();
				}
			});
		}
	}
});
</script>

<style lang="css" scoped>
.chart-component {
   aspect-ratio: 1 / 1;
   width: 100% !important;
   height: 100% !important;  
}
</style>