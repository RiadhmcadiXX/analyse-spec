<template>
	<form class="student-filters" onsubmit="return false;">
		<fieldset class="options-filters">
			<legend>Étudiants</legend>
			<!-- Filter type choice -->
			<div class="filter_type_choice">
				<button @click="filterType = 'basic'" class="filter-type" :class="{ 'selected': filterType === 'basic' }">
					Basique
				</button>
				<button @click="filterType = 'advanced'" class="filter-type"
					:class="{ 'selected': filterType === 'advanced' }">
					Avancé
				</button>
			</div>
			<!-- Basic filters -->
			<div v-if="filterType === 'basic'" style="overflow: auto">
				<!-- Date de naissance -->
				<div>
					<input type="checkbox" :id="random_prefix + 'filter_birth_date'" :name="random_prefix + 'filter_birth_date'"
						v-model="basicFilters.birth_date.enabled">
					<label :for="random_prefix + 'filter_birth_date'">Date de naissance</label>
					<div v-if="basicFilters.birth_date.enabled" class="filter_birth_date">
						<select v-model="basicFilters.birth_date._mode">
							<option value="exact">Exacte</option>
							<option value="min">Minimum</option>
							<option value="max">Maximum</option>
							<option value="range">Plage</option>
						</select>
						<div v-if="basicFilters.birth_date._mode !== 'max'">
							<label :for="random_prefix + 'filter_date_value1'">
								{{ basicFilters.birth_date._mode === 'range' ? 'Début' : 'Date' }}
							</label>
							<input type="date" :min="filterData.minDate.toISOString().split('T')[0]"
								:max="filterData.maxDate.toISOString().split('T')[0]" :id="random_prefix + 'filter_date_value1'"
								v-model="basicFilters.birth_date._value1">
						</div>
						<div v-if="basicFilters.birth_date._mode !== 'min' && basicFilters.birth_date._mode !== 'exact'">
							<label :for="random_prefix + 'filter_date_value2'">
								{{ basicFilters.birth_date._mode === 'range' ? 'Fin' : 'Date' }}
							</label>
							<input type="date" :min="filterData.minDate.toISOString().split('T')[0]"
								:max="filterData.maxDate.toISOString().split('T')[0]" :id="random_prefix + 'filter_date_value2'"
								v-model="basicFilters.birth_date._value2">
						</div>
					</div>
				</div>
				<!-- Sexe biologique -->
				<div>
					<input type="checkbox" :id="random_prefix + 'filter_biological_sex'" :name="random_prefix + 'filter_biological_sex'"
						v-model="basicFilters.biological_sex.enabled">
					<label :for="random_prefix + 'filter_biological_sex'">Sexe biologique</label>
					<select v-if="basicFilters.biological_sex.enabled" v-model="basicFilters.biological_sex.value">
						<option v-for="sex in filterData.biologicalSexes" :key="sex" :value="sex">{{ sex }}</option>
					</select>
				</div>
				<!-- Statut légal -->
				<div>
					<input type="checkbox" :id="random_prefix + 'filter_legal_status'" :name="random_prefix + 'filter_legal_status'"
						v-model="basicFilters.legal_status.enabled">
					<label :for="random_prefix + 'filter_legal_status'">Statut légal</label>
					<select v-if="basicFilters.legal_status.enabled" v-model="basicFilters.legal_status.value">
						<option v-for="status in filterData.legalStatuses" :key="status" :value="status">{{ status }}</option>
					</select>
				</div>
				<!-- Population -->
				<div>
					<input type="checkbox" :id="random_prefix + 'filter_population'" :name="random_prefix + 'filter_population'"
						v-model="basicFilters.population.enabled">
					<label :for="random_prefix + 'filter_population'">Population</label>
					<select v-if="basicFilters.population.enabled" v-model="basicFilters.population.value">
						<option v-for="population in filterData.populations" :key="population" :value="population">{{ population }}</option>
					</select>
				</div>
				<!-- Cohort -->
				<div>
					<input type="checkbox" :id="random_prefix + 'filter_cohort'" :name="random_prefix + 'filter_cohort'" v-model="basicFilters.cohort.enabled">
					<label :for="random_prefix + 'filter_cohort'">Cohorte</label>
					<select v-if="basicFilters.cohort.enabled" v-model="basicFilters.cohort.value">
						<option v-for="cohort in filterData.cohorts" :key="cohort" :value="cohort">{{ cohort }}</option>
					</select>
				</div>
			</div>
			<!-- Advanced filters -->
			<div v-if="filterType === 'advanced'" style="overflow: auto">
				<div v-for="filter in dynamicFilters" :key="filter.name">
					<input type="checkbox" :id="random_prefix + 'filter_' + filter.name" :name="random_prefix + 'filter_' + filter.name"
						v-model="filter.enabled">
					<label :for="random_prefix + 'filter_' + filter.name">{{ filter.name }}</label>
					<!-- Enable/Disable -->
					<template v-if="filter.enabled">
						<!-- String -->
						<template v-if="filter.dataType === 'string'">
							<template v-for="(select, index) in filter._ref_values" :key="index">
								<div class="dynamic-filter">
									<select v-model="filter._ref_values[index]">
										<option v-for="option in filter._options" :key="option" :value="option">{{ option }}</option>
									</select>
									<button class="remove-filter" type="button" @click="filter.removeFilter(index)"
										:disabled="!filter.canRemoveFilter">x
									</button>
								</div>
								<div v-if="index < filter._ref_values.length - 1" class="separator"></div>
							</template>
							<button v-show="filter.canAddFilter" class="add-filter" type="button" @click="filter.addFilter()">
								Ajouter
							</button>
						</template>
						<!-- Boolean -->
						<template v-else-if="filter.dataType === 'boolean'">
							<div class="dynamic-filter">
								<select v-model="filter._ref_value">
									<option :value="true">{{ filter._options[0] }}</option>
									<option :value="false">{{ filter._options[1] }}</option>
								</select>
							</div>
						</template>
						<!-- Number -->
						<template v-else-if="filter.dataType === 'number'">
							<div class="dynamic-filter">
								<div>
									<label :for="random_prefix + 'filter_' + filter.name + '_mode'">Mode</label>
									<select v-model="filter._mode" style="width: min-content;">
										<option value="exact">Exacte</option>
										<option value="min">Minimum</option>
										<option value="max">Maximum</option>
										<option value="range">Plage</option>
									</select>
								</div>
								<div v-if="filter._mode !== 'max'" style="flex-grow: 1;">
									<label :for="random_prefix + 'filter_' + filter.name + '_value1'">
										{{ filter._mode === 'range' || filter._mode === 'min' ? 'Minimum' : 'Valeur' }}
									</label>
									<input type="number" :min="filter._min" :max="filter._max"
										:id="random_prefix + 'filter_' + filter.name + '_value1'" v-model="filter._ref_value1">
								</div>
								<div v-if="filter._mode !== 'min' && filter._mode !== 'exact'" style="flex-grow: 1;">
									<label :for="random_prefix + 'filter_' + filter.name + '_value2'">
										{{ filter._mode === 'range' || filter._mode === 'max' ? 'Maximum' : 'Valeur' }}
									</label>
									<input type="number" :min="filter._min" :max="filter._max"
										:id="random_prefix + 'filter_' + filter.name + '_value2'" v-model="filter._ref_value2">
								</div>
							</div>
						</template>
						<!-- Date -->
						<template v-else-if="filter.dataType === 'date'">
							<div class="dynamic-filter">
								<div>
									<label :for="random_prefix + 'filter_' + filter.name + '_mode'">Mode</label>
									<select v-model="filter._mode" style="width: min-content;">
										<option value="exact">Exacte</option>
										<option value="min">Minimum</option>
										<option value="max">Maximum</option>
										<option value="range">Plage</option>
									</select>
								</div>
								<div v-if="filter._mode !== 'max'" style="flex-grow: 1;">
									<label :for="random_prefix + 'filter_' + filter.name + '_value1'">
										{{ filter._mode === 'range' || filter._mode === 'min' ? 'Minimum' : 'Valeur' }}
									</label>
									<input type="date" :min="filter._min" :max="filter._max"
										:id="random_prefix + 'filter_' + filter.name + '_value1'" v-model="filter._ref_value1">
								</div>
								<div v-if="filter._mode !== 'min' && filter._mode !== 'exact'" style="flex-grow: 1;">
									<label :for="random_prefix + 'filter_' + filter.name + '_value2'">
										{{ filter._mode === 'range' || filter._mode === 'max' ? 'Maximum' : 'Valeur' }}
									</label>
									<input type="date" :min="filter._min" :max="filter._max"
										:id="random_prefix + 'filter_' + filter.name + '_value2'" v-model="filter._ref_value2">
								</div>
							</div>
						</template>
					</template>
				</div>
			</div>
		</fieldset>
	</form>
</template>

<script setup lang="ts">
import { computed, reactive, ref } from 'vue';
import { Student } from '@/types';

const random_prefix = Math.random().toString(36).substring(2, 7);

// eslint-disable-next-line no-undef
const props = defineProps({
	students: {
		type: Array as () => Student[],
		required: true
	}
});

const filterType = ref('basic');
type FilterMode = "exact" | "min" | "max" | "range";
const modeValues = ["exact", "min", "max", "range"] as const;


const filterData = computed(() => {
	const birthdatesMS = props.students.map(student => Date.parse(student.birthday as any as string));

	return {
		minDate: new Date(Math.min(...birthdatesMS)),
		maxDate: new Date(Math.max(...birthdatesMS)),
		biologicalSexes: [...new Set(props.students.map(student => student.gender))],
		legalStatuses: [...new Set(props.students.map(student => student.status))],
		populations: [...new Set(props.students.map(student => student.population))],
		cohorts: [...new Set(props.students.map(student => student.cohort))]
	};
});

const basicFilters = reactive({
	birth_date: {
		enabled: false,
    _mode: "range" as FilterMode,
    _value1: "",
		_value2: "",
	},
	biological_sex: {
		enabled: false,
		value: ""
	},
	legal_status: {
		enabled: false,
		value: ""
	},
	population: {
		enabled: false,
		value: ""
	},
	cohort: {
		enabled: false,
		value: ""
	}
});

const filteredStudents = computed(() => {
	if (filterType.value === "basic") {
		return props.students.filter(student => {
			if (basicFilters.birth_date.enabled) {
				if (!student.birthday) return false;
				const date1 = new Date(basicFilters.birth_date._value1);
				const date2 = new Date(basicFilters.birth_date._value2);
				switch (basicFilters.birth_date._mode) {
					case "exact":
						if (student.birthday !== date1) return false;
						break;
					case "min":
						if (student.birthday < date1) return false;
						break;
					case "max":
						if (student.birthday > date2) return false;
						break;
					case "range":
						if (student.birthday < date1 || student.birthday > date2) return false;
						break;
				}
			}
			if (basicFilters.biological_sex.enabled && student.gender?.toUpperCase() !== basicFilters.biological_sex.value.toUpperCase()) return false;
			if (basicFilters.legal_status.enabled && student.status?.toUpperCase() !== basicFilters.legal_status.value.toUpperCase()) return false;
			if (basicFilters.population.enabled && student.population?.toUpperCase() !== basicFilters.population.value.toUpperCase()) return false;
			if (basicFilters.cohort.enabled && student.cohort?.toUpperCase() !== basicFilters.cohort.value.toUpperCase()) return false;
			return true;
		});
	} else if (filterType.value === "advanced") {
		return props.students.filter(student => {
			for (const filter of dynamicFilters.value) {
				if (!filter.enabled) continue;
				const value = student[filter.name];
				if (filter.dataType === "string") {
					if (typeof value !== "string") return false;
					else if (!filter._ref_values.includes(value)) return false;
				} else if (filter.dataType === "boolean") {
					if (value !== filter._ref_value) return false;
				} else if (filter.dataType === "number") {
					if (typeof value !== "number") return false;
					else if (filter._mode === "exact" && value !== filter._ref_value1) return false;
					else if (filter._mode === "min" && value < filter._ref_value1) return false;
					else if (filter._mode === "max" && value > filter._ref_value2) return false;
					else if (filter._mode === "range" && (value < filter._ref_value1 || value > filter._ref_value2)) return false;
				} else if (filter.dataType === "date") {
					if (typeof value !== "string") return false;
					const date = new Date(value);
					date.setHours(0, 0, 0, 0); // Set time to midnight to avoid timezone issues
					const date1 = new Date(new Date(filter._ref_value1).getTime() + new Date(filter._ref_value1).getTimezoneOffset() * 60000);
					const date2 = new Date(new Date(filter._ref_value2).getTime() + new Date(filter._ref_value2).getTimezoneOffset() * 60000);
					console.log(`Date: ${date}\nDate1: ${date1}\nDate2: ${date2}`);
					if (isNaN(date.getTime())) return false; // Invalid date
					else if (filter._mode === "exact" && date.getTime() !== date1.getTime()) return false;
					else if (filter._mode === "min" && date < date1) return false;
					else if (filter._mode === "max" && date > date2) return false;
					else if (filter._mode === "range" && (date < date1 || date > date2)) return false;
				}
			}
			return true;
		});
	}
   return props.students;
});
// eslint-disable-next-line no-undef
defineExpose({
	filters: basicFilters,
	filteredStudents
});

const types = ["undefined", "string", "number", "boolean", "object", "symbol", "bigint", "function"] as const;
const studentProperties = computed(() => {
	// Get the properties of the Student type
	return Object.keys(props.students[0] ?? {}) as (keyof Student)[];
});

const typeAnalysis = computed(() => {
	// Map all the values of each property to their types
	const propertyTypeMap: { [property in keyof Student]: (typeof types[number])[] } = {};

	for (const student of props.students)
		for (const key of studentProperties.value)
			propertyTypeMap[key]
				? propertyTypeMap[key]?.push(typeof student[key])
				: propertyTypeMap[key] = [typeof student[key]];

	interface PropertyAnalysis {
		sampleValues: any[];
		foundTypes: Set<typeof types[number]>;
		typeWeights: Partial<{ [key in typeof types[number]]: number }>;
		get dominantType(): typeof types[number];
		get isDateString(): boolean;
	}

	// For each property, produce an analysis of the types found
	const analysis: {
		sample: Student[],
		results: { [property in keyof Student]: PropertyAnalysis }
	} = {
		sample: props.students,
		results: {}
	};

	for (const key of studentProperties.value) {
		const typeInstances = propertyTypeMap[key] ?? [];
		const uniqueTypes = new Set(typeInstances);
		// Calculate the weights of each type
		const typeWeights: { [key: string]: number } = {};
		for (const type of types)
			typeWeights[type] = Math.round(typeInstances.filter(t => t === type).length / typeInstances.length * 100) / 100;

		analysis.results[key] = {
			sampleValues: props.students.map(student => student[key]),
			foundTypes: uniqueTypes,
			typeWeights: typeWeights,
			get dominantType() {
				return Object.entries(this.typeWeights).sort((a, b) => b[1] - a[1])[0][0] as typeof types[number];
			},
			get isDateString() {
				if (this.dominantType !== "string")
					return false;

				// 1 = date string, 0 = not a string, -1 = not a date string
				const dateMatches = this.sampleValues.map(value => {
					if (typeof value !== "string")
						return 0;

					const isValidDate = !isNaN(new Date(value).getTime());
					const looksLikeDate = (/\d{4}[/-]\d{2}[/-]\d{2}/.test(value) || /\d{1,2}[/-]\d{1,2}[/-]\d{2,4}/.test(value)
					);
					return looksLikeDate && isValidDate ? 1 : -1;
				});

				// check if most of the values are valid date strings
				const stringThatAreDates = dateMatches.filter(value => value === 1).length;
				const totalStrings = dateMatches.filter(value => value !== 0).length;
				const ratio = stringThatAreDates / totalStrings;

				return ratio > 0.5;
			}
		};
	}

	console.log("Type Analysis:", analysis);

	return analysis;
});

// Create dynamic filters for each property of the student analysis results
const dynamicFilters = computed(() => {

	type DynamicFilter<T = { [key: string]: any }> = {
		name: keyof T;
		enabled: boolean;
		dataType: "string" | "boolean" | "number" | "date";
		get value(): any;
	} & ({
		dataType: "string";
		_options: string[];
		_ref_values: string[];
		get value(): string[];
		addFilter: () => void;
		removeFilter: (index: number) => void;
		get canAddFilter(): boolean;
		get canRemoveFilter(): boolean;
	} | {
		dataType: "boolean";
		_options: [true: string, false: string];
		_ref_value: boolean;
		get value(): boolean;
	} | {
		dataType: "number";
		_min: number;
		_max: number;
		_mode: "exact" | "min" | "max" | "range";
		_ref_value1: number;
		_ref_value2: number;
		get value(): [number, number] | number;
	} | {
		dataType: "date";
		_min: string;
		_max: string;
		_mode: "exact" | "min" | "max" | "range";
		_ref_value1: string;
		_ref_value2: string;
		get value(): [Date, Date] | Date;
	});

	const filters: DynamicFilter<Student>[] = [];

	for (const key in studentProperties.value) {
		const analysis = typeAnalysis.value.results[studentProperties.value[key]];

		if (!analysis) continue;

		else if (analysis.dominantType === "string") {
			if (analysis.isDateString) {
				// Date string
				const timeConvertedStrings = analysis.sampleValues.map(v => new Date(v).getTime()).filter(v => typeof v === "number" && !isNaN(v));
				const minDate = new Date(Math.min(...timeConvertedStrings)).toISOString().split('T')[0];
				const maxDate = new Date(Math.max(...timeConvertedStrings)).toISOString().split('T')[0];

				filters.push(reactive({
					name: studentProperties.value[key],
					enabled: false,
					dataType: "date",
					_min: minDate,
					_max: maxDate,
          _mode: "range" as FilterMode,
          _ref_value1: minDate,
					_ref_value2: maxDate,
					get value() {
						return this._mode === "range"
							? [new Date(this._ref_value1), new Date(this._ref_value2)]
							: new Date(this._ref_value1);
					}
				})as DynamicFilter<Student>);
			} else {
				// String
				const uniqueValues = [...new Set(analysis.sampleValues.map(value => value).filter(v => typeof v === "string"))];
				filters.push(reactive({
					name: studentProperties.value[key],
					enabled: false,
					dataType: analysis.dominantType,
					_options: uniqueValues,
					_ref_values: [uniqueValues[0]],
					get value() {
						return this._ref_values;
					},
					addFilter() {
						const nextValue = this._options.find(value => !this._ref_values.includes(value));
						this._ref_values.push(nextValue ?? "");
					},
					removeFilter(index: number) {
						this._ref_values.splice(index, 1);
					},
					get canAddFilter() {
						const nextValue = this._options.find((value: any) => !this._ref_values.includes(value));
						return this._ref_values.length < uniqueValues.length && nextValue;
					},
					get canRemoveFilter() {
						return this._ref_values.length > 1;
					}
				})as DynamicFilter<Student>);
			}
		} else if (analysis.dominantType === "boolean")
			// Boolean
			filters.push(reactive({
				name: studentProperties.value[key],
				enabled: false,
				dataType: analysis.dominantType,
				_options: ["True/Oui/Vrai", "False/Non/Faux"],
				_ref_value: false,
				get value() {
					return this._ref_value;
				}
			} )as DynamicFilter<Student>);

		else if (analysis.dominantType === "number") {
			// Number
			const min = Math.min(...analysis.sampleValues.map(value => value).filter(v => typeof v === "number"));
			const max = Math.max(...analysis.sampleValues.map(value => value).filter(v => typeof v === "number"));

			filters.push(reactive({
				name: studentProperties.value[key],
				enabled: false,
				dataType: analysis.dominantType,
				_min: min,
				_max: max,
        _mode: "exact" as FilterMode,
				_ref_value1: min,
				_ref_value2: max,
				get value() {
					return this._mode === "range" ? [this._ref_value1, this._ref_value2] : this._ref_value1;
				}
			}) as DynamicFilter<Student>);
		} else {
			console.info(`Dynamic filter for ${studentProperties.value[key]} (${analysis.dominantType}) is not supported. Skipping...`);
			continue;
		}
	}

	return filters;
});
</script>

<style lang="css" scoped>
form {
	display: flex;
	flex-direction: column;
	justify-content: space-between;
	flex-grow: 1;
}

fieldset {
	padding: 1rem;
	border: 1px solid black;
	display: flex;
	flex-direction: column;
	gap: 0.5rem;
}

fieldset>legend {
	font-weight: bold;
	padding-inline: 0.25rem;
}

fieldset>div {
	display: flex;
	flex-direction: column;
	gap: 0.25rem;
}

fieldset.options-filters {
	position: absolute;
	width: 100%;
	height: 100%;
}

.dynamic-filter {
	display: flex;
	align-items: center;
	gap: 0.5rem;
}

div:has(> input[type="checkbox"]:not(:checked)) {
	opacity: 0.5;
}

div:has(> input[type="checkbox"]:checked) {
	outline: 0.1em solid #00000055;
	outline-offset: 0.12em;
}

button.remove-filter {
	height: 1.5em;
	aspect-ratio: 1;
	border: 0.1px solid red;
	border-radius: 0.35em;
	background-color: transparent;
	color: red;
	transition: all 0.2s ease-in-out;
}

button.remove-filter:hover {
	background-color: red;
	color: white;
	cursor: pointer;
	transition: all 0.2s ease-in-out;
}

button.remove-filter:disabled {
	background-color: transparent;
	color: red;
	cursor: not-allowed;
	transition: all 0.2s ease-in-out;
	opacity: 0.5;
}


button.add-filter {
	height: 1.5em;
	aspect-ratio: 1;
	border: 0.1px solid green;
	border-radius: 0.35em;
	background-color: transparent;
	color: green;
	transition: all 0.2s ease-in-out;
}

button.add-filter:hover {
	background-color: green;
	color: white;
	cursor: pointer;
	transition: all 0.2s ease-in-out;
}

button.add-filter:disabled {
	background-color: transparent;
	color: green;
	cursor: not-allowed;
	transition: all 0.2s ease-in-out;
	opacity: 0.5;
}

.separator {
	width: 100%;
	height: 1px;
	background-color: black;
	margin: 0.25rem 0;
	opacity: 0.75;
}

.filter_type_choice {
	display: grid;
	gap: 0;
	grid-template-columns: 1fr 1fr;
	;
	border: 2px solid #63ae2b;
	border-radius: 0.35em;
}

.filter-type {
	height: 2em;
	background-color: transparent;
	transition: all 0.2s ease-in-out;
	cursor: pointer;
}

.filter-type.selected {
	color: white;
	background-color: #63ae2b;
}

.filter-type:not(.selected):hover {
	background-color: #63ae2b33;
}

.filter-type.selected:hover {
	background-color: #63ae2bcc;
}
</style>