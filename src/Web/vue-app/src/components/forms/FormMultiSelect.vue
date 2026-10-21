<template>
  <div class="form-multiselect">
    <label v-if="label" :for="name" class="form-multiselect__label">
      {{ label }}<span v-if="isRequired" class="form-multiselect__required">*</span>
    </label>

    <div class="form-multiselect__container" @click="toggleDropdown">
      <div class="form-multiselect__selected">
        <span v-if="!modelValue.length">{{ placeholder }}</span>
        <span v-for="option in modelValue" :key="option.name" class="form-multiselect__tag">
          {{ option.name }}
          <button type="button" @click.stop="removeOption(option)">
            &times;
          </button>
        </span>
      </div>
      <span class="form-multiselect__arrow">▾</span>
    </div>

    <ul v-show="isOpen" class="form-multiselect__options">
      <li v-for="option in options" :key="option.Name"
          @click="toggleOption(option)"
          :class="{ selected: isSelected(option) }">
        {{ option.name }}
      </li>
    </ul>

    <div v-if="!isValid" class="form-multiselect__error">Ce champ est requis.</div>
  </div>
</template>

<script lang="ts" setup>
import { ref, watch, defineProps, defineEmits } from 'vue';
import type { FormOption } from '@/types/formOption';
import type { FormOptionProgramme} from "@/types/formOptionProgramme";

const props = defineProps<{
  name: string;
  label?: string;
  placeholder?: string;
  options: FormOptionProgramme[];
  modelValue: FormOptionProgramme[];
  required?: boolean;
}>();

const emit = defineEmits<{
  (e: 'update:modelValue', value: FormOptionProgramme[]): void
}>();

const isOpen = ref(false);
const isValid = ref(true);

function toggleDropdown() {
  isOpen.value = !isOpen.value;
}

function isSelected(option: FormOptionProgramme) {
  return props.modelValue.some(o => o.name === option.name);
}

function toggleOption(option: FormOptionProgramme) {
  let updated: FormOptionProgramme[];
  if (isSelected(option)) {
    updated = props.modelValue.filter(o => o.name !== option.name);
  } else {
    updated = [...props.modelValue, option];
  }
  emit('update:modelValue', updated);
  validate();
}

function removeOption(option: FormOptionProgramme) {
  const updated = props.modelValue.filter(o => o.name !== option.name);
  emit('update:modelValue', updated);
  validate();
}

function validate() {
  isValid.value = !(props.required && props.modelValue.length === 0);
}

watch(() => props.modelValue, () => validate(), { immediate: true });

const isRequired = props.required ?? false;
</script>

<style scoped>
.form-multiselect {
  position: relative;
  font-family: sans-serif;
  margin-bottom: 1rem;
}

.form-multiselect__label {
  font-weight: bold;
  margin-bottom: 0.5rem;
  display: block;
}

.form-multiselect__required {
  color: red;
  margin-left: 0.25rem;
}

.form-multiselect__container {
  border: 1px solid #ccc;
  padding: 0.5rem;
  border-radius: 4px;
  cursor: pointer;
  background: #fff;
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  align-items: center;
}

.form-multiselect__selected {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
}

.form-multiselect__tag {
  background: #e0f0ff;
  padding: 0.25rem 0.5rem;
  border-radius: 3px;
  display: flex;
  align-items: center;
}

.form-multiselect__tag button {
  background: none;
  border: none;
  font-size: 1rem;
  margin-left: 0.5rem;
  cursor: pointer;
}

.form-multiselect__arrow {
  margin-left: auto;
}

.form-multiselect__options {
  list-style: none;
  padding: 0;
  margin: 0;
  position: absolute;
  top: 100%;
  left: 0;
  right: 0;
  background: white;
  border: 1px solid #ccc;
  border-radius: 4px;
  max-height: 200px;
  overflow-y: auto;
  z-index: 1000;
}

.form-multiselect__options li {
  padding: 0.5rem;
  cursor: pointer;
}

.form-multiselect__options li.selected {
  background-color: #e0f0ff;
  font-weight: bold;
}

.form-multiselect__error {
  color: red;
  font-size: 0.85rem;
  margin-top: 0.25rem;
}
</style>
