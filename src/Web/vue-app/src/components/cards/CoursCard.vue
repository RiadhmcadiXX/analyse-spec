<template>
  <div class="cours-card" :style="styleByCode">
    <h3 class="mb-2">{{ cours.titre }}</h3>
    <p><strong>Code :</strong> {{ cours.code }}</p>
    <p>Note : {{ cours.note === 0 ? 'Pas de note' : cours.note }}</p>

    <p v-if="cours.anneeSession"><strong>Session :</strong> {{ cours.anneeSession }}</p>
    <p v-if="cours.remarque"><em>{{ cours.remarque }}</em></p>
  </div>
</template>



<script setup lang="ts">
import { computed } from "vue"

// 🔸 assignation à une variable locale au lieu d’utiliser `props.cours`
import { defineProps } from 'vue'

const props = defineProps<{
  cours: {
    code: string
    titre: string
    note: number
    anneeSession?: string
    remarque?: string
  }
}>()


// 🔸 Utilisation directe de `cours.code` au lieu de `props.cours.code`
const styleByCode = computed(() => {
  const prefix = props.cours.code?.substring(0, 3)

  if (props.cours.note < 60) {
    return {
      backgroundColor: "#ffe5e5",
      color: "#b91c1c"
    }
  }

  const colors = {
    "601": { bg: "#e0f2ff", text: "#005ea2" },
    "340": { bg: "#f3e8ff", text: "#6b21a8" },
    "109": { bg: "#e6ffed", text: "#256029" },
    "604": { bg: "#fffbe6", text: "#a16207" },
    "ND":  { bg: "#ffe6f0", text: "#be185d" },
  }

  const color = colors[prefix] ?? { bg: "#f0f0f0", text: "#1a1a1a" }

  return {
    backgroundColor: color.bg,
    color: color.text
  }
})
</script>





<style scoped>
.cours-card {
  width: 250px;
  padding: 16px;
  border-radius: 12px;
  transition: box-shadow 0.2s ease;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
  border: 1px solid #ddd;
}

.cours-card:hover {
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

.cours-card h3 {
  font-size: 1rem;
  margin-top: 12px;
}

</style>
