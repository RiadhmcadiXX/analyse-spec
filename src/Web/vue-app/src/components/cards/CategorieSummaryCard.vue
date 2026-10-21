<template>
  <div class="summary-card" :style="styleByProgress">
    <p class="progress-text">{{ current }} / {{ total }}</p>
  </div>
</template>

<script setup lang="ts">
import { defineProps, computed } from 'vue'

const props = defineProps<{
  current: number
  total: number
  label: string
}>()

const styleByProgress = computed(() => {
  if (props.current >= props.total) {
    // Succès — vert pâle
    return {
      backgroundColor: "#e6ffed",
      color: "#256029"
    }
  } else {
    // Incomplet — orange pâle
    return {
      backgroundColor: "#fff7e6",
      color: "#a16207"
    }
  }
})
</script>

<style scoped>
.summary-card {
  width: 100px;
  padding: 16px;
  border-radius: 12px;
  transition: box-shadow 0.2s ease;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
  border: 1px solid #ddd;
  text-align: center;
}

.label {
  font-size: 0.875rem;
  margin-bottom: 4px;
}

.progress-text {
  font-size: 1.5rem;
  font-weight: bold;
}
</style>
