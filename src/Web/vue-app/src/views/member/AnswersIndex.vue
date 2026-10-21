<template>
	<div class="content-grid content-grid--subpage content-grid--subpage-table" style="gap: 1rem">
		<form onsubmit="return false;">
			<!-- Choix de la question -->
			<fieldset>
				<legend>Question</legend>
				<div class="top-fieldset">
					<select v-model="selectedQuestionId">
						<option v-for="question in allQuestions" :value="question.id" :key="question.id?.toString()">
							{{ question.title }}
						</option>
					</select>
					<div id="compare_mode_wrapper">
						<label for="compare_mode">
							Mode Comparaison &nbsp;
							<input type="checkbox" id="compare_mode" name="compare_mode" v-model="compareMode" />
						</label>
					</div>
				</div>
			</fieldset>
		</form>
		<AnswerVisualizer :selected-question-id="selectedQuestionId" :students="allStudents" :answers="allAnswers" :options="allOptions" />
		<AnswerVisualizer :selected-question-id="selectedQuestionId" :students="allStudents" :answers="allAnswers" :options="allOptions" v-if="compareMode" />
	</div>
	<!--   <div class="content-grid content-grid&#45;&#45;subpage content-grid&#45;&#45;subpage-table">-->
	<!--      <div class="content-grid__header">-->
	<!--         <h1 class="back-link-title">Liste des étudiants</h1>-->
	<!--         <div class="content-grid__filters">-->
	<!--            <SearchInput v-model="searchValue" />-->
	<!--         </div>-->
	<!--      </div>-->

	<!--       <Card>-->
	<!--         <DataTable :headers="etudiantHeader" :search-value="searchValue" :is-loading="enChargement"-->
	<!--            :items="tableStudents" />-->
	<!--      </Card> -->
	<!--   </div>-->
</template>

<script lang="ts" setup>
import { useI18n } from "vue3-i18n";
import { onMounted, ref } from "vue";
import DataTable from "@/components/layouts/items/DataTable.vue";
import Card from "@/components/layouts/items/Card.vue";
import SearchInput from "@/components/layouts/items/SearchInput.vue";

import { useAnswerService, useOptionService, useQuestionService, useStudentService } from "@/inversify.config";
import { Answer, Option, Question, Student } from "@/types";
import AnswerVisualizer from "@/components/answers/AnswerVisualizer.vue";

const compareMode = ref(false);

const studentService = useStudentService();
const answerService = useAnswerService();
const optionService = useOptionService();
const questionService = useQuestionService();

const allStudents = ref<Student[]>([]);
const allAnswers = ref<Answer[]>([]);
const allOptions = ref<Option[]>([]);
const allQuestions = ref<Question[]>([]);

const enChargement = ref(false);

onMounted(async () => {
	enChargement.value = true;
	allStudents.value = await studentService.getAllStudents();
	allAnswers.value = await answerService.getAllAnswers();
	allOptions.value = await optionService.getAllOptions();
	allQuestions.value = await questionService.getAllQuestions();
	enChargement.value = false;
	selectedQuestionId.value = allQuestions.value?.[0]?.id?.toString() ?? null;
});

const { t } = useI18n();
const searchValue = ref("");

const selectedQuestionId = ref<string | null>(null);

// const etudiantHeader = [
//    { text: "Numéro Da", value: "cp", sortable: true, width: 90 },
//    { text: "Nom, prénom", value: "firstAndLastname", sortable: true, width: 150 },
//    { text: "Email", value: "admEmail", sortable: true, width: 200 },
//    { text: "Sexe", value: "biological_sex", sortable: true, width: 100 },
//    { text: "Population", value: "population", sortable: true, width: 120 },
//    { text: "Programme", value: "prog", sortable: true, width: 130 },
// ];
//
// const tableStudents = computed(() =>
//     (filtered.value.students ?? []).map((x: any) => ({
//       id: x.id,
//       cp: x.cp,
//       firstAndLastname: x.firstAndLastName,
//       admEmail: x.admEmail,
//       birthday: x.birthday,
//       biological_sex: x.gender ?? '',
//       legal_status: x.status,
//       population: x.population,
//       prog: x.progDec
//     }))
// );

</script>

<style scoped>
.top-fieldset {
	display: flex;
	flex-direction: row;
	flex-wrap: nowrap;
	gap: 1rem;
}

#compare_mode_wrapper {
	flex-shrink: 0;
	display: flex;
	flex-direction: row;
	flex-wrap: nowrap;
	gap: 0.5rem;
	align-items: center;
	border: 1px solid rgba(92, 92, 92, 0.2);
	border-radius: 4px;
	padding-inline: 4px;
	background-color: #F8F8F8
}

#compare_mode_wrapper input[type="checkbox"] {
	width: 1.5rem;
	height: 1.5rem;
	opacity: 1;
	position: relative;
	border: 1px solid rgba(92, 92, 92, 0.2);
}

fieldset {
	padding: 1rem;
	border: 1px solid black;
	display: flex;
	flex-direction: column;
	gap: 0.5rem;
}

fieldset > legend {
	font-weight: bold;
	padding-inline: 0.25rem;
}

fieldset > div {
	display: flex;
	flex-direction: column;
	gap: 0.25rem;
}
</style>