<template>
  <div class="question-form">
    <div class="header">
      <div>
        <label for="search">Rechercher une question :</label>
        <input
            type="text"
            id="search"
            v-model="search"
            placeholder="Ex: S1ER_Q01"
            class="search-input"
        />
      </div>
      <button @click="handleSave">Enregistrer</button>
    </div>

    <ul class="question-list">
      <li
          v-for="question in paginatedQuestions"
          :key="question.id"
          class="question-item"
      >
        <strong>{{ question.title }}</strong> – {{ question.label }}
        <input
            v-model="question.label"
            class="question-form"
            placeholder="Ex: Origine de l'étudiant"
        />
      </li>
    </ul>

    <div class="pagination">
      <button @click="prevPage" :disabled="currentPage === 1">Précédent</button>
      <span>Page {{ currentPage }} sur {{ totalPages }}</span>
      <button @click="nextPage" :disabled="currentPage === totalPages">Suivant</button>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted } from "vue";
import { useQuestionService } from "@/inversify.config";
import { notifySuccess, notifyError } from "@/notify";
import { Question } from "@/types";
const enChargement = ref(false);
const questionService = useQuestionService();

const allQuestions = ref<Question[]>([]);
const search = ref("");
const currentPage = ref(1);
const pageSize = 10;

onMounted(async () => {

  allQuestions.value = await questionService.getAllQuestions();
  
  try {
    console.log( " Valeurs des questions au ounmounted :" + allQuestions.value, allQuestions.value.length);
    
  } catch (e) {
    notifyError("Erreur lors du chargement des questions.");
  }
});

const filteredQuestions = computed(() =>
    allQuestions.value.filter((q) =>
        q.title?.toLowerCase().includes(search.value.toLowerCase())
    )
);

const totalPages = computed(() =>
    Math.ceil(filteredQuestions.value.length / pageSize)
    
);

console.log("Taille de la liste filtrée :" + filteredQuestions.value.length);

const paginatedQuestions = computed(() => {
  const start = (currentPage.value - 1) * pageSize;
  return filteredQuestions.value.slice(start, start + pageSize);
});

function nextPage() {
  if (currentPage.value < totalPages.value) currentPage.value++;
}

function prevPage() {
  if (currentPage.value > 1) currentPage.value--;
}

async function handleSave() {
  try {
    await questionService.updateQuestion(allQuestions.value as Question[]); 
    notifySuccess("Questions mises à jour avec succès !");
  } catch (e) {
    notifyError("Erreur lors de la mise à jour des questions.");
  }
}
</script>

<style scoped>
.question-form {
  max-width: 600px;
  margin: 30px auto;
  background: #f4f9f9;
  border: 1px solid #d0e0e3;
  padding: 20px;
  border-radius: 8px;
}

.header {
  display: flex;
  margin-bottom: 15px;
  justify-content: space-between;
  width: 100%;
}

.search-input {
  padding: 8px;
  margin-top: 5px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.question-list {
  list-style: none;
  padding: 0;
  margin: 0;
}

.question-item {
  padding: 10px;
  border-bottom: 1px solid #c7d8d9;
  font-size: 15px;
}

.pagination {
  margin-top: 15px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

button {
  background-color: #63a69f;
  color: white;
  border: none;
  padding: 6px 12px;
  border-radius: 4px;
  cursor: pointer;
}

button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}
</style>
