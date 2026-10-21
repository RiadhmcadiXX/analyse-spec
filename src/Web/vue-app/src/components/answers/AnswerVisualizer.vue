<template>
	<div style="display: grid; grid-template-columns: 1fr 1fr; gap: 0.5rem;">
		<div class="wrapper">
			<!-- Filtres Étudiants -->
			<StudentFilterFieldset ref="studentFilterFieldset" :students="(students as Student[])" />
			<!-- Nombre d'étudiants -->
			<div class="card">
				<div class="card__title-container">
					<h2>Étudiants</h2>
					<p class="stats-number">{{ filtered.students?.length }}</p>
				</div>
			</div>
		</div>
		<div>
			<div class="card">
				<div class="card__content">
					<ChartComponent type="pie" :data="chartData" :options="chartOptions1" />
				</div>
			</div>
		</div>
	</div>
</template>

<script lang="ts" setup>
import { useI18n } from "vue3-i18n";
import { computed, onMounted, ref, toRefs } from "vue";
import ChartComponent from "@/components/answers/ChartComponent.vue";
import { ChartConfiguration } from "chart.js/auto";
import { useAnswerService, useOptionService, useStudentService } from "@/inversify.config";
import { Answer, Option, Student } from "@/types";
import StudentFilterFieldset from "@/components/filters/StudentFilterFieldset.vue";


// eslint-disable-next-line 
const props = defineProps<{
	selectedQuestionId: string | null;
	students: Student[];
	answers: Answer[];
	options: Option[];
}>();

const { selectedQuestionId, students, answers, options } = toRefs(props);

const studentFilterFieldset = ref<InstanceType<typeof StudentFilterFieldset> | null>(null);

const filtered = computed(() => {
	const students = studentFilterFieldset.value?.filteredStudents;

	return {
		students: students,
		answers: answers.value?.filter(answer =>
			students?.some((student: any) => student.id?.toString() === answer.idStudent?.id?.toString()) &&
			options.value?.find(option => option.id?.toString() === answer.idOption?.id?.toString())?.idQuestion?.id?.toString() === selectedQuestionId.value
		),
		options: options.value?.filter(option => option.idQuestion?.id?.toString() === selectedQuestionId.value)
	}
});

const chartData = computed<ChartConfiguration["data"]>(() => {
	const label = "SPEC 1 & 2";
	console.debug("changed dataset");
	return {
		labels: filtered.value.options.map(option => option.title),
		datasets: [{
			label,
			data: filtered.value.options.map(option =>
				filtered.value.answers.filter(answer => answer.idOption?.id?.toString() === option.id?.toString()).length
			)
		}]
	};
});

const chartOptions1: ChartConfiguration["options"] = {
	responsive: true,
	plugins: {},
	scales: {
		x: {
			display: false,
			type: "category",
			labels: filtered.value.options.map(option => option.title)
		}
	}
};
</script>

<style scoped>
.wrapper {
	display: flex;
	flex-direction: column;
	gap: 0.5rem;
}

.stats-number {
	font-size: 2.5rem;
	line-height: 1em;
}

fieldset {
	padding: 1rem;
	border: 1px solid black;
	display: flex;
	flex-direction: column;
	gap: 0.5rem;
}
</style>