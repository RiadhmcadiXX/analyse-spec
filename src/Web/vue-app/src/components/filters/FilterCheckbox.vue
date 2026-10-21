<template>
    <div>
        <input type="checkbox" :id="id" :checked="modelValue" @change="toggleFilter">
        <label :for="id">{{ label }}</label>
    </div>
</template>

<script setup lang="ts">defineProps<{
  id: string;
  label: string;
  modelValue: boolean;
}>();

const emit = defineEmits(["update:modelValue"]);

const toggleFilter = (event: Event) => {
  emit("update:modelValue", (event.target as HTMLInputElement).checked);
};</script>
